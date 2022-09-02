using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using MyBox;
using System.Collections.Generic;
using MyBox.Internal;
using Object = UnityEngine.Object;

namespace EnemyAI
{
	// EnemyHealth is a the enemy NPC specific health manager.
	// Any in-game entity that reacts to a shot must have a HealthManager script.
	public class EnemyHealth : HealthManager
	{
		[Tooltip("The current NPC health.")]
		public float health = 100f;
		[Tooltip("The NPC health HUD prefab.")]
		public GameObject healthHUD;
		[Tooltip("The game object particle emitted when hit.")]
		public GameObject bloodSample;
		[Tooltip("Use headshot damage multiplier?")]
		public bool headshot;

		private float totalHealth;                                  // The total NPC initial health.
		[SerializeField]
		public Transform weapon;                                   // The NPC weapon.
		private Transform hud;                                      // The current NPC health HUD on scene.
		private RectTransform healthBar;                            // The NPC health bar on HUD.
		private float originalBarScale;                             // The initial NPC health bar size.
		private HealthBillboardManager healthUI;                    // The NPC health HUD.
		private Animator anim;                                      // The NPC animator controller.
		private StateController controller;
		// The NPC AI FSM controller.
		[SerializeField]
		private ConfigHouseEnemy configHouse;

		public enum TypeTarget
		{
			Enemy,
			Civil
		};

		public TypeTarget mytype;

		private void Awake()
		{
			// Create the health HUD.
			hud = GameObject.Instantiate(healthHUD, transform).transform;

			// Set up the references.
			totalHealth = health;
			healthBar = hud.transform.Find("Bar").GetComponent<RectTransform>();
			healthUI = hud.GetComponent<HealthBillboardManager>();
			originalBarScale = healthBar.sizeDelta.x;
			anim = GetComponent<Animator>();
			controller = GetComponent<StateController>();

			// Find the NPC weapon.
			/*if (weapon == null)
			{
				foreach (Transform child in anim.GetBoneTransform(HumanBodyBones.RightHand))
				{
					weapon = child.Find("muzzle");
					if (weapon != null)
					{
						break;
					}
				}
			}

			weapon = weapon.parent;*/
		}

		private void Start()
		{
			configHouse = GameObject.FindObjectOfType<ConfigHouseEnemy>();
		}

		[ButtonMethod()]
		public void TakeDamageDebug()
		{
			health = 0;
			if (health <= 0)
			{
				if (!dead)
					Kill();

				// Shooting a dead body? Just apply shot force on the ragdoll part.
				//bodyPart.GetComponent<Rigidbody>().AddForce(100f * direction.normalized, ForceMode.Impulse);

			}

			// Kill the NPC?

		}
		// Receive damage from shots taken.
		public override void TakeDamage(Vector3 location, Vector3 direction, float damage, Collider bodyPart, GameObject origin = null)
		{
			// Headshot multiplier. On default values, instantly kills NPC.
			if (!dead && headshot && bodyPart.transform == anim.GetBoneTransform(HumanBodyBones.Head))
			{
				// Default damage multiplier is 10x.
				damage *= 10;
				// Call headshot HUD callback, if any.
				GameObject.FindGameObjectWithTag("GameController").SendMessage("HeadShotCallback", SendMessageOptions.DontRequireReceiver);
			}

			// Create spouted blood particle on shot location.
			Object.Instantiate<GameObject>(bloodSample, location, Quaternion.LookRotation(-direction), this.transform);
			// Take damage received from current health.
			health -= damage;

			// Is the NPC alive?
			if (!dead)
			{
				// Trigger hit animation.
				if(!anim.IsInTransition(3) && anim.GetCurrentAnimatorStateInfo(3).IsName("No hit"))
					anim.SetTrigger("Hit");
				// Show Health bar HUD.
				healthUI.SetVisible();
				UpdateHealthBar();
				// Update FSM related references.
				controller.variables.feelAlert = true;
				controller.personalTarget = controller.aimTarget.position;
			}
			// Time to die.
			if (health <= 0)
			{
				// Kill the NPC?
				if (!dead)
					Kill();

				// Shooting a dead body? Just apply shot force on the ragdoll part.
				bodyPart.GetComponent<Rigidbody>().AddForce(100f * direction.normalized, ForceMode.Impulse);
			}
		}

		// Remove unecessary components on killed NPC and set as dead.
		public void Kill()
		{
			configHouse = GameManager.instance.RandonHouse();
			switch (mytype)
			{
				case TypeTarget.Enemy:
					GameManager.instance.AddEnemyDead();
					if (configHouse != null)
					{
						
						configHouse.RandomEnemyTypesAndSpan();

					}
					else
					{
						configHouse = GameObject.FindObjectOfType<ConfigHouseEnemy>();
						configHouse.RandomEnemyTypesAndSpan();
					}
					break;
				case TypeTarget.Civil:
					GameManager.instance.AddCivilDead();
					if (configHouse != null)
					{
						configHouse.RespawOneCivil();
					}
					else
					{
						configHouse = GameObject.FindObjectOfType<ConfigHouseEnemy>();
					}
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}



				// Destroy all other MonoBehaviour scripts attached to the NPC.
				foreach (MonoBehaviour mb in this.GetComponents<MonoBehaviour>())
				{
					if (this != mb)
						Destroy(mb);
				}
				Destroy(this.GetComponent<NavMeshAgent>());
				RemoveAllForces();
				anim.enabled = false;
				Destroy(weapon.gameObject);
				Destroy(hud.gameObject);
				dead = true;
				Destroy(this.gameObject,3f);

		}



		// Update health bar HUD to current NPC health.
		private void UpdateHealthBar()
		{
			float scaleFactor = health / totalHealth;

			healthBar.sizeDelta = new Vector2(scaleFactor * originalBarScale, healthBar.sizeDelta.y);
		}

		// Remove existing forces and set ragdoll parts as not kinematic to interact with physics.
		private void RemoveAllForces()
		{
			foreach (Rigidbody member in GetComponentsInChildren<Rigidbody>())
			{
				member.isKinematic = false;
				member.velocity = Vector3.zero;
			}
		}

		IEnumerator ResetEnemy()
		{
			yield return new WaitForSeconds(3);
			this.transform.localPosition = new Vector3(0, 0, 0);
			configHouse.RandomEnemyTypesAndSpan();

		}
	}
}