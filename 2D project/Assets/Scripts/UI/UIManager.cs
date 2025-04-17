using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public PlayerStarBar playerStarBar;

    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    public SceneLoadEventSO unloadedSceneEvent;
    public VoidEventSO loadDataEvent;
    public VoidEventSO gameOverEvent;
    public VoidEventSO backToMenuEvent;
    public FloatEventSO syncVolumeEvent;

    [Header("广播")]
    public VoidEventSO pauseEvent;

    [Header("组件")]
    public GameObject gameOverPanel;
    public GameObject restartBtn;
    public Button SettingsBtn;
    public GameObject pausePanel;
    public Slider volumeSlider;


    private void Awake()
    {
        SettingsBtn.onClick.AddListener(TogglePausePanel);
    }
    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        unloadedSceneEvent.LoadRequestEvent +=OnUnloadedSceneEvent;
        loadDataEvent.OnEventRaised += OnloadDataEvent;
        gameOverEvent.OnEventRaised += OnGameOverEvent;
        backToMenuEvent.OnEventRaised += OnloadDataEvent;
        syncVolumeEvent.OnEventRaised+=OnSyncVolumeEvent;
    }
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        unloadedSceneEvent.LoadRequestEvent -=OnUnloadedSceneEvent;
        loadDataEvent.OnEventRaised -= OnloadDataEvent;
        gameOverEvent.OnEventRaised -= OnGameOverEvent;
        backToMenuEvent.OnEventRaised -= OnloadDataEvent;
        syncVolumeEvent.OnEventRaised-=OnSyncVolumeEvent;
    }

    private void OnSyncVolumeEvent(float amount)
    {
       volumeSlider.value=(amount+80) / 100;
    }

    private void TogglePausePanel()
    {
            if(pausePanel.activeInHierarchy)
            {
                pausePanel.SetActive(false);
                Time.timeScale=1;
            }else
            {
                pauseEvent.RaiseEvent();
                pausePanel.SetActive(true);
                Time.timeScale=0;
            }
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
