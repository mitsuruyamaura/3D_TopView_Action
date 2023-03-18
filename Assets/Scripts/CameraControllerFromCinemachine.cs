using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControllerFromCinemachine : MonoBehaviour
{
    [Header("�ǐՂ���Q�[���I�u�W�F�N�g")]
    public GameObject targetObj;

    [SerializeField]
    private float cameraRotateSpeed = 80.0f;     // �J�����̉�]���x

    [SerializeField]
    private float maxLimit = 45.0f;             // X �������̉��͈�

    [SerializeField]
    private float minLimit = 25.0f;


    void Update() {
        if (Input.GetMouseButton(1)) {
            // �J�����̉�]
            RotateCamera();
        }
    }

    /// <summary>
    /// targetObj �����ɂ����J�����̌��]��]
    /// </summary>
    private void RotateCamera() {

        // �}�E�X�̓��͒l���擾
        float x = Input.GetAxis("Mouse X");
        float z = Input.GetAxis("Mouse Y");

        // �J������Ǐ]�Ώۂ̎��͂����]��]������
        transform.RotateAround(targetObj.transform.position, Vector3.up, x * Time.deltaTime * cameraRotateSpeed);

        //�J�����̉�]���̏����l���Z�b�g
        var localAngle = transform.localEulerAngles;

        // X ���̉�]�����Z�b�g
        localAngle.x += z;

        // X �����ғ��͈͓��Ɏ��܂�悤�ɐ���
        if (localAngle.x > maxLimit) {
            localAngle.x = maxLimit;
        }

        if (localAngle.x < minLimit) {
            localAngle.x = minLimit;
        }

        // �J�����̉�]
        transform.localEulerAngles = localAngle;
    }

    /// <summary>
    /// �ǐՑΏۂ�ݒ�
    /// </summary>
    /// <param name="target"></param>
    public void SetTarget(GameObject target) {
        targetObj = target;

        if (TryGetComponent(out CinemachineVirtualCamera camera)) {
            camera.Follow = targetObj.transform;    
        }
        //GetComponent<CinemachineVirtualCamera>().Follow = targetObj.transform;
    }
}
