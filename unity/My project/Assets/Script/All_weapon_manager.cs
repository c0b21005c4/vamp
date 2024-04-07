using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_weapon_manager : MonoBehaviour
{

    public GameObject gamedirector;
    public timer timer_script;
    public float timeCount;
    
    //武器の辞書　0なら無効
	public Dictionary<string, int> Weapon_dic = new Dictionary<string, int>() 
	{
		//Key=武器の名称、value=武器のレベル
        {"bible", 0},
        {"bullet", 0},
        {"holy_water", 0},
        {"ax", 0},
        {"rune", 0},
        {"division_shot", 0}
    };

    //武器ごとのダメージを保存しておく辞書
    public Dictionary<string, int> Weapon_damage_dic = new Dictionary<string, int>() 
	{
		//Key=武器の名称、value=武器の総ダメージ量
        {"bible", 0},
        {"bullet", 0},
        {"holy_water", 0},
        {"ax", 0},
        {"rune", 0},
        {"division_shot", 0}
    };

    //武器ごとの取得時間を保存しておく辞書
    public Dictionary<string, int> Weapon_gettime_dic = new Dictionary<string, int>() 
	{
		//Key=武器の名称、value=武器の総ダメージ量
        {"bible", 0},
        {"bullet", 0},
        {"holy_water", 0},
        {"ax", 0},
        {"rune", 0},
        {"division_shot", 0}
    }; 

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
    }

    //武器のレベルを取得する関数
    public int Get_Weapon_Lv(string weapon)
	{
		return Weapon_dic[weapon];
	}

    //武器のレベルアップに使用する関数
    public void Lv_up(string weapon)
    {
        Weapon_dic[weapon] += 1;

        //武器を取得した時の時間を保存しておく
        if (Weapon_dic[weapon] == 1)
        {
            Weapon_gettime_dic[weapon] = (int)timeCount;
        }
        if (weapon == "bible")
        {
            GameObject bible_Generator = GameObject.Find("Generator_bible");
            Generator_bible bible_Generator_script = bible_Generator.GetComponent<Generator_bible>();
            
            bible_Generator_script.Lv = Weapon_dic[weapon];
            bible_Generator_script.speed = bible_Generator_script.Lv;
            bible_Generator_script.power = bible_Generator_script.Lv+5;
        }         
        
        else if (weapon == "bullet")
        {
            GameObject Generator = GameObject.Find("Generator_bullet");
            Generator_bullet script = Generator.GetComponent<Generator_bullet>();
            script.Lv = Weapon_dic[weapon];

            script.interval = 1.0f-(0.1f*script.Lv+0.1f);
        }

        else if (weapon == "holy_water")
        {
            GameObject obj = GameObject.Find("player/holy_water_generator");
            holy_water_generator script = obj.GetComponent<holy_water_generator>();

            script.lv = Weapon_dic[weapon];

            switch (script.lv){
                case 1:
                {
                    script.generate_interval = 2.0f;
                    script.zone_power = 10;
                    script.zone_interval = 1.0f;
                    script.zone_size = 4;
                    script.drop_radius = 3;

                    break;
                }

                case 2:
                {
                    script.generate_interval = 1.5f;
                    script.zone_power = 10;
                    script.zone_interval = 1.0f;
                    script.zone_size = 4;
                    script.drop_radius = 3;

                    break;
                }

                case 3:
                {
                    script.generate_interval = 1.5f;
                    script.zone_power = 20;
                    script.zone_interval = 1.0f;
                    script.zone_size = 5;
                    script.drop_radius = 3;

                    break;
                }

                case 4:
                {
                    script.generate_interval = 1.5f;
                    script.zone_power = 20;
                    script.zone_interval = 1.0f;
                    script.zone_size = 5;
                    script.drop_radius = 4;

                    break;
                }

                case 5:
                {
                    script.generate_interval = 1.5f;
                    script.zone_power = 20;
                    script.zone_interval = 2.0f;
                    script.zone_size = 6;
                    script.drop_radius = 4;

                    break;
                }

                case 6:
                {
                    script.generate_interval = 1.0f;
                    script.zone_power = 20;
                    script.zone_interval = 2.0f;
                    script.zone_size = 6;
                    script.drop_radius = 4;

                    break;
                }

                case 7:
                {
                    script.generate_interval = 1.0f;
                    script.zone_power = 30;
                    script.zone_interval = 2.0f;
                    script.zone_size = 7;
                    script.drop_radius = 4;

                    break;
                }

                case 8:
                {
                    script.generate_interval = 0.5f;
                    script.zone_power = 30;
                    script.zone_interval = 2.0f;
                    script.zone_size = 7;
                    script.drop_radius = 4;

                    break;
                }
                
                default:
                {
                    Debug.Log("エラーですよー！");
                    break;
                }
            }
        }

        else if (weapon == "ax")
        {
            GameObject obj = GameObject.Find("ax_generater");
            ax_generater script = obj.GetComponent<ax_generater>();

            script.lv = Weapon_dic[weapon];

            switch (script.lv){
                case 1:
                {
                    script.ax_num = 1;
                    script.generate_interval = 3.0f;
                    script.generate_interval_series = 0.1f;
                    script.ax_power = 50;
                    script.size = 3;

                    break;
                }

                case 2:
                {
                    script.ax_num = 2;
                    script.generate_interval = 3.0f;
                    script.generate_interval_series = 0.1f;
                    script.ax_power = 60;
                    script.size = 3;

                    break;
                }

                case 3:
                {
                    script.ax_num = 3;
                    script.generate_interval = 3.0f;
                    script.generate_interval_series = 0.1f;
                    script.ax_power = 70;
                    script.size = 3;

                    break;
                }

                case 4:
                {
                    script.ax_num = 3;
                    script.generate_interval = 3.0f;
                    script.generate_interval_series = 0.1f;
                    script.ax_power = 80;
                    script.size = 3;

                    break;
                }

                case 5:
                {
                    script.ax_num = 4;
                    script.generate_interval = 3.0f;
                    script.generate_interval_series = 0.1f;
                    script.ax_power = 90;
                    script.size = 3;

                    break;
                }

                case 6:
                {
                    script.ax_num = 4;
                    script.generate_interval = 3.0f;
                    script.generate_interval_series = 0.1f;
                    script.ax_power = 100;
                    script.size = 4;

                    break;
                }

                case 7:
                {
                    script.ax_num = 5;
                    script.generate_interval = 3.0f;
                    script.generate_interval_series = 0.1f;
                    script.ax_power = 110;
                    script.size = 4;

                    break;
                }

                case 8:
                {
                    script.ax_num = 6;
                    script.generate_interval = 3.0f;
                    script.generate_interval_series = 0.1f;
                    script.ax_power = 120;
                    script.size = 4;

                    break;
                }

                default:
                {
                    Debug.Log("エラーですよー！");
                    break;
                }
            }
        }

        else if (weapon == "rune")
        {
            GameObject Generator_rune_tracer = GameObject.Find("Generator_rune_tracer");
            Generator_rune_tracer Generator_rune_script = Generator_rune_tracer.GetComponent<Generator_rune_tracer>();
            
            Generator_rune_script.Lv = Weapon_dic[weapon];
        }

        else if (weapon == "division_shot")
        {
            GameObject obj = GameObject.Find("Generator_division_shot");
            division_shot_generater script = obj.GetComponent<division_shot_generater>();

            script.lv = Weapon_dic[weapon];
            switch (script.lv)
            {
                case 1:
                {
                    script.power = 10;
                    script.interval = 3.0f;
                    break;
                }

                case 2:
                {
                    script.power = 20;
                    script.interval = 3.0f;
                    break;
                }

                case 3:
                {
                    script.power = 20;
                    script.interval = 2.0f;
                    break;
                }

                case 4:
                {
                    script.power = 20;
                    script.interval = 2.0f;
                    break;
                }

                case 5:
                {
                    script.power = 30;
                    script.interval = 2.0f;
                    break;
                }

                case 6:
                {
                    script.power = 40;
                    script.interval = 2.0f;
                    break;
                }

                case 7:
                {
                    script.power = 50;
                    script.interval = 2.0f;
                    break;
                }

                case 8:
                {
                    script.power = 50;
                    script.interval = 1.0f;
                    break;
                }

                default:
                {
                    Debug.Log("errrrrrrrrrrrrrrrrrror!!");
                    break;
                }
            }
        }
    }
}
