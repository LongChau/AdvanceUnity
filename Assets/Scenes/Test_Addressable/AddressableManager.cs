using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : MonoBehaviour
{
    public string assetAddress = "AssetAddress";
    public GameObject myGameObject;
    public AssetReference assetRef;

    // Start is called before the first frame update
    void Start()
    {
        var newAsset = assetRef.InstantiateAsync(Vector3.zero, Quaternion.identity);
    }

    [ContextMenu("LoadAssetAsync")]
    public void LoadAssetAsync()
    {
        print("AddressableManager.LoadAssetAsync()");
        Addressables.LoadAssetAsync<GameObject>(assetAddress).Completed += OnLoadDone;
    }

    private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        try
        {
            // In a production environment, you should add exception handling to catch scenarios such as a null result.
            if (obj.Result == null)
            {
                print($"Null result {obj.DebugName}");
                return;
            }

            print("AddressableManager.OnLoadDone()");
            myGameObject = obj.Result;
            Addressables.LoadAssetAsync<GameObject>(assetAddress).Completed -= OnLoadDone;
        }
        catch (InvalidKeyException ex)    
        {
            print(ex);
            throw;
        }
    }
}
