using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class ObstacleShop : MonoBehaviour
{
    public GameObject topOncePanel;

	[SerializeField] public List<ObstacleShopItem> ObstacleShopItemList;
    public GameObject ItemTemplate;
	public GameObject g;
	[SerializeField] public Transform ShopScrollView;
	Button buyButton;
    Button equipButton;
	[SerializeField] Animator noCoinsAnim;
    [SerializeField] int EquippedItemIndex;

    [SerializeField] public List<bool> purchasedObstaclesList = new List<bool>(10);
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <ObstacleShopItemList.Count; i++)
        {
            purchasedObstaclesList.Add(false);
        }
        LoadPurchasedObstacles();

        // Check for equipped obstacles
        if (!PlayerPrefs.HasKey("Obstacle Equipped"))
        {
            PlayerPrefs.SetInt("Obstacle Equipped", 8);
            PlayerPrefs.Save();
        }
        EquippedItemIndex = PlayerPrefs.GetInt("Obstacle Equipped");

        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        int len = ObstacleShopItemList.Count;
        for (int i = 0; i < len; i++)
        {
            Debug.Log(purchasedObstaclesList[i]);
        }
        ObstacleShopItemList[EquippedItemIndex].isEquipped = true;
        ObstacleShopItemList[EquippedItemIndex].isPurchased = true;
        savePurchasedObstacles(EquippedItemIndex);
        LoadPurchasedObstacles();
        for (int i=0;i<len;i++) {
        	g = Instantiate(ItemTemplate, ShopScrollView);
        	g.transform.GetChild(0).GetComponent<Image>().sprite = ObstacleShopItemList[i].itemImage;
        	g.transform.GetChild(2).GetComponent<Text>().text = ObstacleShopItemList[i].price.ToString();
        	buyButton = g.transform.GetChild(3).GetComponent<Button>();
            equipButton = g.transform.GetChild(4).GetComponent<Button>();
            equipButton.interactable = !ObstacleShopItemList[i].isEquipped;
            if (ObstacleShopItemList[i].isEquipped)
            {
                buyButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(true);
                equipButton.transform.GetChild(0).GetComponent<Text>().text = "EQUIPPED";
            }
            //buyButton.interactable = !ObstacleShopItemList[i].isPurchased;
            buyButton.interactable = !(purchasedObstaclesList[i]);
            if (purchasedObstaclesList[i])
            {
                changeButtonText(buyButton, i, "PURCHASED");
                equipButton.gameObject.SetActive(true);
            }
        	buyButton.AddEventListener(i, OnShopItemBtnClicked);
            equipButton.AddEventListener(i, OnEquipButtonClicked);
            if (i == 10)
            {
                buyButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(true);
            }
        }
        Destroy(ItemTemplate);
    }

    void OnShopItemBtnClicked(int itemIndex) {
        PurchaseHandler ph = new PurchaseHandler();

        // Mark the item purchased

        if (ph.hasEnoughCoins(ObstacleShopItemList[itemIndex].price))
        {
            ph.useCoins(ObstacleShopItemList[itemIndex].price);
            ObstacleShopItemList[itemIndex].isPurchased = true;
            // Add the obstacle to the purchased list
            //purchasedObstaclesList.Add(ObstacleShopItemList[itemIndex].isPurchased);
            purchasedObstaclesList[itemIndex] = ObstacleShopItemList[itemIndex].isPurchased;
            // Disable the button
            buyButton = ShopScrollView.GetChild(itemIndex).GetChild(3).GetComponent<Button>();
            buyButton.interactable = false;
            // Set the text as Purchased;
            buyButton.transform.GetChild(0).GetComponent<Text>().text = "PURCHASED";
            buyButton.gameObject.SetActive(false);
            savePurchasedObstacles(itemIndex);
            OnEquipButtonClicked(itemIndex);
        }
        else
        {
            //noCoinsAnim.SetTrigger("noCoins");
            Debug.Log("Not Enough Coins");
        }
        


        /*if (PurchaseHandler.instance.hasEnoughCoins(ObstacleShopItemList[itemIndex].price)) {
    		PurchaseHandler.instance.useCoins(ObstacleShopItemList[itemIndex].price);
	    	//Debug.Log("Item Index : "+itemIndex);
	    	// Mark the item Purchased 
	    	ObstacleShopItemList[itemIndex].isPurchased = true;
	    	// Disable the button
	    	buyButton = ShopScrollView.GetChild(itemIndex).GetChild(3).GetComponent<Button>();
	    	buyButton.interactable = false;
	    	// Set the text as purchased
	    	buyButton.transform.GetChild(0).GetComponent<Text>().text = "PURCHASED";
	    }
	    else {
	    	noCoinsAnim.SetTrigger("noCoins");
	    	Debug.Log("You don't have Enough Coins!!");
	    }*/
    }

    void OnEquipButtonClicked(int itemIndex)
    {
        if (itemIndex==10 && PlayerPrefs.GetInt("Already topped") == 0)
        {
            topOncePanel.SetActive(true);
            return;
        }
        Button clickedBtn;
        Debug.Log("Inside on Equip button clicked ");
        for (int i = 0; i < ObstacleShopItemList.Count; i++)
        {
            if (i==itemIndex)
            {
                ObstacleShopItemList[itemIndex].isEquipped = true;
                clickedBtn = ShopScrollView.GetChild(i).GetChild(3).GetComponent<Button>();
                clickedBtn.gameObject.SetActive(false);
                equipButton = ShopScrollView.GetChild(i).GetChild(4).GetComponent<Button>();
                equipButton.interactable = false;
                equipButton.gameObject.SetActive(true);
                equipButton.transform.GetChild(0).GetComponent<Text>().text = "Equipped";
                EquippedItemIndex = itemIndex;
                PlayerPrefs.SetInt("Obstacle Equipped", EquippedItemIndex);
                PlayerPrefs.Save();
            }
            else
            {
                ObstacleShopItemList[itemIndex].isEquipped = false;
                equipButton = ShopScrollView.GetChild(i).GetChild(4).GetComponent<Button>();
                equipButton.interactable = true;
                equipButton.transform.GetChild(0).GetComponent<Text>().text = "Equip";
            }
            Debug.Log(i + " is Equipped : " + ObstacleShopItemList[i].isEquipped);
        }
    }

    void changeButtonText(Button btn, int index, string status)
    {
        btn.transform.GetChild(0).GetComponent<Text>().text = status;
    }

    void savePurchasedObstacles(int index)
    {
        List<bool> tempPurchasedObstaclesList = purchasedObstaclesList;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/purchasedObstaclesList.items");
        tempPurchasedObstaclesList[index] = true;
        bf.Serialize(file, tempPurchasedObstaclesList);
        file.Close();
        Debug.Log("Purchased Obstacles List Saved");
    }


    void LoadPurchasedObstacles()
    {
        List<bool> tempPurchasedObstaclesList = new List<bool>();
        if (File.Exists(Application.persistentDataPath + "/purchasedObstaclesList.items"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/purchasedObstaclesList.items", FileMode.Open);
            tempPurchasedObstaclesList = (List<bool>)bf.Deserialize(file);
            for (int i = 0; i < ObstacleShopItemList.Count; i++)
            {
                purchasedObstaclesList[i] = tempPurchasedObstaclesList[i];
            }
            file.Close();
            Debug.Log("Purchased Obstacles List loaded");
        }
        else
        {
            Debug.Log("No data saved related to purchased obstacles list");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickTopOnceOkButton()
    {
        topOncePanel.SetActive(false);
    }
}

[System.Serializable] 
public class ObstacleShopItem {
	public Sprite itemImage;
	public int price;
	public bool isPurchased;
    public bool isEquipped;
}
