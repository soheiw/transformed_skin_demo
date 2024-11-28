using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : MonoBehaviour
{
    public GameObject particleObject1;
    public GameObject particleObject2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") //Object�^�O�̕t�����Q�[���I�u�W�F�N�g�ƏՓ˂���������
        {
            Instantiate(particleObject1, this.transform.position, this.transform.rotation); //�p�[�e�B�N���p�Q�[���I�u�W�F�N�g����
            //Destroy(this.gameObject); //�Փ˂����Q�[���I�u�W�F�N�g���폜
            Instantiate(particleObject2, this.transform.position, this.transform.rotation);
        }
    }
}