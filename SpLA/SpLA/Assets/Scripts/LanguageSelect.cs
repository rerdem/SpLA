using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

/// <summary>
/// Generates buttons for each file in the StreamingAssets folder.
/// </summary>
public class LanguageSelect : MonoBehaviour {

	public Transform contentPanel;
	public GameObject buttonPrefab;

	private FileInfo[] info;

	void Start () {
		string dirPath = Application.streamingAssetsPath.ToString();
		DirectoryInfo dir = new DirectoryInfo(dirPath);
		info = dir.GetFiles("*.json");
		addButtons();
	}

	/// <summary>
	/// Adds the buttons.
	/// </summary>
	private void addButtons() {
		foreach (FileInfo f in info) {
			//Debug.Log(f.Name);
			GameObject newButton = GameObject.Instantiate(buttonPrefab);
			newButton.transform.SetParent(contentPanel);

			SampleButton sampleButton = newButton.GetComponent<SampleButton>();
			sampleButton.setup(f.Name.ToString());
		}
	}
}
