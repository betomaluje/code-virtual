using UnityEngine;

public class AnteaterHealth : MonoBehaviour
{
    [SerializeField] private float timeForFirstDecrease = 5f;
    [SerializeField] private float timeForDecreaseHealth = 2f;
    [SerializeField] private int decreaseRate = 2;
    [SerializeField] private int healthPerAnt = 4;

    [Header("Particles")]
    [SerializeField] private Transform feedParticles;

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(DecreaseHealth), timeForFirstDecrease, timeForDecreaseHealth);
    }

    private void DecreaseHealth()
    {
        _health.Damage(decreaseRate);
    }

    ///<summary>
    ///Called from the AntCollector script. Feeds the Anteater.
    ///</summary>
    ///<param name="amount">the amount of ants</param>
    public void Feed(int amount)
    {
        _health.Heal(amount * healthPerAnt);

        if (feedParticles != null)
        {
            Instantiate(feedParticles, transform.position + Vector3.up * 2, Quaternion.identity);
        }
    }
}
