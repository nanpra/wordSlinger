using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class uiManager : MonoBehaviour
{
    [SerializeField] public RectTransform mainScreen, gameWinPanel, gameLosePanel;
    public Image[] letters;
    public TextMeshProUGUI[] t;
    private Color fadeInColor = new Color(0.754f, 0.385f, 0.074f, 1f);
    private Color TextfadeInColor = new Color(1, 1, 1, 1);

    public void Start()
    {
        mainScreen.DOAnchorPos(Vector2.zero, 1f);
        StartCoroutine(fadeInDelay());
    }

    private void LetterAnim()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i].DOColor(fadeInColor, 0.5f);
            t[i].DOColor(TextfadeInColor, 0.5f);
        }
    }

    private IEnumerator fadeInDelay()
    {
        yield return new WaitForSeconds(1);
        LetterAnim();
    }

}
