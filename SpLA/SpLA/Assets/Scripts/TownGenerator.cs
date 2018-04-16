using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates a town depending on the number of exercises to be completed.
/// </summary>
public class TownGenerator : MonoBehaviour {

	[Header("Chunks")]
	public GameObject[] chunks;
	public GameObject[] npcs;
	public GameObject wall;
	public GameObject goalGround;
	public GameObject goal;

	private int currentPosition = 0;
	private int goalPosition = 0;
	private GameObject[] npcObjects;

	/// <summary>
	/// Setup the town depending on the specified NPC count.
	/// </summary>
	/// <param name="npccount">Number of NPCs.</param>
	public void setup(int npccount) {
		npcObjects = new GameObject[npccount];

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

			npcObjects[npccount - i] = Instantiate(npcs[Random.Range(0, npcs.Length)], new Vector2((currentPosition + 5) * 0.7f, 1), Quaternion.identity) as GameObject;
			if (i != 1) {
				npcObjects[npccount - i + 1] = Instantiate(npcs[Random.Range(0, npcs.Length)], new Vector2((currentPosition + 16) * 0.7f, 1), Quaternion.identity) as GameObject;
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

	/// <summary>
	/// Places the exit.
	/// </summary>
	public void placeExit() {
		Instantiate(goal, new Vector2 (goalPosition * 0.7f, 0), Quaternion.identity);
	}

	/// <summary>
	/// Gets all NPCs in town.
	/// </summary>
	/// <returns>An array of all spawned NPCs.</returns>
	public GameObject[] getNpcs() {
		return npcObjects;
	}
}
