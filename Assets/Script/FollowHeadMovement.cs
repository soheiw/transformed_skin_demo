using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHeadMovement : MonoBehaviour
{
    public Transform tracker; // トラッカーオブジェクトを指定
    private Vector3 initialRotation; // 初期の角度

    void Start()
    {
        // 初期の角度を記録
        initialRotation = transform.eulerAngles;
    }

    void Update()
    {
        if (tracker != null)
        {
            // トラッカーの変化量を取得
            Vector3 deltaRotation = tracker.rotation.eulerAngles - initialRotation;

            // 初期の角度に変化量を加えた角度を計算
            Vector3 modifiedRotation = initialRotation + deltaRotation;

            // オブジェクトの角度を変更
            transform.eulerAngles = modifiedRotation;

            // トラッカーの変化量をリセット
            tracker.rotation = Quaternion.Euler(initialRotation);
        }
    }
}