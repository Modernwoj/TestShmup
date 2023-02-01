using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float reloadInterval;
    private float bulletTime = 0f;
    private Collider2D coll;

    private int invincibilityBlinks = 4; //invincibility duration = invincibilityBlinks * blinkTime
    private float blinkInterval= 0.25f;
    private int Blinks;
    private float blinkTime;

    private bool hit = false;
    private SpriteRenderer rend;

    private bool moveUp = false;
    private bool moveDown = false;
    private bool shoot = false;


    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = true;
    }

    private void OnDisable()
    {
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentState != GameManager.gameState.Gameplay)
            return;
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        shoot = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return);

        if (shoot)
            UpdateShooting();
        else bulletTime = 0;

        if (hit)
            UpdateBlinking();
        else
        {
            blinkTime = 0;
            Blinks = 0;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        if (moveUp)
            pos.y += moveSpeed * Time.fixedDeltaTime;
        if (moveDown)
            pos.y -= moveSpeed * Time.fixedDeltaTime;
        if (pos.y > GameManager.yBounds) pos.y = GameManager.yBounds;
        if (pos.y < -GameManager.yBounds) pos.y = -GameManager.yBounds;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (hit)            //ensuring that if 2 planes hit at once, only 1 life is taken
            return;
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            hit = true;
            GameManager.instance.TakeLife();
        }
    }

    void UpdateBlinking()
    {
        blinkTime -= Time.deltaTime;
        if (blinkTime <= 0)
        {
            rend.enabled = !rend.enabled;
            Blinks++;
            blinkTime = blinkInterval;
            if (Blinks >= invincibilityBlinks)
            {
                hit = false;
                Blinks = 0;
            }
        }
    }


    void UpdateShooting()
    {
        bulletTime -= Time.deltaTime;
        if(bulletTime <= 0)
        {
            var newObj = Instantiate(bullet);
            newObj.transform.position = new Vector3(transform.position.x + coll.bounds.extents.x, transform.position.y, transform.position.z);
            bulletTime = reloadInterval;
        }
    }
}
