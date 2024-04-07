using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextController : MonoBehaviour
{    

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 300, 0));
    }

    // Update is called once per frame
    void Update()
    {
        //コルーチンを使用するための関数StartCoroutine
        StartCoroutine(DestroyObject());
    }

    /*IEnumeratorはコルーチンを作る際に使うおまじない
    コルーチンは途中で処理を中断できるまとまり
    指定された秒数後にオブジェクトを削除する関数*/
    private IEnumerator DestroyObject()
    {
        //コルーチンだとWaitForSecondsが使える
        yield return new WaitForSeconds(0.6f);
        Destroy(this.gameObject);
    }
}