using UnityEngine;
using System.Collections;

public class TreeControler : MonoBehaviour, Interactable
{
	public int lives = 3;
	public int fallSpeed = 8;
	public Rigidbody rigidBody;


	public void Start() {
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.isKinematic = true;	//The tree is an static object
	}

    public void interact()
    {
        removeLife();
    }


	//Catch the tree and remove one life
	public void removeLife() {
		lives--;

		if(lives == 0){
			rigidBody.isKinematic = false;
			rigidBody.AddForce(transform.forward * fallSpeed);

			StartCoroutine(destroyTree());
		}
	}


	public IEnumerator destroyTree() {
		yield  return new WaitForSeconds(7);

		Destroy(gameObject);
		GlobalStore.instance.addRessources(Ressources.Wood, 1);
	}
}

