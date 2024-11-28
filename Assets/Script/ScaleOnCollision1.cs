using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ScaleOnCollision1: MonoBehaviour
{
    public Transform targetObject;    // 特定のオブジェクト
    public GameObject destroyObject;
    public float scalePercentage = 0.3f;  // 初期値からの縮小割合
    public float scaleSpeed = 5f;     // スケール変更の速度
    public float minPercentage = 0.1f;  // 最小縮小割合

    private Vector3 originalScale;    // 元のスケール
    private bool isColliding;         // 衝突中かどうかのフラグ

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == targetObject)
        {
            isColliding = true;
        }

        // オブジェクトを消す
        if (collision.gameObject.tag == "B") //Objectタグの付いたゲームオブジェクトと衝突したか判別
        {
            destroyObject.SetActive(false);
            Debug.Log("Object set to inactive");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform == targetObject)
        {
            isColliding = false;
        }
    }

    private void Update()
    {
        if (isColliding)
        {
            // 衝突中ならスケールを変更
            float targetScalePercentage = Mathf.Max(scalePercentage, minPercentage);
            float targetScaleY = originalScale.y * targetScalePercentage;
            float targetScaleX = originalScale.x * 1.4f;
            float targetScaleZ = originalScale.z * 1.4f;

            Vector3 targetScale = new Vector3(targetScaleX, targetScaleY, targetScaleZ);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);


        }
        else
        {
            // 衝突していないなら元のスケールに戻す
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, scaleSpeed * Time.deltaTime * 2);
        }
    }
}