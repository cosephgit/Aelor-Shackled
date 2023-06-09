using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

    public delegate void OnHealthChanged (float previousHealth, float health);
	public event OnHealthChanged onHealthChanged = delegate {};

	public float maxHealth = 100;
	public float health;

	public float oldHealth;

    public Slider playerSlider;
    public bool isPlayer;

	bool canChange;
	public bool isEnemy;
	public Slider enemySlider;

	void Awake () {
		health = maxHealth;

		if (isEnemy) {
			enemySlider.value = health;
		}

        if (isPlayer) {
			playerSlider.value = health;
		}
	}

	public void TakeDamage (float damage) {
		canChange = true;
		float oldHealth = health;
		
		if (canChange) {
			health -= damage;
			health = Mathf.Clamp(health, 0, maxHealth);
			onHealthChanged(oldHealth, health);
		}

		if (isEnemy) {
			enemySlider.value = 100f * health / maxHealth;
			if (health <= 0) {
				BattleManager.instance.BattleEnd(true);
			}
		}

        if (isPlayer) {
			playerSlider.value = 100f * health / maxHealth;
			if (health <= 0) {
				BattleManager.instance.BattleEnd(false);
			}
		}
	}

	public void AddHealth(float h) {
		health += h;
		health = Mathf.Clamp(health, 0, maxHealth);
		onHealthChanged(oldHealth, health);
	}
}

