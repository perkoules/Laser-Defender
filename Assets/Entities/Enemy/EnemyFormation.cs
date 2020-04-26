﻿using UnityEngine;
using System.Collections;

public class EnemyFormation: MonoBehaviour {
	
	public GameObject projectile;
	public float health = 150f;
	public float projectileSpeed = 10f;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150; 

	public AudioClip fireSound;
	public AudioClip deathSound;

	private ScoreKeeper scoreKeeper;

	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0){
				Die ();
			}
		}
	}
	
	void Update(){
		float probability = shotsPerSeconds * Time.deltaTime;
		if(Random.value < probability){
			Fire ();
		}
	}
	
	
	void Fire(){
		GameObject laser = Instantiate (projectile,transform.position,Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity =new Vector2 (0, -projectileSpeed); 
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	void Die(){
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
		AudioSource.PlayClipAtPoint(deathSound,transform.position);
	}
}
