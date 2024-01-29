using System.Collections.Generic;
using UnityEngine;

public class RandomizeBooks : MonoBehaviour
{
	[SerializeField] private List<Transform> booksPositionsList = new List<Transform>();
	[SerializeField] private List<Joke> jokesList = new List<Joke>();

	void Start()
	{
		PickRandomTransforms();
	}

	void PickRandomTransforms()
	{
		// Check if the list has at least 3 transforms
		if (booksPositionsList.Count < 3)
		{
			Debug.LogError("Not enough transforms in the list.");
			return;
		}

		List<Transform> tempTransformsList = new List<Transform>(booksPositionsList);

		for (int i = 0; i < 3; i++)
		{
			int randomIndex = Random.Range(0, tempTransformsList.Count);
			Transform selectedTransform = tempTransformsList[randomIndex];
			Debug.Log("Selected Transform: " + selectedTransform.name);

			tempTransformsList.RemoveAt(randomIndex);
		}

		for (int i = 0; i < tempTransformsList.Count; i++)
		{
			jokesList[i].transform.position = tempTransformsList[i].transform.position;
		}
	}
}
