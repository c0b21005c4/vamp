using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_block : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //クリックされたら呼び出される関数(Eventtrigerに設定すること)
    //メモ  Eventtrigerにはscriptではなくscriptをアタッチしたオブジェクトを入れること
    public void OnClick()
    {
        //Substring(開始位置, 長さ)でweapon1_blockの番号の1の部分だけ取り出す
        string num_string = this.name.Substring(6, 1);
        //-1するのはリストのインデックスは0から始まるため
        //int.Parse("文字列")で文字列をintに変換するdouble.Parse("文字列")だとdoubleになる
        int num_int = int.Parse(num_string)-1;

        GameObject window_obj = GameObject.Find("Lv_up_window(Clone)");
        Lv_up_window window_script = window_obj.GetComponent<Lv_up_window>();

        window_script.selected(num_int);
    }
}
