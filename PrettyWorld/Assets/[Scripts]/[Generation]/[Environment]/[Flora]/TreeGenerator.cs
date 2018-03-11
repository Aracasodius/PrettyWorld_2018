using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//float ChunkSize = (meshSettings.numVertsPerLine * meshSettings.meshScale) * 0.9433962264150943f; // get the chunk size in units
//float UnitsPerVert = ChunkSize / (meshSettings.numVertsPerLine - 3); //the units between 2 vertices

namespace EnvironmentGeneration
{
    public class TreeGenerator
    {
        private float _mapHeight;

        private HeightMap _worldHeightMap;
        private MeshSettings _meshSettings;
        private TextureData _textureData;

        public TreeGenerator(HeightMap HeightMap, MeshSettings meshSettings, TextureData textureData)
        {
            _meshSettings = meshSettings;
            _textureData = textureData;

            _mapHeight = HeightMap.maxValue + Mathf.Abs(_worldHeightMap.minValue);
        }
        
        //Public accessors
        public Vector3[] GetTreePositions(HeightMap chunkHeightMap)
        {
            List<Vector3> spawnAblePixelsList = new List<Vector3>();
            float[,] heightValues = GetHeightPercentages(chunkHeightMap);
            heightValues = ArrayModifiers.Invert(heightValues, false, true);
            Vector2[] spawnAblePixels = GetSpawnAblePixels(heightValues);

            Vector3 delta = new Vector3(-26, 0, -26);

            for (int i = 0; i < spawnAblePixels.Length; i++)
            {
                int x = (int)spawnAblePixels[i].x;
                int z = (int)spawnAblePixels[i].y;
                spawnAblePixelsList.Add(new Vector3(x, heightValues[x, z] * (_meshSettings.meshWorldSize / 7.5f), z) + delta);
            }
            return spawnAblePixelsList.ToArray();
        }

        //Modifiers & Tools
        private float[,] GetHeightPercentages(HeightMap HeightMap)
        {
            float[,] heightPercentage = HeightMap.values;
            for (int x = 0; x < heightPercentage.GetLength(0); x++)
            {
                for (int y = 0; y < heightPercentage.GetLength(1); y++)
                {
                    heightPercentage[x, y] = ((HeightMap.values[x, y] + Mathf.Abs(HeightMap.minValue)) / _mapHeight);
                }
            }
            return heightPercentage;
        }
        private Vector2[] GetSpawnAblePixels(float[,] values)
        {
            //Set the min/max tree spawn height
            float minTreeHeight = 0f;
            float maxTreeHeight = 0f;
            for (int i = 0; i < _textureData.layers.Length; i++)
            {
                if (_textureData.layers[i].canHoldTrees)
                {
                    if (_textureData.layers[i].startHeight < minTreeHeight || minTreeHeight == 0)
                        minTreeHeight = _textureData.layers[i].startHeight;

                    if (_textureData.layers[i].startHeight > maxTreeHeight)
                        maxTreeHeight = _textureData.layers[i].startHeight;
                }
                else
                {
                    if (i > 0)
                    {
                        if (_textureData.layers[i - 1].canHoldTrees)
                        {
                            maxTreeHeight = _textureData.layers[i].startHeight;
                        }
                    }
                }
            }

            //Make a list of all the spawnable pixels
            List<Vector2> spawnablePixels = new List<Vector2>();
            for (int x = 1; x < values.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < values.GetLength(1) - 1; y++)
                {
                    if (values[x, y] >= minTreeHeight && values[x, y] <= maxTreeHeight)
                    {
                        spawnablePixels.Add(new Vector2(x, y));
                    }
                }
            }

            return spawnablePixels.ToArray();
        }
    }
}
