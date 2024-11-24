using UnityEngine;

public class SkeletonDataLogger : MonoBehaviour
{
    public OVRSkeleton ovrSkeleton; // OVRSkeletonコンポーネント

    void Start()
    {
        if (ovrSkeleton == null)
        {
            ovrSkeleton = GetComponent<OVRSkeleton>();
            if (ovrSkeleton == null)
            {
                Debug.LogError("OVRSkeletonが見つかりません。");
                return;
            }
        }
    }

    void Update()
    {
        LogBoneData();
    }

    void LogBoneData()
    {
        // ボーンの情報を取得
        foreach (var bone in ovrSkeleton.Bones)
        {
            if (bone != null)
            {
                Vector3 position = bone.Transform.position;
                Vector3 rotation = bone.Transform.rotation.eulerAngles;

                // ボーンの位置と回転を表示
                Debug.Log($"Bone: {bone.Id}, Position: {position}, Rotation: {rotation}");
            }
        }
    }
}