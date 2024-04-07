using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune_tracer : MonoBehaviour
{
    //ダメージ量
    int power = 20;
    //消えるまでの時間
    float destroy_time = 7.0f;
    public GameObject rune_shadow;
   
    GameObject player;
    PlayerScript player_script;
    
    //それぞれの位置を取得しておく変数
    Vector3 player_pos;
    Vector3 rune_pos;

    //残像に使うオブジェクトを保存
    public GameObject shadow_obj;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        player_script = player.GetComponent<PlayerScript>();

        //残像のオブジェクトを生成
        make_rune_shadow();
        //自信を消すコルーティンを開始する
        StartCoroutine("Destroy_rune");
    }

    // Update is called once per frame
    void Update()
    { 
        //x, yにランダムで決定したx, yを代入する
        float x = this.GetComponent<Rigidbody2D>().velocity.x;
        float y = this.GetComponent<Rigidbody2D>().velocity.y;

        player_pos = player_script.transform.position;

        //画面の上まで行ったら
        if (this.transform.position.y >= player_pos.y + 7.0f)
        {
            //yスピードに-1をかけて方向を逆にする
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(x, -y);
            //めり込み防止のためにpositionを画面の上から少し下に設定する
            this.transform.position = new Vector2(this.transform.position.x, player_pos.y+6.9f); 
        }   
        
        //画面の下まで行ったら
        else if(this.transform.position.y <= player_pos.y - 7.0f)
        {
            //yスピードに-1をかけて方向を逆にする
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(x, -y);
            //めり込み防止のためにpositionを画面の下から少し上に設定する
            this.transform.position = new Vector2(this.transform.position.x, player_pos.y-6.9f); 
        }

        //画面の右まで行ったら
        if (this.transform.position.x >= player_pos.x + 12.5f)
        {
            //xスピードに-1をかけて方向を逆にする
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-x, y);
            //めり込み防止のためにpositionを画面の右から少し左に設定する
            this.transform.position = new Vector2(player_pos.x+12.4f, this.transform.position.y);
        }   

        //画面の左まで行ったら
        else if(this.transform.position.x <= player_pos.x - 12.5f)
        {
            //xスピードに-1をかけて方向を逆にする
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-x, y);
            //めり込み防止のためにpositionを画面の左から右に設定する
            this.transform.position = new Vector2(player_pos.x-12.4f, this.transform.position.y);
        }
    }


    void LateUpdate()
    {
        //shadow_objがあったら
        if (shadow_obj)
        {
            //残像オブジェクトの位置を滑らかにrune_tracerの後ろをついてくるように設定する
            //Vector3.Lerp(始まりの位置, 終わりの位置, 現在の位置{どちらかというとスピード})　shadow_objから自分まで18.0fのスピードで滑らかに移動する
            shadow_obj.transform.position = Vector3.Lerp(shadow_obj.transform.position, this.transform.position, 18.0f * Time.deltaTime);   
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
		//重なっているオブジェクトが敵だったら
        if(collision.gameObject.tag == "enemy")
        {
		    enemy enemyscript;
            enemyscript = collision.GetComponent<enemy>();
            enemyscript.Damage(power, "rune");
            enemyscript.invincible_dic["rune"] = true;
        }
	}
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            enemy enemyscript;
            enemyscript = collision.GetComponent<enemy>();
            enemyscript.invincible_dic["rune"] = false;
        }
    }

    //destroy_time秒後に自分自身を破壊するコルーティン
    IEnumerator Destroy_rune()
    {
        yield return new WaitForSeconds(destroy_time);
        Destroy(this.gameObject);
    }      

    public void make_rune_shadow()
    {
        //runeオブジェクトをplayerから出すようにする
        rune_pos = this.transform.position;

        //rune_shadowプレハブをobjに取得
        shadow_obj = Instantiate (rune_shadow, rune_pos, Quaternion.identity);
    }
}

