using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("监听事件")]
    public SceneLoadEventSO sceneLoadEvent;
    public VoidEventSO afterSceneLoadedEvent;
    public VoidEventSO loadDataEvent;
    public VoidEventSO backToMenuEvent;

    public PlayerInputControl InputControl;
    public Vector2 inputDirection;

    [Header("基本参数")]
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;

    public float speed;
    public float jumpForse;

    public float hurtForce;
    public bool isHurt;

    public bool isDead;

    public bool isAttack;

    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;
    public Collider2D coll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();
        InputControl = new PlayerInputControl();
        InputControl.Gameplay.Jump.started += Jump;
        //攻击
        InputControl.Gameplay.Attack.started += PlayerAttack;

        coll = GetComponent<Collider2D>();
        InputControl.Enable();
    }

    private void OnEnable()
    {
        
        sceneLoadEvent.LoadRequestEvent +=OnLoadEvent;
        afterSceneLoadedEvent.OnEventRaised +=OnAfterSceneLoadedEvent;
        loadDataEvent.OnEventRaised += OnLoadDataEvent;
        backToMenuEvent.OnEventRaised += OnLoadDataEvent;
    }

    private void OnDisable()
    {
        InputControl.Disable();
        sceneLoadEvent.LoadRequestEvent -=OnLoadEvent;
        afterSceneLoadedEvent.OnEventRaised -=OnAfterSceneLoadedEvent;
        loadDataEvent.OnEventRaised -= OnLoadDataEvent;
        backToMenuEvent.OnEventRaised -= OnLoadDataEvent;
    }

   

    private void OnAfterSceneLoadedEvent()
    {
        InputControl.Gameplay.Enable();
    }

    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        InputControl.Gameplay.Disable();
    }
    //读取游戏进度
    private void OnLoadDataEvent()
    {
        isDead = false;
    }
    private void Update()
    {
        inputDirection = InputControl.Gameplay.Move.ReadValue<Vector2>();

        CheckState();
    }

    private void FixedUpdate()
    {
        if(!isHurt && !isAttack)
            Move();

    }

    private void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if(inputDirection.x < 0)
            faceDir = -1;

        transform.localScale =new Vector3(faceDir, 1, 1);
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        // Debug.Log("JUMP");
        if(physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForse, ForceMode2D.Impulse);
    }

    private void PlayerAttack(InputAction.CallbackContext obj)
    {
     playerAnimation.PlayAttack();
     isAttack=true;
    }
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x),0).normalized;

        rb.AddForce(dir*hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        InputControl.Gameplay.Disable();
    }
    private void CheckState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }
}
