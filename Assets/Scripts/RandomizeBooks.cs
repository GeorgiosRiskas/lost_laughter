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
		if (booksPositionsList.Count < 3)
		{
			Debug.LogError("Not enough transforms in the list.");
			return;
		}

		List<Transform> tempTransformsList = new List<Transform>(booksPositionsList);

		// Shuffle the tempTransformsList to randomize the positions
		for (int i = 0; i < tempTransformsList.Count; i++)
		{
			Transform temp = tempTransformsList[i];
			int randomIndex = Random.Range(i, tempTransformsList.Count);
			tempTransformsList[i] = tempTransformsList[randomIndex];
			tempTransformsList[randomIndex] = temp;
		}

		// Assign positions from the shuffled list to jokesList
		for (int i = 0; i < jokesList.Count && i < tempTransformsList.Count; i++)
		{
			jokesList[i].transform.position = tempTransformsList[i].position;
		}
	}
}
