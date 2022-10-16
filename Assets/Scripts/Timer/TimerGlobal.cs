using UnityEngine;
using UnityEngine.UI;

public class TimerGlobal : MonoBehaviour
{

    #region ------------------------------------------ Fields ------------------------------------

    public static TimerGlobal SharedInstance;
    public float playTime;
    public int minutesPassed;
    private bool firstUpdateDone = false;
    private bool secondUpdateDone = false;
    private bool thirdUpdateDone = false;
    public static int lastMaxDMG = 10;
    public static float lastMaxSpeed = 0.5f;
    [SerializeField] private Text _minutesText, _secondsText;

    #endregion --------------------------------------- Fields ------------------------------------

    #region ------------------------------------------ Mono ------------------------------------

    private void Awake()
    {
        SharedInstance = this;
        _minutesText = GameObject.FindGameObjectWithTag("MinutesText").GetComponent<Text>();
        _secondsText = GameObject.FindGameObjectWithTag("SecondsText").GetComponent<Text>();
        lastMaxDMG = 10;
        lastMaxSpeed = 0.5f;
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
        CheckIfShouldUpgradeEnemies();
    }

    #endregion --------------------------------------- Methods ------------------------------------
    private void CheckIfShouldUpgradeEnemies()
    {
        
        if (minutesPassed >= 2 && minutesPassed<5)
        {
            if (!firstUpdateDone)
            {
                UpgradEnemies(10, 0.5f);
                firstUpdateDone = true;
            }
            
        }else if (minutesPassed >= 5 && minutesPassed<8)
        {
            if (!secondUpdateDone)
            {
                UpgradEnemies(15, 1f);
                secondUpdateDone = true;
            }
                
        }
        else if(minutesPassed>=8)
        {
            if (!thirdUpdateDone)
            {
                UpgradEnemies(20, 1);
                thirdUpdateDone = true;
            }
                
        }
    }
    private void UpgradEnemies(int dmg, float speed)
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        foreach(var enemy in enemies)
        {
            enemy.damage += dmg;
            enemy._movementSpeed += speed;
            lastMaxDMG = enemy.damage;
            lastMaxSpeed = enemy._movementSpeed;
        }
    }
}
