﻿using UnityEngine;
using System;
using System.Collections;

public class BuildBuildingEvent : UserEvent {

	private static GameObject building;
    public static float START_HEIGHT = -500;

    public override void execute(){
		if(SetBuildingPositionController.instance.building == null)
		{
			//Get the position where the player is looking at the terrain
			RaycastHit hit = RayCastManager.startRayCast(50);
			
			
			//Buildings can only be build on terrains
			try{
				if(hit.transform.gameObject.tag == "Terrain"){		
					building = createSelectedBuilding();
					building.transform.position = new Vector3(hit.point.x, START_HEIGHT, hit.point.z);


					enableColliders(false);
					SetBuildingPositionController.instance.building = building;
					SetBuildingPositionController.instance.buildingScript = building.GetComponent<Building>();
				}
			} 
			catch(Exception e){

			}
		} else
		{
			SetBuildingPositionController.instance.building = null;
			SetBuildingPositionController.instance.buildingScript = null;

			if(building != null & building.transform.position.y != START_HEIGHT)
            {
				enableColliders(true);
				//adjustTerrain(building);
				building.GetComponent<Building>().build();
			}
		}
	}


	//Adjust the terrain under the building
	private void adjustTerrain(GameObject building){
		Terrain terrain = Terrain.activeTerrain;

		//Calculate length and width of the mesh in Terrain Coordinates
		Vector3 size = building.GetComponentInChildren<MeshRenderer>().bounds.size;
		size = Math.translateVector3ToTerrainCoordinate(size, Terrain.activeTerrain);
		float width = size.x;
		float length = size.z;

		//Translate the position in a Terrainposition
		Vector3 pos = Math.translateVector3ToTerrainCoordinate(building.transform.position, Terrain.activeTerrain);

		//Modify the heightmap
		float height = terrain.terrainData.GetHeight((int)(pos.x-width/2), (int)(pos.z-length/2));
		float[,] heightmap = terrain.terrainData.GetHeights((int)pos.x, (int)pos.z, (int)width, (int)length);
		for(int x=0; x<heightmap.GetLength(0); x++){
			for(int z=0; z<heightmap.GetLength(1); z++){
				heightmap[x, z] = height/terrain.terrainData.size.y;
			}
		}
        Debug.Log("X:" + (int)(pos.x - width / 2) + "  Y:" + (int)(pos.z - length / 2));
		terrain.terrainData.SetHeights((int)(pos.x-width/2), (int)(pos.z-length/2), heightmap);
	}


	//Enable/Disable the colliders of the building
	private void enableColliders(bool b){
		Collider[] colliders = building.GetComponentsInChildren<Collider>();
		foreach(Collider collider in colliders){
			collider.enabled = b;
		}
	}


	public GameObject createSelectedBuilding(){
        return Instantiate(BuildingManager.instance.selectedBuilding.gameObject);
	}
}