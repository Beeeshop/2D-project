using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIManager : MonoBehaviour
{
    public PlayerStarBar playerStarBar;

    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    public SceneLoadEventSO unloadedSceneEvent;
    public VoidEventSO loadDataEvent;
    public VoidEventSO gameOverEvent;
    public VoidEventSO backToMenuEvent;

    [Header("组件")]
    public GameObject gameOverPanel;
    public GameObject restartBtn;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        unloadedSceneEvent.LoadRequestEvent +=OnUnloadedSceneEvent;
        loadDataEvent.OnEventRaised += OnloadDataEvent;
        gameOverEvent.OnEventRaised += OnGameOverEvent;
        backToMenuEvent.OnEventRaised += OnloadDataEvent;
    }
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        unloadedSceneEvent.LoadRequestEvent -=OnUnloadedSceneEvent;
        loadDataEvent.OnEventRaised -= OnloadDataEvent;
        gameOverEvent.OnEventRaised -= OnGameOverEvent;
        backToMenuEvent.OnEventRaised -= OnloadDataEvent;
    }

    private void OnGameOverEvent()
    {
        //Debug.Log("DEAD");
        gameOverPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(restartBtn);
    }

    private void OnloadDataEvent()
    {
        //Debug.Log("Close");
        gameOverPanel.SetActive(false);
    }

    private void OnUnloadedSceneEvent(GameSceneSO sceneToLoad, Vector3 arg1, bool arg2)
    {
        var isMenu = sceneToLoad.sceneType == SceneType.Menu;
        playerStarBar.gameObject.SetActive(!isMenu);
        
    }

    private void OnHealthEvent(Character character)
    {
       var persentage = character.currentHealth / character.maxHealth;
       playerStarBar.OnHealthChange(persentage);
    }
}
