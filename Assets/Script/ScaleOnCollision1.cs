using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ScaleOnCollision1: MonoBehaviour
{
    public Transform targetObject;    // ����̃I�u�W�F�N�g
    public GameObject destroyObject;
    public float scalePercentage = 0.3f;  // �����l����̏k������
    public float scaleSpeed = 5f;     // �X�P�[���ύX�̑��x
    public float minPercentage = 0.1f;  // �ŏ��k������

    private Vector3 originalScale;    // ���̃X�P�[��
    private bool isColliding;         // �Փ˒����ǂ����̃t���O

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

        // �I�u�W�F�N�g������
        if (collision.gameObject.tag == "B") //Object�^�O�̕t�����Q�[���I�u�W�F�N�g�ƏՓ˂���������
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
            // �Փ˒��Ȃ�X�P�[����ύX
            float targetScalePercentage = Mathf.Max(scalePercentage, minPercentage);
            float targetScaleY = originalScale.y * targetScalePercentage;
            float targetScaleX = originalScale.x * 1.4f;
            float targetScaleZ = originalScale.z * 1.4f;

            Vector3 targetScale = new Vector3(targetScaleX, targetScaleY, targetScaleZ);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);


        }
        else
        {
            // �Փ˂��Ă��Ȃ��Ȃ猳�̃X�P�[���ɖ߂�
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, scaleSpeed * Time.deltaTime * 2);
        }
    }
}