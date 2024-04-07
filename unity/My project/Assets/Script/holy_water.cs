using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holy_water : MonoBehaviour
{
    private Vector2 target;

    public GameObject DamageZone;

    float speed;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        GameObject holy_water_generator = GameObject.Find("player/holy_water_generator");
        holy_water_generator script = holy_water_generator.GetComponent<holy_water_generator>();

        speed = script.water_speed;
        angle = script.water_angle;
    }

    public void Create(Vector2 place)
    {
        target = place;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.localEulerAngles += new Vector3(0, 0, angle*Time.deltaTime);
        if (new Vector2(transform.position.x, transform.position.y) == target)
        {
            Destroy(this.gameObject);
            GameObject damage_zone = Instantiate(DamageZone);
            damage_zone.transform.position = this.transform.position;
        }
    }
}
