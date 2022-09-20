using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    public GameObject disableObject;
    private int mySceneBuildIndex;
    private int levelToLoad;
    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    private void Awake()
    {
        mySceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (mySceneBuildIndex != 3 && Input.GetKeyDown(KeyCode.Escape) || !disableObject.activeInHierarchy)
        {
            FadeToLevel(3);
        }
        if (mySceneBuildIndex == 3 && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
    IEnumerator ObjectPassProcess(int level)
    {
        yield return new WaitForSeconds(2f);
        FadeToLevel(level);
    }
    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void LoadFirstObject()
    {
        FadeToLevel(13);
    }
    public void LoadSecondObject()
    {
        FadeToLevel(9);
    }
    public void LoadThirdObject()
    {
        FadeToLevel(10);
    }
    public void LoadFourthObject()
    {
        FadeToLevel(11);
    }
    public void LoadFifthObject()
    {
        FadeToLevel(12);
    }
    public void LoadFirstHouse()
    {
        FadeToLevel(4);
    }
    public void LoadSecondHouse()
    {
        FadeToLevel(5);
    }
    public void LoadThirdHouse()
    {
        FadeToLevel(6);
    }
    public void LoadFourthHouse()
    {
        FadeToLevel(7);
    }
    public void LoadFifthHouse()
    {
        FadeToLevel(8);
    }
    public void ShowMenu()
    {
        if (mySceneBuildIndex != 3)
        {
            dialogueUI.CloseDialogueBox();
            FadeToLevel(3);
        }
        else
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}
