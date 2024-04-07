using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bible: MonoBehaviour
{
    //bibleの回転移動に使用する変数達。statusはGenerator_bibleから設定される
    float x;
    float y;
    public List<int> status; 
    private GameObject player;

    //生成されてから消滅するまでの時間
    float kill_time = 5.0f;
    float time = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player"); 
    }
    // Update is called once per frame
    void Update()
    {
        //status= {number, speed, power, radius}; numberに16.5fをかけることによって8角形の頂点の場所それぞれにbibleを生成する。
        x = status[3] * Mathf.Sin(Time.time * status[1] + (16.5f*status[0]));
        y = status[3] * Mathf.Cos(Time.time * status[1] + (16.5f*status[0]));

        //回転移動するためにpositionを書き換える
        transform.position = new Vector2(x+player.transform.position.x, y+player.transform.position.y);

        //一定時間が経ったら、自信を削除する
        time += Time.deltaTime;
        if (time > kill_time)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
		//重なっているオブジェクトが敵だったら
        if(collision.gameObject.tag == "enemy")
        {
		    enemy enemyscript;
            enemyscript = collision.GetComponent<enemy>();
            enemyscript.Damage(status[2], "bible");
            enemyscript.invincible_dic["bible"] = true;
        }
	}

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            enemy enemyscript;
            enemyscript = collision.GetComponent<enemy>();
            enemyscript.invincible_dic["bible"] = false;
        }
    }
}