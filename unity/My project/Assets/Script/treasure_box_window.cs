using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class treasure_box_window : MonoBehaviour
{
    public string choice_weapon_name = "None";
    public Dictionary<string, List<string>> weapon_data = new Dictionary<string, List<string>>();

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        Lv_up_select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Lv_up_select()
    {
        //All_weapon_manager.csを取得
        GameObject weapon_manager = GameObject.Find("player/All_weapon_manager");
        All_weapon_manager weapon_script = weapon_manager.GetComponent<All_weapon_manager>();
        //Weapon_dicをコピー。新規に作ることによって参照型ではなくなり、元の辞書が変わらなくなる。
        var weapon_dic_copy = new Dictionary<string, int>(weapon_script.Weapon_dic);

        //Key:武器の名前、Value:Lv,Lv0の時のテキスト,Lv1以上のテキスト　これらを格納したリスト
        weapon_data.Add("bible", new List<string>(){weapon_dic_copy["bible"].ToString(),"The Bible moves in a circle around you, damaging enemies it touches", "Increased biblical speed and attack power"});
        weapon_data.Add("bullet", new List<string>(){weapon_dic_copy["bullet"].ToString(),"At regular intervals, the gun is aimed at the location where the mouse is located", "Shorter intervals between shots"});
        weapon_data.Add("holy_water", new List<string>(){weapon_dic_copy["holy_water"].ToString(),"Holy water falls around you and spreads over a certain area", "Shorter intervals of holy water fall, increased range and attack power"});
        weapon_data.Add("ax", new List<string>(){weapon_dic_copy["ax"].ToString(),"hoge", "hogehoge"});
        weapon_data.Add("full_recovery1", new List<string>(){"0", "Recovers its own HP to the maximum", "Recovers its own HP to the maximum"});
        weapon_data.Add("full_recovery2", new List<string>(){"0", "Recovers its own HP to the maximum", "Recovers its own HP to the maximum"});
        weapon_data.Add("full_recovery3", new List<string>(){"0", "Recovers its own HP to the maximum", "Recovers its own HP to the maximum"});

        while (choice_weapon_name == "None")
        {
            //もし選ぶ元の辞書が空になったら(ここで書いておかないとElementAtでindexErrorを起こす)
            if (weapon_dic_copy.Count == 0)
            {
                choice_weapon_name = "full_recovery1";
                continue;
            }

            KeyValuePair<string, int> choice_weapon = weapon_dic_copy.ElementAt(Random.Range(0, weapon_dic_copy.Count));

            if (choice_weapon.Value != 8)
            {
                //選ばれた武器をレベルアップ可能として辞書に格納
                choice_weapon_name = choice_weapon.Key;
                //二回選ばれるのを防ぐためにコピーした辞書からは削除しておく
                weapon_dic_copy.Remove(choice_weapon.Key);
            }

            //ランダムで取ってきた武器のLvが8なら
            else
            {
                //選ばずに削除
                weapon_dic_copy.Remove(choice_weapon.Key);
            }
        }

        GameObject gazo = GameObject.Find("weapon_image");
        Sprite sprite = Resources.Load<Sprite>(choice_weapon_name);
        SpriteRenderer image = gazo.GetComponent<SpriteRenderer>();
        image.sprite = sprite;

        if (choice_weapon_name == "full_recovery1")
        {
            image.transform.localScale = new Vector3(0.2f, 0.2f, 1);
        }

        else if (choice_weapon_name == "rune")
        {
            image.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void selected()
    {
        GameObject weapon_manager = GameObject.Find("player/All_weapon_manager");
        All_weapon_manager weapon_script = weapon_manager.GetComponent<All_weapon_manager>();

        if (choice_weapon_name == "full_recovery1")
        {
            //GameDirectorというオブジェクトをhierarchyから探して取得
            GameObject director = GameObject.Find("GameDirector");
			//取得したGameDirectorオブジェクトのGameDirectorというコンポーネント(C#スクリプト)を取得してその中のfull_recovery1関数を呼び出す。
            director.GetComponent<GameDirector>().full_recovery();
        }
        else
        {
            weapon_script.Lv_up(choice_weapon_name);
        }
        Debug.Log(choice_weapon_name);
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }
}
