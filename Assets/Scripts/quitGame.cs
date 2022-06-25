using UnityEngine;

public class quitGame : MonoBehaviour
{
    public AudioClip audioClipQuit;
    private bool triggerQuit = false;

   public void QuitGame()
    {
        if (!triggerQuit)
        {
            triggerQuit = true;
            audioSourceMaster.audioSourceEfx.PlayOneShot(audioClipQuit);
            fadeObj.fadeObjInScene.FadeGo(false, 3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerQuit)
        {
            if (fadeObj.fadeObjInScene.FadeEventIsFinish())
            {
                Application.Quit();
                enabled = false;
            }
        }
    }
}
