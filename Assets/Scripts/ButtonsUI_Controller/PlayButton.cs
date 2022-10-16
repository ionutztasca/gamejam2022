using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{

    #region ---------------------------------------- MONO ----------------------------------------

    private void Awake()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ButtonsUI_Controller.SharedInstance.PlayButton_Clicked(); });
    }

    #endregion ------------------------------------- Mono ----------------------------------------
    
}
