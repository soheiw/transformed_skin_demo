using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravitycut : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトがオブジェクトBであるかを確認
        if (collision.gameObject.CompareTag("ono"))
        {
            // オブジェクトBにRigidbodyがアタッチされているかを確認
            Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (targetRigidbody != null)
            {
                // useGravityを有効にする
                targetRigidbody.useGravity = true;

                targetRigidbody.constraints = RigidbodyConstraints.None;
            }
        }
    }
}