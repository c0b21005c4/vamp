using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ax_generater : MonoBehaviour
{
    private GameObject weapon_manager;
    private All_weapon_manager weapon_script;

    public int lv;

    public GameObject AxPrefab;
    private float time = 0f;
    private float time_series = 0f;

    public int ax_num;
    private int now_ax_num;

    public float generate_interval;
    public float generate_interval_series;

    public int ax_power;

    public float size; 
    // Start is called before the first frame update
    void Start()
    {
        weapon_manager = GameObject.Find("player/All_weapon_manager");
        weapon_script = weapon_manager.GetComponent<All_weapon_manager>();
        this.lv = weapon_script.Get_Weapon_Lv("ax");
        this.generate_interval = 3.0f;
        this.generate_interval_series = 0.1f;

        this.ax_num = 1;

        this.ax_power = 15;
        this.size = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (lv != 0){
            this.time += Time.deltaTime;
            if (time > generate_interval)
            { 
                this.time_series += Time.deltaTime;
                if (time_series > generate_interval_series){
                    now_ax_num++;

                    GameObject player = GameObject.Find("player");

                    GameObject ax = Instantiate(AxPrefab);
                    ax.transform.position = player.transform.position;
                    ax.transform.localScale = new Vector3(size, size, size);

                    time_series = 0f;
                    if (now_ax_num == ax_num)
                    {
                        time = 0f;
                        now_ax_num = 0;
                    }
                }
            }
        }
    }
}
