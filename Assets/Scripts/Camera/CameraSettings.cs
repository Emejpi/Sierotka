using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour {
    [Header("Max Rotation")]
    public int maxRotationAngleUp = 70;
    public int maxRotationAngleDown = 50;
    [Header("Speed")]
    public float rotationSpeed = 8;
    [Tooltip("damingTime to czas przez ktory kamera spowolania po dotarciu do odpowiedniej pozycji. Takie wytlumienie ruchu zeby wygladalo plynniej")]
    public float damingTime = 0.3f;
    public float zoomingSpeed = 2;
    public float focusingOnPlayerSpeed = 1000;
    [Header("Position")]
    public float distanceFromCharacter = 16;
    [Tooltip("X to lewo prawo, Y gora dol")]
    public Vector2 position = new Vector2(0,0);
    [Header("Position")]
    public bool slowCharacterSwitch;

}
