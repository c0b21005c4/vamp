using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gameover_window : MonoBehaviour
{
    float survived;
    int player_lv;
    public int enemies_defeated=0;
    string DPS_values;
    public Dictionary<string, float> Weapon_DPS_dic = new Dictionary<string, float>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject bible_Generator = GameObject.Find("Generator_bible");
        Generator_bible bible_Generator_script = bible_Generator.GetComponent<Generator_bible>();
        //各データを取得するために様々なオブジェクトを持ってくる
        //生き延びた時間とレベルを取ってくる
        GameObject gamedirector = GameObject.Find("GameDirector");
        timer timer_script = gamedirector.GetComponent<timer>();
        GameDirector gamedirector_script = gamedirector.GetComponent<GameDirector>();

        //生き延びた時間をtimer_scriptからとってくる
        survived = timer_script.timeCount;
        //playerのlvをgamedirectorから取ってくる
        player_lv = gamedirector_script.player_lv;
        //倒した敵の数をgamedirectorから取ってくる
        enemies_defeated = gamedirector_script.enemies_defeated;

        //weaponのLv取得
        GameObject All_weapon_manager = GameObject.Find("All_weapon_manager");
        All_weapon_manager All_weapon_manager_script = All_weapon_manager.GetComponent<All_weapon_manager>();
        
        //textを書き換えるためにtextを取得する
        GameObject main_text_obj = transform.Find("gameover_Canvas/main_info_text").gameObject;
        TextMeshProUGUI main_text = main_text_obj.GetComponent<TextMeshProUGUI>();

        //武器の辞書を回しながら
        foreach(var dict in All_weapon_manager_script.Weapon_dic)
        {
            //武器のlvが0出なければ
            if (All_weapon_manager_script.Weapon_dic[dict.Key] != 0)
            {
                //取得した時間を引いて、使用時間を辞書に保存
                All_weapon_manager_script.Weapon_gettime_dic[dict.Key] = (int)timer_script.timeCount-All_weapon_manager_script.Weapon_gettime_dic[dict.Key];
                //使用した時間が0出なければ(0で割ることを防ぐ)
                if (All_weapon_manager_script.Weapon_gettime_dic[dict.Key] != 0)
                {
                    //1秒あたりのダメージ量を計算する
                    float dps = (int)(100*(float)All_weapon_manager_script.Weapon_damage_dic[dict.Key]/(float)(All_weapon_manager_script.Weapon_gettime_dic[dict.Key]));
                    Weapon_DPS_dic.Add(dict.Key, dps/100);
                }
                //使用した時間が0であれば
                else
                {
                    //0を辞書に登録
                    Weapon_DPS_dic.Add(dict.Key, 0);
                }
            }
            //DPSの辞書に0を登録
            else
            {
                Weapon_DPS_dic.Add(dict.Key, 0);
            }
        }

        //親オブジェクトを取得  
        GameObject parent = transform.Find("gameover_Canvas").gameObject;
        //子どもたちを格納した配列を取得
        var children = GetChildren(parent);
        //上部分のtextに生き延びた時間、プレイヤーのlv、倒した敵の数を表示する
        main_text.SetText($"{(int)survived/60}:{(int)survived%60}\n{player_lv}\n{enemies_defeated}");    
    
        //Join（区切り文字, list）で区切り文字で連結した文字列になる。今回だと二回改行をはさんで、武器のlvやダメージが表示される
        string lv_str = String.Join(",", All_weapon_manager_script.Weapon_dic.Values);
        //Split(区切り文字)で文字列を区切り文字で区切って、リストや配列にする
        string[] lv_values = lv_str.Split(",");
        string damage_str = String.Join(",", All_weapon_manager_script.Weapon_damage_dic.Values);
        string[] damage_values = damage_str.Split(",");
        string time_str = String.Join(",", All_weapon_manager_script.Weapon_gettime_dic.Values);
        string[] time_values = time_str.Split(",");
        string DPS_str = String.Join(",", Weapon_DPS_dic.Values);
        string[] DPS_values = DPS_str.Split(",");
        Debug.Log(lv_values.GetType());

        //文字列をリストに格納
        List<string[]> values = new List<string[]>(){lv_values, damage_values, time_values, DPS_values};

        //それぞれのテキストオブジェクトを取得して、それらにvaluesを代入する。
        for(int i=0; i<children.Length; i++ )
        {
            if (i < 4)
            {
                TextMeshProUGUI weapon_text = children[i].GetComponent<TextMeshProUGUI>();
                weapon_text.SetText(String.Join("\n\n", values[i][0..5]));
            }
            else
            {
                TextMeshProUGUI weapon_text = children[i].GetComponent<TextMeshProUGUI>();
                weapon_text.SetText(String.Join("\n\n", values[i%4][5..]));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // parent直下の子オブジェクトをforループで取得するGetChildrenを定義
    //GameObject[]でGameObjectの配列を戻り値とすることを宣言している(配列とリストは別物)
    private static GameObject[] GetChildren(GameObject parent)
    {
        //childCountとGetchild()はtransform型にしかないのでここで取得する
        var parentTransform = parent.transform;

        // 子オブジェクトを格納する配列作成
        //-5をしているのはimageとtextのオブジェクト以外を格納しないようにするため
        var children = new GameObject[8];

        // 0～個数-1までの子を順番に配列に格納
        for (var i = 0; i < children.Length; ++i)
        {
            //GetChildは元々用意されている子オブジェクトを返す関数
            //最後の.gameObjectが無いとTransform型で帰ってくる
            children[i] = parentTransform.GetChild(i).gameObject;
        }

        // 子オブジェクトが格納された配列
        return children;
    }
}
