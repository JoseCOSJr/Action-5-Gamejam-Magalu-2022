using UnityEngine;

public class buildPart : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject objMark = null;
    private piece pieceHere = null;
    private Collider2D colliderHere;
    public buildPart buildPartDown;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        colliderHere = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if((!buildPartDown || (buildPartDown.HavePiece() && !buildPartDown.pieceHere.roofPiece)))
        {
            colliderHere.enabled = true;
        }
        else
        {
            colliderHere.enabled = false;
            if (pieceHere)
            {
                SetPiece(null);
            }
        }
    }

    public void Mark(bool isMark)
    {
        objMark.SetActive(isMark);
    }

    public void SetPiece(piece pieceX)
    {
        pieceHere = pieceX;
        if (pieceX)
        {
            spriteRenderer.sprite = pieceX.GetSprite();
        }
        else
        {
            spriteRenderer.sprite = null;
        }
        Mark(false);
        if (pieceHere)
        {
            pieceHere.gameObject.SetActive(false);
        }
    }

    public bool HavePiece()
    {
        return pieceHere;
    }

    private void OnMouseDown()
    {
        if (HavePiece())
        {
            SetPiece(null);
        }
    }

    public Sprite GetSprite()
    {
        return spriteRenderer.sprite;
    }
}
