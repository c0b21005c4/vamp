using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator: MonoBehaviour
{
    //敵のprefabをそれぞれの変数に保持
    public GameObject bat_prefab;
    public GameObject slime_prefab;
    public GameObject skull_prefab;
    public GameObject red_slime_prefab;
    public GameObject green_bat_prefab;
    public GameObject ghost_prefab;
    public GameObject goblin_prefab;
    public GameObject dead_man_prefab;

    float x;
    float y;

    //イベントを一度だけ発動させるのに使用するbool
    bool circle_event_3 = false;
    bool circle_event_5 = false;

    private float interval;
    int minutes;
    //時間
    private float game_time = 0f;
    float pretime;

    int enemy_time = 0;

    public GameObject player_tag;
    
    //ここにカメラの縦横のrangeを所得する引数を設定
    [SerializeField]private float width = 12.5f;
    [SerializeField]private float height = 7f;

    //上下左右どこから生成するかを決定するリスト
    List<List<float>> gene_list = new List<List<float>>();

    PlayerScript player_script;
    //まさかのVector2にしないとfloatエラーになるという仕様
    private Vector2 player_pos;

    // Start is called before the first frame update
    void Start()
    {
        //敵の出現の時間間隔を決定
        interval = 100;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    //Time.deltaTimeを使用する際はFixedUpdateの方が良い
    void FixedUpdate()
    {
        enemy_time++;
        //時間計測
        game_time += Time.deltaTime;

        minutes = (int)game_time/60;

        //分ごとに敵の生成挙動を変える
        if (minutes == 0)
        {
            create_enemy(bat_prefab);
        }

        else if (minutes == 1)
        {
            create_enemy(slime_prefab);
        }

        else if(minutes == 2)
        {
            create_enemy(bat_prefab, 2, 20);
        }

        else if(minutes == 3 || minutes == 4)
        {
            if (!circle_event_3)
            {
                create_enemy_circle(goblin_prefab, slime_prefab, 100);
                circle_event_3 = true;
            }
            create_enemy(bat_prefab, 2, 10);
            create_enemy(slime_prefab, 1, 10);
        }

        else if(minutes == 5)
        {
            if (!circle_event_5)
            {
                create_enemy_circle(skull_prefab, red_slime_prefab, 300);
                circle_event_5 = true;
            }
            create_enemy(red_slime_prefab, 2);
            create_enemy(green_bat_prefab, 2);
        }

        else if(minutes == 6)
        {
            create_enemy(ghost_prefab, 3);
        }

        else if (minutes == 7)
        {
            create_enemy(goblin_prefab, 2);
        }

        else if (minutes == 8)
        {
            create_enemy(slime_prefab, 2, 100);
            create_enemy(bat_prefab, 2, 80);
            create_enemy(ghost_prefab, 1);
            create_enemy(goblin_prefab, 1);
        }

        else if (minutes == 9)
        {
            create_enemy(red_slime_prefab, 4);
            create_enemy(green_bat_prefab, 3);
            create_enemy(ghost_prefab, 2);
            create_enemy(goblin_prefab, 2);
        }

        else if (minutes == 10)
        {
            create_enemy(dead_man_prefab);
        }
    }

    //敵を出現させる関数(出現させる敵, 出現させる頻度(4なら頻度が4倍になる), hpの増分)
    public void create_enemy(GameObject enemy_prefab,int frequency=1, int increase_hp=0)
    {
        //敵の出現する間隔intervalをfrequencyで割ることで出現頻度を調整する
        if (enemy_time % (int)(interval/frequency) == 0)
        {
            //playerの位置を所得
            player_tag = GameObject.Find("player");
            player_script = player_tag.GetComponent<PlayerScript>();
            player_pos = player_script.transform.position;

            //リストに格納
            gene_list.Add(new List<float>{player_pos.x + width,player_pos.y + Random.Range(-height,height)});
            gene_list.Add(new List<float>{player_pos.x - width ,player_pos.y + Random.Range(-height,height)});
            gene_list.Add(new List<float>{player_pos.x + Random.Range(-width,width) ,player_pos.y + height});
            gene_list.Add(new List<float>{player_pos.x + Random.Range(-width,width) ,player_pos.y - height});

            //enemyをインスタンス化する(生成する)
            GameObject enemy= Instantiate(enemy_prefab);
            //生成した敵の座標をランダムに決定する
            List<float> pos = gene_list[Random.Range(0,gene_list.Count)];
            enemy.transform.position = new Vector2(pos[0],pos[1]);

            enemy enemyscript;
            enemyscript = enemy.GetComponent<enemy>();
            enemyscript.hp += increase_hp; 
            
            //リスト初期化
            gene_list.Clear();
        }
    } 

    // プレイヤーを中心として楕円形に敵を生成する関数（ボスとなる敵のprefab, 周囲に出現させる敵のpregab, hpの増分）
    public void create_enemy_circle(GameObject boss_enemy_prefab, GameObject enemy_prefab, int increase_hp=0)
    {
        //周りに出現させる敵を保存する
        GameObject circle_enemy;
        //playerの位置を所得
        player_tag = GameObject.Find("player");
        player_script = player_tag.GetComponent<PlayerScript>();
        player_pos = player_script.transform.position;

        for (float i=0; i<=360; i++)
        {
            if (i % 5 == 0)
            {
                //enemyをインスタンス化する(生成する)
                circle_enemy= Instantiate(enemy_prefab);
                
                x = Mathf.Sin(i);
                y = Mathf.Cos(i);

                circle_enemy.transform.position = new Vector2(player_pos.x+x*16,player_pos.y+y*10);

                enemy enemyscript;
                enemyscript = circle_enemy.GetComponent<enemy>();
                enemyscript.hp += increase_hp;
                enemyscript.ene_speed = 0.2f; 
            }
        }

        GameObject boss_enemy= Instantiate(boss_enemy_prefab, new Vector3(player_pos.x +10, player_pos.y, 0), Quaternion.identity);
        enemy boss_script = boss_enemy.GetComponent<enemy>();
        boss_script.ene_speed = 0.3f;
    }    
}