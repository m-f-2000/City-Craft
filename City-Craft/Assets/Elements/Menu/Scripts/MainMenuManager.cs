using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    #region Variables

    [Header("On/Off")]
    [Space(5)] [SerializeField] bool showBackground;
    [SerializeField] bool showSocial1;
    [SerializeField] bool showSocial2;
    [SerializeField] bool showSocial3;
    [SerializeField] bool showVersion;
    [SerializeField] bool showFade;

    [Header("Scene")]
    [Space(10)] [SerializeField] string sceneToLoad;

    [Header("Sprites")]
    [Space(10)] [SerializeField] Sprite logo;
    [SerializeField] Sprite background;
    [SerializeField] Sprite buttons;

    [Header("Color")]
    [Space(10)] [SerializeField] Color32 mainColor;
    [SerializeField] Color32 secondaryColor;

    [Header("Version")]
    [Space(10)] [SerializeField] string version = "v.0105";

    [Header("Texts")]
    [Space(10)] [SerializeField] string play = "Play";
    [SerializeField] string settings = "Settings";
    [SerializeField] string quit = "Quit";

    [Header("Social")]
    [Space(10)] [SerializeField] Sprite social1Icon;
    [SerializeField] string social1Link;
    [Space(5)]
    [SerializeField] Sprite social2Icon;
    [SerializeField] string social2Link;
    [Space(5)]
    [SerializeField] Sprite social3Icon;
    [SerializeField] string social3Link;
    List<string> links = new List<string>();

    [Header("Audio")]
    [Space(10)] [SerializeField] float defaultVolume = 0.8f;
    [SerializeField] AudioClip uiClick;
    [SerializeField] AudioClip uiHover;
    [SerializeField] AudioClip uiSpecial;


    [Header("Components")]
    [SerializeField] GameObject homePanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject bannerPanel;
    [SerializeField] Image social1Image;
    [SerializeField] Image social2Image;
    [SerializeField] Image social3Image;
    [SerializeField] Image logoImage;
    [SerializeField] Image backgroundImage;

    [Header("Fade")]
    [Space(10)] [SerializeField] Animator fadeAnimator;

    [Header("Color Elements")]
    [Space(5)] [SerializeField] Image[] mainColorImages;
    [SerializeField] TextMeshProUGUI[] mainColorTexts;
    [SerializeField] Image[] secondaryColorImages;
    [SerializeField] TextMeshProUGUI[] secondaryColorTexts;
    [SerializeField] Image[] buttonsElements;

    [Header("Texts")]
    [Space(10)] [SerializeField] TextMeshProUGUI playText;
    [SerializeField] TextMeshProUGUI settingsText;
    [SerializeField] TextMeshProUGUI quitText;
    [SerializeField] TextMeshProUGUI versionText;   

    [Header("Settings")]
    [Space(10)] [SerializeField] Slider volumeSlider;
    [SerializeField] TMP_Dropdown resolutionDropdown;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

    Resolution[] resolutions;

    #endregion

    void Start()
    {
        SetStartUI();
        SetStartVolume();
    }

    private void SetStartUI()
    {
        homePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void UIEditorUpdate()
    {
        
        #region Sprites

        if (logoImage != null)
        {
            logoImage.sprite = logo;
            logoImage.color = mainColor;
            logoImage.SetNativeSize();
        }

        if (backgroundImage != null)
        {
            backgroundImage.gameObject.SetActive(showBackground);
            backgroundImage.sprite = background;
            backgroundImage.SetNativeSize();
        }

        for (int i = 0; i < mainColorImages.Length; i++)
        {
            mainColorImages[i].color = mainColor;
        }

        for (int i = 0; i < mainColorTexts.Length; i++)
        {
            mainColorTexts[i].color = mainColor;
        }

        for (int i = 0; i < secondaryColorImages.Length; i++)
        {
            secondaryColorImages[i].color = secondaryColor;
        }

        for (int i = 0; i < secondaryColorTexts.Length; i++)
        {
            secondaryColorTexts[i].color = secondaryColor;
        }

        for (int i = 0; i < buttonsElements.Length; i++)
        {
            buttonsElements[i].sprite = buttons;
        }

        fadeAnimator.gameObject.SetActive(showFade);

        #endregion


        #region Texts

        if (playText != null)
            playText.text = play;

        if (settingsText != null)
            settingsText.text = settings;

        if (quitText != null)
            quitText.text = quit;

        versionText.gameObject.SetActive(showVersion);
        if (versionText != null)
            versionText.text = version;

        #endregion

    }


    #region Levels
    public void LoadLevel()
    {
        fadeAnimator.SetTrigger("FadeOut");

        StartCoroutine(WaitToLoadLevel());
    }

    IEnumerator WaitToLoadLevel()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion


    #region Audio

    public void SetVolume(float _volume)
    {
        AudioListener.volume = _volume;

        PlayerPrefs.SetFloat("Volume", _volume);
    }

    void SetStartVolume()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", defaultVolume);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }

    public void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void UIClick()
    {
        audioSource.PlayOneShot(uiClick);
    }

    public void UIHover()
    {
        audioSource.PlayOneShot(uiHover);
    }

    public void UISpecial()
    {
        audioSource.PlayOneShot(uiSpecial);
    }

    #endregion


    #region Graphics & Resolution Settings

    public void SetQuality(int _qualityIndex)
    {
        QualitySettings.SetQualityLevel(_qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void PrepareResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            if(!options.Contains(option))
                options.Add(option);

            if(i == resolutions.Length - 1)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int _resolutionIndex)
    {
        Resolution resolution = resolutions[_resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    #endregion
}
