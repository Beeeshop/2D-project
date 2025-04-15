using UnityEngine;
using UnityEngine.AddressableAssets;
[CreateAssetMenu(menuName = "Game Scene/GameSceneSo")]
public class GameSceneSO : ScriptableObject
{
    public SceneType sceneType;
    public AssetReference sceneReference;
}
