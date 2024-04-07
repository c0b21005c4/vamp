using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class division_shot_move : MonoBehaviour
{
    float speed;

    float time=0f;
    float limit;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10.0f;
        limit = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time+=Time.deltaTime;
        this.transform.Translate(new Vector3(0, speed*Time.deltaTime, 0));
        if (time > limit)
        {
            Destroy(this.gameObject);
        }
    }
}
