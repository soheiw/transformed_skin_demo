using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    public GameObject objectA;
    //public GameObject target;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "B") //Object�^�O�̕t�����Q�[���I�u�W�F�N�g�ƏՓ˂���������
        {
            objectA.SetActive(false);
            Debug.Log("Object set to inactive");
        }
    }
}
