using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

[RequireComponent (typeof(AndroidNotificationHandler))]
public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text energyText;
    [SerializeField] Button playButton;
    [SerializeField] AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] int maxEnergy = 5;
    [SerializeField] int energyRechargeDuration = 1;

    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    int energy;
    DateTime energyReady;

    private void Start()
    {
        OnApplicatonFocus(true);
    }

    // this is done in case we minimize the app during the invoke counter
    private void OnApplicatonFocus(bool hasFocus)
    {
        if (!hasFocus) { return; }
        CancelInvoke();

        highScoreText.text = $"High Score:\n {PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0)}";

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);
        if (energy == 0 )
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);

            if (energyReadyString == string.Empty) { return; } // in case of errors

            energyReady = DateTime.Parse(energyReadyString);
            if(DateTime.Now > energyReady)
            {
                playButton.interactable = true;
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            // this is done for when the menu is opened with 0 energy left to update the counter
            else
            {
                playButton.interactable = false;
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
            }
        }

        energyText.text = $"Play ({energy})";
    }

    private void EnergyRecharged()
    {
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
        energyText.text = $"Play ({energy})";
        playButton.interactable = true;
    }

    public void Play()
    {
        if (energy < 1) { return; }
        
        energy --;
        PlayerPrefs.SetInt(EnergyKey, energy);
        if (energy == 0)
        {
            energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());
#if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(energyReady);
#endif
        }
        SceneManager.LoadScene(1);
        
    }
}
