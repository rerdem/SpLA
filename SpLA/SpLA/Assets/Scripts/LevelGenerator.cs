using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	[Header("Ground Tiles")]
	public GameObject grassSingle;
	public GameObject grassLeft;
	public GameObject grassMid;
	public GameObject grassRight;
	public GameObject stoneSingle;
	public GameObject stoneLeft;
	public GameObject stoneMid;
	public GameObject stoneRight;
	public GameObject bridge;
	public GameObject spikes;
	public GameObject start;
	public GameObject goal;

	[Header("Generation Parameters")]
	public int minPlatformSize = 1;
	public int maxPlatformSize = 10;
	public int maxHazardSize = 3;
	public int maxHeight = 3;
	public int maxDrop = -2;

	public int platforms = 100;

	[Range (0.0f, 1.0f)]
	public float hazardChance = 0.5f;
	[Range (0.0f, 1.0f)]
	public float bridgeChance = 0.1f;

	private int blockNum = 1;
	private int blockHeight = 0;
	private bool isHazard = false;

	// Use this for initialization
	void Start () {
		//start tile
		Instantiate(start, new Vector2 (0, 0), Quaternion.identity);

		//generate level
		for (int i = 1; i < platforms; i++) {
			if ((!isHazard) && (Random.value < hazardChance)) {
				int hazardSize = Mathf.RoundToInt(Random.Range(1, maxHazardSize));
				blockNum += hazardSize;
				isHazard = true;
			}
			else {
				isHazard = false;

				//platform generation
				int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));
				blockHeight = blockHeight + Mathf.RoundToInt(Random.Range(maxDrop, maxHeight));

				//goal tile
				if (i == (platforms - 1)) {
					Instantiate(goal, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);
					break;
				}

				if (platformSize > 1) {
					for (int j = 0; j < platformSize; j++) {
						if (j == 0) {
							Instantiate(grassLeft, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);
						}
						else if (j == (platformSize - 1)) {
							Instantiate(grassRight, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);
						}
						else {
							Instantiate(grassMid, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);
						}
						blockNum++;
					}
				}
				else {
					Instantiate(grassSingle, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);
					blockNum++;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
