using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class stage : MonoBehaviour
{
    public List<pedidoObjt> pedidoObjtList;
    public List<buildPart> buildPartList;
    private List<pedidoObjt> pedidoObjtsPassed = new List<pedidoObjt>();
    private int idLastRequest = 0;
    private float countTimer = 30f;
    private float timeDelivey = 0f;
    private float addTimer = 0f;
    public Text textTimer, textScores;
    private int scores = 0;
    public GameObject gameOverGoObjt, gameObjectWinGoScene;
    public respawPieces respawPieces;
    public AudioClip clipTimeRecovery, clipTimeIsOver, clipRequestIsComplete;
    private AudioSource audioSource;
    private bool triggerTimeWait=false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pedidoObjtsPassed.Count == pedidoObjtList.Count)
        {
            //Venceu
            gameObjectWinGoScene.SetActive(true);
            enabled = false;
        }
        else 
        {
            List<pedidoObjt> pedidoObjtsAtv = pedidoObjtList.FindAll(x => x.gameObject.activeInHierarchy);
            pedidoObjt pedidoComplet = pedidoObjtsAtv.Find(x => x.IsMake(buildPartList.ToArray()));

            if (pedidoComplet)
            {
                pedidoComplet.gameObject.SetActive(false);
                pedidoObjtsPassed.Add(pedidoComplet);
                int addScore = 100 + 900 * 30 / ((int)timeDelivey + 30);
                AddScores(addScore);
                for (int i = 0; i < buildPartList.Count; i++)
                {
                    buildPartList[i].SetPiece(null);
                }

                timeDelivey = 0f;

                if (!pedidoObjtList.Exists(x => x.gameObject.activeInHierarchy))
                {
                    audioSource.PlayOneShot(clipTimeRecovery);
                    audioSource.PlayOneShot(clipRequestIsComplete);
                    addTimer += 30f;
                    TakeOneRequest();

                    Sprite[] sprites = pedidoComplet.GetSpritesArray();
                    for (int i = 0; i < sprites.Length; i++)
                    {
                        if (sprites[i])
                        {
                            respawPieces.RemovePieces(sprites[i]);
                        }
                    }
                }
            }
            else
            {
                timeDelivey += Time.deltaTime;

                if (!pedidoObjtList.Exists(x => x.gameObject.activeInHierarchy))
                {
                    TakeOneRequest();
                    countTimer += 30f;
                    triggerTimeWait = false;
                }

                if (countTimer > 0f)
                {
                    if (addTimer > 0f)
                    {
                        float add = 40f * Time.deltaTime;
                        if (add > addTimer)
                        {
                            add = addTimer;
                        }
                        addTimer -= add;
                        countTimer += add;
                        triggerTimeWait = false;
                    }
                    else
                    {
                        countTimer -= Time.deltaTime;
                        if (countTimer < 0)
                        {
                            countTimer = 0f;
                        }

                        if (!triggerTimeWait && countTimer <= clipTimeIsOver.length)
                        {
                            triggerTimeWait = true;
                            audioSource.PlayOneShot(clipTimeIsOver);
                        }
                    }

                    textTimer.text = "Timer" + "\n" + Mathf.FloorToInt(countTimer);
                }
                else
                {
                    if (pedidoObjtList.FindAll(x => x.gameObject.activeInHierarchy).Count >= 3 || idLastRequest >= pedidoObjtList.Count)
                    {
                        //Game Over
                        //Debug.Log("Game Over");
                        gameOverGoObjt.SetActive(true);
                        enabled = false;
                    }
                    else
                    {
                        TakeOneRequest();
                        triggerTimeWait = false;
                        countTimer += 30f;
                    }
                }
            }
        } 
    }

    private void TakeOneRequest()
    {
        if (idLastRequest < pedidoObjtList.Count)
        {
            pedidoObjtList[idLastRequest].gameObject.SetActive(true);
            Sprite[] sprites = pedidoObjtList[idLastRequest].GetSpritesArray();

            //respawPieces.RestartPieces();
            for (int i = 0; i < sprites.Length; i++)
            {
                respawPieces.AddPieceChoice(sprites[i]);
            }

            idLastRequest += 1;
        }
    }

    private void AddScores(int add)
    {
        scores += add;
        textScores.text = "Scores" + "\n" + scores;
    }
}
