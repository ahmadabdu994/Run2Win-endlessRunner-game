using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // classes
    [System.Serializable]
    public class MoveSettings
    {
        public float forwardVelocity = 10;
        public float jumpVelocity = 16;
    }

    [System.Serializable]
    public class PhysicalSettings
    {
        public float speedLess = 0.75f;
    }


    // take an objects or referances

    [SerializeField] public GameObject restartGame;
    public MoveSettings movementSettings = new MoveSettings();
    public PhysicalSettings physicSettings = new PhysicalSettings();
    public static int coinCount;
    public bool IsCoinActive = true;
    public GameObject coinCountDisplay;
    public bool scoreIsActive = true;
    private float score = 0.0f;
    [SerializeField] public Text scoreText;
    public Vector3 velocity;
    public AudioSource audioSource;
    [SerializeField] public AudioClip jumpSound;
    [SerializeField] public AudioClip slideSound;

    public Rigidbody rb;
    public Animator an;
    public CapsuleCollider col;
    public float colSize;
    public float colHight;
    public Vector3 colCenter;
    public int jumpInput = 0;
    public bool isGrounded = false;
    public bool isDead = false;
    public float rightLeftSpeed = 10;
    public float rightLeftMovement = 0;
    private int slideInput = 0;



    // Start method
    void Start()
    {
        enableAccessToUnityComponant();
    }

    public void enableAccessToUnityComponant()
    {
        rb = GetComponent<Rigidbody>();
        an = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        velocity = Vector3.zero;

        col = GetComponent<CapsuleCollider>();
        colHight = col.height;
        colCenter = col.center;
    }


    // FixedUpdate method
    void FixedUpdate()
    {
        InputHandling();
        Run();
        CheckGround();
        Jump();
        MoveXAxis();
        Slide();
        rb.velocity = velocity;
        movementSettings.forwardVelocity += 0.09f * Time.deltaTime;
    }


    // update method
    void Update()
    {
        scoreCalc();
        coinCountDisplay.GetComponent<Text>().text = "" + coinCount;
    }

    // functions or methods
    void Run()
    {
        velocity.z = movementSettings.forwardVelocity;
    }

    void MoveXAxis()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(rightLeftMovement, transform.position.y, transform.position.z), Time.deltaTime * rightLeftSpeed);
    }

    void InputHandling()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            jumpInput = 1;
            col.height = colHight;
            col.center = colCenter;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {

            if (rightLeftMovement == 0)
            {
                rightLeftMovement = 3;
            }
            else if (rightLeftMovement == -3)
            {
                rightLeftMovement = 0;
            }
            col.height = colHight;
            col.center = colCenter;

        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (rightLeftMovement == 0)
            {
                rightLeftMovement = -3;
            }
            else if (rightLeftMovement == 3)
            {
                rightLeftMovement = 0;
            }
            col.height = colHight;
            col.center = colCenter;


        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            slideInput = 1;
            colSize = an.GetFloat("ColliderSize");
            if (colSize > 0.2f & colSize < 1)
            {
                col.height = 1;
                col.center = new Vector3(col.center.x, 0.50f, col.center.z);
            }

        }


    }

    void CheckGround()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, 0.5f);
        isGrounded = false;
        rb.useGravity = true;
        foreach (var hit in hits)
        {
            if (!hit.collider.isTrigger)
            {
                if (velocity.y <= 0)
                {
                    rb.position = Vector3.MoveTowards(rb.position, new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z), Time.deltaTime * 10);
                }
                rb.useGravity = false;
                isGrounded = true;
                break;
            }
        }
    }

    void Jump()
    {
        if (jumpInput == 1 && isGrounded)
        {
            velocity.y = movementSettings.jumpVelocity;
            an.SetTrigger("Jump");
            audioSource.PlayOneShot(jumpSound);
            an.Play("Big Jump");
        }
        else if (jumpInput == 0 && isGrounded)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y -= physicSettings.speedLess;
        }
        jumpInput = 0;

    }

    void Slide()
    {
        if (slideInput == 1 && isGrounded)
        {
            an.SetTrigger("Slide");
            slideInput = 0;
            audioSource.PlayOneShot(slideSound);
            an.Play("Slide");
        }

    }

    public void killPlayer()
    {
        isDead = true;
        movementSettings.forwardVelocity = 0;
        an.SetTrigger("Die");
        col.height = 0.8f;
        GetComponent<PlayerController>().enabled = false;// to stop player controller 
        restartGame.SetActive(true);
        saveCoins();
        highscoreDisplay();

    }

    void scoreCalc()
    {
        if (scoreIsActive)
        {
            score += 2 * Time.deltaTime;
            scoreText.text = ((int)score).ToString();
        }
    }

    public void highscoreDisplay()
    {
        scoreIsActive = false;
        if (PlayerPrefs.GetFloat("Highscore") < score)
        {
            PlayerPrefs.SetFloat("Highscore", score);
        }
    }

    public void saveCoins()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + coinCount);
        PlayerPrefs.Save();
    }
}
