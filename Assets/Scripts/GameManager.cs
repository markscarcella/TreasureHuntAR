using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class GameManager : MonoBehaviour {

    #region SINGLETON PATTERN
    public static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    _instance = container.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }
    #endregion

    [Header("Drag the items you need to collect into this array")]
	public PuzzleItem[] goalItems;
	[Header("Drag the Menu UI here")]
	public Canvas menuUI;
	[Header("Drag the Game UI here")]
	public Canvas gameUI;
	[Header("Drag the Win UI here")]
	public Canvas winUI;
	[Header("Drag the available inventory slots here")]
	public UnityEngine.UI.Image[] inventorySlots;
	[Header("This list keeps track of your inventory")]
	public List<PuzzleItem> inventory;

	[Header("Drag the info text box image here")]
	public UnityEngine.UI.Image infoTextBox;
	[Header("Drag the info text object here")]
	public Text infoText;
	[Header("Drag the info image background here")]
	public UnityEngine.UI.Image infoImage;
	[Header("Drag the info image here")]
	public UnityEngine.UI.Image infoImageItem;
   
    bool infoActive;

	void Start () {
        inventory = new List<PuzzleItem>();
		ShowMenu();
        HideInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (!goalItems.Except(inventory).Any())
        {
            EndGame();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (infoActive)
            {
                HideInfo();
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                SendActivate(ray);
            }        
        }
    }


	public void ShowMenu()
	{
		VuforiaBehaviour.Instance.enabled = false;
		menuUI.enabled = true;
		gameUI.enabled = false;
		winUI.enabled = false;
	}

    public void StartGame()
    {
		VuforiaBehaviour.Instance.enabled = true;
		menuUI.enabled = false;
        gameUI.enabled = true;
        winUI.enabled = false;
    }

    public void EndGame()
    {
		VuforiaBehaviour.Instance.enabled = false;
		menuUI.enabled = false;
        gameUI.enabled = false;
		winUI.enabled = true; 
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SendActivate(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {            
            hit.collider.gameObject.SendMessageUpwards("Activate",SendMessageOptions.DontRequireReceiver);
        }
	}

	public void AddToInventory(PuzzleItem item)
	{
		inventory.Add(item);
		if (inventory.Count < inventorySlots.Length)
		{
			inventorySlots[inventory.Count-1].sprite = item.puzzleItemImage;
		}
        item.gameObject.SetActive(false);
        InspectInventory(inventory.Count-1);
	}

	public void ShowInfo(string text, Sprite image = null)
	{
        if (text != "")
        {
            infoText.text = text;
			infoTextBox.gameObject.SetActive(true);
        }
        if (image != null)
        {
            infoImageItem.sprite = image;
            infoImage.gameObject.SetActive(true);
		}
        infoActive = true;
	}

	public void HideInfo()
	{
        infoActive = false;
        infoTextBox.gameObject.SetActive(false);
        infoImage.gameObject.SetActive(false);
	}

    public void InspectInventory(int slot)
    {
        if (inventory.Count > slot)
        {
            ShowInfo(inventory[slot].puzzleItemDescription, inventory[slot].puzzleItemImage);
        }
    }
}