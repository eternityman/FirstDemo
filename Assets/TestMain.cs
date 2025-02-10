using PathFind;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TestMain : MonoBehaviour
{
    public GameObject cubeA;
    public GameObject cubeB;
    public GameObject cubeC;

    public string test;

    public uint len;

    // Start is called before the first frame update
    void Start()
    {
        int[] arr = new[] { 21, 25, 49, 25, 16, 8 };
        DSHeap<int> heap = new DSHeap<int>(arr, false);
        heap.PrintHeap();
        Debug.Log("==================SortByAscending=====================");
        heap.SortByAscending();
        heap.PrintHeap();
        Debug.Log("==================SortByDescending=====================");
        heap.SortByDescending();
        heap.PrintHeap();
        Debug.Log("==================SortDescending=====================");
        heap.SortDescending();
        heap.PrintHeap();
        Debug.Log("==================SortAscending=====================");
        heap.SortAscending();
        heap.PrintHeap();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            cubeB.transform.localPosition = cubeB.transform.InverseTransformPoint(cubeA.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            cubeA.transform.position = cubeA.transform.TransformPoint(cubeB.transform.localPosition);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            cubeA.transform.position = cubeA.transform.TransformPoint(cubeC.transform.localPosition);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            string result = AbbrevString(test, (int)len);
            Debug.Log(result);
            Debug.Log(GetByteLength(result));
        }
    }

    /// <summary>
    /// 求字符长度
    /// </summary>
    /// <param name="str"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string AbbrevString(string str, int maxLength)
    {
        int totalLength = str.Length;
        int curLength = 0;
        if (totalLength * 3 < maxLength)
        {
            return str;
        }

        for (int i = 0; i < totalLength; i++)
        {
            char tempChar = str[i];
            curLength += System.Text.Encoding.UTF8.GetBytes(tempChar.ToString()).Length;
            if (curLength > maxLength)
            {
                return str.Substring(0, i);
            }
        }

        return str;
    }

    public static int GetByteLength(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return 0;
        }

        return System.Text.Encoding.UTF8.GetBytes(str).Length;
    }
}