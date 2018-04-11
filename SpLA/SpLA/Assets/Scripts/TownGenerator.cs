using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownGenerator : MonoBehaviour {

	[Header("Chunks")]
	public GameObject[] chunks;
	public GameObject[] npcs;
	public GameObject wall;
	public GameObject goalGround;
	public GameObject goal;

	//[Header("NPCs")]
	//public int npcs = 3;

	private int currentPosition = 0;
	private int goalPosition = 0;

	public void setup(int npccount) {
		//place left wall
		Instantiate(wall, new Vector2 (currentPosition * 0.7f, 0), Quaternion.identity);
		currentPosition += 9;

		//place and populate chunks
		int lastChunk = 0;
		int nextChunk = 0;
		for (int i = npccount; i > 0; i -= 2) {
			//choose Chunk, but never the same as the last
			if (i == npccount) {
				nextChunk = Random.Range(0, chunks.Length);
				lastChunk = nextChunk;
			}
			else {
				while (nextChunk == lastChunk) {
					nextChunk = Random.Range(0, chunks.Length);
				}
				lastChunk = nextChunk;
			}

			Instantiate(chunks[nextChunk], new Vector2 (currentPosition * 0.7f, 0), Quaternion.identity);

			Instantiate(npcs[Random.Range(0, npcs.Length)], new Vector2((currentPosition + 5) * 0.7f, 1), Quaternion.identity);
			if (i != 1) {
				Instantiate(npcs[Random.Range(0, npcs.Length)], new Vector2((currentPosition + 16) * 0.7f, 1), Quaternion.identity);
			}
			currentPosition += 23;
		}

		//place goal ground
		Instantiate(goalGround, new Vector2 (currentPosition * 0.7f, 0), Quaternion.identity);
		goalPosition = currentPosition;
		currentPosition += 3;

		//place right wall
		Instantiate(wall, new Vector2 (currentPosition * 0.7f, 0), Quaternion.identity);
	}
	
	public void placeExit() {
		Instantiate(goal, new Vector2 (goalPosition * 0.7f, 0), Quaternion.identity);
		//trigger on screen message?
	}

	// Update is called once per frame
	void Update () {
	}
}
