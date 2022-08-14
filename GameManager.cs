using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Kaydet;
public class GameManager : MonoBehaviour
{
    GameObject SeciliObje;
    GameObject SeciliPlatform;
    cember _Cember;
    public bool HaraketVar;
    [SerializeField] private GameObject[] Paneller;
    [SerializeField] private AudioSource[] Sesler;

    BellekYonetimi _bellekYonetimi = new BellekYonetimi();
    public int HedefStandSayisi;
    int TamamlananStandSayisi;

    public void Start()
    {
        Time.timeScale = 1;


    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100)) // ýsýn gönderdik
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if (SeciliObje != null && SeciliPlatform != hit.collider.gameObject) // bir cemberi göndermek
                    {

                        Stand _stand = hit.collider.GetComponent<Stand>();

                        //secili cember dolu yere gitmeye çalýþýrsa eski yerine geri gönderme
                        if (_stand._Cemberler.Count != 4 && _stand._Cemberler.Count != 0) //sayý 4 ve 0 e eþit deðilse aralarda bir iþlem yapýcaz o zaman bunlarý yap dicez sonra else deyip 4 eþitse ne yapýcaz onu söylücez
                        {

                            if (_Cember.Renk == _stand._Cemberler[^1].GetComponent<cember>().Renk) //RENKLERÝN KONTROLÜ seçilen yeni standýn en üstündeki çemberin rengi ile seçilen çemberin renginin ayný olmasý gerekiyor
                            {
                                SeciliPlatform.GetComponent<Stand>().SoketDegistirme(SeciliObje);
                                _Cember.HareketEt("PozisyonDegistir", hit.collider.gameObject, _stand.MusaitSoket(), _stand.HareketPozisyonu);
                                _stand.BosSoket++;
                                _stand._Cemberler.Add(SeciliObje);
                                _stand.CemberKontrolEt();
                                SeciliObje = null;
                                SeciliPlatform = null;
                                CemberSecmeSesi();
                            }
                            else //RENKLER UYMUYORSA OTUR OTURDUÐUN YERE
                            {
                                _Cember.HareketEt("SoketeGeriGit");
                                SeciliObje = null;
                                SeciliPlatform = null;
                                CemberGeriGirmeSesi();
                            }

                        }
                        else if (_stand._Cemberler.Count == 0)//0 a eþitse Þunlarý yap bunu yazma nedenimiz boþ standýn rengi olmuyacaðý için hata mesajý alýrdýk o yüzden yazdýk bunu
                        {
                            SeciliPlatform.GetComponent<Stand>().SoketDegistirme(SeciliObje);
                            _Cember.HareketEt("PozisyonDegistir", hit.collider.gameObject, _stand.MusaitSoket(), _stand.HareketPozisyonu);
                            _stand.BosSoket++;
                            _stand._Cemberler.Add(SeciliObje);
                            _stand.CemberKontrolEt();
                            SeciliObje = null;
                            SeciliPlatform = null;
                            CemberSecmeSesi();
                        }
                        else
                        {
                            _Cember.HareketEt("SoketeGeriGit");
                            SeciliObje = null;
                            SeciliPlatform = null;
                            CemberGeriGirmeSesi();
                        }

                    }
                    else if (SeciliPlatform == hit.collider.gameObject) //cemberi geri yerine koyma;
                    {
                        _Cember.HareketEt("SoketeGeriGit");
                        SeciliObje = null;
                        SeciliPlatform = null;
                        CemberGeriGirmeSesi();
                    }
                    else //hicbirsey secili deðilse
                    {
                        Stand _Stand = hit.collider.GetComponent<Stand>(); //standýn scriptine eriþtik SCRÝPTEN SCRÝPT KONTROL ETME
                        SeciliObje = _Stand.EnYukaridakiCember();
                        _Cember = SeciliObje.GetComponent<cember>(); //secili objenin cember scriptini caðýrdýk
                        HaraketVar = true;

                        if (_Cember.hareketEdebilirMi)
                        {
                            _Cember.HareketEt("Secim", null, null, _Cember._AitOlduguStand.GetComponent<Stand>().HareketPozisyonu);

                            SeciliPlatform = _Cember._AitOlduguStand;
                        }
                    }
                }
            }
        }

    }

    public void StandTamamlandi() // burada da level atlama ekraný falan yapabiliriz ki yapýcaz
    {
        TamamlananStandSayisi++;
        if (TamamlananStandSayisi == HedefStandSayisi)
        {
            Time.timeScale = 0;
            Paneller[4].SetActive(true);  // kazandýn paneli çýkýcak
            _bellekYonetimi.VeriKaydet_Int("SonLevel", _bellekYonetimi.VeriOku_I("SonLevel") + 1); //var olan levelin üstüne 1 ekleme yaptýk önemli kaydet ile sayýyý kaydettik okuma ile sayýyý okuduk ve üstüne bir ekledik
        }
    }
    public void ButonIslemleri(string deger)
    {
        switch (deger)
        {
            case "MainMenu":
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                break;
            case "Pause":
                Time.timeScale = 0f;
                Paneller[0].SetActive(true);
                break;
            case "Next":
                Paneller[4].SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Time.timeScale = 1f;
                break;
            case "Resume":
                Time.timeScale = 1f;
                Paneller[0].SetActive(false);
                break;
            case "Tekrar":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1f;
                Paneller[0].SetActive(false);
                break;
            case "Settings":
                Time.timeScale = 0f;
                Paneller[0].SetActive(false);
                Paneller[1].SetActive(true);
                break;
            case "Quit":
                Application.Quit();
                break;
            case "Mute":
                Paneller[2].SetActive(true);
                Paneller[3].SetActive(false);
                break;
            case "Unmute":
                Paneller[2].SetActive(false);
                Paneller[3].SetActive(true);
                break;
        }
    }

    public void CemberSecmeSesi()
    {
        Sesler[0].Play();
    }
    public void CemberGeriGirmeSesi()
    {
        Sesler[1].Play();
    }


}
