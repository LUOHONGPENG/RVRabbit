using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    /// <summary>
    /// Useful function for clearing the child objects 
    /// </summary>
    /// <param name="tf"></param>
    public static void ClearChildItem(UnityEngine.Transform tf)
    {
        foreach (UnityEngine.Transform item in tf)
        {
            UnityEngine.Object.Destroy(item.gameObject);
        }
    }

    /// <summary>
    /// Useful function for Calculate angle. For example, from Target to (0,1)
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static float CalculateAngle(Vector2 from, Vector2 to)
    {
        float angle;
        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? angle : -angle;
    }

    /// <summary>
    /// Useful function for calculating the UI position of a 3D object
    /// </summary>
    /// <param name="objPos"></param>
    /// <param name="tarCamera"></param>
    /// <returns></returns>
    public static Vector3 CalculateScreenUIPos(Vector3 objPos, Camera tarCamera)
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(tarCamera, objPos);
        screenPos = new Vector2(screenPos.x * 1920f / Screen.width, screenPos.y * 1080f / Screen.height);
        Vector2 targetPos = new Vector2(screenPos.x - 1920f / 2, screenPos.y - 1080f / 2);
        return new Vector3(targetPos.x, targetPos.y, 0);
    }

    /// <summary>
    /// Randomly pick one index with weight
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static int GetRandomIndexIntArray(int[] array)
    {
        int totalWeight = 0;
        //Sum up
        for (int i = 0; i < array.Length; i++)
        {
            totalWeight += array[i];
        }

        //Calculate
        if (totalWeight > 0)
        {
            int ran = Random.Range(0, totalWeight);
            for (int i = 0; i < array.Length; i++)
            {
                ran -= array[i];
                if (ran < 0)
                {
                    return i;
                }
            }
        }
        return -1;
    }
}
