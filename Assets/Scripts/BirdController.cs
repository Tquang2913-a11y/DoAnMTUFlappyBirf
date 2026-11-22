using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float flapForce = 5f;
    Rigidbody2D rb;

    void Start() { rb = GetComponent<Rigidbody2D>(); }

    void Update()
    {
        var gm = GameManager.Instance;

        if (gm.State == GameState.Ready) { rb.simulated = false; return; }
        rb.simulated = true;
        if (gm.State != GameState.Playing) return;

           if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rb.linearVelocity = Vector2.up * flapForce;
            gm.PlayFlap();
        }

        float angle = Mathf.Clamp(rb.linearVelocity.y * 6f, -60f, 30f);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (transform.position.y > 5.5f) gm.GameOver();
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Obstacle"))
            GameManager.Instance.GameOver();
    }
}