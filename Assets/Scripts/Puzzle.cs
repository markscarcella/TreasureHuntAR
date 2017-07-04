using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    [TextArea(3, 10),Header("Write your puzzle hint here")]
    public string instructions;
    [Header("Drag the puzzle items needed to solve the puzzle here")]
	public PuzzleItem[] puzzleItems;
    [Header("Is the puzzle complete?")]
	public bool isComplete;

    private void Awake()
    {
		foreach (Transform child in GetComponentsInChildren<Transform>())
		{
			if (child.GetComponent<MeshRenderer>() != null && child.GetComponent<Collider>() == null)
			{
				child.gameObject.AddComponent<MeshCollider>();
			}
		}
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckItems();
    }
    
    public void CheckItems()
    {
        if (puzzleItems.Length == 0)
        {
            return;
        }            
        foreach (PuzzleItem puzzleItem in puzzleItems)
        {
            if (!GameManager.Instance.inventory.Any(item => item.gameObject == puzzleItem.gameObject))
            {
                return;
            }
        }
        isComplete = true;
    }

    void Activate()
    {
        if (!isComplete)
        {
            GameManager.Instance.ShowInfo(instructions);
		}
    }
}
