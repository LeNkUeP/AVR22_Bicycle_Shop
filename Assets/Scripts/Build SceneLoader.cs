using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildSceneLoader : MonoBehaviour
{
    void Start()
    {
        if (!Application.isEditor)
        {
            // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
            //SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
            SceneManager.LoadScene("Workshop Area", LoadSceneMode.Additive);
            SceneManager.LoadScene("Storage Area", LoadSceneMode.Additive);
            SceneManager.LoadScene("Outside Area", LoadSceneMode.Additive);
            SceneManager.LoadScene("Customer Area", LoadSceneMode.Additive);
        }
    }
}