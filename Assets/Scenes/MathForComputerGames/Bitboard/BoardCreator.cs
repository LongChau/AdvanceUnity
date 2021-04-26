using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace UnityAdvance.Bitboard
{
    public class BoardCreator : MonoBehaviour
    {
        const int POINT_DIRT = 10;
        const int POINT_DESERT = 2;

        public GameObject[] tilePrefabs;
        public GameObject housePrefab;
        public GameObject treePrefab;

        public TextMeshProUGUI txtScore;
        GameObject[] tiles;
        long dirtBitboard = 0; // Create dirt bitboard with 64-bit integer.
        long desertBitboard = 0;
        long treeBitboard = 0;
        long playerBitboard = 0;

        int score = 0;

        [ContextMenu("Test_PadLeft")]
        void Test_PadLeft()
        {
            string str = "forty-two";
            char pad = '0';

            Debug.Log(str.PadLeft(str.ToCharArray().Length + 1, pad));
            Debug.Log(str.PadLeft(15, pad));
            Debug.Log(str.PadLeft(2, pad));
        }

        // Start is called before the first frame update
        void Start()
        {
            tiles = new GameObject[64];
            // Consider the bitboard is the one-dimension array. 
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    int randomTile = UnityEngine.Random.Range(0, tilePrefabs.Length);
                    Vector3 pos = new Vector3(c, 0, r);
                    var tile = Instantiate(tilePrefabs[randomTile], pos, Quaternion.identity);
                    tile.name = $"{tile.tag}_{c}_{r}";
                    tiles[r * 8 + c] = tile;
                    // Check for dirt.
                    if (tile.CompareTag("Dirt"))
                    {
                        // Now in one-dimension array board which cell is "Dirt" will set as 1. Others is 0.
                        dirtBitboard = SetCellState(dirtBitboard, r, c);
                        PrintBitboard("Dirt", dirtBitboard);
                    }
                    else if (tile.CompareTag("Desert"))
                    {
                        desertBitboard = SetCellState(desertBitboard, r, c);
                        PrintBitboard("Desert", desertBitboard);
                    }
                }
            }

            var desert_dirt = dirtBitboard | desertBitboard;
            PrintBitboard("Desert_Dirt", desert_dirt);

            Debug.Log($"Dirt cells = {CellCount(dirtBitboard)}");
            InvokeRepeating("PlantTree", 1, 1);
        }

        void PrintBitboard(string name, long bitBoard)
        {
            // We convert the int64 to binary.
            var binary = Convert.ToString(bitBoard, 2);
            // Then we padleft for 64 bit 0. To show the binary in 64-bit binary with pre-0 before 1.
            var pad_binary = binary.PadLeft(64, '0');
            Debug.Log($"{name}: {pad_binary}");
        }

        int CellCount(long bitboard)
        {
            int count = 0;
            long bb = bitboard;
            // We count the bitboard if there is any value = 1 we increase the "count" value.
            while (bb != 0)
            {
                bb &= bb - 1;
                count++;
            }
            return count;
        }

        void CalculateScore(long bitboard)
        {
            score = CellCount(bitboard & dirtBitboard) * POINT_DIRT + CellCount(bitboard & desertBitboard) * POINT_DESERT;
            txtScore.SetText($"Score: {score}");
        }

        long SetCellState(long bitboard, int row, int col)
        {
            // This is cell position in one-dimension board array. 
            var cell_position = row * 8 + col;
            // We shift it. 
            long newBit = 1L << cell_position;
            return bitboard |= newBit;
        }

        bool GetCellState(long bitboard, int row, int col)
        {
            // This is cell position in one-dimension board array. 
            var cell_position = row * 8 + col;
            // We shift it. 
            long mask = 1L << cell_position;
            return (bitboard & mask) != 0;
        }

        void PlantTree()
        {
            int rand_row = UnityEngine.Random.Range(0, 8);
            int rand_col = UnityEngine.Random.Range(0, 8);
            // To know that the cell is valid for place tree we 'mask' dirtBB with the playerBB.
            long validBB = dirtBitboard & ~playerBitboard;
            if (GetCellState(validBB, rand_row, rand_col))
            {
                var tree = Instantiate(treePrefab);
                tree.transform.parent = tiles[rand_row * 8 + rand_col].transform;
                tree.transform.localPosition = Vector3.zero;
                treeBitboard = SetCellState(treeBitboard, rand_row, rand_col);
            }
        }

        private void Update()
        {
            // Place house.
            PlaceHouse();
        }

        private void PlaceHouse()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool isHitSomething = Physics.Raycast(ray, out hit);
                if (isHitSomething && hit.collider != null)
                {
                    var hitObj = hit.collider.gameObject;
                    var row = (int)hitObj.transform.position.z;
                    var column = (int)hitObj.transform.position.x;
                    // Check if the cell is valid to place the house.
                    // It must be a dirt without tree.
                    long validBB = (dirtBitboard | desertBitboard) & ~treeBitboard;
                    if (GetCellState(validBB, row, column))
                    {
                        var house = Instantiate(housePrefab);
                        house.transform.parent = hit.collider.gameObject.transform;
                        house.transform.localPosition = Vector3.zero;
                        playerBitboard = SetCellState(playerBitboard, row, column);
                        CalculateScore(playerBitboard);
                    }
                }
            }
        }
    }
}
