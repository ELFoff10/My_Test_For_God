using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	[SerializeField]
	private List<SpriteRenderer> _spriteRenderer;
	
	private Animator _animator;
	private PlayerMovement _playerMovement;
	private static readonly int MoveSide = Animator.StringToHash("MoveSide");

	private void Start()
	{
		_animator = GetComponent<Animator>();
		_playerMovement = GetComponent<PlayerMovement>();
	}

	private void Update()
	{
		MoveAnimation();
		SpriteFlipX();
	}

	private void MoveAnimation()
	{
		if (_playerMovement.MoveDir.x != 0 || _playerMovement.MoveDir.y != 0)
		{
			_animator.SetBool(MoveSide, true);
		}
		else
		{
			_animator.SetBool(MoveSide, false);
		}
	}

	private void SpriteFlipX()
	{
		foreach (var sprites in _spriteRenderer)
		{
			sprites.flipX = _playerMovement.MoveDir.x < 0;
		}
	}
}