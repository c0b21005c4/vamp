using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TextMeshPro型を使用する際に必要となる
using TMPro;
using UnityEngine.UI;
//ElementAt()を使用する際に必要となる。
using System.Linq;

public class Lv_up_window : MonoBehaviour
{
    //取得したい子オブジェクト達を持つ親オブジェクト
    [SerializeField] private GameObject parent;
    //レベルアップで選ぶことのできる武器を三つ格納したリスト
    public List<string> three_choice_weapon_name = new List<string>();
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
        // weapon_script.Lv_up(weapon);
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
        weapon_data.Add("ax", new List<string>(){weapon_dic_copy["ax"].ToString(), "Throws an ax randomly", "Increases ax damage, quantity, and frequency" });
        weapon_data.Add("rune", new List<string>(){weapon_dic_copy["rune"].ToString(),"rune_tracer attack enemys", "interval is shorter"});
        weapon_data.Add("division_shot", new List<string>(){weapon_dic_copy["division_shot"].ToString(), "Fire a division_shot. Fires a small division_shot when division_shot touches an enemy.", "Increases the power and frequency of division_shot." });
        weapon_data.Add("full_recovery1", new List<string>(){"0", "Recovers its own HP to the maximum", "Recovers its own HP to the maximum"});
        weapon_data.Add("full_recovery2", new List<string>(){"0", "Recovers its own HP to the maximum", "Recovers its own HP to the maximum"});
        weapon_data.Add("full_recovery3", new List<string>(){"0", "Recovers its own HP to the maximum", "Recovers its own HP to the maximum"});

        //三つ武器が選ばれるまでループ
        while (three_choice_weapon_name.Count != 3)
        {
            //もし選ぶ元の辞書が空になったら(ここで書いておかないとElementAtでindexErrorを起こす)
            if (weapon_dic_copy.Count == 0)
            {
                //辞書に同じkeyは付けることができないのでCountによって少しkeyの名前を変えている。
                if (three_choice_weapon_name.Count == 0)
                {
                    //全回復を追加する
                    three_choice_weapon_name.Add("full_recovery3");
                }
                else if (three_choice_weapon_name.Count == 1)
                {
                    //全回復を追加する
                    three_choice_weapon_name.Add("full_recovery2");
                }
                else
                {
                    //全回復を追加する
                    three_choice_weapon_name.Add("full_recovery1");
                }
                //以降の処理をスキップしてwhileループ
                continue;
            }
            //KeyValuePairはKeyとValueを両方持つことができる型。ElementAt()を使うと
            KeyValuePair<string, int> choice_weapon = weapon_dic_copy.ElementAt(Random.Range(0, weapon_dic_copy.Count));
            //選ばれた武器のLvが8出なかったら（まだレベルアップできるなら）
            if (choice_weapon.Value != 8)
            {
                //選ばれた武器をレベルアップ可能として辞書に格納
                three_choice_weapon_name.Add(choice_weapon.Key);
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

        //GetChildren()は自分で定義したimageとtextの子オブジェクトを全て取得する関数、childrenにはimageとtextの子オブジェクトが格納された配列が入る
        var children = GetChildren(parent);
        //子オブジェクトの数だけループ
        for (int i = 0; i < children.Length; i++)
        {
            //Imageの処理
            if (i <= 2)
            {
                //Spriteは画像をSceneにドラッグ＆ドロップしてできるオブジェクトの型。つまり貼り付けたい画像
                Sprite sprite = Resources.Load<Sprite>(three_choice_weapon_name[i]);
                //ImageはGameObjectの持っているコンポーネントで、これにより画像を表示する
                Image image = children[i].GetComponent<Image>();
                //ImageのspriteがエディターでいうSource Imageである。
                image.sprite = sprite;
            } 

            //textの処理
            else 
            {
                //3DならTextMeshPro型、2DならTextMeshProUGUI型を使用する    
                TextMeshProUGUI weapon_text = children[i].GetComponent<TextMeshProUGUI>();
                //選ばれた武器をKeyとしてLvを取得してLvが0だったら
                if (weapon_data[three_choice_weapon_name[(i%3)]][0] == "0")
                {
                    //武器自身についての説明を表示
                    //SetText()はTextMeshProUGUI型に元々定義されている。テキストの内容を変えるメソッド
                    weapon_text.SetText(weapon_data[three_choice_weapon_name[(i%3)]][1]);
                }
                //選ばれた武器がLv1以上なら
                else
                {
                    //レベルアップしたらどうなるのかの説明を表示
                    weapon_text.SetText(weapon_data[three_choice_weapon_name[(i%3)]][2]);
                }
            }
        }
    }

    // parent直下の子オブジェクトをforループで取得するGetChildrenを定義
    //GameObject[]でGameObjectの配列を戻り値とすることを宣言している(配列とリストは別物)
    private static GameObject[] GetChildren(GameObject parent)
    {
        //childCountとGetchild()はtransform型にしかないのでここで取得する
        var parentTransform = parent.transform;

        // 子オブジェクトを格納する配列作成
        //-5をしているのはimageとtextのオブジェクト以外を格納しないようにするため
        var children = new GameObject[parentTransform.childCount-2];

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

    public void selected(int choice_number)
    {
        GameObject weapon_manager = GameObject.Find("player/All_weapon_manager");
        All_weapon_manager weapon_script = weapon_manager.GetComponent<All_weapon_manager>();

        if (three_choice_weapon_name[choice_number].Substring(0, three_choice_weapon_name[choice_number].Length-1) == "full_recovery")
        {
            //GameDirectorというオブジェクトをhierarchyから探して取得
            GameObject director = GameObject.Find("GameDirector");
			//取得したGameDirectorオブジェクトのGameDirectorというコンポーネント(C#スクリプト)を取得してその中のfull_recovery関数を呼び出す。
            director.GetComponent<GameDirector>().full_recovery();
        }
        else
        {
            weapon_script.Lv_up(three_choice_weapon_name[choice_number]);
        }
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }
}
