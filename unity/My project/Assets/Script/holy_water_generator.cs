using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holy_water_generator : MonoBehaviour
{
    private GameObject weapon_manager;
    private All_weapon_manager weapon_script;

    public GameObject HolyWaterPrefab;
    private float time = 0f;

    public int lv;

    public float generate_interval;

    public float water_speed;
    public float water_angle;

    public int zone_power;
    public float zone_interval;
    public int zone_size;

    public int drop_speed;
    public int drop_radius;

    float x;
    float y;


    // Start is called before the first frame update
    void Start()
    {
        weapon_manager = GameObject.Find("player/All_weapon_manager");
        weapon_script = weapon_manager.GetComponent<All_weapon_manager>();
        lv = weapon_script.Get_Weapon_Lv("holy_water");

        generate_interval = 2.0f;

        water_speed = 10.0f;
        water_angle = 100.0f;

        zone_power = 10;
        zone_interval = 1.0f;
        zone_size = 4;

        drop_speed = 5;
        drop_radius = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (lv != 0)
        {
            time += Time.deltaTime;
        
            if(time > generate_interval)
            {
                GameObject holywater = Instantiate(HolyWaterPrefab);
                holy_water holywater_script = holywater.GetComponent<holy_water>();

                x = drop_radius * Mathf.Sin(Time.time * drop_speed);
                y = drop_radius * Mathf.Cos(Time.time * drop_speed);

                GameObject player = GameObject.Find("player");
                holywater_script.Create(new Vector2(x, y) + new Vector2(player.transform.position.x, player.transform.position.y));

                holywater.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
                time = 0f;
            }
        }
    }
}
