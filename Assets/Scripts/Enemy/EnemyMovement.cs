using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	private EnemyStats _enemy;
	private Transform _player;
	private bool _isRadiusAttack;

	private void Start()
	{
		_enemy = GetComponent<EnemyStats>();
		_player = FindObjectOfType<PlayerMovement>().transform;
	}

	private void Update()
	{
		
		transform.position = Vector2.MoveTowards(transform.position, _player.transform.position,
			_enemy.CurrentMoveSpeed * Time.deltaTime);
	}
}