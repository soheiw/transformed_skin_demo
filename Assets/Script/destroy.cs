using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    public GameObject objectA;
    //public GameObject target;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "B") //Objectタグの付いたゲームオブジェクトと衝突したか判別
        {
            objectA.SetActive(false);
            Debug.Log("Object set to inactive");
        }
    }
}
