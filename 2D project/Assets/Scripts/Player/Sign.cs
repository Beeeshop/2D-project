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

    private Character playerCharacter;
    public GameObject signSprite;
    private IInteractable targetItem;
    private bool canPress;
    private void Awake()
    {
        playerCharacter = playerTrans.GetComponent<Character>();
        anim=signSprite.GetComponent<Animator>();
        playerInput=new PlayerInputControl();
        playerInput.Enable();
    }
    private void OnDisable()
    {
        canPress=false;
    }
    private void OnEnable()
    {
        InputSystem.onActionChange += OnActionChange;
        playerInput.Gameplay.Confirm.started += OnConfirm;
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

    private void OnConfirm(InputAction.CallbackContext obj)
    {
        if(canPress)
        {
            targetItem.TriggerAction(playerCharacter);
            GetComponent<AudioDefination>()?.PlayAudioClip();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = other.GetComponent<IInteractable>();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        canPress=false;
    }
}
