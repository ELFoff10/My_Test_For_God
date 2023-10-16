using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObject/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
	[SerializeField]
	private ProjectileBehaviour _prefab;
	public ProjectileBehaviour Prefab => _prefab;
	
	[SerializeField]
	private ProjectileBehaviour _prefabGun;
	public ProjectileBehaviour PrefabGun => _prefabGun;
	
	[SerializeField]
	private int _maxCartridges;
	public int MaxCartridges => _maxCartridges;
	
	[SerializeField]
	private float _damage;
	public float Damage => _damage;
	
	[SerializeField]
	private float _speed;
	public float Speed => _speed;
	
	[SerializeField]
	private float _cooldownDuration;
	public float CooldownDuration => _cooldownDuration;
	
	[SerializeField]
	private int _pierce;
	public int Pierce => _pierce;
	
	[SerializeField]
	private int _level;
	public int Level => _level;
	
	[SerializeField]
	private ProjectileController _nextLevelPrefab;
	public ProjectileController NextLevelPrefab => _nextLevelPrefab;
	
	[SerializeField]
	private string _name;
	public string Name => _name;
	
	[SerializeField]
	private string _description;
	public string Description => _description;
	
	[SerializeField]
	private Sprite _icon;
	public Sprite Icon => _icon;
}