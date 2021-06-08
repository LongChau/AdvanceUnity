using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class GridController : MonoBehaviour
    {
        [SerializeField]
        private NodeController _nodePrefab;
        [SerializeField]
        private List<NodeController> _listNodes;

        [SerializeField]
        private Vector2 _cellSize = Vector2.one;
        [SerializeField]
        private Vector2 _spacing = Vector2.zero;

        [SerializeField]
        private int _totalRow;
        [SerializeField]
        private int _totalColumn;

        public List<NodeController> ListNodes => _listNodes;

        public Vector2 CellSize => _cellSize;

        public NodeController[,] Nodes = new NodeController[0, 0];

        // Testing
        //[Header("Test data:")]
        //[SerializeField]
        //private List<Vector2> _blockNodes = new List<Vector2>();
        //

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        private void Init()
        {
            _listNodes = new List<NodeController>();
            Nodes = new NodeController[_totalRow, _totalColumn];
            // Create grid map.
            CreateGrid();
            //ApplyBlockNodes();
        }

        //[ContextMenu("ApplyBlockNodes")]
        //private void ApplyBlockNodes()
        //{
        //    // Apply block nodes.
        //    foreach (var pos in _blockNodes)
        //    {
        //        Nodes[(int)pos.x, (int)pos.y].NodeType = ENodeType.Hill;
        //    }
        //}

        private void CreateGrid()
        {
            for (int columnIndex = 0; columnIndex < _totalRow; columnIndex++)
            {
                for (int rowIndex = 0; rowIndex < _totalColumn; rowIndex++)
                {
                    var node = Instantiate(_nodePrefab, this.transform);
                    node.transform.position = new Vector2(rowIndex * _cellSize.x + _spacing.x, columnIndex * _cellSize.y + _spacing.y);
                    node.name = $"Node_{rowIndex}_{columnIndex}";
                    node.NodeData.Row = rowIndex;
                    node.NodeData.Column = columnIndex;
                    _listNodes.Add(node);
                    Nodes[rowIndex, columnIndex] = node;
                }
            }
        }

        public NodeController GetNodeCtrl(int row, int column)
        {
            bool isValidColumn = (0 <= column && column < _totalColumn);
            bool isValidRow = (0 <= row && row < _totalRow);
            if (!isValidColumn || !isValidRow) return null;
            return Nodes[row, column];
        }
    }
}
