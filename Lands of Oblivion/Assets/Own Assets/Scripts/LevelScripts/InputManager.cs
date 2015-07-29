﻿using UnityEngine;
using System;

public class InputManager : MonoBehaviour {

	public static InputManager instance = null;

	public Transform playerTransform = null;

	//All KeyCodes
	public KeyCode cutTree = KeyCode.Mouse0;


	
	void Start () {
		instance = this;
	}
	

	void Update () {
		//CutTree
		if(Input.GetKeyDown(cutTree)){
			CutTreeEvent userEvent = new CutTreeEvent();
			userEvent.execute();
		}
	}
}