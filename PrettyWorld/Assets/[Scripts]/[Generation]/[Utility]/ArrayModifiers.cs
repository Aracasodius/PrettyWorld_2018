using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentGeneration
{
    public class ArrayModifiers
    {
        public static float[,] RotateArray(int RotateAmount, float[,] source)
        {
            int size = source.GetLength(0);
            float[,] rotatedArray = new float[size, size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    rotatedArray[x, y] = source[size - y - 1, x];
                }
            }

            return rotatedArray;
        }

        public static float[,] Invert(float[,] source, bool InvertX, bool InvertY)
        {
            int size = source.GetLength(0);
            float[,] invertedArray = new float[size, size];
            if (InvertX)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        if (InvertY)
                            invertedArray[x, y] = source[size - x - 1, size - y - 1];

                        else
                            invertedArray[x, y] = source[size - x - 1, y];
                    }
                }
            }
            else
            {
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        invertedArray[x, y] = source[x, size - y - 1];
                    }
                }
            }

            return invertedArray;
        }
    }
}
