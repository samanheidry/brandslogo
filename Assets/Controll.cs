using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class Brand
{
    public string BrandName;
    public Sprite BrandLogo;
}

public class Controll : MonoBehaviour
{
    public List<Brand> BrandList;

    public TextMeshProUGUI RandomBrandNameText;

    public Image RightButtons, LeftButtons, UpButtons, DownButtons;

    public int WinningIndex;

    [Header("Score:")]
    public int CurrentScore;
    public TextMeshProUGUI ScoreText;

    [Header("Sounds:")]
    public AudioSource WinSound;
    public AudioSource LoseSound;

    [Header("Timer:")]
    public TextMeshProUGUI TimerText;
    public float TimerDuration = 5;
    public bool IsTimerActive = false;
    public float DelayBeforeStart = 3;

    [Header("Panels:")]
    public GameObject WinPanel;
    public GameObject LosePanel;

    public bool IsGameOver = false;

    void Start()
    {
        RandomizeBrands();
    }


    public void RandomizeBrands()
    {
        if (!IsGameOver)
        {
            List<int> chosenIndices = new List<int>();

            int rightIndex, leftIndex, upIndex, downIndex;

            do
            {
                rightIndex = UnityEngine.Random.Range(0, BrandList.Count);
            } while (chosenIndices.Contains(rightIndex));
            chosenIndices.Add(rightIndex);

            do
            {
                leftIndex = UnityEngine.Random.Range(0, BrandList.Count);
            } while (chosenIndices.Contains(leftIndex));
            chosenIndices.Add(leftIndex);

            do
            {
                upIndex = UnityEngine.Random.Range(0, BrandList.Count);
            } while (chosenIndices.Contains(upIndex));
            chosenIndices.Add(upIndex);

            do
            {
                downIndex = UnityEngine.Random.Range(0, BrandList.Count);
            } while (chosenIndices.Contains(downIndex));
            chosenIndices.Add(downIndex);


            RightButtons.GetComponent<Buttenn_Controol>().BrandName = BrandList[rightIndex].BrandName;
            RightButtons.GetComponent<Image>().sprite = BrandList[rightIndex].BrandLogo;



            LeftButtons.GetComponent<Buttenn_Controol>().BrandName = BrandList[leftIndex].BrandName;
            LeftButtons.GetComponent<Image>().sprite = BrandList[leftIndex].BrandLogo;


            UpButtons.GetComponent<Buttenn_Controol>().BrandName = BrandList[upIndex].BrandName;
            UpButtons.GetComponent<Image>().sprite = BrandList[upIndex].BrandLogo;


            DownButtons.GetComponent<Buttenn_Controol>().BrandName = BrandList[downIndex].BrandName;
            DownButtons.GetComponent<Image>().sprite = BrandList[downIndex].BrandLogo;


            int randomSelectedIndex = UnityEngine.Random.Range(0, chosenIndices.Count);
            WinningIndex = chosenIndices[randomSelectedIndex];
            RandomBrandNameText.text = BrandList[WinningIndex].BrandName;

            Invoke("StartGameAfterDelay", DelayBeforeStart);
        }
    }

    public void StartGameAfterDelay()
    {
        IsTimerActive = true;
    }

    void Update()
    {
        UpdateTimer();
    }

    public void CheckSelectedBrand(string brandName)
    {
        if (brandName == BrandList[WinningIndex].BrandName)
        {
            WinSound.Play();
            UpdateScore(5);
            TimerDuration = 10;
        }
        else
        {
            LoseSound.Play();
            UpdateScore(-5);
            TimerDuration = 10;
        }

        RandomizeBrands();
    }

    public void UpdateScore(int change)
    {
        CurrentScore += change;

        if (CurrentScore >= 40)
        {
            WinPanel.SetActive(true);
            IsTimerActive = false;
            IsGameOver = true;
        }

        if (CurrentScore <= 0)
        {
            CurrentScore = 0;
            LosePanel.SetActive(true);
            IsTimerActive = false;
            IsGameOver = true;
        }

        ScoreText.text = CurrentScore.ToString();

        IsTimerActive = false;
        TimerText.text = "";
        CancelInvoke();
    }

    public void UpdateTimer()
    {
        if (IsTimerActive)
        {
            TimerDuration -= Time.deltaTime;

            if (TimerDuration <= 0)
            {
                UpdateScore(-5);
                TimerDuration = 5;
                RandomizeBrands();
            }

            TimerText.text = "Timer: " + TimerDuration.ToString("N0");
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
