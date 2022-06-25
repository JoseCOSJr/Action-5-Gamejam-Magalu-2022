using UnityEngine;

public class audioSourceMaster : MonoBehaviour
{
    public static AudioSource audioSourceEfx = null;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSourceEfx = GetComponent<AudioSource>();
    }
}
