using UnityEngine;

public class Player : MonoBehaviour
{
    public static int SCORE = 0;

    public float thrustForce = 100f;
    public float rotationSpeed = 120f;

    private Rigidbody2D _rigid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime;
        float rotation = Input.GetAxis("Rotate") * Time.deltaTime;

        Vector2 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);
    }
}
