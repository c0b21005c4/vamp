using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 //#############################追加##########################
 //#############################追加ここまで###################

public class enemy : MonoBehaviour
{
    [SerializeField] private Text damageText; //ダーメージテキストを格納
    private GameObject canvas;//親にするキャンバスを格納
    private GameObject target;
    private GameObject gamedirector;
    private GameDirector gamedirector_script;
    private GameObject All_weapon_manager;
    private All_weapon_manager All_weapon_manager_script;

    private float rad;
    
    public float ene_speed = 1;

    public int hp;

    // 経験値や宝箱を入れる
    public GameObject drop;

    static bool invincible_bible = false;
    static bool invincible_bullet = false;
    static bool invincible_holy_water = false;
    static bool invincible_ax = false;
    static bool invincible_rune = false;
    static bool invincible_division_shot = false;

    public Dictionary<string, bool> invincible_dic = new Dictionary<string, bool>() 
	{
		//Key=武器の名称、value=武器のレベル
        {"bible", invincible_bible},
        {"bullet", invincible_bullet},
        {"holy_water", invincible_holy_water},
        {"ax", invincible_ax},
        {"rune", invincible_rune},
        {"division_shot", invincible_division_shot}
        
    };

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        target = GameObject.Find("player");
        gamedirector = GameObject.Find("GameDirector");
        gamedirector_script = gamedirector.GetComponent<GameDirector>();
        GameObject All_weapon_manager = target.transform.Find("All_weapon_manager").gameObject;
        All_weapon_manager_script = All_weapon_manager.GetComponent<All_weapon_manager>();
    }
 
    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            gamedirector_script.enemies_defeated++;
            Destroy(this.gameObject);
            //死んだ場所にdrop品を落とす
            GameObject go = Instantiate(drop);
            go.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }

        Vector2 my_pos = target.transform.position;

        //ここの3行で敵の座標を所得する
        Transform myTransform = this.transform;
        Vector2 ene_pos = myTransform.position;
        // Vector3 p = new Vector3(0.001f, 0f, 0f);
        
        Vector2 targeting = (my_pos - ene_pos).normalized;
        
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(targeting.x * ene_speed,targeting.y * ene_speed);
    }
    
    public void Damage(int damage, string weapon_name) 
        {
            if (GetComponent<SpriteRenderer>().isVisible)
            {

                if (!invincible_dic[weapon_name])
                {
                    this.hp -= damage;
                    All_weapon_manager_script.Weapon_damage_dic[weapon_name] += damage;
                    if (canvas)
                    {
                        Text text;
                        text = Instantiate(damageText, new Vector3(0,0,0), Quaternion.identity);
                        text.transform.SetParent(canvas.transform, false);
                        text.text = damage.ToString();
                        text.transform.position = this.transform.position;
                    }
                }
            }
        }
    //#############################追加ここまで###################

}
