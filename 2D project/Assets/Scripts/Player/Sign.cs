using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    private PlayerInputControl playerInput;
    private Animator anim;
    public Transform playerTrans;
    public GameObject signSprite;
    private bool canPress;
    private void Awake()
    {
        
        anim=signSprite.GetComponent<Animator>();
        playerInput=new PlayerInputControl();
        playerInput.Enable();
    }

    private void OnEnable()
    {
        InputSystem.onActionChange += OnActionChange;
    }

    private void OnActionChange(object obj, InputActionChange ActionChange)
    {
        if(ActionChange==InputActionChange.ActionStarted)
        {
            //Debug.Log(((InputAction)obj).activeControl.device);
            var d = ((InputAction)obj).activeControl.device;
            switch(d.device)
            {
                case Keyboard:
                    anim.Play("Keyboard");
                    break;

            }
        }
    }

    private void Update()
    {
        signSprite.GetComponent<SpriteRenderer>().enabled= canPress;
        signSprite.transform.localScale = playerTrans.localScale;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Interactable"))
        {
            canPress = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        canPress=false;
    }
}
