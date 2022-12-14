using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NetworkPlayerState : BaseNetworkState<NetworkPlayerController>
{
    public NetworkPlayerState(NetworkPlayerController networkPlayerController) : base(networkPlayerController)
    {
        ;
    }

    protected void MovePlayer()
    {
        Vector3 movementInputTo3DSpace = new Vector3(context.MovementInput.x, 0, context.MovementInput.y);

        // Move Character
        Vector3 movementVector = Vector3.Lerp(context.Rb.velocity, movementInputTo3DSpace * (context.CurrentSpeed + 1 - context.Acceleration), context.Acceleration);
        context.Rb.velocity = movementVector;


        // Capture the direction to which the legs have to face
        if (movementInputTo3DSpace.magnitude > 0.01f)
        {
            context.lastDirectionFaced = context.Rb.velocity.normalized;
        }

        // Rotate The legs to the last DirectionFaced
        context.SetFacingLocalPosition(
            Vector3.Slerp(
                context.GetFacingLocalPosition(),
                context.lastDirectionFaced,
                context.TurnSpeed).normalized
                );

        // Set animator Values
        float moveSpeedClamped = context.Rb.velocity.magnitude / context.CurrentSpeed;
        if (moveSpeedClamped < 0.01f)
            moveSpeedClamped = 0f;

        context.Animator.SetFloat("speed", moveSpeedClamped);

    }
}

