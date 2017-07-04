using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour {

	[Header("Enter the code here")]
	public string code;
    [Header("Drag the code wheels here")]
    public CombinationWheel[] wheels;
	[Header("Drag the end-cap objects here")]
	public Transform[] endCaps;

	Puzzle associatedPuzzle;
    bool isOpening;
    bool isOpen;
    Quaternion[] targetRotations;

	// Use this for initialization
	void Start () {
        associatedPuzzle = GetComponent<Puzzle>();

        targetRotations = new Quaternion[2];
		targetRotations[0] = endCaps[0].rotation * Quaternion.AngleAxis(90, transform.forward);
		targetRotations[1] = endCaps[1].rotation * Quaternion.AngleAxis(90, -transform.forward);
	}

    // Update is called once per frame
    void Update () {
	    CheckCode();
        if (isOpening && !isOpen)
        {
			endCaps[0].rotation = Quaternion.Lerp(endCaps[0].rotation, targetRotations[0], 5 * Time.deltaTime);
			endCaps[1].rotation = Quaternion.Lerp(endCaps[1].rotation, targetRotations[1], 5 * Time.deltaTime);
            if (endCaps[0].rotation == targetRotations[0])
            {
                isOpen = true;
                isOpening = false;
            }
        }
	}

    public void CheckCode()
    {
        string currentCode = "";
        foreach (CombinationWheel wheel in wheels)
        {
            currentCode += wheel.currentNumber.ToString();
        }

        if (!associatedPuzzle.isComplete && currentCode == code)
        {
            associatedPuzzle.isComplete = true;
            isOpening = true;
        }
	}
}
