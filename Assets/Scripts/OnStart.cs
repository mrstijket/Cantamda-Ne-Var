using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnStart : MonoBehaviour
{
    [SerializeField] float loadLevelTime = 3.5f;
    [SerializeField] float waitTextGo = 5.5f;
    public Animator animator;
    private int mySceneBuildIndex;
    private void Awake()
    {
        mySceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Start()
    {
        FadeToLevel();
    }
    public void FadeToLevel()
    {
        StartCoroutine(FadeProcess());
    }
    IEnumerator FadeProcess()
    {
        yield return new WaitForSeconds(waitTextGo);
        animator.SetTrigger("FadeNext");
        yield return new WaitForSeconds(loadLevelTime);
        SceneManager.LoadScene(mySceneBuildIndex + 1);
    }
}
