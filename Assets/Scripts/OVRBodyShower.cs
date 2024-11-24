using UnityEngine;

public class SkeletonDataLogger : MonoBehaviour
{
    public OVRSkeleton ovrSkeleton; // OVRSkeleton�R���|�[�l���g

    void Start()
    {
        if (ovrSkeleton == null)
        {
            ovrSkeleton = GetComponent<OVRSkeleton>();
            if (ovrSkeleton == null)
            {
                Debug.LogError("OVRSkeleton��������܂���B");
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
        // �{�[���̏����擾
        foreach (var bone in ovrSkeleton.Bones)
        {
            if (bone != null)
            {
                Vector3 position = bone.Transform.position;
                Vector3 rotation = bone.Transform.rotation.eulerAngles;

                // �{�[���̈ʒu�Ɖ�]��\��
                Debug.Log($"Bone: {bone.Id}, Position: {position}, Rotation: {rotation}");
            }
        }
    }
}