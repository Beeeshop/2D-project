using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    public Sprite closeSprite;
    public bool isDone;


    private void Awake()
    {
     spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = isDone ? openSprite : closeSprite;        
    }
    public void TriggerAction(Character character)
    {
        Debug.Log("Open Chest!");
        if(!isDone)
        {
            OpenChest(character);
        }
    }

 
    private void OpenChest(Character character)
    {
        spriteRenderer.sprite = openSprite;
        isDone = true;
        this.gameObject.tag = "Untagged";
        if (character != null)
        {
            character.Heal(50f); // 回复20点生命
            Debug.Log("Player healed by chest!");
        }
    }
    
 
}
