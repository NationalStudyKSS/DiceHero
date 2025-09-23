using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ī�޶� ��ǥ�� ���� �����̵��� �ϴ� Ŭ����
/// </summary>
public class CameraMover : MonoBehaviour
{
    [SerializeField] Transform _target; // ī�޶� ���� ��ǥ
    [SerializeField] Vector3 _offset; // ī�޶��� ������ ��ġ
    [SerializeField] Vector3 _targetOffset; // ��ǥ�� ������ ��ġ

    private void Update()
    {
        if (_target != null)
        {
            // ī�޶��� ��ġ�� ��ǥ�� ��ġ�� ����
            transform.position = _target.position + _offset - _targetOffset;
        }
    }
}
