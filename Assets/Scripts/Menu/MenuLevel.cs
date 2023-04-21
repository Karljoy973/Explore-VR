﻿using UnityEngine;

public class MenuLevel : InteractableObject {
    
    [SerializeField] public Vector3 TeleportPosition;
    [SerializeField] public Vector3 TeleportRotation;
    
    public override void OnInteract() {
        Menu.instance.SelectedLevel = this;
    }
    
}