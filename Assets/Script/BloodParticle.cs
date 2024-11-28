using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : MonoBehaviour
{
    public GameObject particleObject1;
    public GameObject particleObject2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") //Objectタグの付いたゲームオブジェクトと衝突したか判別
        {
            Instantiate(particleObject1, this.transform.position, this.transform.rotation); //パーティクル用ゲームオブジェクト生成
            //Destroy(this.gameObject); //衝突したゲームオブジェクトを削除
            Instantiate(particleObject2, this.transform.position, this.transform.rotation);
        }
    }
}