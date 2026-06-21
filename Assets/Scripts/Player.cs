using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] float moveSpeed = 6;
    Animator anim;
    Rigidbody2D rb;
   
    float maxHealth = 100;
    float currentHealth;

    bool dead = false;

    float moveHorizontal, moveVertical;
    Vector2 movement;

    

    int facingDirection = 1;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();


        currentHealth = maxHealth;
        healthText.text = maxHealth.ToString();
    }


    private void Update()
    {

        // for testing
        if(Input.GetKey(KeyCode.Space))
        {
            Hit(10);
        }
        if(dead)
        {
            movement = Vector2.zero;
            anim.SetFloat("velocity", 0);
            return;
        }
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical).normalized;

        anim.SetFloat("velocity", movement.magnitude);

        if(movement.x != 0)
        {
            facingDirection = movement.x > 0 ? 1:-1;

        }
        transform.localScale = new Vector2(facingDirection, 1);


    }

    private void FixedUpdate()
    {
        
        
        rb.linearVelocity = movement * moveSpeed;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    public void Hit(int damage)
    {
        anim.SetTrigger("Hit");
        currentHealth -= damage;
        healthText.text = Mathf.Clamp(currentHealth, 0, maxHealth).ToString();


        if(currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        dead = true;
        // call game over
    }
}
