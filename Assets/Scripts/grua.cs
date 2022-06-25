using UnityEngine;

public class grua : MonoBehaviour
{
    public Transform transformPosGet;
    private Vector3 posPiece;
    private piece pieceHere = null;
    private buildPart buildPartX = null;
    private bool isDrag = false;
    private Animator animator;
    public LayerMask layerMask;
    private AudioSource audioSourceEfx;
    public AudioClip audioClipGo, audioClipGoBack, audioClipGetObj, audioClipDrag, audioClipCancelDrag, audioClipPutPiece;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSourceEfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pieceHere)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            if (Input.GetButton("Fire1"))
            {
                if (isDrag)
                {
                    pieceHere.transform.position = mousePosition;
                    Collider2D targetObject = Physics2D.OverlapPoint(mousePosition, layerMask);
                    if (targetObject)
                    {
                        if (buildPartX && targetObject != buildPartX.gameObject)
                        {
                            buildPartX.Mark(false);
                        }
                        buildPartX = targetObject.GetComponent<buildPart>();
                        if (buildPartX.HavePiece())
                        {
                            buildPartX = null;
                        }
                        else
                        {
                            buildPartX.Mark(true);
                        }
                    }
                    else
                    {
                        if (buildPartX)
                        {
                            buildPartX.Mark(false);
                        }
                    }
                }
                else
                {
                    Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
                    if (targetObject && targetObject.gameObject == pieceHere.gameObject)
                    {
                        isDrag = true;
                        audioSourceEfx.PlayOneShot(audioClipDrag);
                    }
                }
            }
            else
            {
                if (isDrag)
                {
                    isDrag = false;
                    if (buildPartX)
                    {
                        buildPartX.SetPiece(pieceHere);
                        pieceHere = null;
                        buildPartX = null;
                        audioSourceEfx.PlayOneShot(audioClipPutPiece);
                    }
                    else
                    {
                        pieceHere.transform.localPosition = posPiece;
                        audioSourceEfx.PlayOneShot(audioClipCancelDrag);
                    }
                }
            }
        }
        else if (Input.GetButtonDown("Jump") && !animator.GetBool("Go") && !animator.GetBool("Back"))
        {
            animator.SetBool("Go", true);
            audioSourceEfx.PlayOneShot(audioClipGo);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pieceHere)
        {
            pieceHere = collision.GetComponent<piece>();
            if (pieceHere)
            {
                pieceHere.SetCanMove(false);
                pieceHere.transform.SetParent(transformPosGet);
                posPiece = pieceHere.transform.localPosition;
                audioSourceEfx.PlayOneShot(audioClipGetObj);
            }
        }
    }

    public void EndAnima()
    {
        if (animator.GetBool("Go"))
        {
            animator.SetBool("Go", false);
            animator.SetBool("Back", true);
            audioSourceEfx.PlayOneShot(audioClipGoBack);
        }
        else if (animator.GetBool("Back"))
        {
            animator.SetBool("Back", false);
        }
    }
}
