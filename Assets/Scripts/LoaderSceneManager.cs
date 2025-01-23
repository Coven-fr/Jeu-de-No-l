using UnityEngine.SceneManagement;

public class LoaderSceneManager : Singleton<LoaderSceneManager>
{    
    public void LoadScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
