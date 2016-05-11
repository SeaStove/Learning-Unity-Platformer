using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;
        public int currentHealth {
            get { return _currentHealth; }
            set { _currentHealth = Mathf.Clamp(value,0,maxHealth);}
        }
        private int _currentHealth;

        public void Init()
        {
            currentHealth = maxHealth;
        }

    }

    public PlayerStats playerStats = new PlayerStats();

    public int fallBoundry = -20;

    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        playerStats.Init();
        if (statusIndicator == null)
        {
            Debug.LogError("No status indicator referenced on player");
        }
        else
        {
            statusIndicator.SetHealth(playerStats.currentHealth, playerStats.maxHealth);
        }
    }

    public void Update()
    {
        if (transform.position.y <= fallBoundry)
        {
            DamagePlayer(999999);
        }
    }

    public void DamagePlayer(int damage)
    {
        playerStats.currentHealth -= damage;
        statusIndicator.SetHealth(playerStats.currentHealth, playerStats.maxHealth);
        if (playerStats.currentHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }
    }

    public void HealPlayer(int heal)
    {
        playerStats.currentHealth += heal;
        statusIndicator.SetHealth(playerStats.currentHealth, playerStats.maxHealth);
    }

}
