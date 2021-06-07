using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class NodeController : MonoBehaviour
    {
        [SerializeField]
        private Node _node;

        [SerializeField]
        private Sprite _normalSprite;
        [SerializeField]
        private Sprite _destinationSprite;
        [SerializeField]
        private Sprite _pathSprite;

        [SerializeField]
        private SpriteRenderer _render;

        public Node NodeData
        {
            get => _node;
            set
            {
                _node = value;
            }
        }

        public NodeController ParentNode { get; set; }

        public float X => transform.position.x;
        public float Y => transform.position.y;

        // Start is called before the first frame update
        void Start()
        {
            _node.UID = System.Guid.NewGuid().ToString();
        }

        private void OnMouseDown()
        {
            
        }

        //public bool IsTheSame(NodeController comparedNode)
        //{
        //    return _node.Column == comparedNode.GetNodeData.Column && _node.Row == comparedNode.GetNodeData.Row;
        //}

        public bool IsTheSame(NodeController comparedNode) => _node.UID == comparedNode.NodeData.UID;

        internal void ChangeToDestinationColor()
        {
            _render.sprite = _destinationSprite;
        }

        internal void ChangeToPathColor()
        {
            _render.sprite = _pathSprite;
        }
    }
}
