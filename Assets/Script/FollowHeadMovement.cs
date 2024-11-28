using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHeadMovement : MonoBehaviour
{
    public Transform tracker; // �g���b�J�[�I�u�W�F�N�g���w��
    private Vector3 initialRotation; // �����̊p�x

    void Start()
    {
        // �����̊p�x���L�^
        initialRotation = transform.eulerAngles;
    }

    void Update()
    {
        if (tracker != null)
        {
            // �g���b�J�[�̕ω��ʂ��擾
            Vector3 deltaRotation = tracker.rotation.eulerAngles - initialRotation;

            // �����̊p�x�ɕω��ʂ��������p�x���v�Z
            Vector3 modifiedRotation = initialRotation + deltaRotation;

            // �I�u�W�F�N�g�̊p�x��ύX
            transform.eulerAngles = modifiedRotation;

            // �g���b�J�[�̕ω��ʂ����Z�b�g
            tracker.rotation = Quaternion.Euler(initialRotation);
        }
    }
}