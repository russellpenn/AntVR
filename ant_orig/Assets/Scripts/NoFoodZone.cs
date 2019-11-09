using UnityEngine;
using System.Collections;

public class NoFoodZone : MonoBehaviour {

    public GameObject foodPrefab;
    public GameObject player;
	private Transform newObjectTransform;
	private Transform InitialPositionFood;
	private bool firstCookie;

	void Start(){
		firstCookie = false;
	}

	public void InstantiateObject ()
	{
		Vector3 position = (player.transform.forward * 15);
		//position.y = -20;

		Instantiate (foodPrefab, newObjectTransform.position + position , newObjectTransform.rotation);
		InitialPositionFood = newObjectTransform;
	}

	public Transform getInitialPositionFood(){
		Debug.Log ("Initial Position Food: " + InitialPositionFood.position);
		return InitialPositionFood;
	}

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Object exit trigger");
		if (!firstCookie) {
			firstCookie = true;
			newObjectTransform = player.transform;
			InstantiateObject ();
		}
    }

}
