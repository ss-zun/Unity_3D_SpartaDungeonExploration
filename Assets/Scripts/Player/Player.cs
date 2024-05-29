using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public PlayerController controller;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
    }
}