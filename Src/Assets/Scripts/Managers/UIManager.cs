using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text hp;
    public Text money;
    public Text level;
    public Text note;

    public GameObject buildMenu;
    public Button buildButton, destroyButton, upgradeButton;
    Button[] buildTurretButtons;
    Button destroyTurretButton;
    List<Turret> upgradeTurrets = new List<Turret>();
    List<Button> upgradeTurretButtons = new List<Button>();

    public GameObject pauseUI;
    public Image pauseButtonImage;
    public Sprite continueImage, pauseImage;

    public GameObject endMenu;
    public Text endText;

    public GameObject helpMenu;

    public AudioSource audioSource;

    void Start()
    {
        instance = this;
        CreateBuildMenu(BuildManager.instance.turretPrefabs);
    }

    void LateUpdate()
    {
        UpdatePlayerData();
        UpdateEnemyData();
        UpdateBuildMenu();
        if (!GameManager.instance.GetGameState() && endMenu.activeSelf == false) CreateEndUI();
    }

    public void CreateBuildMenu(Turret[] turretPrefabs) {
        buildTurretButtons = new Button[turretPrefabs.Length];
        for(int i = 0; i < turretPrefabs.Length; i++) {
            if (turretPrefabs[i].isBase) {
                Button button = Instantiate(buildButton, buildMenu.transform);
                button.transform.Find("Text").GetComponent<Text>().text = "创建" + turretPrefabs[i].turretName;
                button.GetComponent<BuildButton>().SetTurret(turretPrefabs[i]);
                int p = i;
                button.onClick.AddListener(delegate () {
                    if (BuildManager.instance.BuildTurret(p)) {
                        Player.instance.PayMoney(turretPrefabs[p].value);
                    }
                    UpdateNote("");
                });
                buildTurretButtons[i] = button;
                button.gameObject.SetActive(false);
            }

            CreateUpgradeOptions(turretPrefabs[i]);
        }

        destroyTurretButton = Instantiate(destroyButton, buildMenu.transform);
        destroyTurretButton.onClick.AddListener(delegate () {
            BuildManager.instance.DestroyTurret();
        });
        destroyTurretButton.gameObject.SetActive(false);
    }

    public void UpdateBuildMenu() {
        foreach (Transform button in buildMenu.transform) {
            button.gameObject.SetActive(false);
        }
        if (BuildManager.instance.selectedBlock != null) {
            Turret turret = BuildManager.instance.selectedBlock.GetComponent<BuildBlock>().GetTurret();
            if (turret == null) {
                for (int i = 0; i < buildTurretButtons.Length; i++) {
                    if (BuildManager.instance.turretPrefabs[i].isBase && Player.instance.money >= BuildManager.instance.turretPrefabs[i].value) buildTurretButtons[i].gameObject.SetActive(true);
                }
            }
            else {
                foreach (Turret upgradeOption in turret.upgradeOptions) {
                    if (Player.instance.money >= (upgradeOption.value - turret.value)) {
                        for (int i = 0; i < upgradeTurrets.Count; i++) {
                            if (upgradeTurrets[i] == upgradeOption) {
                                upgradeTurretButtons[i].gameObject.SetActive(true);
                                break;
                            }
                        }
                    }
                }
                destroyTurretButton.gameObject.SetActive(true);
            }
        }
    }

    public void UpdatePlayerData() {
        hp.text = "HP:" + Player.instance.hp.ToString();
        money.text = "MONEY:" + Player.instance.money.ToString();
    }
    public void UpdateEnemyData() {
        level.text = "LEVEL:" + EnemyManager.instance.GetLevel();
    }
    public void UpdateNote(string noteText) {
        note.text = noteText;
    }

    void CreateUpgradeOptions(Turret turret) {
        foreach (Turret upgradeOption in turret.upgradeOptions) {
            /*
            int flag = -1;
            for (int i = 0; i < upgradeTurrets.Count; i++) {
                if (upgradeTurrets[i] == upgradeOption) {
                    flag = i;
                    break;
                }
            }
            if (flag == -1) {
                upgradeTurrets.Add(upgradeOption);
                Button button = Instantiate(upgradeButton, buildMenu.transform);
                button.transform.Find("Text").GetComponent<Text>().text = "升级" + upgradeOption.turretName;
                int p = 0;
                for (int i = 0; i < buildManager.turretPrefabs.Length; i++) {
                    if (buildManager.turretPrefabs[i] == upgradeOption) {
                        p = i;
                        break;
                    }
                }
                button.onClick.AddListener(delegate () {
                    if (buildManager.BuildTurret(p)) {
                        player.PayMoney(upgradeOption.value - turret.value);
                    }
                    UpdateNote("");
                });
                upgradeTurretButtons.Add(button);
                button.gameObject.SetActive(false);
            }
            */
            upgradeTurrets.Add(upgradeOption);
            Button button = Instantiate(upgradeButton, buildMenu.transform);
            button.transform.Find("Text").GetComponent<Text>().text = "升级" + upgradeOption.turretName;
            button.GetComponent<BuildButton>().SetTurret(upgradeOption);
            int p = 0;
            for (int i = 0; i < BuildManager.instance.turretPrefabs.Length; i++) {
                if (BuildManager.instance.turretPrefabs[i] == upgradeOption) {
                    p = i;
                    break;
                }
            }
            button.GetComponent<BuildButton>().SetCost(upgradeOption.value - turret.value);
            button.onClick.AddListener(delegate () {
                if (BuildManager.instance.DestroyTurret() && BuildManager.instance.BuildTurret(p)) {
                    Player.instance.PayMoney(upgradeOption.value - turret.value);
                }
                UpdateNote("");
            });
            upgradeTurretButtons.Add(button);
            button.gameObject.SetActive(false);
            CreateUpgradeOptions(upgradeOption);
        }
    }

    void CreateEndUI() {
        endMenu.SetActive(true);
        if (GameManager.instance.GetGameEnd()) endText.text += "你胜利了";
        else endText.text += "你失败了";
    }

    public void OpenHelpMenu() {
        helpMenu.SetActive(true);
    }

    public void SetVolume(float volume) {
        audioSource.volume = volume;
    }

    public void Pause() {
        if (!GameManager.instance.GetGameState()) return;
        if (pauseUI.activeSelf) {
            Time.timeScale = 1f;
            pauseButtonImage.sprite = pauseImage;
            pauseUI.SetActive(false);
        }
        else {
            Time.timeScale = 0f;
            pauseButtonImage.sprite = continueImage;
            pauseUI.SetActive(true);
        }
    }

    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
