using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DGameSystem : MonoBehaviour
{
    public int START_GOLD = 50;
    // Singleton Design Pattern
    public static DGameSystem instance;

    public static int VERTICAL_NUMBER = 5;
    public static int HORIZONTAL_NUMBER = 14;
    public static float PADDING = 2.5f;
    public float GOLD_SPAWN_RATE = 1;
    public float SCREEN_WIDTH = 9f;
    public float SCREEN_HEIGHT = 5f;
    float count;

    public static TextMeshProUGUI textGold;
    public static GameObject[][] grids;
    public static bool[][] hasPlants;

    public static int gold;
    public static Image virtualImage;
    public static RectTransform virtualRect;

    public static List<GameObject> poolObjects;
    public static List<string> poolNames;

    public static GameObject messageObj;
    public static GameObject playAgainButton;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        poolObjects = new List<GameObject>();
        poolNames = new List<string>();

        textGold = GameObject.Find("TextGold").GetComponent<TextMeshProUGUI>();
        virtualImage = GameObject.Find("VirtualImage").GetComponent<Image>();
        virtualRect = GameObject.Find("VirtualImage").GetComponent<RectTransform>();
        virtualImage.enabled = false;

        messageObj = GameObject.Find("Alert");
        messageObj.SetActive(false);

        playAgainButton = GameObject.Find("PlayAgain");
        playAgainButton.SetActive(false);

        GameObject cell = Resources.Load<GameObject>("Cell") as GameObject;

        Vector3 position = transform.position;
        grids = new GameObject[VERTICAL_NUMBER][];
        hasPlants = new bool[VERTICAL_NUMBER][];

        for (int i = 0; i < VERTICAL_NUMBER; i++)
        {
            grids[i] = new GameObject[HORIZONTAL_NUMBER];
            hasPlants[i] = new bool[HORIZONTAL_NUMBER];

            for (int j = 0; j < HORIZONTAL_NUMBER; j++)
            {
                //grids[i][j] = Instantiate(cell, new Vector3(position.x + PADDING * j, position.y + PADDING * i), Quaternion.identity);
                hasPlants[i][j] = false;
            }
        }

        gold = 0;
        AddGold(START_GOLD);
    }

    void Update()
    {
        count -= Time.deltaTime;
        if (count < 0)
        {
            count = GOLD_SPAWN_RATE;
            Vector3 randomPos = new Vector3(Random.Range(-SCREEN_WIDTH, SCREEN_WIDTH), SCREEN_HEIGHT);

            GameObject coin = DGameSystem.LoadPool("Coin", randomPos);
            float destination = transform.position.y + (int)Random.Range(0, VERTICAL_NUMBER) * PADDING;
            coin.GetComponent<DCoin>().VERTICAL_DESTINATION = destination;
        }
    }

    public static void AddGold(int amount)
    {
        gold += amount;
        textGold.text = gold.ToString();
    }

    public static bool SpendGold(int amount)
    {
        if (amount > gold)
            return false;
        gold -= amount;
        textGold.text = gold.ToString();
        return true;
    }

    public static GameObject LoadPool(string poolName, Vector3 position)
    {
        for (int i = 0; i < poolNames.Count; i++)
        {
            if (string.Compare(poolNames[i], poolName) == 0 && poolObjects[i].activeSelf == false)
            {
                poolObjects[i].SetActive(true);
                poolObjects[i].transform.position = position;
                return poolObjects[i];
            }
        }

        GameObject obj = Instantiate(Resources.Load<GameObject>(poolName) as GameObject, position, Quaternion.identity);
        poolNames.Add(poolName);
        poolObjects.Add(obj);
        return obj;
    }

    public bool PlantTree(PlantStat stat, Vector3 position)
    {
        Vector3 pivot = transform.position - new Vector3(PADDING / 2, PADDING / 2);

        int h_index = (int)((position.x - pivot.x) / PADDING);
        int v_index = (int)((position.y - pivot.y) / PADDING);

        if (h_index >= 0 && h_index < HORIZONTAL_NUMBER && v_index >= 0 && v_index < VERTICAL_NUMBER)
        {
            if (hasPlants[v_index][h_index] == true)
                return false;

            if (SpendGold(stat.price))
            {
                Vector3 gridPosition = new Vector3(transform.position.x + h_index * PADDING, transform.position.y + v_index * PADDING);
                GameObject obj = LoadPool(stat.plantObjName, gridPosition);
                grids[v_index][h_index] = obj;
                hasPlants[v_index][h_index] = true;
                DBattle battle = obj.GetComponent<DBattle>();
                if (battle != null)
                {
                    battle.gridPosX = v_index;
                    battle.gridPosY = h_index;
                }
                return true;
            }
            else
            {
                GameObject alert = LoadPool("Alert", new Vector3(0, 0));
                alert.GetComponentInChildren<TextMeshPro>().text = "Not Enough Money!";
            }
        }

        return false;
    }


    public void RemoveTree(Vector3 position)
    {
        Vector3 pivot = transform.position - new Vector3(PADDING / 2, PADDING / 2);

        int h_index = (int)((position.x - pivot.x) / PADDING);
        int v_index = (int)((position.y - pivot.y) / PADDING);

        if (h_index >= 0 && h_index < HORIZONTAL_NUMBER && v_index >= 0 && v_index < VERTICAL_NUMBER)
        {
            if (grids[v_index][h_index] == null)
                return;

            if (hasPlants[v_index][h_index] == false)
                return;

            DBattle battle = grids[v_index][h_index].GetComponent<DBattle>();
            if (battle != null)
            {
                LoadPool("Explosion", grids[v_index][h_index].transform.position);
                battle.Dead();
            }
            else
            {
                LoadPool("Explosion", grids[v_index][h_index].transform.position);
                grids[v_index][h_index].SetActive(false);
            }

            hasPlants[v_index][h_index] = false;
        }
    }

    public void ShowMessage(string message, float time)
    {
        messageObj.SetActive(true);
        TextMeshPro tmp = messageObj.GetComponentInChildren<TextMeshPro>();
        tmp.text = message;

        Invoke("OffMessage",time);
    }

    public void ShowMessage(string message)
    {
        messageObj.SetActive(true);
        TextMeshPro tmp = messageObj.GetComponentInChildren<TextMeshPro>();
        tmp.text = message;
    }

    public void OffMessage()
    {
        messageObj.SetActive(false);
    }

    public void PlayAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void ShowPlayAgain(float delayTime)
    {
        Invoke("ShowPlayAgainInvoke", delayTime);
    }

    public void ShowPlayAgainInvoke(float delayTime)
    {
        playAgainButton.SetActive(true);
    }
}
