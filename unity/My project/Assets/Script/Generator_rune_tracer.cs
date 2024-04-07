using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator_rune_tracer : MonoBehaviour
{
    //発生させるルーンの情報を格納する変数
    public GameObject rune;
    //時間を保存しておくための変数
    float pretime;
    //ルーンプレハブを発生させる間隔
    public float interval = 1.0f;

    private GameObject weapon_manager;
    All_weapon_manager weapon_script;


    public int Lv = 0;
    int speed = 20;

    //playerに関する情報を保存する変数たち
    GameObject player;
    PlayerScript player_script;
    Vector3 player_pos;

    //rune_tracerを発射する方向を決めるランダムなxとyを保存する変数
    float rnd_x;
    float rnd_y;
    //rune_tracerを発射する方向を保存
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        weapon_manager = GameObject.Find("player/All_weapon_manager");
        weapon_script = weapon_manager.GetComponent<All_weapon_manager>();
        Lv = weapon_script.Get_Weapon_Lv("rune");   
        player = GameObject.Find("player");
        player_script = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Lv >= 1)
        {
            //intervalごとにルーンプレハブを作成する関数を呼び出す
            if ((Time.time - pretime) >= interval)
            {
                pretime = Time.time;
                //Lvの回数分rune_tracerオブジェクトを作成する
                for (int i = 0; i <Lv; i++)
                {
                    make_rune();
                }
            }
        }
    }

    public void make_rune()
    {
        player_pos = player_script.transform.position;

        //rune_tracerプレハブをobjに取得
        GameObject obj = Instantiate (rune, player_pos, Quaternion.identity);

        //クリックした座標の取得（スクリーン座標からワールド座標に変換）
        rnd_x = Random.Range(-1.0f, 1.0f);
        rnd_y = Random.Range(-1.0f, 1.0f);
        //ランダムでどの向きにスピードを与えるかを決める
        direction = new Vector2(rnd_x, rnd_y);
        //directionのベクトルのサイズを1にする
        Vector2 shotForward = Vector2.Scale((direction), new Vector2(1, 1)).normalized;
        //弾に速度を与える
        obj.GetComponent<Rigidbody2D>().velocity = shotForward * speed;
    }
}

