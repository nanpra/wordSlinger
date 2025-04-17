using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordContainerr : MonoBehaviour
{
    [SerializeField] public GameManager gm;
    [SerializeField] public List<string> wordContainer = new List<string>();
    [SerializeField] public List<GameObject> textGameObjects;
    [SerializeField] public TextMeshProUGUI[] correctWords;
    public List<WordListScriptableObject> scriptableWordContainer;

    public bool isGameOver;
    public bool correctAnim;
    public string result;
    private int count,i;

    private TextMeshProUGUI mytext;
    private TextMeshProUGUI mytext1;

    [SerializeField] public TextMeshProUGUI[] selectedLetters1;
    [SerializeField] public TextMeshProUGUI[] selectedLetters2;
    [SerializeField] public TextMeshProUGUI[] selectedLetters3;

    [SerializeField] public RectTransform selectedLetterImage1;
    [SerializeField] public RectTransform selectedLetterImage2;
    [SerializeField] public RectTransform selectedLetterImage3;

    private void Start()
    {
        isGameOver = false;
        LoadWordLists();
    }

    public void LoadWordLists()
    {
        foreach (var wordList in scriptableWordContainer)
        {
            foreach (var word in wordList.words)
            {
                wordContainer.Add(word);
            }
        }
    }

    private void Update()
    {
        GetTheLetter();
        if (textGameObjects.Count == wordContainer[0].Length && gm.attempts > 0)
        {
            gm.CheckWord();
        }
    }
    public bool IsWordCorrect()
    {
        getTheWord();
        foreach (string word in wordContainer)
        {
            if (result == word)
            {
                correctAnim = true;
                isGameOver = false;
                //correctWords[gm.Correct].text = word;
                wordContainer.Remove(word);
                return true;
            }
        }
        return false;
    }
    private void GetTheLetter()
    {
        TextMeshProUGUI[] selectedLetters = null;
        RectTransform selectedLetterImage = null;

        switch (gm.Correct)
        {
            case 0:
                selectedLetters = selectedLetters1;
                selectedLetterImage = selectedLetterImage1;
                break;
            case 1:
                selectedLetters = selectedLetters2;
                selectedLetterImage = selectedLetterImage2;
                if (correctAnim)
                {
                    selectedLetterImage1.DOAnchorPos(Vector2.zero, 1f);
                }
                break;
            case 2:
                selectedLetters = selectedLetters3;
                selectedLetterImage = selectedLetterImage3;
                if (correctAnim)
                {
                    selectedLetterImage2.DOAnchorPos(Vector2.zero, 1f);
                }
                break;
            case 3:
                if (correctAnim)
                {
                    selectedLetterImage3.DOAnchorPos(Vector2.zero, 1f);
                }
                break;
        }

        if (selectedLetters == null || selectedLetterImage == null)
        {
            return;
        }

        if (textGameObjects.Count > 0)
        {
            for (int i = 0; i < textGameObjects.Count; i++)
            {
                TextMeshProUGUI myText = textGameObjects[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                selectedLetters[i].text = myText.text;
            }
        }
        else
        {
            for (int i = 0; i < selectedLetters.Length; i++)
            {
                selectedLetters[i].text = string.Empty;
            }
        }
    }

    public void getTheWord()
    {
        if (textGameObjects == null)
        {
            return;
        }
        else
        {
            for (i = 0; i < textGameObjects.Count; i++)
            {
                mytext = textGameObjects[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                result += mytext.text;
            }
            if (textGameObjects.Count == 3)
            {
                gm.TouchInputREF.enabled = false;
                Invoke("clearResult", 1.5f);
                Invoke("enableTouch", 0.7f);
            }
        }
    }

    public void clearResult()
    {
        textGameObjects.Clear();
        result = null;
    }
    public void enableTouch()
    {
        gm.TouchInputREF.enabled = true;
    }
}