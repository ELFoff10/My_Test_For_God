using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObject/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
	[SerializeField]
	private float _moveSpeed;
	public float MoveSpeed => _moveSpeed;

	[SerializeField]
	private float _maxHealth;
	public float MaxHealth => _maxHealth;

	[SerializeField]
	private float _damage;
	public float Damage => _damage;
	
	[SerializeField]
	private int _experienceGranted;
	public int ExperienceGranted => _experienceGranted;
	
	
}