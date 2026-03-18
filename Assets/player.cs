using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float jumpSpeed;
    private float xVelocity;
    private float yVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xVelocity = Input.GetAxisRaw("Horizontal") * speed;
        yVelocity = Input.GetAxisRaw("Vertical") * jumpStrength;

        Vector3 vectorVelocity = new Vector3 (xVelocity, yVelocity);

        transform.position += vectorVelocity * Time.deltaTime;


    }
}
