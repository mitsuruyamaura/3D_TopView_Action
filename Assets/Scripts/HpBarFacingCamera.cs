using UnityEngine;
using DG.Tweening;

public class HpBarFacingCamera : MonoBehaviour
{
    void LateUpdate() {
        // HpBar�pCanvas�̌�������ɃJ�����̕����Ɍ�����
        LookHpBarToCamera();
    }

    /// <summary>
    /// HpBar�pCanvas�̌�������ɃJ�����̕����Ɍ�����
    /// </summary>
    private void LookHpBarToCamera() {
        // �I�u�W�F�N�g�̉�]���J�����̉�]�Ɗ��S�Ɉ�v������
        transform.rotation = Camera.main.transform.rotation;

        // DOTween���g�p���A�I�u�W�F�N�g���J�����̈ʒu�Ɍ�����
        // �ŏI���ʂ̓J�����̕������������A��]�̊��S��v�ł͂Ȃ��A�J�����̈ʒu�Ɍ�����(���΂���)�`�ɂȂ�
        // ���̂��߁A�I�u�W�F�N�g�̉�]���J�����̉�]�ƈ�v����킯�ł͂Ȃ����A�I�u�W�F�N�g�̑O�ʂ��J�����Ɍ����悤�ɂȂ�
        transform.DOLookAt(Camera.main.transform.position, 0);

        // �J�����̕���������(DOTween �ł͂Ȃ��ꍇ)
        // Camera.main.transform.rotation * Vector3.forward �̓J�����̑O�����x�N�g�����v�Z
        // Camera.main.transform.rotation * Vector3.up �̓J�����̏�����x�N�g�����v�Z
        // �I�u�W�F�N�g�̓J�����ɐ��΂��A�J�����̕����Ɍ������ADOTween �Ɠ����ŃI�u�W�F�N�g�̉�]���J�����̉�]�Ɗ��S�Ɉ�v����킯�ł͂Ȃ�
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }
}