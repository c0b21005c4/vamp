using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class division_shot_damage : MonoBehaviour
{
    int power;
    // Start is called before the first frame update
    void Start()
    {
        GameObject division_shot_generater = GameObject.Find("Generator_division_shot");
        division_shot_generater script = division_shot_generater.GetComponent<division_shot_generater>();

        power = script.power;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
		//重なっているオブジェクトが敵だったら
        if(collision.gameObject.tag == "enemy")
        {
		    enemy enemyscript;
            enemyscript = collision.GetComponent<enemy>();
            enemyscript.Damage(power, "division_shot");
            enemyscript.invincible_dic["division_shot"] = true;
        }
	}

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            enemy enemyscript;
            enemyscript = collision.GetComponent<enemy>();
            enemyscript.invincible_dic["division_shot"] = false;
        }
    }
}
