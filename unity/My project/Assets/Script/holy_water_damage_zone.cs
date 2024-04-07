using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holy_water_damage_zone : MonoBehaviour
{
    private float time = 0f;
    private float interval;
    private float damage_time = 0f;
    private float damage_interval;
    private int size;

    int power;
    // Start is called before the first frame update
    void Start()
    {
        GameObject holy_water_generator = GameObject.Find("player/holy_water_generator");
        holy_water_generator script = holy_water_generator.GetComponent<holy_water_generator>();

        interval = script.zone_interval;
        power = script.zone_power;
        size = script.zone_size;

        damage_interval = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2 (size,size);

        time += Time.deltaTime;
        if(time > interval)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        damage_time += Time.deltaTime;
		//重なっているオブジェクトが敵だったら
        if(collision.gameObject.tag == "enemy")
        {
		    enemy enemyscript;
            enemyscript = collision.GetComponent<enemy>();
            enemyscript.Damage(power, "holy_water");
            enemyscript.invincible_dic["holy_water"] = true;
            if (damage_time > damage_interval)
            {
                enemyscript.invincible_dic["holy_water"] = false;
                damage_time = 0f;
            }
        }
	}

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            enemy enemyscript;
            enemyscript = collision.GetComponent<enemy>();
            enemyscript.invincible_dic["holy_water"] = false;
        }
    }
}
