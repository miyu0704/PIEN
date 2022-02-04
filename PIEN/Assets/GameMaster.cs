using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class GameMaster : MonoBehaviour
{
    public enum Scene
    {
        TITLE,
        TUTORIAL,
        TUTORIAL2,
        PLAY,
        CLEAR,
        GAMEOVER
    }

    static public Scene GameScenes;
    GameObject Player;

    static int myScore;    //スコア
    float Elapsed;  //経過時間
    public float LimitTime = 45.0f;

    //音関係
    AudioSource myAudio;
    public AudioClip TitleBGM;   //ぴえんの歌
    public AudioClip PLAYBGM;//ゲーム中のBGM

    //画像関係
    public Image imgTitle;
    public Image imgTutorial;
    public Image imgTutorial2;
    public Image imgGameover;
    public Image imgClear;

    //テキスト関係
    public Text TitleNavtxt;
    public Text TutoRial1Navtxt;
    public Text TutoRial2Navtxt;
    public Text Timetxt;
    public Text scoretxt;
    public Text Cscoretxt;
    public Text ClearNavtxt;
    public Text GameoverNavtxt;

    //プレイ用オブジェクト
    public GameObject Face1;
    public GameObject Face2;
    public GameObject Face3;
    public GameObject Face4;
    public GameObject Face5;
    public GameObject FacePIEN;
    public GameObject FaceRedPIEN;

    int FaceNumber;

    // Start is called before the first frame update
    void Start()
    {
        //ウィンドウサイズ定義
        Screen.SetResolution(1024, 768, false, 60);
        
        //音源の確保
        myAudio = GetComponent<AudioSource>();
        myAudio.clip = TitleBGM;
        myAudio.Play();


        Player = GameObject.FindGameObjectWithTag("Player");
        Ready();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameScenes)
        {
            case Scene.TITLE:
                
                imgTitle.gameObject.SetActive(true);
                TxtBring();
                TitleNavtxt.gameObject.SetActive(Elapsed < 0.8f);

                if(Input.GetMouseButtonDown(0))
                {
                    GameScenes = Scene.TUTORIAL;
                }
                break;

            case Scene.TUTORIAL:

                imgTitle.gameObject.SetActive(false);

                imgTutorial.gameObject.SetActive(true);
                TutoRial1Navtxt.text = "CLICK to Next Page";
                TxtBring();
                TutoRial1Navtxt.gameObject.SetActive(Elapsed < 0.8f);

                if (Input.GetMouseButtonDown(0))
                {
                    GameScenes = Scene.TUTORIAL2;
                }

                break;

            case Scene.TUTORIAL2:

                
                imgTutorial.gameObject.SetActive(false);
                imgTutorial2.gameObject.SetActive(true);

                TutoRial2Navtxt.text = "CLICK to Play Start!";
                TxtBring();
                TutoRial2Navtxt.gameObject.SetActive(Elapsed < 0.8f);

                if (Input.GetMouseButtonDown(0))
                {
                    PlayStart();
                }
                break;

            case Scene.PLAY:
                Elapsed += Time.deltaTime;

                scoretxt.text = "SCORE : " + myScore.ToString();
                if (Elapsed>= LimitTime)
                {
                    GameScenes = Scene.CLEAR;
                }
                else
                {
                    Timetxt.text = (LimitTime - Elapsed).ToString("f2") + "s";
                }
                break;

            case Scene.CLEAR:
                if (!imgClear.gameObject.activeSelf)
                {
                    Stop();
                    myAudio.clip = TitleBGM;
                    myAudio.Play();
                    imgClear.gameObject.SetActive(true);
                    Cscoretxt.text = "SCORE : " + myScore.ToString();
                    ClearNavtxt.text = "CLICK  to  Title";
                }
                TxtBring();
                ClearNavtxt.gameObject.SetActive(Elapsed < 0.8f);

                if (Input.GetMouseButtonDown(0))
                {
                    myAudio.clip = TitleBGM;
                    myAudio.Play();
                    imgClear.gameObject.SetActive(false);
                    Ready();
                }

                break;

            case Scene.GAMEOVER:
                if (!imgGameover.gameObject.activeSelf)
                {
                    Stop();
                    imgGameover.gameObject.SetActive(true);
                    GameoverNavtxt.text = "CLICK  to  Title";
                }
                TxtBring();
                GameoverNavtxt.gameObject.SetActive(Elapsed < 0.8f);

                if (Input.GetMouseButtonDown(0))
                {
                    myAudio.clip = TitleBGM;
                    myAudio.Play();
                    imgGameover.gameObject.SetActive(false);
                    Ready();
                }
                break;

        }
    }

    void Ready() //全体の初期化処理
    {
        GameScenes = Scene.TITLE;
        //myAudio.PlayOneShot(BGM1);
        imgTitle.gameObject.SetActive(true);
        TitleNavtxt.text= "CLICK  to  Start";
        TutoRial1Navtxt.text="";
        TutoRial2Navtxt.text= "";
        Timetxt.text="";
        scoretxt.text="";
        Cscoretxt.text = "";
        ClearNavtxt.text = "";
        GameoverNavtxt.text="";
        Elapsed = 0.0f;

    }

    void TxtBring()
    {
        Elapsed += Time.deltaTime;
        Elapsed %= 1.0f;
    }

    void PlayStart()
    {
        myAudio.Stop();
        myAudio.clip = PLAYBGM;
        myScore = 0;
        GameScenes = Scene.PLAY;
        scoretxt.text = "SCORE : 0";
        imgTutorial2.gameObject.SetActive(false);
        Elapsed = 0.0f;
        StartCoroutine("FaceSpawner");
        myAudio.Play();
    }

    void ChangeScore(int Point)
    {
        if (Point > 0)
        {
         
        }
        myScore += Point;
        scoretxt.text = "SCORE : " + myScore.ToString().PadLeft(6, '0');
    }

    void Stop()
    {
        myAudio.Stop();
        Timetxt.text = "0.00s";
        Elapsed = 0.0f;
        StopCoroutine("FaceSpawner");
    }

    

    IEnumerator FaceSpawner() //的生成　レベルによって変えたいなぁぁぁぁ
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

            float RanX = Random.Range(-6.5f, 6.5f);
            Vector2 pos = new Vector2(RanX, 5.0f);
            Vector2 AddPower = new Vector2(RanX, 0.0f);

            if (Random.value < 0.25f)
            {
                FaceNumber = Random.Range(1, 5);

                switch (FaceNumber)
                {
                    case 1:
                        Instantiate(Face1, pos, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(Face2, pos, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(Face3, pos, Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(Face4, pos, Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(Face5, pos, Quaternion.identity);
                        break;
                }
            }
            else if (Random.value < 0.5f)
            {
                Instantiate(FacePIEN, pos, Quaternion.identity);
            }
            else if (Random.value < 0.25f)
            {
                Instantiate(FaceRedPIEN, pos, Quaternion.identity);
            }

        }
    }
    static public void ScoreAdd(string objectname)
    {
        switch (objectname)
        {
            case "FacePIEN(Clone)":
                myScore += 100;
                break;
            case "FaceRedPIEN(Clone)":
                GameScenes = Scene.GAMEOVER;
                Debug.Log("OK");
                break;
            default:
                if (myScore - 50 >= 0)
                    myScore -= 50;
                break;
        }
    }
}