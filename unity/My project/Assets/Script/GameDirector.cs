using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//UIを使うときは以下の一行を忘れないこと
using UnityEngine.UI;



public class GameDirector : MonoBehaviour
{
    //画面上部のhpゲージに必要な変数
    GameObject hp;
    GameObject exp_bar;

    //Lvupに必要な経験値の数
    int necessary_exp = 3;
    
    //playerのレベル
    public int player_lv = 15;
    public int enemies_defeated = 0;
    
    //Lv_up_windowに関する変数
    public GameObject Lv_up_window_prefab;
    private GameObject Lv_up_window_obj;
    Lv_up_window Lv_up_window_script;

    public GameObject gameover_window_prefab;

    //playerオブジェクトを格納
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.hp = GameObject.Find("hp");
        this.exp_bar = GameObject.Find("exp_bar");
        this.exp_bar.GetComponent<Image>().fillAmount = 0;

        player = GameObject.Find("player"); 
        //playerを基準として座標を指定しないとズレる　Z座標を-1にしないと聖書とか聖水が前に出てくる
        GameObject Lv_up_window_obj = Instantiate(Lv_up_window_prefab, new Vector3(player.transform.position.x, player.transform.position.y-0.19f, -1.0f), Quaternion.identity); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (this.exp_bar.GetComponent<Image>().fillAmount == 1)
        {
            player = GameObject.Find("player"); 
            //playerを基準として座標を指定しないとズレる　Z座標を-1にしないと聖書とか聖水が前に出てくる
            GameObject Lv_up_window_obj = Instantiate(Lv_up_window_prefab, new Vector3(player.transform.position.x, player.transform.position.y-0.19f, -1.0f), Quaternion.identity);
            //Lv_upに必要な経験値数を増やす
            this.necessary_exp += 3;
            this.exp_bar.GetComponent<Image>().fillAmount = 0;
            player_lv++;
        }
    }

    public void Damage()
    {
        this.hp.GetComponent<Image>().fillAmount -= 0.02f;
        if (this.hp.GetComponent<Image>().fillAmount <= 0)
        {
            Game_over();
        }
    }

    public void get_exp()
    {

        this.exp_bar.GetComponent<Image>().fillAmount += 1.0f/necessary_exp;
    }
    public void full_recovery()
    {
        this.hp.GetComponent<Image>().fillAmount = 1.0f;
    }

    public void Game_over()
    {
        GameObject gameover_window_obj = Instantiate(gameover_window_prefab, new Vector3(player.transform.position.x, player.transform.position.y, -1.0f), Quaternion.identity);
        Time.timeScale = 0;
    }
}
