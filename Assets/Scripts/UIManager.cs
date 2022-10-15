//Script by Ionut
//Version: 0.0.1
//What does this script do?

using UnityEngine;
using UnityEngine.UI;

namespace Framework.Custom
{
    ///<summary>
    /// What does this script do?
    ///</summary>

    public class UIManager: MonoBehaviour
    {

        public  Text fatScore;
        public Text health;
        public Text missionsLeft;
        
        public void UpdateUIFatScore(float value)
        {
            fatScore.text = "Fat Percentage: "+value.ToString()+"%";
        }
        public void UpdateUIHealth(float value)
        {
            health.text = "Health: " + value.ToString();
        }
        public void UpdateUIMissions(float value)
        {
            missionsLeft.text = "Missions status:\n" + value.ToString()+"/3 collected";
        }
    }
}
