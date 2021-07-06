using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BallShop : MonoBehaviour
{
    public GameObject becomeTopperPanel;

	[SerializeField] public List<BallShopItem> BallShopItemList;
	public GameObject ItemTemplate;
	public GameObject g;
	[SerializeField] public Transform ShopScrollView;
	Button buyButton;
    Button equipButton;
	[SerializeField] Animator noCoinsAnim;
    [SerializeField] int EquippedItemIndex;


    [SerializeField] public List<bool> purchasedBallsList = new List<bool>(10);
    //[SerializeField] public bool[] purchaseBallsList = new bool[10];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < BallShopItemList.Count; i++)
        {
            purchasedBallsList.Add(false);
            
        }
        LoadPurchasedBalls();
        //purchasedBallsList =  LoadPurchasedBalls();
        /*for (int i=0;i<purchasedBallsList.Count;i++)
        {
            purchasedBallsList[i] = false;
        }*/
        /*for (int i = 0; i < purchasedBallsList.Count; i++)
        {
            //purchasedBallsList[i] = false;
            Debug.Log(purchasedBallsList[i]);
        }*/
        if (!PlayerPrefs.HasKey("Ball Equipped"))
        {
            PlayerPrefs.SetInt("Ball Equipped", 0);
            PlayerPrefs.Save();
        }
        EquippedItemIndex = PlayerPrefs.GetInt("Ball Equipped");
        if (EquippedItemIndex == 10 && PlayerPrefs.GetInt("isTop") == 0)
        {
            EquippedItemIndex = 0;
        }
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        int len = BallShopItemList.Count;
        for (int i = 0; i < len; i++)
        {
            Debug.Log(purchasedBallsList[i]);
        }
        BallShopItemList[EquippedItemIndex].isEquipped = true;
        BallShopItemList[EquippedItemIndex].isPurchased = true;
        savePurchasedBalls(EquippedItemIndex);
        LoadPurchasedBalls();
        for (int i=0;i<len;i++) {
        	g = Instantiate(ItemTemplate, ShopScrollView);
        	g.transform.GetChild(0).GetComponent<Image>().sprite = BallShopItemList[i].itemImage;
        	g.transform.GetChild(2).GetComponent<Text>().text = BallShopItemList[i].price.ToString();
        	buyButton = g.transform.GetChild(3).GetComponent<Button>();
            equipButton = g.transform.GetChild(4).GetComponent<Button>();
            equipButton.interactable = !BallShopItemList[i].isEquipped;
            if (BallShopItemList[i].isEquipped)
            {
                buyButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(true);
                equipButton.transform.GetChild(0).GetComponent<Text>().text = "EQUIPPED";
            }
            //buyButton.interactable = !BallShopItemList[i].isPurchased;   
            buyButton.interactable = !(purchasedBallsList[i]);
            if (purchasedBallsList[i])
            {
                changeButtonText(buyButton, i, "PURCHASED");
                equipButton.gameObject.SetActive(true);
            }
            //configureButton(i);
        	buyButton.AddEventListener(i, OnShopItemBtnClicked);
            equipButton.AddEventListener(i, OnEquipButtonClicked);

            if (i == 10)
            {
                buyButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(true);
                /*if (PlayerPrefs.GetInt("isTop") != 1)
                {
                    equipButton.interactable = true;
                }*/
            }

            /*if (BallShopItemList[i].isPurchased)
            {
                purchasedBallsList.Add(BallShopItemList[i]);
            }*/
        }
        Destroy(ItemTemplate);
    }

    // Called when buy button is clicked for a ball
    void OnShopItemBtnClicked(int itemIndex) {
        PurchaseHandler ph = new PurchaseHandler();
    	//Debug.Log("Item Index : "+itemIndex);
    	// Mark the item Purchased 

    	if (ph.hasEnoughCoins(BallShopItemList[itemIndex].price)) {
    		ph.useCoins(BallShopItemList[itemIndex].price);
	    	BallShopItemList[itemIndex].isPurchased = true;
            // Add the Ball to the list
            //purchasedBallsList.Add(BallShopItemList[itemIndex].isPurchased);
            purchasedBallsList[itemIndex] = BallShopItemList[itemIndex].isPurchased;
            // Disable the button
            buyButton = ShopScrollView.GetChild(itemIndex).GetChild(3).GetComponent<Button>();
            buyButton.interactable = false;
            // Set the text as purchased
            buyButton.transform.GetChild(0).GetComponent<Text>().text = "PURCHASED";
            buyButton.gameObject.SetActive(false);
            savePurchasedBalls(itemIndex);
            //configureButton(itemIndex);
            OnEquipButtonClicked(itemIndex);


        }
	    else {
	    	//noCoinsAnim.SetTrigger("noCoins");
	    	Debug.Log("Not Enough Coins");
	    }
        
    }

    // Called when equip button is clicked for purchased balls
    void OnEquipButtonClicked(int itemIndex)
    {

        if (itemIndex==10 && PlayerPrefs.GetInt("isTop") == 0)
        {
            becomeTopperPanel.SetActive(true);
            return;  
        }
        //PlayerPrefs.SetInt("Ball Material Equipped", itemIndex);
        Button clickedBtn;
        Debug.Log("Inside on equip button clikced");
        //buyButton.gameObject.SetActive(false);
        //TODO : Complete function for when equip button is clicked
        for (int i = 0; i < BallShopItemList.Count; i++)
        {
            if(i == itemIndex)
            {

                BallShopItemList[itemIndex].isEquipped = true;
                clickedBtn = ShopScrollView.GetChild(i).GetChild(3).GetComponent<Button>();
                clickedBtn.gameObject.SetActive(false);
                equipButton = ShopScrollView.GetChild(i).GetChild(4).GetComponent<Button>();
                equipButton.interactable = false;
                equipButton.gameObject.SetActive(true);
                equipButton.transform.GetChild(0).GetComponent<Text>().text = "Equipped";
                EquippedItemIndex = itemIndex;
                PlayerPrefs.SetInt("Ball Equipped", EquippedItemIndex);
                PlayerPrefs.Save();
            }
            else
            {
                BallShopItemList[itemIndex].isEquipped = false;
                equipButton = ShopScrollView.GetChild(i).GetChild(4).GetComponent<Button>();
                equipButton.interactable = true;
                //equipButton.gameObject.SetActive(true);
                equipButton.transform.GetChild(0).GetComponent<Text>().text = "Equip";
            }
            Debug.Log(i +" Is Equipped : " + BallShopItemList[i].isEquipped);
        }
        

    }


    // To change the text of button if the item is purchased
    void changeButtonText(Button btn, int index, string status)
    {
        btn.transform.GetChild(0).GetComponent<Text>().text = status; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void savePurchasedBalls(int index)
    {
        //List<bool> tempPurchasedBallsList = new List<bool>(BallShopItemList.Count);
        List<bool> tempPurchasedBallsList = purchasedBallsList;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/purchasedBallsList.items");
        /*for (int i = 0; i < BallShopItemList.Count; i++)
        {
            tempPurchasedBallsList[i] = BallShopItemList[i].isPurchased;
        }*/
        tempPurchasedBallsList[index] = true; 
        bf.Serialize(file, tempPurchasedBallsList);
        file.Close();
        Debug.Log("Purchased Balls List Saved");
    }

    void LoadPurchasedBalls()
    {
        List<bool> tempPurchasedBallsList = new List<bool>();
        if (File.Exists(Application.persistentDataPath + "/purchasedBallsList.items"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/purchasedBallsList.items", FileMode.Open);
            tempPurchasedBallsList = (List<bool>)bf.Deserialize(file);
            for (int i = 0; i < BallShopItemList.Count; i++)
            {
                purchasedBallsList[i] = tempPurchasedBallsList[i];
            }
            //purchasedBallsList = tempPurchasedBallsList;
            file.Close();
            Debug.Log("Purchased Balls List Loaded");
            //return true;
        }
        else
        {
            Debug.Log("No data saved related to purchased Balls List.");
            /*for(int i=0;i<BallShopItemList.Count;i++)
            {
                purchasedBallsList[i] = false;
            }*/
            //return false;
        }
    }

    public void onClickBecomeTopperOkButton()
    {
        becomeTopperPanel.SetActive(false);
    }
}

[System.Serializable] 
public class BallShopItem {
	public Sprite itemImage;
	public int price;
	public bool isPurchased;
    public bool isEquipped;
}
