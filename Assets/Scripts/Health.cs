using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Slider _healthBar;
    private Image _healthFill;
    private TMP_Text _conditionText;
    private Player _player;

    private int _maxHealth;

    private void Start()
    {
        _healthBar = transform.Find("HealthBar").GetComponent<Slider>();
        _healthFill = _healthBar.fillRect.GetComponent<Image>();
        _conditionText = transform.Find("ConditionText").GetComponent<TMP_Text>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        _maxHealth = _player.GetDefaultHealth();
        _healthBar.maxValue = _maxHealth;
    }

    private void Update()
    {
        _healthBar.value = _player.GetPlayerHealth();
        var healthPercent = (float)_healthBar.value / _maxHealth;
        _healthFill.color = Color.Lerp(Color.red, Color.green, healthPercent);

        _conditionText.text = _player.GetPlayerCondition();
    }
}
