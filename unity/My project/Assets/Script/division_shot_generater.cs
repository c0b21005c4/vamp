using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class division_shot_generater : MonoBehaviour
{
    private GameObject weapon_manager;
    private All_weapon_manager weapon_script;

    public int lv;

    public GameObject division_shot_prefab;

    private GameObject player;
    private GameObject[] targets;

    private GameObject closeEnemy;

    float time=0f;
    public float interval;

    public int power;

    // Start is called before the first frame update
    void Start()
    {
        weapon_manager = GameObject.Find("player/All_weapon_manager");
        weapon_script = weapon_manager.GetComponent<All_weapon_manager>();
        this.lv = weapon_script.Get_Weapon_Lv("division_shot");

        player = GameObject.Find("player");

        interval = 5.0f;
        power = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (lv != 0){
            time += Time.deltaTime;
            if (time > interval)
            {
                targets = GameObject.FindGameObjectsWithTag("enemy");
                float closeDist = 1000;
        
                foreach (GameObject t in targets)
                {
                    float tDist = Vector3.Distance(player.transform.position, t.transform.position);
                    if(closeDist > tDist)
                    {
                        closeDist = tDist;
                        closeEnemy = t;
                    }
                }

                Vector3 dt = closeEnemy.transform.position - player.transform.position;
                float rad = Mathf.Atan2 (dt.y, dt.x);
                float degree = rad * Mathf.Rad2Deg - 90;

                GameObject shot = Instantiate(division_shot_prefab);
                shot.transform.position = player.transform.position;
                shot.transform.Rotate(0, 0, degree);

                time = 0;
            }
        }
    }
}
