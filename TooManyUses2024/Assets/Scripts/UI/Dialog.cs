using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public Transform box;
    public CanvasGroup background;

    private void OnEnable()
    {
        background.alpha = 0;
        background.LeanAlpha(2, 5.5f);

        box.localPosition = new Vector2(0, -Screen.height);
        box.LeanMoveLocalY(0, 5.5f).setEaseOutExpo().delay = 0.5f;
    }



    private void CloseDialog() {
        background.LeanAlpha(0, 7.5f);


        box.LeanMoveLocalY(-Screen.height, 5.5f).setEaseOutExpo().setOnComplete(OnComplete);

    }

    void OnComplete() {
        gameObject.SetActive(false);

    }
}
