using System.Collections;
using UnityEngine;

namespace Utility
{
    public class MobileUtilsScript : MonoBehaviour
    {
        private int framesPerSec;
        private const float Frequency = 1.0f;
        private string fps;

        private void Start()
        {
            StartCoroutine(FPS());
        }

        private IEnumerator FPS()
        {
            for (;;) {
                // Capture frame-per-second
                var lastFrameCount = Time.frameCount;
                var lastTime = Time.realtimeSinceStartup;
                yield return new WaitForSeconds(Frequency);
                var timeSpan = Time.realtimeSinceStartup - lastTime;
                var frameCount = Time.frameCount - lastFrameCount;

                // Display it

                fps = $"FPS: {Mathf.RoundToInt(frameCount / timeSpan)}";
                Debug.Log(fps);
            }
        }

        private void OnGUI()
        {
            GUIStyle headStyle = new GUIStyle();
            headStyle.fontSize = 60;
            GUI.Label(new Rect(100, 100, 150, 200), fps, headStyle);
        }
    }
}