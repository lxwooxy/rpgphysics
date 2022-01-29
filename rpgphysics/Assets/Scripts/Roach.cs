using UnityEngine;

public class Roach : MonoBehaviour
{
    private bool isDead;
    private Animator anim;
    private Collider2D myCollider;
    void Start()
    {
        anim = GetComponent<Animator> ();
        myCollider = GetComponent<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            anim.SetBool("isDead", true);
            isDead = true;
        }
    }
     void Update()
    {
        if (isDead)
			Die();
    }
    public void Die()
    {
        if (isDead) 
        {
            isDead = true;
            Destroy (gameObject,1);
        } 
    }
}
