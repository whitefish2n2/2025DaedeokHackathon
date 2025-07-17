using Unity.Cinemachine;
using UnityEngine;
using Util;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 1.0f;
    private float gravityValue = -9.8f;
    private Animator animator;
    private State currentState = State.Idle;
    private Position currentPosition = Position.Standing;
    private bool stunning;
    private bool running;
    
    private CinemachineImpulseSource impulseSource;
    
    public bool watchingRight { get; private set; }
    

    public bool startRunning = false;

    public bool stopRunning = false;

    private float zTemp;
    private void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        animator = GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
        animator.Play("Idle");
        zTemp = transform.position.z;
    }

    public void Zoom(bool status)
    {
        if (status)
        {
            stunning = true;
            if(watchingRight) transform.eulerAngles = new Vector3(transform.eulerAngles.x,-240,transform.eulerAngles.z);
            else transform.eulerAngles = new Vector3(transform.eulerAngles.x,-120,transform.eulerAngles.z);
            switch (currentPosition)
            {
                case Position.Standing:
                    animator.CrossFade("Rifle Aiming Idle",0.05f);
                    break;
                case Position.Kneel:
                    animator.CrossFade("Idle Crouching Aiming",0.05f);
                    break;
                case Position.Prone:
                    animator.CrossFade("Prone Idle",0.05f);
                    break;
            }
        }
        else
        {
            stunning = false;
            switch (currentPosition)
            {
                case Position.Standing:
                    animator.CrossFade("Rifle Idle",0.1f);
                    break;
                case Position.Kneel:
                    animator.CrossFade("Idle Crouching",0.1f);
                    break;
                case Position.Prone:
                    animator.CrossFade("Prone Idle",0.1f);
                    break;
            }
        }
    }

    public void Heal()
    {
        stunning = true;
        switch (currentPosition)
        {
            case Position.Standing:
                animator.CrossFade("Reloading",0.1f);
                break;
            case Position.Kneel:
                animator.CrossFade("Reload",0.1f);
                break;
            case Position.Prone:
                animator.CrossFade("Prone Reloading",0.1f);
                break;
        }
    }

    public void CancelHeal()
    {
        stunning = false;
        switch (currentPosition)
        {
            case Position.Standing:
                animator.CrossFade("Rifle Idle",0.01f);
                break;
            case Position.Kneel:
                animator.CrossFade("Idle Crouching",0.01f);
                break;
            case Position.Prone:
                animator.CrossFade("Prone Idle",0.01f);
                break;
        }
    }

    public void Shot()
    {
        impulseSource.GenerateImpulse();//카메라 흔들기
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            startRunning = true;
            stopRunning = false;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            startRunning = false;
            stopRunning = true;
        }
        if (stunning) return;
        else
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (currentPosition == Position.Standing)
            {
                currentPosition = Position.Kneel;
                animator.CrossFade("Rifle Stand To Kneel", 0.1f);
            }
            else if (currentPosition == Position.Kneel)
            {
                currentPosition = Position.Standing;
                animator.CrossFade("Rifle Idle",0.3f);
            }
            else if (currentPosition == Position.Prone)
            {
                currentPosition = Position.Kneel;
                animator.CrossFade("Rifle Prone To Kneel", 0.3f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentPosition == Position.Standing)
            {
                currentPosition = Position.Prone;
                animator.CrossFade("Rifle Prone To Kneel", 0.1f);
                animator.CrossFade("Rifle Kneel To Prone", 0.1f);
            }
            else if (currentPosition == Position.Kneel)
            {
                currentPosition = Position.Prone;
                animator.CrossFade("Rifle Kneel To Prone",0.3f);
            }
            else if (currentPosition == Position.Prone)
            {
                currentPosition = Position.Standing;
                animator.CrossFade("Rifle Kneel To Prone", 0.1f);
                animator.CrossFade("Rifle Prone To Kneel", 0.1f);
            }
        }
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) playerVelocity.y = 0f;
        // todo: 창용이의 input system으로 바꾸기
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0f);
        controller.Move(move * (Time.deltaTime * playerSpeed));
        if (move != Vector3.zero)
        {
            watchingRight = move.x > 0;
            if (currentState == State.Dying) return;
            if (watchingRight && currentPosition != Position.Prone) move.z = -1f; 
            gameObject.transform.forward = move;
            if (currentState != State.Moving)
            {
                switch (currentPosition)
                {
                    case Position.Standing:
                        if(!startRunning && !running) animator.CrossFade("Rifle Walk", 0.1f);
                        else
                        {
                            AudioManager.Instance.PlaySound(SoundType.Walk);
                            animator.CrossFade("Idle To Running", 0.1f);
                            running = true;
                            startRunning = false;
                        }
                        break;
                    case Position.Kneel:
                        if(!startRunning && !running) animator.CrossFade("Crouch Walking", 0.1f);
                        else
                        {
                            animator.CrossFade("Crouch Running",0.1f);
                            running = true;
                            startRunning = false;
                        }
                        break;
                    case Position.Prone:
                        animator.CrossFade("Prone Forward", 0.1f);
                        break;
                }
                
                currentState = State.Moving;
            }
            else if (startRunning)
            {
                startRunning = false;
                running = true;
                if (currentPosition == Position.Prone) return;
                switch (currentPosition)
                {
                    case Position.Standing:
                        animator.CrossFade("Idle To Running", 0.1f);
                        break;
                    case Position.Kneel:
                        animator.CrossFade("Crouch Running",0.1f);
                        break;
                }
            }
            else if (stopRunning)
            {
                stopRunning = false;
                running = false;
                switch (currentPosition)
                {
                    case Position.Standing:
                        if(!startRunning) animator.CrossFade("Rifle Walk", 0.1f);
                        break;
                    case Position.Kneel:
                        if(!startRunning) animator.CrossFade("Crouch Walking", 0.1f);
                        break;
                    case Position.Prone:
                        animator.CrossFade("Prone Forward", 0.1f);
                        break;
                }
            }
        }
        else {
            if (currentState == State.Dying) return;
            
            if (currentState != State.Idle)
            {
                currentState = State.Idle;
                if(watchingRight) transform.eulerAngles = new Vector3(transform.eulerAngles.x,-240,transform.eulerAngles.z);
                switch (currentPosition)
                {
                    case Position.Standing:
                        animator.CrossFade("Rifle Idle", 0.1f);
                        break;
                    case Position.Kneel:
                        animator.CrossFade("Idle Crouching", 0.1f);
                        break;
                    case Position.Prone:
                        animator.CrossFade("Prone Idle", 0.1f);
                        break;
                }
                
            }
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    
    
    private enum State
    {
        Moving,
        Idle,
        Running,
        Dying,
    }

    private enum Position
    {
        Standing,
        Kneel,
        Prone,
    }

    public void DoDie()
    {
        animator.CrossFade("Dying", 0.001f);
    }
}
