using UnityEngine;

public class pressStartInitial : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            GetComponent<goToScene>().GoToScene();
            enabled = false;
        }
    }
}
