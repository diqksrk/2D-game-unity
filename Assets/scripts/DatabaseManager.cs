using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour {

    // 데이터베이스 필요 이유 : 1. 씬 이동. A(이벤트 true false 재생성이므로 다시 실행됨) <-> B 이벤트 다시 발동? 
    // 데이터베이스 false라면 참조하면 실행 안시켜도 됨.
    // 2. 세이브와 로드.
    // 3. 미리 만들어두면 편하다 ! . 아이템 호출 등.

    static public DatabaseManager instance;

    private PlayerStat thePlayerStat;

    public GameObject prefabs_Floating_Text;
    public GameObject parent;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    public string[] var_name;
    public float[] var;

    public string[] switches_name;
    public bool[] switches;

    public List<Item> itemList = new List<Item>();

    private void FloatingText(int number, string color)
    {
        Vector3 vector = thePlayerStat.transform.position;
        vector.y += 60;

        GameObject clone = Instantiate(prefabs_Floating_Text, vector, Quaternion.Euler(Vector3.zero));
        clone.transform.position = vector;
        clone.GetComponent<FloatingText>().text.text = number.ToString();
        if (color=="GREEN")
            clone.GetComponent<FloatingText>().text.color = Color.green;
        else if (color == "BLUE")
            clone.GetComponent<FloatingText>().text.color = Color.blue;
        clone.GetComponent<FloatingText>().text.fontSize = 25;
        clone.transform.SetParent(parent.transform);
    }
  

    // Use this for initialization
    void Start () {
        thePlayerStat = FindObjectOfType<PlayerStat>();
        itemList.Add(new Item(10001, "빨간 포션", "체력을 50 채워주는 마법의 물약", Item.ItemType.Use));
        itemList.Add(new Item(10002, "파란 포션", "마나를 15 채워주는 기적의 물약", Item.ItemType.Use));
        itemList.Add(new Item(10003, "농축 빨간 포션", "체력을 350 채워주는 기적의 농축물약", Item.ItemType.Use));
        itemList.Add(new Item(10004, "농축 파란 포션", "마나를 80 채워주는 기적의 농축 물약", Item.ItemType.Use));
        itemList.Add(new Item(11001, "랜덤 상자", "랜덤으로 포션이 나온다. 낮은 확률로 꽝", Item.ItemType.Use));
        itemList.Add(new Item(20001, "짧은 검", "기본적인 용사의 검", Item.ItemType.Equip,3));
        itemList.Add(new Item(20002, "무딘 도끼", "기본적인 용사의 도끼", Item.ItemType.Equip, 4));
        itemList.Add(new Item(20301, "사파이어 반지", "1초에 hp 1을 회복시켜주는 마법 반지", Item.ItemType.Equip,0,0,1));
        itemList.Add(new Item(30001, "고대 유물의 조각 1", "반으로 쪼개진 고대 유물의 파편", Item.ItemType.Quest));
        itemList.Add(new Item(30002, "고대 유물의 조각 2", "반으로 쪼개진 고대 유물의 파편", Item.ItemType.Quest));
        itemList.Add(new Item(30003, "고대 유물", "고대 유적에 잠들어있던 고대의 유물", Item.ItemType.Quest));
    }

    public void UseItem(int _itemID)
    {
        switch (_itemID)
        {
            case 10001:
                if (thePlayerStat.hp >= thePlayerStat.currentHP + 50)
                    thePlayerStat.currentHP += 50;
                else
                    thePlayerStat.currentHP = thePlayerStat.hp;
                FloatingText(50, "GREEN");
                Debug.Log("hp가 50 회복");
                break;
            case 10002:
                if (thePlayerStat.mp >= thePlayerStat.currentMP + 15)
                    thePlayerStat.currentMP += 15;
                else
                    thePlayerStat.currentMP = thePlayerStat.mp;
                FloatingText(50, "BLUE");
                Debug.Log("mp가 15 회복");
                break;

        }
    }
}
