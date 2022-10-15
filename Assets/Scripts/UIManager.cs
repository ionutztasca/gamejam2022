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

        
        public void UpdateUIFatScore(float value)
        {
            fatScore.text = "Fat Percentage: "+value.ToString()+"%";
        }
    }
}
