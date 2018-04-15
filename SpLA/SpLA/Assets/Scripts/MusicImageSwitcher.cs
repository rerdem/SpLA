using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicImageSwitcher : MonoBehaviour {

	public Sprite onSprite;
	public Sprite offSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.gm.playMusic) {
			gameObject.GetComponent<Image>().sprite = onSprite;
		}
		else {
			gameObject.GetComponent<Image>().sprite = offSprite;
		}
	}
}
