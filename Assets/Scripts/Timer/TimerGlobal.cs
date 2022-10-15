using UnityEngine;
using UnityEngine.UI;

public class TimerGlobal : MonoBehaviour
{

    #region ------------------------------------------ Fields ------------------------------------

    public static TimerGlobal SharedInstance;
    public float playTime;
    public int minutesPassed;

    [SerializeField] private Text _minutesText, _secondsText;

    #endregion --------------------------------------- Fields ------------------------------------

    #region ------------------------------------------ Mono ------------------------------------

    private void Awake()
    {
        SharedInstance = this;
        _minutesText = GameObject.FindGameObjectWithTag("MinutesText").GetComponent<Text>();
        _secondsText = GameObject.FindGameObjectWithTag("SecondsText").GetComponent<Text>();
    }

    private void Update()
    {
        playTime += Time.deltaTime;
        UpdateUI();
    }

    #endregion --------------------------------------- Mono ------------------------------------

    #region ------------------------------------------ Methods ------------------------------------

    private void UpdateUI()
    {
        _minutesText.text = "0" + (int)minutesPassed + ":";
        if (playTime < 10.0f) _secondsText.text = "0" + (int)playTime;
        else if (playTime > 10.0f && playTime < 60.0f) _secondsText.text = "" + (int)playTime;
        else if (playTime > 60.0f )
        {
            playTime -= 60;
            minutesPassed++;
        }
    }

    #endregion --------------------------------------- Methods ------------------------------------

}
