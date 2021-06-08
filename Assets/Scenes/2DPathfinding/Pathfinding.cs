using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pathfinding
{
    public class Pathfinding : MonoBehaviour
    {
        [SerializeField]
        private GridController _gridCtrl;

        [SerializeField]
        private List<NodeController> _listOpen = new List<NodeController>();
        [SerializeField]
        private List<NodeController> _listClosed = new List<NodeController>();

        [SerializeField]
        private List<NodeController> _listWalkableNodes = new List<NodeController>();

        [SerializeField]
        private bool _isFoundPath;

        [SerializeField]
        private NodeController _currentNode;

        [SerializeField, Header("Final path")]
        private List<NodeController> _listResult = new List<NodeController>();

        [SerializeField]
        private int _directions = 4;

        private Dictionary<int, Vector3> _dictDirections = new Dictionary<int, Vector3>()
        {
            // Fire emblem style movement
            { 0, new Vector3(0f, 1f, 0f) },
            { 1, new Vector3(0f, -1f, 0f) },
            { 2, new Vector3(1f, 0f, 0f) },
            { 3, new Vector3(-1f, 0f, 0f) },

            // 8 path movement
            { 4, new Vector3(-1f, 1f, 0f) },
            { 5, new Vector3(1f, 1f, 0f) },
            { 6, new Vector3(-1f, -1f, 0f) },
            { 7, new Vector3(1f, -1f, 0f) },
        };


        // Test...
        [SerializeField]
        private NodeController _from;
        [SerializeField]
        private NodeController _to;

        // Start is called before the first frame update
        void Start()
        {
        }

        [ContextMenu("FindPath")]
        private void Test_FindPath()
        {
            FindPath(_from, _to);
        }

        private void FindPath(NodeController from, NodeController to)
        {
            _listOpen.Add(from);    // Usually the current standing node.
            while (_listOpen.Count > 0 && !_isFoundPath)
            {
                SearchForPath(from, to);
            }
            // End finding path.
            Debug.Log("End finding path.");
            if (!_isFoundPath)
                Debug.Log("There is no valid path");
        }

        public void SearchForPath(NodeController from, NodeController to)
        {
            var sortedList = _listOpen.OrderBy(node => node.NodeData.FCost).ToList();
            _listOpen = sortedList;

            _currentNode = _listOpen[0];    // get lowest score

            if (!_listClosed.Contains(_currentNode))
                _listClosed.Add(_currentNode);   // add lowest score tile to closed list
            _listOpen.Remove(_currentNode);  // remove lowest score tile from opened list

            if (_currentNode.IsTheSame(to))
            {
                // PATH FOUND. End loop
                Debug.Log("Path found");
                GetFinalPath(from, to);
                return;
                // or break;
            }

            // find adjacent tiles
            for (int directionIndex = 0; directionIndex < _directions; directionIndex++)
            {
                var direction = _dictDirections[directionIndex];
                var newRow = _currentNode.NodeData.Row + (int)direction.x;
                var newColumn = _currentNode.NodeData.Column + (int)direction.y;
                var newNode = _gridCtrl.GetNodeCtrl(newRow, newColumn);

                // Not a valid node.
                if (newNode == null) continue;  // This direction is invalid. Please check another direction.
                if (newNode.NodeType != ENodeType.Dirtroad) continue; // Check for block nodes. 

                newNode.NodeData.HCost = CalculateManhattanDistance(newNode, to);
                newNode.NodeData.GCost = CalculateManhattanDistance(newNode, _currentNode);

                // ignore if duplicate
                if (!_listWalkableNodes.Contains(newNode))
                {
                    // Retrieve all walkable tiles
                    _listWalkableNodes.Add(newNode);
                }
            }

            foreach (var neighborNode in _listWalkableNodes)
            {
                if (_listClosed.Contains(neighborNode)) // if tile in closed list then ignore it
                    continue;   // go to next tile

                //Calculate its score
                float moveCost = neighborNode.NodeData.FCost;//Get the F cost of that neighbor

                if (moveCost < neighborNode.NodeData.GCost || !_listOpen.Contains(neighborNode))
                {
                    neighborNode.ParentNode = _currentNode;

                    if (!_listOpen.Contains(neighborNode))  // if not in opened list
                    {
                        // add to open list
                        _listOpen.Add(neighborNode);
                    }
                }
            }
        }

        private void GetFinalPath(NodeController from, NodeController to)
        {
            Debug.Log("GetFinalPath()");
            List<NodeController> listFinalPath = new List<NodeController>();//List to hold the path sequentially
            NodeController currentNode = to;//Node to store the current node being checked

            currentNode.ChangeToDestinationColor();

            while (!currentNode.IsTheSame(from))//While loop to work through each node going through the parents to the beginning of the path
            {
                listFinalPath.Add(currentNode);//Add that node to the final path
                currentNode = currentNode.ParentNode;//Move onto its parent node
                currentNode.ChangeToPathColor();
            }

            listFinalPath.Reverse();//Reverse the path to get the correct order
            _listResult = listFinalPath;//Set the final path

            _isFoundPath = true;
        }

        public float CalculateManhattanDistance(NodeController node_1, NodeController node_2)
        {
            return Math.Abs(node_1.X - node_2.X) + Math.Abs(node_1.Y - node_2.Y);
        }
    }
}
