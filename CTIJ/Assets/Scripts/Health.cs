using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float damageAmount)
    {
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
            dead = true;
            }
        }
    }

   
}
