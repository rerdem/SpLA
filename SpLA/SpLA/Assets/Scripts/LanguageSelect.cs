using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LanguageSelect : MonoBehaviour {

	private FileInfo[] info;
	public Transform contentPanel;
	public GameObject buttonPrefab;

	// Use this for initialization
	void Start () {
		string dirPath = Application.streamingAssetsPath.ToString();
		DirectoryInfo dir = new DirectoryInfo(dirPath);
		info = dir.GetFiles("*.json");
		addButtons();
	}
	
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
