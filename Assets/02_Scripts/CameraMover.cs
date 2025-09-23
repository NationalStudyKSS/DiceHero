using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라가 목표를 따라서 움직이도록 하는 클래스
/// </summary>
public class CameraMover : MonoBehaviour
{
    [SerializeField] Transform _target; // 카메라가 따라갈 목표
    [SerializeField] Vector3 _offset; // 카메라의 오프셋 위치
    [SerializeField] Vector3 _targetOffset; // 목표의 오프셋 위치

    private void Update()
    {
        if (_target != null)
        {
            // 카메라의 위치를 목표의 위치로 설정
            transform.position = _target.position + _offset - _targetOffset;
        }
    }
}
