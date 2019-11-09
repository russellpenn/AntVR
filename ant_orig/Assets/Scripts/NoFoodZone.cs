using UnityEngine;
using System.Collections;

public class NoFoodZone : MonoBehaviour {

    public GameObject foodPrefab;
    public GameObject player;
	private Transform newObjectTransform;
	private Transform InitialPositionFood;
	private bool firstCookie;

    private Transform toUseTransform;
    public Transform cupcakePickedPosition;

    void Start(){
		firstCookie = false;
	}

	public void InstantiateObject ()
	{
		Vector3 position = (player.transform.forward * 15);
		//position.y = -20;

		var firstCupcake = Instantiate (foodPrefab, newObjectTransform.position + position , newObjectTransform.rotation);

        InitialPositionFood = newObjectTransform;


        var cupcakePicked = Random.Range(1, 3);
        for (int i = 1; i < 4; i++)
        {
            var cupcakeClone = Instantiate(foodPrefab, new Vector3(newObjectTransform.position.x+(i * 70.0F), newObjectTransform.position.y+i * 0.5F, newObjectTransform.position.z+i * 60.0F), newObjectTransform.rotation);

            var cupcakeClones_2 = Instantiate(foodPrefab, new Vector3(newObjectTransform.position.x - (i * 70.0F), newObjectTransform.position.y + i * 0.5F, newObjectTransform.position.z - i * 60.0F), newObjectTransform.rotation);


            if (i == cupcakePicked)
            {
                cupcakePickedPosition = cupcakeClone.transform;
            }
        }



        


    }

    public Transform getCupcakePosition()
    {
        return cupcakePickedPosition;
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
