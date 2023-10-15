using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	[Header("Weapon ScriptableObject")]
	public WeaponScriptableObject WeaponData;

	private float _currentCooldown;
	protected PlayerMovement PlayerMovement;

	protected virtual void Start()
	{
		PlayerMovement = FindObjectOfType<PlayerMovement>();
		_currentCooldown = WeaponData.CooldownDuration;
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	protected virtual void Attack()
	{
	}
}