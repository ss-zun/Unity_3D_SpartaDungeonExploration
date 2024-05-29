using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller { get; private set; }
    public PlayerCondition condition { get; private set; }

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}