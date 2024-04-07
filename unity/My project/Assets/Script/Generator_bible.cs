using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator_bible : MonoBehaviour
{
    //生成するbible_prefabを格納する変数
    public GameObject bible;
    GameObject bible_obj;
    GameObject player;

    //bibleのステータス
    public int speed;
    public int Lv;
    public int radius;
    public int power;

    //bibleを生成する間隔を設定
    private float time = 0f;
    private float generate_interval = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        //LvをAll_weapon_managerから取得して、各パラメータを初期化する
        GameObject weapon_manager = GameObject.Find("player/All_weapon_manager");
        All_weapon_manager weapon_script = weapon_manager.GetComponent<All_weapon_manager>();
        Lv = weapon_script.Get_Weapon_Lv("bible");
        speed = Lv;
        power = Lv+5;
        radius = 4;
        player = GameObject.Find("player"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Lv != 0)
        {
            //timeに0.1ずつ加算していくイメージ
            time += Time.deltaTime;
            //生成する間隔に達したら
            if (time > generate_interval)
            {
                //Lvの回数分bibleを発生させる(Lvが1なら1つ、Lvが4なら4つのbibleを生成)
                for (int i = 0; i <Lv; i++)
                {
                    //iを引数として与えることで、発生する場所をそれぞれ違う場所にすることができる
                    make(i);
                }
                //timeを初期化
                time = 0f;
            }
        }
    }

    public void make(int number)
    {
        //bible_prefabをbible_objに取得
        GameObject bible_obj = Instantiate (bible, this.transform.position, Quaternion.identity);
        bible bible_script = bible_obj.GetComponent<bible>();
        //生成したbibleのステータスを設定
        bible_script.status = new List<int>(){number, speed, power, radius};
        //playerを親オブジェクトに設定する
        bible_obj.transform.parent = player.transform;
    }
}