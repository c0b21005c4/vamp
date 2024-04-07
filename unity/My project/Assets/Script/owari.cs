using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class owari : MonoBehaviour
{
    public GameObject hajime;
    Camera MainCamera; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Time.timeScale = 0;
        Destroy(this.gameObject);
        GameObject go = Instantiate(hajime);
        MainCamera = Camera.main;
        go.transform.parent = MainCamera.transform;
        go.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
}
