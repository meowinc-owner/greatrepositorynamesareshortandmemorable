using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameObject player;
    public static List<GameObject> enemies = new List<GameObject>();
    public static List<GameObject> bullets = new List<GameObject>();
    public static List<GameObject> walls = new List<GameObject>();
    public static List<GameObject> weapons = new List<GameObject>();
    
    [Header("the level maker pieces ✌️")]
    public int levelNumber = 0;

    public Texture2D[] levels;
    public CinemachineCamera cam;

    public Color playerColor;
    public GameObject playerPrefab;

    public Color greenWallColor;
    public GameObject greenWallPrefab;
    public Color blueWallColor;
    public GameObject blueWallPrefab;
    public Color orangeWallColor;
    public GameObject orangeWallPrefab;

    public Color enemyColor;
    public GameObject[] enemyPrefabs;

    public Color weaponColor;
    public GameObject[] weaponPrefabs;
    
    public TMP_Text levelText;
    public TMP_Text enemiesText;
    public TMP_Text hpText;

    private int levelsProgressed = 0;
    
    [SerializeField] private CountdownBeforeGame countdownBeforeGame;

    void Awake()
    {
        CreateLevel(levelNumber);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null &&  enemies.Count == 0) // if beaten lvl
        {
            levelNumber++;
            levelsProgressed++;
            levelNumber %= levels.Length; // when all levels compl, return to first lvl
            CreateLevel(levelNumber);
            countdownBeforeGame.Restart();
        }
        else if (player != null)
        {
            levelText.text = "level " + (levelNumber+1).ToString() + " (" + levelsProgressed + " progressed)";
            enemiesText.text = "enemies left: " + enemies.Count;
            Soldier s = player.GetComponent<Soldier>();
            hpText.text = "health: " + s.hp;
        }else if (player == null)
        {
            SceneManager.LoadScene("gameOver");
        }
    }

    private void ResetGame()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }
        for (int i = 0; i < bullets.Count; i++)
        {
            Destroy(bullets[i]);
        }
        for (int i = 0; i < walls.Count; i++)
        {
            Destroy(walls[i]);
        }
        for (int i = 0; i < weapons.Count; i++)
        {
            Destroy(weapons[i]);
        }
        Destroy(player);
        
        enemies.Clear();
        bullets.Clear();
        walls.Clear();
        weapons.Clear();
    }
    
    public void CreateLevel(int LevelNumber)
    {
        ResetGame();
        if (LevelNumber >= 0 && LevelNumber < levels.Length)
        {
            Texture2D level = levels[LevelNumber];
            for (int y = 0; y < level.height; y++)
            {
                for (int x = 0; x < level.width; x++)
                {
                    Color pixel = level.GetPixel(x, y);
                    if (pixel == playerColor)
                    {
                        player = Instantiate(playerPrefab, new Vector3(x, y, 0), Quaternion.identity);
                        player.GetComponent<Soldier>().startingWeapon = weaponSelector.GetWeapon();
                    }else if (pixel == greenWallColor)
                    {
                        walls.Add(Instantiate(greenWallPrefab, new Vector3(x, y, 0), Quaternion.identity));
                    }else if (pixel == blueWallColor)
                    {
                        walls.Add(Instantiate(blueWallPrefab, new Vector3(x, y, 0), Quaternion.identity));
                    }else if (pixel == orangeWallColor)
                    {
                        walls.Add(Instantiate(orangeWallPrefab, new Vector3(x, y, 0), Quaternion.identity));
                    }else if (pixel == enemyColor)
                    {
                        enemies.Add(Instantiate(enemyPrefabs[Random.Range(0,enemyPrefabs.Length)], new Vector3(x, y, 0), Quaternion.identity));
                    }else if (pixel == weaponColor)
                    {
                        weapons.Add(Instantiate(weaponPrefabs[Random.Range(0,weaponPrefabs.Length)], new Vector3(x, y, 0), Quaternion.identity));
                    }
                    
                }
            }
            cam.Follow = player.transform;
            cam.LookAt = player.transform;
            
        }
    }
}