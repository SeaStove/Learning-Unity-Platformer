using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [System.Serializable]
    public class EnemyStats
    {
        private int _currentHealth;        

        public int maxHealth = 100;
        public int currentHealth
        {
            get { return _currentHealth; }
            set
            {
                _currentHealth = Mathf.Clamp(value, 0, maxHealth);
            }
        }

        public int damage = 40;

        public void Init()
        {
            currentHealth = maxHealth;
        }
    }

    public EnemyStats stats = new EnemyStats();
    public Transform deathParticles;

    public float shakeAmount = 0.1f;
    public float shakeLength = 0.1f;

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        stats.Init();

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);
        }
        if (deathParticles == null)
        {
            Debug.LogError("No Dp referenced on enemy");
        }
    }

    public void DamageEnemy(int damage)
    {
        stats.currentHealth -= damage;
        if (stats.currentHealth <= 0)
        {
            GameMaster.KillEnemy(this);
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);
        }
    }

    void OnCollisionEnter2D(Collision2D colliderInfo)
    {
        Player player = colliderInfo.collider.GetComponent<Player>();
        if (player != null)
        {
            player.DamagePlayer(stats.damage);
            DamageEnemy(99999);
        }

    }
}
