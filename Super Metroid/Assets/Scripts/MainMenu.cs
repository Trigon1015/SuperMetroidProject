using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;
    // Start is called before the first frame update



    void Start()
    {
        AudioManager.instance.PlayMainMenuMusic();
        PlayerAbilityTracker.canMorphball = false;
        PlayerAbilityTracker.canDropBomb = false;
        PlayerAbilityTracker.canDoubleJump = false;
        PlayerAbilityTracker.canDash = false;
        PlayerAbilityTracker.canFreeze = false;
        PlayerAbilityTracker.canGravityReverse = false;
        PlayerController.bossBeat = false;
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("UI Canvas"));
    }

    public void NewGame()
    {

        SceneManager.LoadScene(newGameScene);
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Game Quit");
    }
}
