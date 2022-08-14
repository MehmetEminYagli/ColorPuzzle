using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Kaydet;
public class LevelManager : MonoBehaviour
{
    public Button[] Butonlar;
    public int Level;
    public Sprite KilitButton;

    BellekYonetimi _BellekYonetimi = new BellekYonetimi();
    void Start()
    {
        


        int mevcutLevel = _BellekYonetimi.VeriOku_I("SonLevel"); // oyuncunun en son hangi levelde kaldýðýný aldýk

        int Index = 1;
        for (int i = 0; i < Butonlar.Length; i++)
        {
            if (Index < mevcutLevel)
            {
                Butonlar[i].GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
                Butonlar[i].interactable = true;
                //zurnanýn zýrt dediði kýsýma geldik öyle bir kod yazmalýyýz ki hangi butona basarsak onun level dizaynýný getirmeli teker teker yapamayýz belki 1000 tane level olucak
                int SahneIndex = Index + 1; 
                Butonlar[i].onClick.AddListener(delegate { SahneYukle(SahneIndex); });
            }
            else
            {

                Butonlar[i].GetComponent<Image>().sprite = KilitButton;
                Butonlar[i].interactable = false;
               // Butonlar[i].enabled = false; böylede yapabilirsin butonlara týklayamamayý
            }
            Index++;
            
        }


    }
    public void SahneYukle(int SahneIndex)
    {
        SceneManager.LoadScene(SahneIndex);
        //Debug.Log(index);
    }

    public void GeriDon()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
