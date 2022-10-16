using System.Collections;
using UnityEngine;

public class Loading_Screen : MonoBehaviour
{

    #region ---------------------------------------- Fields ----------------------------------------

    public static Loading_Screen SharedInstance;

    private Animator _loadingScreenAnimator;

    #endregion ---------------------------------------- Fields ----------------------------------------

    #region ---------------------------------------- Mono ----------------------------------------

    private void Awake()
    {
        Singleton();
        _loadingScreenAnimator = this.gameObject.GetComponent<Animator>();
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

    #endregion ---------------------------------------- Mono ----------------------------------------

    #region ---------------------------------------- Methods ----------------------------------------

    public void LoadingScreen_FadeIn()
    {
        _loadingScreenAnimator.SetTrigger("fadein");
        _loadingScreenAnimator.ResetTrigger("fadeout");
        StartCoroutine(Wait());
    }

    public void LoadingScreen_FadeOut()
    {
        _loadingScreenAnimator.SetTrigger("fadeout");
        _loadingScreenAnimator.ResetTrigger("fadein");
        
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1.1f);
        LoadingScreen_FadeOut();
    }

    #endregion ---------------------------------------- Methods ----------------------------------------

}
