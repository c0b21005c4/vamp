using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator_bullet : MonoBehaviour
{
    public GameObject bullet;
    GameObject obj;
    float pretime;
    public float interval = 100000;
    private GameObject weapon_manager;
    All_weapon_manager weapon_script;
    public int Lv = 0;
    int speed = 50;

    // Start is called before the first frame update
    void Start()
    {
        weapon_manager = GameObject.Find("player/All_weapon_manager");
        weapon_script = weapon_manager.GetComponent<All_weapon_manager>();
        Lv = weapon_script.Get_Weapon_Lv("bullet");
    }
    // Update is called once per frame
    void Update()
    {
        if (Lv >= 1)
        {
            if ((Time.time - pretime) >= interval)
            {
                pretime = Time.time;
                make_bullet();
            }
        }
    }
    public void make_bullet()
    {
        //bulletプレハブをobjに取得
        GameObject obj = Instantiate (bullet, weapon_manager.transform.position, Quaternion.identity);
        // クリックした座標の取得（スクリーン座標からワールド座標に変換）
        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2型で作るとtransform.positionがVector3なので計算できない
        Vector3 shotForward = Vector3.Scale((mouse_pos - weapon_manager.transform.position), new Vector3(1, 1, 0)).normalized;
        // 弾に速度を与える
        obj.GetComponent<Rigidbody2D>().velocity = shotForward * speed;

    }
}
