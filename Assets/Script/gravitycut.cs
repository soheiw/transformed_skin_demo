using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravitycut : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g���I�u�W�F�N�gB�ł��邩���m�F
        if (collision.gameObject.CompareTag("ono"))
        {
            // �I�u�W�F�N�gB��Rigidbody���A�^�b�`����Ă��邩���m�F
            Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                // useGravity��L���ɂ���
                targetRigidbody.useGravity = true;

                targetRigidbody.constraints = RigidbodyConstraints.None;
            }
        }
    }
}