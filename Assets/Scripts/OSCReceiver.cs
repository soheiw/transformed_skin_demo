using UnityEngine;
using OscJack;

public class OscJackServer : MonoBehaviour
{
    [SerializeField] int port = 8000;
    OscServer server;

    private Animator animator;

    void OnEnable()
    {
        server = new OscServer(port);
        animator = GetComponent<Animator>();

        server.MessageDispatcher.AddCallback(
            "/OscJack/bone/position",
            (string address, OscDataHandle data) => {
                var position = new Vector3(
                    data.GetElementAsFloat(0),
                    data.GetElementAsFloat(1),
                    data.GetElementAsFloat(2)
                );

                // ボーン名を取得
                string boneName = address.Split('/')[3];
                Transform bone = animator.transform.Find(boneName);
                if (bone != null)
                {
                    bone.position = position;
                }
            }
        );

        server.MessageDispatcher.AddCallback(
            "/OscJack/bone/rotation",
            (string address, OscDataHandle data) => {
                var rotation = new Vector3(
                    data.GetElementAsFloat(0),
                    data.GetElementAsFloat(1),
                    data.GetElementAsFloat(2)
                );

                // ボーン名を取得
                string boneName = address.Split('/')[3];
                Transform bone = animator.transform.Find(boneName);
                if (bone != null)
                {
                    bone.rotation = Quaternion.Euler(rotation);
                }
            }
        );
    }

    void OnDisable()
    {
        server.Dispose();
        server = null;
    }
}