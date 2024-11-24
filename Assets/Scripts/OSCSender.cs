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
        // Animatorからボーン情報を取得して送信
        SendBoneData("Hips");
        SendBoneData("Spine");
        SendBoneData("Chest");
        SendBoneData("Neck");
        SendBoneData("Head");
        // 必要なボーンを追加
    }

    void SendBoneData(string boneName)
    {
        Transform bone = animator.transform.Find(boneName);
        if (bone != null)
        {
            var position = bone.position;
            var rotation = bone.rotation.eulerAngles;

            // 位置を送信
            client.Send($"/OscJack/bone/{boneName}/position", position.x, position.y, position.z);
            // 回転を送信
            client.Send($"/OscJack/bone/{boneName}/rotation", rotation.x, rotation.y, rotation.z);
        }
    }
}