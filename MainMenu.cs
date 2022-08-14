using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Kaydet;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] MenuPanel;
    [SerializeField] private AudioSource[] Sesler;
    BellekYonetimi _bellekYonetimi = new BellekYonetimi();
    void Start()
    {
        _bellekYonetimi.KontrolVeTanimlama(); //bellekten oyun ilk açýldýðýnda kontrol ettik
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MainMenuButtons(string deger)
    {
        switch (deger)
        {
            case"play":
                    Load();
                break;
            case "settings":
                MenuPanel[1].SetActive(false);
                MenuPanel[2].SetActive(false);
                MenuPanel[3].SetActive(true);
                break;
            case "Levels":
                SceneManager.LoadScene("Levels");
                break;
            case "Back":
                MenuPanel[1].SetActive(true);
                MenuPanel[2].SetActive(false);
                MenuPanel[3].SetActive(false);
                break;
            case "Quit":
                Application.Quit();
                break;
        }
    }
    public void Load()
    {
        SceneManager.LoadScene(_bellekYonetimi.VeriOku_I("SonLevel"));
        
    }
}
