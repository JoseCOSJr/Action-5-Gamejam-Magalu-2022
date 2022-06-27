using UnityEngine;
using UnityEngine.UI;

public class pedidoObjt : MonoBehaviour
{
    [SerializeField]
    private Image[] imageSpritesArray = new Image[12];

    private void OnEnable()
    {
        for (int i = 0; i < imageSpritesArray.Length; i++)
        {
            if (imageSpritesArray[i].sprite == null)
            {
                imageSpritesArray[i].color = Color.clear;
            }
        }
    }

    public bool IsMake(buildPart[] buildParts)
    {
        int correctPartsCount = 0;

        for(int i=0; i<imageSpritesArray.Length; i++)
        {
            if(imageSpritesArray[i].sprite == buildParts[i].GetSprite())
            {
                correctPartsCount += 1;
            }
        }

        return correctPartsCount == imageSpritesArray.Length;
    }

    public Sprite[] GetSpritesArray()
    {
        Sprite[] sprites = new Sprite[imageSpritesArray.Length];

        for(int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = imageSpritesArray[i].sprite;
        }

        return sprites;
    }
}
