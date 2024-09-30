using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class PlayControllerUI : MonoBehaviour
    {
        [SerializeField]
        private Toggle playToggle;

        public void OnPlayTogglePressed()
        {
            playToggle.targetGraphic.enabled = !playToggle.isOn;
        }
    }
}