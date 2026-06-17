using UnityEngine;

public class RazanPMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // «·Õ—ﬂ… Ì„Ì‰ ÊÌ”«— Ê›Êﬁ Ê Õ  »√“—«— WASD √Ê «·√”Â„
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(transform.position + move * speed * Time.deltaTime);

        // «·ﬁ›“ »“— «·„”«›… (Space)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // «· Õﬁﬁ „‰ √‰ «··«⁄» Ì·„” «·√—÷ ·Ìﬁ›“ „Ãœœ«
    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}