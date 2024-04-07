using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RETRY_button : MonoBehaviour
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
        //現在のシーン名を取得してloadすることで初期化できる
        SceneManager.LoadScene (SceneManager.GetActiveScene().name);
    }
}
