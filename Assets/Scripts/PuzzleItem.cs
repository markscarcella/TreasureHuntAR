using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleItem : MonoBehaviour {

    [Header("Drag the puzzle item image here")]
    public Sprite puzzleItemImage;
    [TextArea(3,3),Header("Write your puzzle item description here")]
    public string puzzleItemDescription;

	// Use this for initialization
	void Start () {
		foreach (Transform child in GetComponentsInChildren<Transform>())
		{
			if (child.GetComponent<Collider>() == null)
			{
				child.gameObject.AddComponent<MeshCollider>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activate()
    {
        GameManager.Instance.AddToInventory(this);
    }
}
