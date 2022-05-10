using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    Player playerScript;
    [SerializeField]
    Animator playerAnimator;
    [SerializeField]
    SpriteRenderer playerSpriteRenderer;

    Vector2 playerMoveDir;
    public Vector2 PlayerMoveDir
    {
        get
        {
            return playerMoveDir;
        }
        set
        {
            playerMoveDir = value;

            if (playerMoveDir.x < 0)
            {
                playerSpriteRenderer.flipX = true;
            }
            else if (playerMoveDir.x > 0)
            {
                playerSpriteRenderer.flipX = false;
            }
        }
    }

    private void Awake()
    {
        #region Caching
        playerScript = playerScript == null ? GetComponent<Player>() : playerScript;
        playerAnimator = playerAnimator == null ? GetComponent<Animator>() : playerAnimator;
        playerSpriteRenderer = playerSpriteRenderer == null ? GetComponent<SpriteRenderer>() : playerSpriteRenderer;
        #endregion

        playerScript.OnPlayerMove += SetMoveValue;
        playerScript.OnPlayerMove += () => playerAnimator.SetBool("IsMove", true);
        playerScript.OnPlayerMoveStop += () => playerAnimator.SetBool("IsMove", false);
    }

    public void SetMoveValue()  //  플레이어 애니메이션 조정에 필요한 값 초기화
    {
        PlayerMoveDir = playerScript.MoveDir;

    }
}
