using UnityEngine;

public class levelBuilder : MonoBehaviour
{
    int[,] tileArray = new int[36,72];
    public void copyAr(int [,] curLevel)
    {
        for (int x = 0; x < tileArray.GetLength(0); x++)
            for(int y = 0; y < tileArray.GetLength(1); y++)
            {
                tileArray[x, y] = curLevel[x, y];
            }
    }
}
