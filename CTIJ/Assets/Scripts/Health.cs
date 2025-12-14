using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float hitCooldown = 0.3f;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private float lastHitTime = -999f;


    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        if (Time.time < lastHitTime + hitCooldown || dead)
            return;

        lastHitTime = Time.time;

        currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0, startingHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if(!dead)
            {
                
                anim.SetTrigger("die");
                GetComponent<PlayerController>().enabled = false;
                var bc = GetComponent<BoxCollider2D>();
                bc.size = new Vector2(bc.size.y, 0.12f);
                dead = true;
            
            }


        }
    }

   
}
