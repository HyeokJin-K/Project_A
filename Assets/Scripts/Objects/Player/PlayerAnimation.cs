using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAnimation : MonoBehaviour
{
    #region Public Field

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

    #endregion

    #region Private Field

    [SerializeField]
    Player playerScript;

    [SerializeField]
    Animator playerAnimator;

    [SerializeField]
    SpriteRenderer playerSpriteRenderer;

    Vector2 playerMoveDir;

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle

    private void Awake()
    {
        #region Caching

        playerScript = playerScript == null ? GetComponent<Player>() : playerScript;

        playerAnimator = playerAnimator == null ? GetComponent<Animator>() : playerAnimator;

        playerSpriteRenderer = playerSpriteRenderer == null ? GetComponent<SpriteRenderer>() : playerSpriteRenderer;

        #endregion

        playerScript.OnPlayerMove += () => PlayerMoveDir = playerScript.MoveDir;

        playerScript.OnPlayerMove += () => playerAnimator.SetBool("IsMove", true);

        playerScript.OnPlayerMoveStop += () => playerAnimator.SetBool("IsMove", false);
    }

    #endregion    
}
