using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetRotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] bool reverseRotation = false;

    [Header("Pulse Settings")]
    [SerializeField] bool pulsate = false;
    [SerializeField] float maxScale = 2f;
    [SerializeField] float minScale = 0.2f;
    [SerializeField][Range(0f, 1f)] float pulsationSpeed = 1f;
    bool growing = false;

    void Update() {
        Rotate();
        Pulsate();
    }

    private void Pulsate() {
        if (!pulsate) return;
        Vector3 _currentScale = transform.localScale;
        if (_currentScale.x >= maxScale) growing = false;
        if (_currentScale.x <= minScale) growing = true;
        transform.localScale = growing ? transform.localScale += _currentScale * pulsationSpeed * Time.deltaTime : transform.localScale -= _currentScale * pulsationSpeed * Time.deltaTime;
        
    }

    private void Rotate() {
        Vector3 _newRot = new Vector3(transform.rotation.x, transform.rotation.y, rotationSpeed * Time.deltaTime);
        _newRot = reverseRotation ? -_newRot : _newRot;
        transform.Rotate(_newRot, Space.Self);
    }
}
