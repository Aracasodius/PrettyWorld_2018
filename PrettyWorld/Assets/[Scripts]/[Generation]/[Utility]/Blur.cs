using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentGeneration
{
    public class GaussianBlur
    {
        private float[,] _values;

        public float[,] BlurValues(float[,] values)
        {
            _values = values;

            int width = values.GetLength(0);
            int height = values.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (values[x, y] > 0f)
                    {
                        _values[x, y] = AssignPixelValue(x, y);
                    }
                }
            }

            return _values;
        }

        private float AssignPixelValue(int x, int y)
        {
            int width = _values.GetLength(0);
            int height = _values.GetLength(1);

            float result = 0f;
            if (IsBorderPixel(x, y))
            {
                result = 0.5f;
            }
            else
            {
                result = 1f;
            }

            return result;
        }
        public bool IsBorderPixel(int x, int y)
        {
            int width = _values.GetLength(0) - 1;
            int height = _values.GetLength(1) - 1;

            if (x > 0 && x < width && y > 0 && y < height)
            {
                return (_values[x + 1, y] == 0 || _values[x - 1, y] == 0 || _values[x, y + 1] == 0 || _values[x, y - 1] == 0);
            }
            else
            {
                if(x == 0)
                {
                    if(y == 0) { return (_values[x + 1, y] == 0 || _values[x, y + 1] == 0); }
                    else if(y == height) { return (_values[x + 1, y] == 0 || _values[x, y - 1] == 0); }
                    else { return (_values[x + 1, y] == 0 || _values[x, y + 1] == 0 || _values[x, y - 1] == 0); }
                }
                else if (x == width)
                {
                    if (y == 0) { return (_values[x - 1, y] == 0 || _values[x, y + 1] == 0); }
                    else if (y == height) { return (_values[x - 1, y] == 0 || _values[x, y - 1] == 0); }
                    else { return (_values[x - 1, y] == 0 || _values[x, y + 1] == 0 || _values[x, y - 1] == 0); }
                }
                else if (y == 0)
                {
                    return (_values[x + 1, y + 1] == 0 || _values[x, y + 1] == 0 || _values[x - 1, y] == 0);
                }
                else if (y == height)
                {
                    return (_values[x + 1, y] == 0 || _values[x, y - 1] == 0 || _values[x - 1, y] == 0);
                }
                else
                {
                    return (_values[x + 1, y + 1] == 0 || _values[x, y - 1] == 0 || _values[x - 1, y] == 0 || _values[x, y + 1] == 0);
                }
            }
        }
    }
}
