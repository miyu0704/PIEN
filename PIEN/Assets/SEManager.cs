using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    static int SEFlagCount = -1;
    AudioSource SE;
    public AudioClip SE_OK;     //加点
    public AudioClip SE_NG;     //減点
    public AudioClip SE_OVER;   //GAMEOVER時
    // Start is called before the first frame update
    void Start()
    {
        SE = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SEFlagCount != -1)
        {
            switch (SEFlagCount)
            {
                case 0:
                    SE.clip = SE_OK;
                    SE.Play();
                    break;
                case 1:
                    SE.clip = SE_OVER;
                    SE.Play();
                    break;
                default:
                    SE.clip = SE_NG;
                    SE.Play();
                    break;
            }
            SEFlagCount = -1;
        }
    }

    static public void SEPlay(string objectname)
    {
        switch (objectname)
        {
            case "FacePIEN(Clone)":
                SEFlagCount = 0;
                break;
            case "FaceRedPIEN(Clone)":
                
                break;
            default:
                
                break;
        }
    }
}
