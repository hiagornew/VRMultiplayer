using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevel : MonoBehaviour
{
	[SerializeField]
	private GameObject[] pointBoxes;

	[SerializeField]
	private GameObject boxPrefab;

	[SerializeField]
	private float minscaleX;
	[SerializeField]
	private float MaxscaleX;
	[SerializeField]
	private float minscaleY;
	[SerializeField]
	private float MaxscaleY;

	private List<int> positionsUsed;
	private List<GameObject> objects;

	[SerializeField]
	private int totalObstacles;

	private bool randomstart;

	private void Start()
	{
		positionsUsed = new List<int>();
		objects = new List<GameObject>();
		
	}

	public void SpawAndRandomScale()
	{
			
			GameObject aux;
			int rand = Random.RandomRange(0, pointBoxes.Length);
			for (int j = 0; j < positionsUsed.Count; j++)
			{
				if (positionsUsed[j] == rand)
				{
					return;
				}
			}
			aux = Instantiate(boxPrefab, pointBoxes[rand].transform.position, Quaternion.identity);

			float randScaleX = Random.RandomRange(minscaleX, MaxscaleX);
			float randScaleY = Random.RandomRange(minscaleY, MaxscaleY);
			aux.transform.localScale = new Vector3(randScaleX, randScaleY, 1);
			positionsUsed.Add(rand);
			objects.Add(aux);
			aux.transform.parent = this.transform;

		
	}

	IEnumerator DelatStartRandom()
	{
		yield return new WaitForSeconds(0.3f);
		if (objects.Count > 0)
		{
			for (int i = 0; i < objects.Count; i++)
			{
				Destroy(objects[i].gameObject);
				
			}
			positionsUsed = new List<int>();
			objects.Clear();
		}
		while (positionsUsed.Count < totalObstacles)
			SpawAndRandomScale();
	}

	public void ResetRandom()
	{

		StartCoroutine(DelatStartRandom());
	}
}
