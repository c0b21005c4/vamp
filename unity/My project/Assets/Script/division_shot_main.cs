using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class division_shot_main : MonoBehaviour
{
    public GameObject sub_shot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            GameObject sub1 = Instantiate(sub_shot);
            sub1.transform.position = this.transform.position;
            sub1.transform.Rotate(0, 0, Random.Range(0f, 360.0f));

            GameObject sub2 = Instantiate(sub_shot);
            sub2.transform.position = this.transform.position;
            sub2.transform.Rotate(0, 0, Random.Range(0f, 360.0f));
        }
    }
}
