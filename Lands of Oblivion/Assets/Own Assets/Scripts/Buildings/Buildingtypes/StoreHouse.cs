﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreHouse : Building {

	public static int size = 10;

	public override void finishBuilding(){
		GlobalStore.instance.addSize(size);
	}
}
