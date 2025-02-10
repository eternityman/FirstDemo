using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public float amplitude = 1f; // 振幅
    public float frequency = 1f; // 频率
    public float speed = 1f; // 移动速度

    private float time;

    void Update()
    {
        time += Time.deltaTime * speed;
        float x = time;
        float y = amplitude * Mathf.Cos(frequency * x);
        transform.position = new Vector3(x, y, transform.position.z);
    }
}