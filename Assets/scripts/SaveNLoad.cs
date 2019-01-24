using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveNLoad : MonoBehaviour {

    [System.Serializable] //serializable for computer
    //serialzing is possible when data is 자료형.
    public class Data //data class haing saved player data
    {
        public float playerX;
        public float playerY;
        public float playerZ; //because vector3 is not possible for serializing.

        public int playerLv;
        public int playerHP;
        public int playerMP;

        public int playerCurrentHP;
        public int playerCurrentMP;
        public int playerCurrentEXP;

        public int playerHPR;
        public int playerMPR;

        public int playerATK;
        public int playerDEF;
        public int added_atk;
        public int added_def;
        public int added_hpr;
        public int added_mpr;

        public List<int> playerItemInventory;
        public List<int> playerItemInventoryCount;
        public List<int> playerEquipItem;


        public string mapName;
        public string sceneName;

        public List<bool> swList;
        public List<string> swNameList;
        public List<string> varNmaeList;
        public List<float> varNumberList;
    }

    private PlayerManager thePlayer;
    private PlayerStat thePlayerStat;
    private DatabaseManager theDatabase;
    private Inventory theInven;
    private Equipment theEquip;
    private FadeManager theFade;

    public Data data;

    private Vector3 vector;

    public void CallSave()
    {
        theDatabase = FindObjectOfType<DatabaseManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        thePlayerStat = FindObjectOfType<PlayerStat>();
        theEquip = FindObjectOfType<Equipment>();
        theInven = FindObjectOfType<Inventory>();

        //몬스터는 오크,트롤등이 있으니 무빙오브젝트로 한다.
        data.playerX = thePlayer.transform.position.x;
        data.playerY = thePlayer.transform.position.y;
        data.playerZ = thePlayer.transform.position.z;

        data.playerLv = thePlayerStat.character_Lv;
        data.playerHP = thePlayerStat.hp;
        data.playerMP  = thePlayerStat.mp;
        data.playerCurrentHP = thePlayerStat.currentHP;
        data.playerCurrentMP = thePlayerStat.mp;
        data.playerATK = thePlayerStat.atk;
        data.playerDEF = thePlayerStat.def;
        data.playerHPR = thePlayerStat.recover_hp;
        data.playerMPR = thePlayerStat.recover_mp;
        data.added_atk = theEquip.added_atk;
        data.added_def = theEquip.added_def;
        data.playerHPR = theEquip.added_hpr;
        data.playerMPR = theEquip.added_mpr;

        data.mapName = thePlayer.currentMapName;
        data.sceneName = thePlayer.currentSceneName;

        Debug.Log("기초 데이터 성공");

        data.playerItemInventory.Clear();
        data.playerItemInventoryCount.Clear();
        data.playerEquipItem.Clear();

        for (int i=0; i<theDatabase.var_name.Length; i++)
        {
            data.varNmaeList.Add(theDatabase.var_name[i]);
            data.varNumberList.Add(theDatabase.var[i]);
        }
        for (int i = 0; i < theDatabase.switches_name.Length; i++)
        {
            data.swNameList.Add(theDatabase.switches_name[i]);
            data.swList.Add(theDatabase.switches[i]);
        }
        List<Item> itemLIst = theInven.SaveItem();

        for (int i=0; i<itemLIst.Count; i++)
        {
            Debug.Log("인벤토리 아이템 저장 완료 : " +itemLIst[i].itemID);
            data.playerItemInventory.Add(itemLIst[i].itemID);
            data.playerItemInventoryCount.Add(itemLIst[i].itemCount);
        }

        for (int i=0; i<theEquip.equipItemList.Length; i++)
        {
            data.playerEquipItem.Add(theEquip.equipItemList[i].itemID);
            Debug.Log("장착된 아이템 저장 완료 : " + theEquip.equipItemList[i].itemID);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath+"/SaveFile.dat");
        bf.Serialize(file,data);
        file.Close();

        Debug.Log(Application.dataPath + "의 위치에 저장했습니다.");

    }

    public void CallLoad()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/SaveFile.dat",FileMode.Open);
        
        if (file !=null && file.Length > 0)
        {
            data = (Data)bf.Deserialize(file);

            theDatabase = FindObjectOfType<DatabaseManager>();
            thePlayer = FindObjectOfType<PlayerManager>();
            thePlayerStat = FindObjectOfType<PlayerStat>();
            theEquip = FindObjectOfType<Equipment>();
            theInven = FindObjectOfType<Inventory>();

            theFade = FindObjectOfType<FadeManager>();

            theFade.FadeOut();


            thePlayer.currentMapName = data.mapName;
            thePlayer.currentSceneName = data.sceneName;

            vector.Set(data.playerX, data.playerY, data.playerZ);
            thePlayer.transform.position = vector;

            thePlayerStat.character_Lv = data.playerLv;
            thePlayerStat.hp = data.playerHP;
            thePlayerStat.mp = data.playerMP;
            thePlayerStat.currentHP = data.playerHP;
            thePlayerStat.currentMP = data.playerMP;
            thePlayerStat.currentEXP = data.playerCurrentEXP;
            thePlayerStat.atk = data.playerATK;
            thePlayerStat.def = data.playerDEF;
            thePlayerStat.recover_hp = data.playerHPR;
            thePlayerStat.recover_mp = data.playerMPR;

            theEquip.added_atk = data.added_atk;
            theEquip.added_def = data.added_def;
            theEquip.added_hpr = data.added_hpr; ;
            theEquip.added_mpr = data.added_mpr;

            theDatabase.var = data.varNumberList.ToArray();
            theDatabase.var_name = data.varNmaeList.ToArray();
            theDatabase.switches = data.swList.ToArray();
            theDatabase.switches_name = data.swNameList.ToArray();

            for (int i=0; i<theEquip.equipItemList.Length; i++)
            {
                for (int x=0; x<theDatabase.itemList.Count; x++)
                {
                    if (data.playerEquipItem[i] == theDatabase.itemList[x].itemID)
                    {
                        theEquip.equipItemList[i] = theDatabase.itemList[x];
                        Debug.Log("장착된 아이템을 로드했습니다 : "+theEquip.equipItemList[i].itemID);
                        break;
                    }
                }
            }

            List<Item> itemList = new List<Item>();
            for (int i = 0; i < data.playerItemInventory.Count; i++)
            {
                for (int x = 0; x < theDatabase.itemList.Count; x++)
                {
                    if (data.playerItemInventory[i] == theDatabase.itemList[x].itemID)
                    {
                        itemList.Add(theDatabase.itemList[x]);
                        theEquip.equipItemList[i] = theDatabase.itemList[x];
                        Debug.Log("인벤토리 아이템을 로드했습니다 : "+ theDatabase.itemList[x].itemID);
                        break;
                    }
                }
            }

            for (int i=0; i<data.playerItemInventoryCount.Count; i++)
            {
                itemList[i].itemCount = data.playerItemInventoryCount[i];
            }

            theInven.LoadItem(itemList);
            theEquip.ShowTxT();

            StartCoroutine(WaitCoroutine());


        }
        else
        {
            Debug.Log("저장된 세이브 파일이 없습니다");
        }

        file.Close();
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager theGM = FindObjectOfType<GameManager>();
        theGM.LoadStart();

        SceneManager.LoadScene(data.sceneName);
    }
}
