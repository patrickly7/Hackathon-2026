using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    private TMP_Text _healthText;
    private TMP_Text _conditionText;
    private Player _player;

    private int _maxHealth;

    private void Start()
    {
        _healthText = transform.Find("HealthText").GetComponent<TMP_Text>();
        _conditionText = transform.Find("ConditionText").GetComponent<TMP_Text>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        _maxHealth = _player.GetDefaultHealth();
    }

    private void Update()
    {
        _healthText.text = _player.GetPlayerHealth().ToString();
        _conditionText.text = _player.GetPlayerCondition();
    }
}
