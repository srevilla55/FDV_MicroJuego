using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] public float thrustForce = 100f;
    [SerializeField] public float rotationSpeed = 120f;

    public static int SCORE = 0;
    public static float xBorderLimit, yBorderLimit;

    [SerializeField] public GameObject gun;
    [SerializeField] public GameObject bulletPrefab;

    private Rigidbody2D _rigid;

    Vector2 thrustDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();

        yBorderLimit = Camera.main.orthographicSize + 1;
        xBorderLimit = (Camera.main.orthographicSize + 1) * Screen.width / Screen.height;
    }

    private void FixedUpdate()
    {
        float thrust = Input.GetAxis("Thrust") * thrustForce * Time.deltaTime;
        float rotation = Input.GetAxis("Rotate") * rotationSpeed * Time.deltaTime;
        
        thrustDirection = transform.right;

        transform.Rotate(Vector3.forward, -rotation);
        _rigid.AddForce(thrust * thrustDirection);
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit + 1;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit - 1;
        else if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit + 1;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit - 1;
        transform.position = newPos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            Bullet bulletScript = bullet.GetComponent<Bullet>();

            bulletScript.targetVector = transform.right;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("Salvado no es un meteorito...");
        }
    }
}
