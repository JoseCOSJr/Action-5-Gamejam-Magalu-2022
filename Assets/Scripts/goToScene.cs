using UnityEngine;
using UnityEngine.SceneManagement;

public class goToScene : MonoBehaviour
{
    public string idScene = "SampleScene";
    public AudioClip clipSwicthScene;
    private bool triggerGoToScene = false;
    public bool anyInputGoToScene = false;

    public void GoToScene()
    {
        if (SceneManager.sceneCount == 1 && !fadeObj.fadeObjInScene.gameObject.activeInHierarchy)
        {
            triggerGoToScene = true;
            fadeObj.fadeObjInScene.FadeGo(false, 1f);
            audioSourceMaster.audioSourceEfx.PlayOneShot(clipSwicthScene);
        }
    }

    private void Update()
    {
        if (triggerGoToScene)
        {
            if (fadeObj.fadeObjInScene.FadeEventIsFinish())
            {
                SceneManager.LoadSceneAsync(idScene);
                enabled = false;
            }
        }
        else if (anyInputGoToScene)
        {
            if (Input.anyKeyDown)
            {
                GoToScene();
            }
        }
    }
}
