using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    int power = 3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Destroy_bullet");
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
            enemyscript.Damage(power, "bullet");
            enemyscript.invincible_dic["bullet"] = true;
        }
	}

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            enemy enemyscript;
            enemyscript = collision.GetComponent<enemy>();
            enemyscript.invincible_dic["bullet"] = false;
        }
    }

    IEnumerator Destroy_bullet()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }
}
