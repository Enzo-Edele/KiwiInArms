using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Declaration

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;

    [SerializeField] GameObject endMenu;
    [SerializeField] TMP_Text EndTitle;

    [SerializeField] GameObject credit;

    PlayerController player;

    [SerializeField] Image bossHealthMask;
    float originalBossHealthSize;

    [SerializeField] Image playerHealthMask;
    float originalPlayerHealthSize;

    [SerializeField] GameObject playerHealthBar;
    [SerializeField] GameObject bossHealthBar;

    GameObject previousMenu;

    #endregion
    public static UIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        originalBossHealthSize = bossHealthMask.rectTransform.rect.width;
        originalPlayerHealthSize = playerHealthMask.rectTransform.rect.width;
        ActivateMainMenu();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            ActivatePauseMenu();
        }
    }

    #region Menus
    void ActivateMainMenu() {
        mainMenu.SetActive(true);
    }
    void DeactivateMainMenu() {
        mainMenu.SetActive(false);
    }
    public void ActivatePauseMenu() {
        pauseMenu.SetActive(true);
        ButtonPause(false);
    }
    void DeactivatePauseMenu() {
        pauseMenu.SetActive(false);
    }
    public void ActivateEndMenu() {
        endMenu.SetActive(true);
    }
    public void DeactivateEndMenu() {
        endMenu.SetActive(false);
    }
    public void ActivateCredit()
    {
        credit.SetActive(true);
    }
    public void DeactivateCredit()
    {
        credit.SetActive(false);
    }
    public void ActivateHealth() {
        playerHealthBar.SetActive(true);
        bossHealthBar.SetActive(true);
    }
    public void DeactivateHealth() {
        playerHealthBar.SetActive(false);
        bossHealthBar.SetActive(false);
    }
    #endregion

    public void UpdateBossHealth(float total, float actual)
    {
        bossHealthMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalBossHealthSize * (actual / total));
        print("OISDOIFJSFD");
    }

    public void UpdatePlayerHealth(float total, float actual)
    {
        playerHealthMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalPlayerHealthSize * (actual / total));
    }
    public void EndFight(bool win) {
        if (win)
        {
            EndTitle.text = "You Win";
            player.animator.SetBool("Victory", true);
            ActivateCredit();
        }
        else
        {
            EndTitle.text = "You Lose";
        }
        ActivateEndMenu();
        DeactivateHealth();
    }

    public void SetPlayer(PlayerController newPlayer)
    {
        player = newPlayer;
    }

    public void ButtonPause(bool state)
    {
        if (state)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;
        }
    }
    public void ButtonActivateMenu(GameObject menu)
    {
        menu.SetActive(true);
        //set previous menu
    }
    public void ButtonDectivateMenu(GameObject menu)
    {
        menu.SetActive(false);
        //set previous menu
    }

    public void ButtonChangeSceneString(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
    public void ButtonChangeSceneInt(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void ButtonChangeSceneNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
}
