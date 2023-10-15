using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private FixedJoystick _fixedJoystick;

	public float LastHorizontalVector;
	public float LastVerticalVector;
	public Vector2 MoveDir;
	public Vector2 LastMovedVector;

	private Rigidbody2D _rigidbody2D;
	private PlayerStats _player;

	private void Start()
	{
		_player = GetComponent<PlayerStats>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
		LastMovedVector = new Vector2(1, 0f);
	}

	private void Update()
	{
		InputManagement();
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void InputManagement()
	{
		if (GameManager.Instance.IsGameOver)
		{
			return;
		}

		var moveX = _fixedJoystick.Horizontal;
		var moveY = _fixedJoystick.Vertical;

		MoveDir = new Vector2(moveX, moveY).normalized;

		if (MoveDir.x != 0)
		{
			LastHorizontalVector = MoveDir.x;
			LastMovedVector = new Vector2(LastHorizontalVector, 0f);
		}

		if (MoveDir.y != 0)
		{
			LastVerticalVector = MoveDir.y;
			LastMovedVector = new Vector2(0f, LastVerticalVector);
		}

		if (MoveDir.x != 0 && MoveDir.y != 0)
		{
			LastMovedVector = new Vector2(LastHorizontalVector, LastVerticalVector);
		}
	}

	private void Move()
	{
		if (GameManager.Instance.IsGameOver)
		{
			return;
		}

		_rigidbody2D.velocity =
			new Vector2(MoveDir.x * _player.CurrentMoveSpeed, MoveDir.y * _player.CurrentMoveSpeed);
	}
}