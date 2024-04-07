using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rune_shadow : MonoBehaviour
{
    //rune_tracerと一緒に消えるようにするための時間。
    float destroy_time = 7.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        //destroy_time後に自信を削除するコルーチン
        StartCoroutine("Destroy_rune_shadow");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Destroy_rune_shadow()
    {
        //destroy_time秒待つ
        yield return new WaitForSeconds(destroy_time);
        //自身を削除
        Destroy(this.gameObject);
    }

}
