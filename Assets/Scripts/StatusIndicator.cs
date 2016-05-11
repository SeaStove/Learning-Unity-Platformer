using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour {
    
    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text healthText;

    void Start()
    {
        if (healthBarRect == null)
        {
            Debug.LogError("STATUS INDICATOR: No health bar reference");
        }
        if (healthText == null)
        {
            Debug.LogError("STATUS INDICATOR: No health bar reference");
        }
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        float value = (float)currentHealth / maxHealth;
        //should change health bar color on damage in the future using a Gradient 

        healthBarRect.localScale = new Vector3(value, healthBarRect.localScale.y, healthBarRect.localScale.z);
        healthText.text = currentHealth + "/" + maxHealth + " HP";
    }
}
