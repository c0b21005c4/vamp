using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treasure_box_windouw_ok : MonoBehaviour
{
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
        GameObject window_obj = GameObject.Find("treasure_window(Clone)");
        treasure_box_window window_script = window_obj.GetComponent<treasure_box_window>();

        window_script.selected();
    }
}
