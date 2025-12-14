using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    private float lastHitTime = -999f;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
   

    [Header ("iFrames")]
    [SerializeField] private float invulnerabilityDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damageAmount)
    {
        StartCoroutine(Invulnerability());

        if (Time.time < lastHitTime + invulnerabilityDuration || dead)
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

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(11, 12, true);
        for(int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.9f);
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2));
        }   

        Physics2D.IgnoreLayerCollision(11, 12, false);


    }

   
}
