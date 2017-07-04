using UnityEngine;

public class Chest : MonoBehaviour
{
	[Header("Drag the hinge object here")]
	public Transform hinge;
	[Header("Is the chest opening?")]
	public bool isOpening;
	[Header("Is the chest open?")]
	public bool isOpen;

	Puzzle associatedPuzzle;
	Quaternion targetRotation;

    // Use this for initialization
    void Start()
    {
        associatedPuzzle = GetComponent<Puzzle>();
        targetRotation = hinge.rotation * Quaternion.AngleAxis(-90, transform.right);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpening && !isOpen)
        {
            Debug.Log("Opening!");
            hinge.rotation = Quaternion.Lerp(hinge.rotation, targetRotation, 5 * Time.deltaTime);
            if (hinge.rotation == targetRotation)
            {
                isOpen = true;
                isOpening = false;
            }
        }
    }

    public void Activate()
    {
        if (associatedPuzzle.isComplete)
        {
            isOpening = true;
        }
    }
}
