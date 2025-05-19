using System.Collections;
using UnityEngine;

public class ArrangeChildren : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(ArrangeAfterFrames());
    }

    IEnumerator ArrangeAfterFrames()
    {
        yield return null; // 等待1帧
        yield return null; // 等待第2帧（可选）

        int total = 10;
        int perRow = 5;

        float xStart = -7f;
        float xEnd = 4f;
        float yTop = 1.9f;
        float yBottom = -2.4f;

        float xStep = (xEnd - xStart) / (perRow - 1);

        for (int i = 0; i < total; i++)
        {
            if (i >= transform.childCount)
            {
                Debug.LogWarning("子物体数量不足，停止排列。");
                break;
            }

            int row = i / perRow;
            int col = i % perRow;

            float x = xStart + xStep * col;
            float y = row == 0 ? yTop : yBottom;

            Transform child = transform.GetChild(i);
            child.localPosition = new Vector3(x, y, 0);
        }
    }
}
