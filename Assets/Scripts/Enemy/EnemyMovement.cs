using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] 
	private float _isRadiusAttack = 15f;
	
	private EnemyStats _enemy;
	private Transform _player;

	private void Start()
	{
		_enemy = GetComponent<EnemyStats>();
		_player = FindObjectOfType<PlayerMovement>().transform;
	}

	private void Update()
	{
		if (Vector2.Distance(_player.transform.position, transform.position) <= _isRadiusAttack)
		{
			transform.position = Vector2.MoveTowards(transform.position, _player.transform.position,
				_enemy.CurrentMoveSpeed * Time.deltaTime);
		}
	}
}