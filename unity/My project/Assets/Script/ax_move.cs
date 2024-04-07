using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ax_move : MonoBehaviour
{
    Rigidbody2D rigid2d;
    private float x;
    private float y;

    private float x_vec;
    private float y_vec;

    private float speed;

    private float time = 0f;
    private float del_ax;

    private int power;

    private float rotate;

    // Start is called before the first frame update
    void Start()
    {
        GameObject ax_generater = GameObject.Find("ax_generater");
        ax_generater script = ax_generater.GetComponent<ax_generater>();

        this.del_ax = 3.0f;

        this.speed = 20.0f;
        this.x_vec = 500.0f;
        this.y_vec = 500.0f;
        this.rigid2d = GetComponent<Rigidbody2D>();

        this.x = x_vec * Mathf.Sin(Time.time * speed);
        this.y = Mathf.Abs(y_vec * Mathf.Cos(Time.time * speed));

        this.rigid2d.AddForce(transform.right * x);
        this.rigid2d.AddForce(transform.up * y);

        this.power = script.ax_power;

        this.rotate = -1000.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > del_ax)
        {
            Destroy(this.gameObject);
        }

        this.transform.Rotate(new Vector3(0, 0, rotate*Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
		//重なっているオブジェクトが敵だったら
        if(collision.gameObject.tag == "enemy")
        {
		    enemy enemyscript;
            enemyscript = collision.GetComponent<enemy>();
            enemyscript.Damage(power, "ax");
            enemyscript.invincible_dic["ax"] = true;
        }
	}

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            enemy enemyscript;
            enemyscript = collision.GetComponent<enemy>();
            enemyscript.invincible_dic["ax"] = false;
        }
    }
}
