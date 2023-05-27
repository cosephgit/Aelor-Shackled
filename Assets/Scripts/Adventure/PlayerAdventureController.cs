using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdventureController : MonoBehaviour
{
    [SerializeField]private SpriteRenderer playerSprite;
    [SerializeField]private Animator playerAnimator;
    [SerializeField]private float playerSpeed = 4f;
    Vector3 moveTarget;
    bool moving = false;

    // sets the movement target point for the player
    // this should already be validated within the defined moveable area in the SceneManager
    public void SetMoveTarget(Vector2 pos)
    {
        moveTarget = pos;
        moving = true;
    }

    // update the player position each frame if needed
    private void Update()
    {
        if (moving)
        {
            Vector3 offset = moveTarget - transform.position;

            if (Mathf.Approximately(offset.magnitude, 0))
            {
                playerAnimator.SetFloat("moveX", 0);
                playerAnimator.SetFloat("moveY", 0);
                moving = false;
            }
            else
            {
                float frameSpeed = playerSpeed * Time.deltaTime;
                Vector3 move;

                if (offset.magnitude <= frameSpeed)
                {
                    move = offset;
                }
                else
                {
                    move = offset.normalized * frameSpeed;
                }

                playerAnimator.SetFloat("moveX", move.x);
                playerAnimator.SetFloat("moveY", move.y);

                move += transform.position;

                transform.position = move;
            }
        }
    }
}
