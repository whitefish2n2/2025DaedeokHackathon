using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 1.0f;
    private float gravityValue = -9.8f;
    private Animator animator;
    private State currentState = State.Idle;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) playerVelocity.y = 0f;
        // todo: 창용이의 input system으로 바꾸기
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        controller.Move(move * (Time.deltaTime * playerSpeed));

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            if (currentState != State.Moving)
            {
                animator.CrossFade("Walking (1)", 0.1f);
                currentState = State.Moving;
            }
        }
        else {
            if (currentState != State.Idle)
            {
                animator.CrossFade("Idle", 0.1f);
                currentState = State.Idle;
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private enum State
    {
        Moving,
        Idle,
        Running
    }
}
