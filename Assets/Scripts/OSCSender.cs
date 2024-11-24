using UnityEngine;
using OscJack;

public class OscJackClient : MonoBehaviour
{
    [SerializeField] string ipAddress = "127.0.0.1";
    [SerializeField] int port = 8080;
    OscClient client;

    private Animator animator;

    void OnEnable()
    {
        client = new OscClient(ipAddress, port);
        animator = GetComponent<Animator>();
    }

    void OnDisable()
    {
        client.Dispose();
    }

    void Update()
    {
        // Animator����{�[�������擾���đ��M
        SendBoneData("Hips");
        SendBoneData("Spine");
        SendBoneData("Chest");
        SendBoneData("Neck");
        SendBoneData("Head");
        // �K�v�ȃ{�[����ǉ�
    }

    void SendBoneData(string boneName)
    {
        Transform bone = animator.transform.Find(boneName);
        if (bone != null)
        {
            var position = bone.position;
            var rotation = bone.rotation.eulerAngles;

            // �ʒu�𑗐M
            client.Send($"/OscJack/bone/{boneName}/position", position.x, position.y, position.z);
            // ��]�𑗐M
            client.Send($"/OscJack/bone/{boneName}/rotation", rotation.x, rotation.y, rotation.z);
        }
    }
}