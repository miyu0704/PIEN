using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetAction : MonoBehaviour
{
    void Start()
    {
        //ぴえんのみX座標にランダムに飛ばす
        if (this.gameObject.name == "FacePIEN(Clone)")
        {
            float RanPowX = Random.Range(-4.5f, 4.5f);
            float RanPowY = Random.Range(-5.0f, -2.5f);
            Vector2 AddPower = new Vector2(RanPowX, RanPowY);
            this.GetComponent<Rigidbody2D>().AddForce(AddPower, ForceMode2D.Impulse);
        }
    }
    void Update()
    {
        //常に座標取得
        Vector2 pos = this.gameObject.transform.position;
        Vector2 Player = GameObject.Find("Player").transform.position;

        //ぴえん以外はX座標方向に追尾してくる
        if(this.gameObject.name != "FacePIEN(Clone)")
        {
            Vector2 Pursue = new Vector2((Player.x - pos.x) % 2.0f, 0.0f);
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Pursue, ForceMode2D.Impulse);
        }

        //赤ぴえんに触れるとゲームオーバー
        if(this.gameObject.name == "FaceRedPIEN(Clone)" && Mathf.Abs(Player.x - pos.x) < 0.75f && Mathf.Abs(Player.y - pos.y) < 0.75f)
        {
            SEManager.SEPlay(this.gameObject.name);
            GameMaster.ScoreAdd(this.gameObject.name);
        }

        //画面外に出たとき消す
        if (pos.y < -8.0f)
            Destroy(this.gameObject);
        if (Mathf.Abs(pos.x) > 7.5f)
            Destroy(this.gameObject);

        //クリックしたら消える(Playerオブジェクト座標依存)
        if (Input.GetMouseButtonDown(0))
        {
            if (Mathf.Abs(Player.x - pos.x) < 0.75f && Mathf.Abs(Player.y - pos.y) < 0.75f)
            {
                SEManager.SEPlay(this.gameObject.name);
                GameMaster.ScoreAdd(this.gameObject.name);
                Destroy(this.gameObject);
            }
        }
    }
}
