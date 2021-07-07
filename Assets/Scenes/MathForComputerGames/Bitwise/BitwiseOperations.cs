using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.Bitwise
{
    public class BitwiseOperations : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        public int Add(int a, int b)
        {
            while (b != 0)
            {
                int c = a & b;
                a = a ^ b;
                b = c << 1;
            }
            return a;
        }

        public int Subtract(int a, int b)
        {
            while (b != 0)
            {
                int borrow = (~a) & b;
                a = a ^ b;
                b = borrow << 1;
            }
            return a;
        }

        public int Multiply(int n, int m)
        {
            int answer = 0;
            int count = 0;
            while (m != 0)
            {
                if (m % 2 == 1)
                    answer += n << count;
                count++;
                m /= 2;
            }
            return answer;
        }

        public int division(int dividend, int divisor)
        {
            int remainder = 0;
            int quotient = 1;
            int neg = 1;

            if ((dividend > 0 && divisor < 0) || (dividend < 0 && divisor > 0))
                neg = -1;

            // Convert to positive 
            int tempdividend = Mathf.Abs((dividend < 0) ? -dividend : dividend);
            int tempdivisor = Mathf.Abs((divisor < 0) ? -divisor : divisor);

            if (tempdivisor == tempdividend)
            {
                remainder = 0;
                return 1 * neg;
            }
            else if (tempdividend < tempdivisor)
            {
                if (dividend < 0)
                    remainder = tempdividend * neg;
                else
                    remainder = tempdividend;
                return 0;
            }

            while (tempdivisor << 1 <= tempdividend)
            {
                tempdivisor = tempdivisor << 1;
                quotient = quotient << 1;
            }

            // Call division recursively 
            if (dividend < 0)
                quotient = quotient * neg + division(-(tempdividend - tempdivisor), divisor);
            else
                quotient = quotient * neg + division(tempdividend - tempdivisor, divisor);
            return quotient;
        }

        public void CounterWrapping()
        {
            // So often is the case when you are looping over an array
            // there is the need to check a loop counter isn’t out of bounds.
            // That requires the use of expensive if statements or you can use a bitwise operation.For example you can do this:
            // Change from this...
            //for (int x = 0, u = 0; x < screen.width; x++, u++)
            //{
            //    for (int y = 0, v = 0; y < screen.height; y++, v++)
            //    {
            //        int colour = image.getPixel(u, v);
            //        screen.setPixel(x, y, colour);

            //        // Make sure we don't go outside the image boundary.
            //        if (u >= image.width)
            //            u = 0;
            //        if (v >= image.height)
            //            v = 0;
            //    }
            //}

            // To this...
            // Assumes image dimensions are powers of 2.
            //int widthMask = image.width - 1;
            //int heightMask = image.height - 1;

            //for (int x = 0; x < screen.width; x++)
            //{
            //    for (int y = 0; x < screen.height; y++)
            //    {
            //        int colour = image.getPixel(x & widthMask, y & heightMask);
            //        screen.setPixel(x, y, colour);
            //    }
            //}
        }

        /// <summary>
        /// Swapping values without a temp
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SwapValue(int x, int y)
        {
            // From...
            // To swap to values you can do this:
            int temp = x;
            x = y;
            y = temp;

            // To...
            x ^= y;
            y ^= x;
            x ^= y;
        }

        /// <summary>
        /// Checking for same signs
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsSameSign(int x, int y)
        {
            bool haveSameSign = 0 <= (x ^ y);
            return haveSameSign;
        }

        /// <summary>
        /// Find min, max value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void FindMinMax(int x, int y)
        {
            int max = x ^ ((x ^ y) & -((x < y) ? 1 : 0));
            int min = y ^ ((x ^ y) & -((x < y) ? 1 : 0));
        }
    }
}
