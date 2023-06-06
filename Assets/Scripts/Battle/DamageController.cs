using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    public float damage;

	public ParticleSystem frostBeamEffect;
	public GameObject burnEffect;

	void OnCollisionEnter2(Collision2D c) {
		HitObject(c.gameObject);
        Destroy(gameObject);
		Debug.Log(c);
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (this.gameObject.tag == "FireBurnEffect") {

			if (c.gameObject.CompareTag("Enemy") || c.gameObject.CompareTag("Player")) {
				GameObject effect = Instantiate(burnEffect, this.transform.position + this.transform.up, this.transform.rotation);
				Destroy(effect.gameObject, 3f);
			}
		}

		if (this.gameObject.tag == "FrostbiteBeam") {

			if (c.gameObject.CompareTag("Enemy") || c.gameObject.CompareTag("Player")) {
				ParticleSystem effect = Instantiate(frostBeamEffect, transform.position, transform.rotation);
				Destroy(effect.gameObject, 5f);
			}
			
			if(c.gameObject.CompareTag("Enemy")) {
				c.gameObject.GetComponent<EnemyBattleController>().Froze();
			}
		}

		if (c.gameObject.CompareTag("Shield")) {
			Debug.Log(c);
			Destroy(this.gameObject);
		}

		HitObject(c.gameObject);
        Destroy(gameObject);
	}

	void HitObject(GameObject g) {
		if (g.CompareTag("Shield")) {
			SoundSystemManager.instance.PlaySFX("Shield Hit-001");
			Debug.Log(g);
		}
		else {
			SoundSystemManager.instance.PlaySFX("Attack Hit");
		}
		HealthController health = g.GetComponentInParent<HealthController>();
		if (health != null) {
			health.TakeDamage(damage);
		}
	}

	void Update() {
		if (this.gameObject.tag == "Shield") {
			Debug.Log("UPDATE");
			Destroy(this.gameObject, 5f);
		}
	}
}
