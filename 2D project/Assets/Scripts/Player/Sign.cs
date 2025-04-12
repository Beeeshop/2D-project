using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    private Animator anim;
    public GameObject signSprite;
    private bool canPress;
    private void Awake()
    {
        
        anim=signSprite.GetComponent<Animator>();
    }
    private void Update()
    {
        signSprite.SetActive(canPress);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Interactable"))
        {
            canPress = true;
        }
    }
}
