using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsUI_Controller : MonoBehaviour
{

    #region ---------------------------------------- Fields ----------------------------------------

    public static ButtonsUI_Controller SharedInstance;

    #endregion ---------------------------------------- Fields ----------------------------------------

    #region ---------------------------------------- Mono ----------------------------------------

    private void Awake()
    {
        Singleton();
    }

    public void Singleton()   // Singleton class, only one instance
    {
        if (SharedInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            SharedInstance = this;
            DontDestroyOnLoad(gameObject);   // Singleton Class, preserve the game object
                                             // between scenes because there can be only one
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) LeaveGameButton_Clicked();
    }

    #endregion ---------------------------------------- Mono ----------------------------------------

    #region ---------------------------------------- Methods ----------------------------------------

    public void PlayButton_Clicked()
    {
        StartCoroutine(LoadGameScene());
    }

    private IEnumerator LoadGameScene()
    {
        Loading_Screen.SharedInstance.LoadingScreen_FadeIn();
        yield return new WaitForSecondsRealtime(1.11f);
        SceneManager.LoadScene(1);
    }

    public void LeaveGameButton_Clicked()
    {
        StartCoroutine(LoadSplashScreen());
    }

    private IEnumerator LoadSplashScreen()
    {
        Loading_Screen.SharedInstance.LoadingScreen_FadeIn();
        yield return new WaitForSecondsRealtime(1.11f);
        SceneManager.LoadScene(0);
    }

    public void ExitButton_Clicked()
    {
        StartCoroutine(QuitGame());
    }

    private IEnumerator QuitGame()
    {
        Loading_Screen.SharedInstance.LoadingScreen_FadeIn();
        yield return new WaitForSecondsRealtime(1.11f);
        SceneManager.LoadScene(1);
    }

    #endregion ---------------------------------------- Methods ----------------------------------------

}
