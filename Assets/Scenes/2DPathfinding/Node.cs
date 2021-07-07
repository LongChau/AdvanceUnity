using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    [Serializable]
    public class Node
    {
        [SerializeField]
        private string _uid;
        [SerializeField]
        private float _fCost;
        [SerializeField]
        private float _gCost;
        [SerializeField]
        private float _hCost;

        public int Row;
        public int Column;

        public string UID { get => _uid; set => _uid = value; }
        public float FCost => _fCost = GCost + HCost;
        /// <summary>
        ///G is the movement cost from the start point A to the current square.
        ///So for a square adjacent to the start point A, this would be 1,
        ///but this will increase as we get farther away from the start point.
        /// -> The cost of moving to the next square.
        /// </summary>
        public float GCost { get => _gCost; set => _gCost = value; }
        /// <summary>
        ///H is the estimated movement cost from the current square to the destination point
        ///(we’ll call this point B for Bone!)
        ///This is often called the heuristic because we don’t really know the cost yet – it’s just an estimate.
        /// -> The distance to the goal from this node.
        /// </summary>
        public float HCost { get => _hCost; set => _hCost = value; }

        //public Node ParentNode;
    }
}
