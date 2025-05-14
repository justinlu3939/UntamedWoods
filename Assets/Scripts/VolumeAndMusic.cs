using UnityEngine;
using UnityEngine.UI;

public class VolumeAndMusic : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider musicSlider;
    public Button volumeButton;
    public Button musicButton;
    public Sprite volumeOn;
    public Sprite musicOn;
    public Sprite volumeMute;
    public Sprite musicMute;

    private Image volumeButtonImage;
    
    public void Start()
    {
        volumeSlider.value = 0.5f;
        musicSlider.value = 0.5f;
        volumeButtonImage = volumeButton.GetComponent<Image>();
    }

    public void Update()
    {
        if(volumeSlider.value == 0.0f) {
            //enable the mute image for volume
            volumeButtonImage.sprite = volumeMute;
        }
        else {
            //enable the volume on image
            volumeButtonImage.sprite = volumeOn;
        }

        if(musicSlider.value == 0.0f) {
            //enable the mute button sprite for music
            musicButton.GetComponent<Image>().sprite = musicMute;
        }
        else {
            //enable the play button sprite for music
            musicButton.GetComponent<Image>().sprite = musicOn;
        }
    }

    //private float previousVolume = 0.5f;
    public void muteVolume()
    {
        // if (volumeSlider.value > 0.0f)
        // {
        //     //previousVolume = volumeSlider.value;
        //     volumeSlider.value = 0.0f;
        // }
        // else
        // {
        //     volumeSlider.value = previousVolume;
        // }
        volumeSlider.value = 0.0f;
    }
}
