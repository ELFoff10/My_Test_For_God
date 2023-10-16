using System;
using UniRx;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	[Header("Weapon ScriptableObject")]
	public WeaponScriptableObject WeaponData;
	
	[HideInInspector]
	public int CurrentCartridges;
	
	private PointerClickHold _pointerClickHold;
	private float _currentCooldown;
	
	private CompositeDisposable _disposable;
	protected PlayerMovement PlayerMovement;
	public event Action OnCartridgesUpdate;
	
	protected virtual void Start()
	{
		PlayerMovement = FindObjectOfType<PlayerMovement>();
		_pointerClickHold = FindObjectOfType<PointerClickHold>();
		_currentCooldown = WeaponData.CooldownDuration;
		CurrentCartridges = WeaponData.MaxCartridges;

		Observable.Timer(TimeSpan.FromSeconds(_currentCooldown))
			.Repeat()
			.Subscribe(_ =>
			{
				{
					if (_pointerClickHold.IsHold && CurrentCartridges > 0)
					{
						Attack();
						CurrentCartridges--;
						OnCartridgesUpdate?.Invoke();
					}
				}
			}).AddTo(_disposable);
	}

	private void OnEnable()
	{
		_disposable = new CompositeDisposable();
	}

	private void OnDisable()
	{
		_disposable?.Clear();
	}

	protected virtual void Attack()
	{
	}
}