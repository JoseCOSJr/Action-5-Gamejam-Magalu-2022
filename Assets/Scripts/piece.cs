using UnityEngine;

public class piece : MonoBehaviour
{
    public Sprite spriteHere;
    private bool canMove = true, viewOneTime = false;
    public bool roofPiece = false;

    private void OnEnable()
    {
        canMove = true;
        viewOneTime = false;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 move = Vector3.left * 4f * Time.fixedDeltaTime;
            transform.Translate(move);
        }
    }

    public Sprite GetSprite()
    {
        return spriteHere;
    }

    public void SetCanMove(bool can)
    {
        canMove = can;
    }

    private void OnBecameVisible()
    {
        viewOneTime = true;
    }

    private void OnBecameInvisible()
    {
        if (viewOneTime)
        {
            gameObject.SetActive(false);
        }
    }
}
