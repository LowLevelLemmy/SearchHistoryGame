using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UI_InstancesPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI captionTxt;
    [SerializeField] TextMeshProUGUI countTxt;

    CanvasGroup canvasGroup;

    void OnEnable()
    {
        canvasGroup ??= GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    public void Animate(float fadeDur, float length)
    {
        canvasGroup.DOFade(1, fadeDur);
        DOVirtual.DelayedCall(length, () => AnimateClose(fadeDur));
    }

    public void AnimateClose(float duration)
    {
        canvasGroup.DOFade(0, duration);
    }

    public void SetTexts(string caption, int count)
    {
        SetCaption(caption);
        SetCount(count);
    }

    void SetCaption(string str)
    {
        captionTxt.text = str;
    }

    void SetCount(int count)
    {
        countTxt.text = count.ToString();
    }
}
