using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Enemy e;
    [SerializeField] public BulletFiring bulletScript;
    private WordContainerr wordValidator;
    [SerializeField] public TouchInput TouchInputREF;
    [SerializeField] public BonusLevel2 bonusLevel2;
    [SerializeField] private Transform GamePanel;

    [SerializeField] public uiManager UIManager;
    [SerializeField] public int attempts;
    [SerializeField] public int Correct = 0;
    [SerializeField] public bool isCorrect;
    [SerializeField] public bool wrongWord;

    public string sceneName;
    public Scene currentScene;
    [SerializeField] public TextMeshProUGUI instructions;
    [SerializeField] Image FadeImage;
    [SerializeField] public GameObject startScreen;
    private Color fadeOutColor = new Color(255f, 255f, 255f, 0f);
    public Transform canvasRect;
    [SerializeField] public Vector2 shake;

    //SFX
    [SerializeField] AudioSource GMaudioSource;
    [SerializeField] AudioClip wrongWordClip;
    [SerializeField] AudioClip correctWordClip;

    //levelProgress
    [SerializeField] public Slider slider;
    private float sliderValue;
    [SerializeField] public TextMeshProUGUI progressionName; 


    private BonusLevel1 bonusLevel1;
    private void OnEnable()
    {
        Application.targetFrameRate = 60;
    }
    private void Awake()
    {
        attempts = 3;
        wordValidator = GameObject.Find("WordContainer").GetComponent<WordContainerr>();
        if(bonusLevel1 != null)
        {
            bonusLevel1 = GameObject.Find("newPlayer").GetComponent<BonusLevel1>();
        }

    }
    public void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        FadeImage.DOColor(fadeOutColor, 1f);
    }

    [System.Obsolete]
    private void Update()
    {
        levelFinishCheck();
    }


    public void levelFinishCheck()
    {
        if (Correct == 3)
        {
            instructions.enabled = false;
            StartCoroutine(sliderDelay());
            StartCoroutine(winDelay());
        }
        if (attempts == 0)
        {
            instructions.enabled = false;
            StartCoroutine(loseDelay());
        }
    }

    public void CheckWord()
    {
        isCorrect = wordValidator.IsWordCorrect();
        if (isCorrect)
        {
            Correct++;
          
            GMaudioSource.clip = correctWordClip;
            GMaudioSource.Play();
            wordValidator.result = null;
            wordValidator.textGameObjects.Clear();
            wrongWord = false;
            StartCoroutine(isCorrectDelay());
            if (e != null)
            {
                e.StopTimer();
                e.ResetTimer();
            }
            if(bonusLevel2 != null)
            {
                bonusLevel2.StopTimer();
                bonusLevel2.ResetTimer();
            }
           
        }
        else
        {
            canvasRect.DOShakePosition(0.3f, shake);
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                attempts = 3;
            }
            else
            {
                attempts--;
            }
            
            Handheld.Vibrate();
            
           
            GMaudioSource.clip = wrongWordClip;
            GMaudioSource.Play();
            wordValidator.textGameObjects.Clear();
            wordValidator.result = null;
            wrongWord = true;
            if (e != null)
            {
                e.StopTimer();
                e.ResetTimer();
            }
            if(bonusLevel2 != null)
            {
                 bonusLevel2.StopTimer();
                 bonusLevel2.ResetTimer();
            }
        }
    }

    public void NextLevelBtn()
    {
     
        wordValidator.isGameOver = true;
        Correct = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }
    public void tryAgain()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
    public void startGame()
    {
        startScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    IEnumerator loseDelay()
    {
        yield return new WaitForSeconds(1.5f);
        UIManager.gameLosePanel.DOAnchorPos(new Vector2(0, 50), 0.75f);
        TouchInputREF.enabled = false;
    }
    IEnumerator winDelay()
    {

        yield return new WaitForSeconds(2f);
        GamePanel.gameObject.SetActive(false);
        UIManager.gameWinPanel.DOAnchorPos(new Vector2(0, 50), 0.75f);
        ProgressionName();
        TouchInputREF.enabled = false;
    }
    IEnumerator isCorrectDelay()
    {
        yield return new WaitForSeconds(0.3f);
        isCorrect = false;
    }
    private IEnumerator sliderDelay()
    {
        yield return new WaitForSeconds(2.5f);
        float sliderValue = 0f;

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0 : case 4 :
                sliderValue = 0.25f;
                break;
            case 1:  case 5 :
                sliderValue = 0.5f;
                break;
            case 2:  case 6 :
                sliderValue = 0.75f;
                break;
            case 3:  case 7 :
                sliderValue = 1f;
                break;
        }

        slider.DOValue(sliderValue, 1f);
    }

    private void ProgressionName()
    {
        if (SceneManager.GetActiveScene().buildIndex < 4)
        {
            progressionName.text = "COWBOY";
        }
        else
        {
            progressionName.text = "SHERIFF";
        }
    }
}