using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public Transform playerTrans;
    public Vector3 firstPosition;
    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO; 
   public GameSceneSO firstLoadScene;

   [Header("广播")]
   public VoidEventSO afterSceneLoadedEvent;
    public FadeEventSO fadeEvent;

   [SerializeField] private GameSceneSO currentLoadedScene;
   private GameSceneSO sceneToLoad;
   private Vector3 positionToGo;
   private bool fadeScreen;
   private bool isLoading;
   public float fadeDuration;

    private void Awake()
    {
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference,LoadSceneMode.Additive);
        // currentLoadedScene = firstLoadScene;
        // currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }
    private void Start()
    {
        NewGame();
    }

    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
    }
    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
    }

private void NewGame()
{
    sceneToLoad=firstLoadScene;
    // OnLoadRequestEvent(sceneToLoad,firstPosition,true);
    loadEventSO.RaiseLoadRequestEvennt(sceneToLoad,firstPosition,true);
}
/// <summary>
/// 场景事件加载请求
/// </summary>
/// <param name="locationToLoad"></param>
/// <param name="posToGo"></param>
/// <param name="fadeScreen"></param>

    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        if(isLoading)
        {
            return;
        }
        isLoading=true;
        sceneToLoad = locationToLoad;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;
        if(currentLoadedScene != null)
        {
        StartCoroutine(UnLoadPreviousScene());
        }else{
            LoadNewScene();
        }
    }
    private IEnumerator UnLoadPreviousScene()
    {
        if(fadeScreen)
        {
            //TODO:变黑
            fadeEvent.FadeIn(fadeDuration);
        }
        yield return new WaitForSeconds(fadeDuration);
        yield return currentLoadedScene.sceneReference.UnLoadScene();
        //关闭人物
        playerTrans.gameObject.SetActive(false);
        //加载新场景
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive,true);
        loadingOption.Completed+= OnLoadCompleted;

    }
/// <summary>
/// 场景加载结束后
/// </summary>
/// <param name="handle"></param>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        currentLoadedScene = sceneToLoad;
        playerTrans.position=positionToGo;
        playerTrans.gameObject.SetActive(true);
        if(fadeScreen)
        {
            ///TODO；
            fadeEvent.FadeOut(fadeDuration);
        }
        isLoading=false;
        if(currentLoadedScene.sceneType == SceneType.Location)
             //场景加载完成后事件
        afterSceneLoadedEvent.RaiseEvent();

    }
}
