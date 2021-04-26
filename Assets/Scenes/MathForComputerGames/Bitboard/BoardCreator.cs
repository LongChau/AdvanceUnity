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
        public GameObject[] tilePrefabs;
        public GameObject housePrefab;
        public TextMeshProUGUI txtScore;
        // Create dirt bitboard with 64-bit integer.
        long dirtBitboard = 0;

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
            // Consider the bitboard is the one-dimension array. 
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    int randomTile = UnityEngine.Random.Range(0, tilePrefabs.Length);
                    Vector3 pos = new Vector3(c, 0, r);
                    var tile = Instantiate(tilePrefabs[randomTile], pos, Quaternion.identity);
                    tile.name = $"{tile.tag}_{c}_{r}";
                    // Check for dirt.
                    if (tile.CompareTag("Dirt"))
                    {
                        // Now in one-dimension array board which cell is "Dirt" will set as 1. Others is 0.
                        dirtBitboard = SetCellState(dirtBitboard, r, c);
                        PrintBitboard("Dirt", dirtBitboard);
                    }
                }
            }
            Debug.Log($"Dirt cells = {CellCount(dirtBitboard)}");
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

        long SetCellState(long bitboard, int row, int col)
        {
            // This is cell position in one-dimension board array. 
            var cell_position = row * 8 + col;
            // We shift it. 
            long newBit = 1L << cell_position;
            return bitboard |= newBit;
        }
    }
}
