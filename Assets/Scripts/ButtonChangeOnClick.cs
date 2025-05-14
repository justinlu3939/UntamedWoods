using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeOnClick : MonoBehaviour
{
    public Button buttonImage;
    public Sprite playButton;
    public Sprite pauseButton;
    public GameObject settingsMenu;

    public void clickOnSettingsButton()
    {
        Debug.Log("Settings menu clicked");
        if (settingsMenu.activeSelf)
        {
            buttonImage.GetComponent<Image>().sprite = pauseButton;
            Time.timeScale = 1;
        }
        else
        {
            buttonImage.GetComponent<Image>().sprite = playButton;
            Time.timeScale = 0;
        }
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }
}
