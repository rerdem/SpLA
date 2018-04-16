using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates a platforming level.
/// </summary>
public class LevelGenerator : MonoBehaviour {

	[Header("Tiles")]
	public GameObject grassSingle;
	public GameObject grassLeft;
	public GameObject grassMid;
	public GameObject grassRight;
	public GameObject grassCenter;
	public GameObject stoneSingle;
	public GameObject stoneLeft;
	public GameObject stoneMid;
	public GameObject stoneRight;
	public GameObject stoneCenter;
	public GameObject bridge;
	public GameObject spikes;
	public GameObject start;
	public GameObject startCenter;
	public GameObject goal;
	public GameObject goalCenter;
	public GameObject[] decorations;
	public GameObject[] clouds;
	public GameObject bee;
	public GameObject slime;
	public GameObject snake;

	[Header("Generation Parameters")]
	public int minPlatformSize = 2;
	public int maxPlatformSize = 10;
	public int minHazardSize = 2;
	public int maxHazardSize = 8;
	public int maxHeight = 3;
	public int maxDrop = -2;
	public int groundFillDepth = 10;

	public int platforms = 100;

	[Range (0.0f, 1.0f)]
	public float hazardChance = 0.5f;
	[Range (0.0f, 1.0f)]
	public float decorationChance = 0.5f;
	[Range (0.0f, 1.0f)]
	public float cloudChance = 0.5f;
	[Range (0.0f, 1.0f)]
	public float tileChange = 0.7f;
	[Range (0.0f, 1.0f)]
	public float enemyDensity = 0.1f;

	private int blockNum = 1;
	private int blockHeight = 0;
	private bool isHazard = false;

	void Start () {
		//build left wall
		for (int i = 1; i <= 16; i++) {
			for (int j = -8; j <= 12; j++) {
				Instantiate(grassCenter, new Vector2 ((0 - i) * 0.7f, (2 + j) * 0.7f), Quaternion.identity);
			}
		}

		//start tile
		Instantiate(start, new Vector2 (0, 2), Quaternion.identity);
		for (int k = 1; k < groundFillDepth; k++) {
			Instantiate(startCenter, new Vector2 (0, (2 - (0.7f * k))), Quaternion.identity);
		}

		//generate platforms
		for (int i = 1; i < platforms; i++) {
			//hazard or platform
			if ((!isHazard) && (Random.value < hazardChance)) {
				int hazardSize = Mathf.RoundToInt(Random.Range(minHazardSize, maxHazardSize));

				for (int j = 0; j < hazardSize; j++) {
					Instantiate(spikes, new Vector2 (((blockNum * 0.7f) + 0.4f), (blockHeight - (3 * 0.7f))), Quaternion.identity);
					blockNum++;
				}

				//place cloud
				if (Random.value < cloudChance) {
					Instantiate(clouds[Mathf.RoundToInt(Random.Range(0,clouds.Length))], new Vector2 ((blockNum * 0.7f) + Random.Range(-0.5f, 0.5f), blockHeight + Random.Range(2.0f, 2.5f)), Quaternion.identity);
				}

				isHazard = true;
			}
			else {
				//platform generation
				int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));
				int oldHeight = blockHeight;
				blockHeight = blockHeight + Mathf.RoundToInt(Random.Range(maxDrop, maxHeight));

				if ((oldHeight == blockHeight) && (!isHazard)) {
					Instantiate(bridge, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);
					blockNum++;
				}

				if (platformSize > 1) {
					for (int j = 0; j < platformSize; j++) {
						if (i < (Mathf.RoundToInt(tileChange * platforms))) {
							//place tile
							if (j == 0) {
								Instantiate(grassLeft, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);
							}
							else if (j == (platformSize - 1)) {
								Instantiate(grassRight, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);
							}
							else {
								Instantiate(grassMid, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);
							}

							//place decoration
							if (Random.value < decorationChance) {
								Instantiate(decorations[Mathf.RoundToInt(Random.Range(0,decorations.Length))], new Vector2 (blockNum * 0.7f, blockHeight + 0.7f), Quaternion.identity);
							}

							//place enemies on platforms size 3+
							if (platformSize > 2) {
								if (Random.value < enemyDensity) {
									switch (Mathf.RoundToInt(Random.Range(0, 3))) {
										case 0:
											Instantiate(bee, new Vector2(blockNum * 0.7f, blockHeight + 2f), Quaternion.identity);
											break;
										case 1:
											Instantiate(slime, new Vector2(blockNum * 0.7f, blockHeight + 0.7f), Quaternion.identity);
											break;
										default:
											if ((j != 0) || (j != (platformSize - 1))) {
												Instantiate(snake, new Vector2(blockNum * 0.7f, blockHeight + 1f), Quaternion.identity);
											}
											break;
									}
								}
							}

							//fill below
							for (int k = 1; k < groundFillDepth; k++) {
								Instantiate(grassCenter, new Vector2 (blockNum * 0.7f, (blockHeight - (0.7f * k))), Quaternion.identity);
							}

							blockNum++;
						}
						else {
							//place tile
							if (j == 0) {
								Instantiate(stoneLeft, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);
							}
							else if (j == (platformSize - 1)) {
								Instantiate(stoneRight, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);
							}
							else {
								Instantiate(stoneMid, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);
							}

							//place decoration
							if (Random.value < decorationChance) {
								Instantiate(decorations[Mathf.RoundToInt(Random.Range(0,decorations.Length))], new Vector2 (blockNum * 0.7f, blockHeight + 0.7f), Quaternion.identity);
							}

							//place enemies on platforms size 3+
							if (platformSize > 2) {
								if (Random.value < enemyDensity) {
									switch (Mathf.RoundToInt(Random.Range(0, 3))) {
										case 0:
											Instantiate(bee, new Vector2(blockNum * 0.7f, blockHeight + 2f), Quaternion.identity);
											break;
										case 1:
											Instantiate(slime, new Vector2(blockNum * 0.7f, blockHeight + 0.7f), Quaternion.identity);
											break;
										default:
											if ((j != 0) || (j != (platformSize - 1))) {
												Instantiate(snake, new Vector2(blockNum * 0.7f, blockHeight + 1f), Quaternion.identity);
											}
											break;
									}
								}
							}

							//fill below
							for (int k = 1; k < groundFillDepth; k++) {
								Instantiate(stoneCenter, new Vector2 (blockNum * 0.7f, (blockHeight - (0.7f * k))), Quaternion.identity);
							}

							blockNum++;
						}
					}
				}
				else {
					if (i < (Mathf.RoundToInt(tileChange * platforms))) {
						Instantiate(grassSingle, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);

						for (int k = 1; k < groundFillDepth; k++) {
							Instantiate(grassCenter, new Vector2 (blockNum * 0.7f, (blockHeight - (0.7f * k))), Quaternion.identity);
						}
					}
					else {
						Instantiate(stoneSingle, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);

						for (int k = 1; k < groundFillDepth; k++) {
							Instantiate(stoneCenter, new Vector2 (blockNum * 0.7f, (blockHeight - (0.7f * k))), Quaternion.identity);
						}
					}
					blockNum++;
				}

				//place cloud
				if (Random.value < cloudChance) {
					Instantiate(clouds[Mathf.RoundToInt(Random.Range(0,clouds.Length))], new Vector2 ((blockNum * 0.7f) + Random.Range(-0.5f, 0.5f), blockHeight + Random.Range(2.0f, 2.5f)), Quaternion.identity);
				}

				isHazard = false;
			}
		}

		//goal tile
		Instantiate(goal, new Vector2 (blockNum * 0.7f, blockHeight), Quaternion.identity);
		for (int k = 1; k < groundFillDepth; k++) {
			Instantiate(goalCenter, new Vector2 (blockNum * 0.7f, (blockHeight - (0.7f * k))), Quaternion.identity);
		}
		blockNum += 2;

		//build right wall
		for (int i = 1; i <= 16; i++) {
			for (int j = -8; j <= 15; j++) {
				Instantiate(stoneCenter, new Vector2 ((blockNum + i) * 0.7f, (blockHeight + j) * 0.7f), Quaternion.identity);
			}
		}
	}
}
