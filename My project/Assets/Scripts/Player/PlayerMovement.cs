using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private float horizontalInput;

    private void Awake()
    {
        //reference a gameobjektekt�l
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        
        //Character forg�s
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        //Ugr�s
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }

        //animator param�terek
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);
    
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground"){
            grounded = true;
        }
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && grounded;
    }
}
