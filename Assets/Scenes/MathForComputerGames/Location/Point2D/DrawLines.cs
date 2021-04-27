using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.Location
{
    public class DrawLines : MonoBehaviour
    {
        public int size;
        public int xmax = 200;
        public int ymax = 200;

        Coords point = new Coords(10, 20);

        Coords startPointYAxis = new Coords(0, 100);
        Coords endPointYAxis = new Coords(0, -100);

        Coords startPointXAxis = new Coords(160, 0);
        Coords endPointXAxis = new Coords(-160, 0);

        Coords[] points = new Coords[]
        {
            new Coords(0, 0),
            new Coords(20, 30),
            new Coords(50, 30),
            new Coords(60, 30),
            new Coords(70, 60),
            new Coords(55, 60),
            new Coords(40, -50),
            new Coords(-40, 10),
            new Coords(10, 10),
        };

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(point.ToString());
            Coords.DrawPoint(new Coords(0, 0), 2, Color.red);
            Coords.DrawPoint(point, 2, Color.green);

            Coords.DrawLine(startPointYAxis, endPointYAxis, 1, Color.green);
            Coords.DrawLine(startPointXAxis, endPointXAxis, 1, Color.red);
            Coords.DrawLine(points, 1, Color.white);

            int xOffset = (int)(xmax / (float)size);
            int yOffset = (int)(ymax / (float)size);

            for (int x = -xOffset * size; x < xOffset * size; x += size)
            {
                Coords.DrawLine(new Coords(x, -ymax), new Coords(x, ymax), 0.5f, Color.white);
            }
            for (int y = -yOffset * size; y < yOffset * size; y += size)
            {
                Coords.DrawLine(new Coords(-xmax, y), new Coords(xmax, y), 0.5f, Color.white);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
