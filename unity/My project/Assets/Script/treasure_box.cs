using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treasure_box : MonoBehaviour
{
    public GameObject window;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = GameObject.Find("player");
            GameObject go = Instantiate(window);
            go.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -1.0f);

            Destroy(this.gameObject);
        }
    }
}
