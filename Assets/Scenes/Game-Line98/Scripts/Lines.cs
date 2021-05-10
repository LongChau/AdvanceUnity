using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Line98
{
    //public delegate void ShowBox(int x, int y, int ball);
    //public delegate void PlayCut();
    //public delegate void PlayStart();
    //public delegate void PlayEnd();

    public class Lines
    {
        public bool IsGameOver = false;
        public int Record;
        System.Random rand = new System.Random();
        public const int size = 9;
        public const int balls = 7;
        public const int add_balls = 3;
        //public ShowBox Show_Box;
        //public PlayCut Play_Cut;
        //public PlayEnd Play_End;
        //public PlayStart Play_Start;
        public Action<int, int, int> Show_Box;
        public Action Play_Cut;
        public Action Play_End;
        public Action Play_Start;

        int fromX, fromY;
        bool isbooltaked = false;

        int[,] map;
        /// <summary> Used nodes to check available movement. </summary>
        private bool[,] used_nodes;

        public Lines(Action<int, int, int> showBox, Action playCut, Action playEnd, Action playStart)
        {
            Record = 0;
            this.Show_Box = showBox;
            this.Play_Cut = playCut;
            this.Play_End = playEnd;
            this.Play_Start = playStart;
            map = new int[size, size];
        }
        public void Start()
        {
            clearMap();
            Play_Start();
            AddRandomBalls();
        }

        private void AddRandomBalls()
        {
            for (int i = 0; i < add_balls; i++)
                AddRandomBall();
        }

        private void AddRandomBall()
        {
            int x, y;
            int loop = size * size;
            do
            {
                x = rand.Next(size);
                y = rand.Next(size);
                if (--loop <= 0)
                {

                }
            } while (map[x, y] > 0);
            int ball = rand.Next(1, balls - 1);
            SetMap(x, y, ball);
        }

        private void clearMap()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    SetMap(i, j, 0);
        }

        private void SetMap(int x, int y, int ball)
        {
            map[x, y] = ball;
            Show_Box(x, y, ball);
        }

        public void Click(int x, int y)
        {
            Debug.Log($"Click({x}, {y})");
            if (map[x, y] > 0)
            {
                TakeBall(x, y);
            }
            else
            {
                MoveBall(x, y);
            }
        }

        private void MoveBall(int x, int y)
        {
            if (!isbooltaked) return;
            if (!CanMove(x, y)) return;
            SetMap(x, y, map[fromX, fromY]);
            SetMap(fromX, fromY, 0);
            isbooltaked = false;
            if (!CutLines())
            {
                AddRandomBalls();
                if (GameOver())
                    IsGameOver = true;
            }
        }

        private bool GameOver()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (map[i, j] == 0) return false;
            return true;
        }

        private bool[,] mark;
        private bool CutLines()
        {
            int balls = 0;
            mark = new bool[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    balls += CutLine(i, j, 1, 0);
                    balls += CutLine(i, j, 0, 1);
                    balls += CutLine(i, j, 1, 1);
                    balls += CutLine(i, j, -1, 1);
                }
            if (balls > 0)
            {
                Play_Cut();
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        if (mark[i, j]) SetMap(i, j, 0);
                Record += 6 * balls;
                return true;
            }
            return false;
        }

        private int CutLine(int x0, int y0, int sx, int sy)
        {
            int ball = map[x0, y0];
            if (ball == 0) return 0;
            int count = 0;
            for (int x = x0, y = y0; GetMap(x, y) == ball; x += sx, y += sy)
            {
                count++;
            }
            if (count < 5) return 0;
            for (int x = x0, y = y0; GetMap(x, y) == ball; x += sx, y += sy)
            {
                mark[x, y] = true;
            }
            return count;
        }

        private int GetMap(int x, int y)
        {
            if (!OnMap(x, y)) return 0;
            return map[x, y];
        }

        private bool OnMap(int x, int y)
        {
            return x >= 0 && x < size &&
                   y >= 0 && y < size;
        }

        /// <summary>
        /// Check if the node can be moved.
        /// </summary>
        /// <param name="to_x"></param>
        /// <param name="to_y"></param>
        /// <returns></returns>
        private bool CanMove(int to_x, int to_y)
        {
            used_nodes = new bool[size, size];
            Walk(fromX, fromY, true);
            return used_nodes[to_x, to_y];
        }

        /// <summary>
        /// Pathfinding for the node.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="start"></param>
        private void Walk(int x, int y, bool start = false)
        {
            if (!start)
            {
                if (!OnMap(x, y)) return;
                if (map[x, y] > 0) return;
                if (used_nodes[x, y]) return;
            }
            used_nodes[x, y] = true;
            Walk(x + 1, y);
            Walk(x - 1, y);
            Walk(x, y + 1);
            Walk(x, y - 1);
        }

        private void TakeBall(int x, int y)
        {
            fromX = x;
            fromY = y;
            isbooltaked = true;
        }
    }
}
