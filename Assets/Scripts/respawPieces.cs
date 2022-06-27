using System.Collections.Generic;
using UnityEngine;

public class respawPieces : MonoBehaviour
{
    private List<piece> piecesCanChoice = new List<piece>();
    private List<piece> piecesList = new List<piece>();
    private List<piece> piecesAtived=new List<piece>();
    public List<piece> piecesParts;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < piecesParts.Count; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                piece pieceX = Instantiate(piecesParts[i]);
                pieceX.name = piecesParts[i].name;
                piecesList.Add(pieceX);
                pieceX.transform.SetParent(transform);
                pieceX.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (piecesCanChoice.Count > 0)
        {
            if (piecesAtived.Exists(x => !x.gameObject.activeInHierarchy) || piecesAtived.Count == 0)
            {
                piecesAtived.Clear();
                for (int i = 0; i < 3; i++)
                {
                    Vector3 posNew = Vector3.zero;
                    posNew.y = -4f;
                    posNew.x = 12 + 8f * i;

                    int idPart = Random.Range(0, piecesCanChoice.Count);
                    piece pieceX = piecesList.Find(x => !x.gameObject.activeInHierarchy && x.name == piecesCanChoice[idPart].name);
                    pieceX.transform.SetParent(transform);
                    pieceX.transform.position = posNew;
                    pieceX.gameObject.SetActive(true);
                    piecesAtived.Add(pieceX);
                }
            }
        }
    }

    public void AddPieceChoice(Sprite sprite)
    {
        //Debug.Log(sprite.name);
        piece pieceX = piecesParts.Find(x => x.GetSprite() == sprite);

        if(pieceX && !piecesCanChoice.Exists(x=>x.name == pieceX.name))
        {
            piecesCanChoice.Add(pieceX);
        }
    }

    public void RemovePieces(Sprite sprite)
    {
         piece p = piecesCanChoice.Find(x => x.spriteHere == sprite);
        if (p)
        {
            piecesCanChoice.Remove(p);
        }
    }
}
