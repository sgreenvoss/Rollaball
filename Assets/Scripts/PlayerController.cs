using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    // speed at which player moves.
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    // rigidbody of player.
    private Rigidbody rb;
    private int count;
    private float jumpStrength = 3.0f;
    private int jumpCount = 0;
    private int maxJumps = 2;

    // movement along x and y axes
    private float movementX;
    private float movementY;

    // called before first frame update
    void Start()
    {
        // get and store rigidbody component attached to player.
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winTextObject.SetActive(false);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // Reset vertical velocity
        rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        jumpCount++;

    }
    // called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // create 3d movement vector using x and y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // apply force to rigidbody to move the player.
        rb.AddForce(movement * speed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Jump();
        }

        if (transform.position.y < 0.0f)
        {
            Lose();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Lose();
        }
        else if (collision.gameObject.CompareTag("DynamicBox")) 
        {
            
        }
        else
        {
            jumpCount = 0;
        }
     }

    private void Lose()
    {
        winTextObject.gameObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You lose!";

        gameObject.SetActive(false);
    }

    // called when move input detected
    private void OnMove(InputValue movementValue)
    {
        // convert into vec2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();
        // store each component of movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count > 9)
        {
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You win!!";
            
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            int length = enemies.Length;
            GameObject enemy;
            Light enemyLight;

            for (int i = 0; i < length; i++)
            {
                enemy = enemies[i];
                enemyLight = enemy.GetComponent<Light>();
                if (enemyLight != null)
                {
                    enemyLight.enabled = false;
                }
                enemy.SetActive(false);
            }          
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            setCountText();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
