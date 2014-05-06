using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour {

	public static float castPoint;
	public static float baseDamage;
	public static float baseCooldown;

	public abstract float Cooldown {
		get;
		set;
	}

	public Spell()
	{
	}

	public abstract void CastSpell (Vector3 spawnPoint);
}
