using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyStats : MonoBehaviour
{
	[SerializeField]
	private EnemyScriptableObject _enemyData;
	[SerializeField]
	private Image _healthBar;
	
	[HideInInspector]
	public float CurrentMoveSpeed;
	[HideInInspector]
	public float CurrentHealth;
	[HideInInspector]
	public float CurrentDamage;
	[HideInInspector]
	public int CurrentExperienceGranted;
	
	private const float DeSpawnDistance = 20f;
	private Transform _player;

	private void Awake()
	{
		CurrentMoveSpeed = _enemyData.MoveSpeed;
		CurrentHealth = _enemyData.MaxHealth;
		CurrentDamage = _enemyData.Damage;
		CurrentExperienceGranted = _enemyData.ExperienceGranted;
	}

	private void Start()
	{
		UpdateHealthBar();
		_player = FindObjectOfType<PlayerStats>().transform;
	}

	private void Update()
	{
		if (Vector2.Distance(transform.position, _player.position) >= DeSpawnDistance)
		{
			ReturnEnemy();
		}

		UpdateHealthBar();
	}

	private void OnDestroy()
	{
		EnemySpawner.Instance.OnEnemyKilled();
	}

	public void TakeDamage(float damage)
	{
		CurrentHealth -= damage;

		if (CurrentHealth <= 0)
		{
			Kill();
		}
	}

	private void Kill()
	{
		var playerStats = FindObjectOfType<PlayerStats>();
		playerStats.IncreaseExperience(CurrentExperienceGranted);
		Destroy(gameObject);
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		if (!other.gameObject.CompareTag("Player")) return;

		var player = other.gameObject.GetComponent<PlayerStats>();
		player.TakeDamage(CurrentDamage);
		
		AudioManager.Instance.EventInstances[(int)AudioNameEnum.DamageEnemy].start();
	}

	private void ReturnEnemy()
	{
		var enemySpawner = FindObjectOfType<EnemySpawner>();
		transform.position = _player.position + enemySpawner
			.RelativeSpawnPoints[Random.Range(0, enemySpawner.RelativeSpawnPoints.Count)].position;
	}
	
	private void UpdateHealthBar()
	{
		_healthBar.fillAmount = CurrentHealth / _enemyData.MaxHealth;
	}
}