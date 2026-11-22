using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float interval = 1.8f;
    public float minY = -2f;
    public float maxY = 2f;
    float timer;

    void Start() { timer = interval; }

    void Update()
    {
        if (GameManager.Instance.State != GameState.Playing) return;
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            Vector3 pos = new Vector3(transform.position.x, Random.Range(minY, maxY), 0);
            Instantiate(pipePrefab, pos, Quaternion.identity);
        }
    }
}