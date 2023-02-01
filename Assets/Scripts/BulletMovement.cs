using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private float destructionRange;

    // Start is called before the first frame update
    void Start()
    {
        destructionRange = (Camera.main.orthographicSize * Screen.width / Screen.height + 1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos.x += moveSpeed * Time.deltaTime / 1f;
        if (pos.x > destructionRange)
            Destroy(gameObject);
        else
            transform.position = pos;
    }
}
