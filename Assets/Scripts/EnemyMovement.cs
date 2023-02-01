using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private float destructionRange;

    // Start is called before the first frame update
    void Start()
    {
        destructionRange = (Camera.main.orthographicSize * Screen.width / Screen.height + 1) * (-1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos.x -= moveSpeed * Time.deltaTime / 1f;
        if (pos.x < destructionRange)
            Destroy(gameObject);
        else
            transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag.Equals("Bullet"))
        {
            GameManager.instance.IncreaseScore();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
