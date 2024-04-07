using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{ 
	private GameObject GameDirector;
    GameDirector GameDirector_script;
	GameObject pre_exp;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
		//移動に使用する関数Moveを呼び出す
		Move();
	}

	//トリガーとなるオブジェクトとコライダーが重なり合っている間呼び出される関数
	void OnTriggerStay2D(Collider2D collision)
	{
		//重なっているオブジェクトが敵だったら
        if(collision.gameObject.tag == "enemy")
        {
			//GameDirectorというオブジェクトをhierarchyから探して取得
            GameObject director = GameObject.Find("GameDirector");
			//取得したGameDirectorオブジェクトのGameDirectorというコンポーネント(C#スクリプト)を取得してその中のDamege関数を呼び出す。
            director.GetComponent<GameDirector>().Damage();
        }

		if (collision.gameObject.tag == "exp")
		{
			GameDirector = GameObject.Find("GameDirector");
			GameDirector_script = GameDirector.GetComponent<GameDirector>();
			GameDirector_script.get_exp();
			Destroy(collision.transform.parent.gameObject);
		}
	}

	// void OnTriggerExit2D(Collider2D collision)
	// {
	// 	if (collision.gameObject.tag == "exp" && this.transform.position == collision.transform.position)
	// 	{
			
	// 	}
	// }

	//移動に使用する関数Move()
	void Move()
    {
		/*GetAxisRaw（Horizontal）はAや←が入力されている時は-1、何も入力されていない時は0、
		Dや→が入力されている時は１を返す。
		同じようにVerticalは、sと↓が入力されている時は-1、何も入力されていない時は0、
		wと↑が入力されている時は１を返す*/
		float beside = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		//0で初期化しないとキーを押した方向に動き続けるのでここで初期化する
		Vector2 speed = new Vector2(0, 0);

		//Rigidbody2Dのvelocityは速度を表す。これを変換することでオブジェクトを動かす。
		//Aキーが押されたら
		if(beside == -1)
		{
			//左方向の速度を付ける
			speed += new Vector2(-10, 0);
		}
		
		//Dキーが押されたら
		else if(beside == 1)
		{ 
			speed += new Vector2(10, 0);
		} 

		//Wキーが押されたら
		if(vertical == 1)
		{ 
			speed += new Vector2(0, 10);
		} 

		//Sキーが押されたら
		else if(vertical == -1)
		{ 
			speed += new Vector2(0, -10);
		} 

		if(speed.x != 0 && speed.y != 0)
		{
			speed = new Vector2(speed.x / 1.4f, speed.y / 1.4f);	
		}

		this.GetComponent<Rigidbody2D>().velocity = speed;
	}
}

