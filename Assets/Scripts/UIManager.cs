using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private Image BackgroundIndicator;
    [SerializeField]
    private Image FillBackgroundIndicator;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    public void SetBackgroundIndicator(Sprite _sprite)
    {

        BackgroundIndicator.sprite = _sprite;
        FillBackgroundIndicator.sprite = _sprite;
        BackgroundIndicator.SetNativeSize();
        FillBackgroundIndicator.SetNativeSize();
        FillBackgroundIndicator.fillAmount = 1;
        BackgroundIndicator.enabled = true;
        FillBackgroundIndicator.enabled = true;
    }

    public void FillBackground(float _amout)
    {
        FillBackgroundIndicator.fillAmount = 1 - _amout;
        Debug.Log("Amout : " + _amout);
        if (FillBackgroundIndicator.fillAmount < 0.01f)
        {
            BackgroundIndicator.enabled = false;
            FillBackgroundIndicator.enabled = false;
        }
    }
}
