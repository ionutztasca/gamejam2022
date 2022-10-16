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

        public Text fatScore;
        public RectTransform fatScale;
        public Text health;
        public RectTransform healthScale;
        public Text missionsLeft;
        
        public void UpdateUIFatScore(float value)
        {
            fatScore.text = "Fat: "+value.ToString()+"%";
            fatScale.localScale = new Vector2(fatScale.localScale.x, Mathf.Clamp(value / 100f, 0, 1));
        }
        public void UpdateUIHealth(float value)
        {
            health.text = "HP: " + value.ToString();
            healthScale.localScale = new Vector2(Mathf.Clamp(value / 100f, 0, 1), healthScale.localScale.y);
        }
        public void UpdateUIMissions(float value)
        {
            missionsLeft.text = "Missions status:\n" + value.ToString()+"/3 collected";
        }
    }
}
