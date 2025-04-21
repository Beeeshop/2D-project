using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavePoint : MonoBehaviour,IInteractable
{
    [Header("�㲥")]
    public VoidEventSO saveDataEvent;

    [Header("��������")]
    public SpriteRenderer spriteRenderer;
    public GameObject lightObj;
    public Sprite darkSprite;
    public Sprite lightSprite;

    public UnityEvent OnSave;

    public bool isDone;


    private void OnEnable()
    {
        spriteRenderer.sprite = isDone ? lightSprite : darkSprite;
        lightObj.SetActive(isDone);
        
    }

    public void TriggerAction(Character character)
    {
        if(!isDone) 
       {
            isDone = true;
            spriteRenderer.sprite = lightSprite;
            lightObj.SetActive(true);
            OnSave?.Invoke();
            //TODO:��������
            saveDataEvent.RaiseEvent();

            this.gameObject.tag = "Untagged";

        }
    }
}
