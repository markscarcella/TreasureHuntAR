using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationWheel : MonoBehaviour {

    [Header("Current number on wheel")]
	public int currentNumber;
	[Header("Rotation axle point for wheel")]
	public Transform axle;

	bool isSpinning;
	Quaternion targetRotation;

	// Use this for initialization
	void Start () {
		targetRotation = axle.localRotation * Quaternion.AngleAxis(-90, axle.right);
	}
	
	// Update is called once per frame
	void Update () {

        if (isSpinning)
        {
			axle.localRotation = Quaternion.Lerp(axle.localRotation, targetRotation, 5 * Time.deltaTime);
            if (axle.localRotation == targetRotation)
            {
                currentNumber = (currentNumber + 1) % 4;
                targetRotation = axle.localRotation * Quaternion.AngleAxis(-90, axle.right);
                isSpinning = false;
            }
		}
	}

    public void Activate()
    {
        isSpinning = true;
    }
}
