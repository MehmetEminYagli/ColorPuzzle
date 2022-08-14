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
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100)) // �s�n g�nderdik
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if (SeciliObje != null && SeciliPlatform != hit.collider.gameObject) // bir cemberi g�ndermek
                    {

                        Stand _stand = hit.collider.GetComponent<Stand>();

                        //secili cember dolu yere gitmeye �al���rsa eski yerine geri g�nderme
                        if (_stand._Cemberler.Count != 4 && _stand._Cemberler.Count != 0) //say� 4 ve 0 e e�it de�ilse aralarda bir i�lem yap�caz o zaman bunlar� yap dicez sonra else deyip 4 e�itse ne yap�caz onu s�yl�cez
                        {

                            if (_Cember.Renk == _stand._Cemberler[^1].GetComponent<cember>().Renk) //RENKLER�N KONTROL� se�ilen yeni stand�n en �st�ndeki �emberin rengi ile se�ilen �emberin renginin ayn� olmas� gerekiyor
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
                            else //RENKLER UYMUYORSA OTUR OTURDU�UN YERE
                            {
                                _Cember.HareketEt("SoketeGeriGit");
                                SeciliObje = null;
                                SeciliPlatform = null;
                                CemberGeriGirmeSesi();
                            }

                        }
                        else if (_stand._Cemberler.Count == 0)//0 a e�itse �unlar� yap bunu yazma nedenimiz bo� stand�n rengi olmuyaca�� i�in hata mesaj� al�rd�k o y�zden yazd�k bunu
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
                    else //hicbirsey secili de�ilse
                    {
                        Stand _Stand = hit.collider.GetComponent<Stand>(); //stand�n scriptine eri�tik SCR�PTEN SCR�PT KONTROL ETME
                        SeciliObje = _Stand.EnYukaridakiCember();
                        _Cember = SeciliObje.GetComponent<cember>(); //secili objenin cember scriptini ca��rd�k
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

    public void StandTamamlandi() // burada da level atlama ekran� falan yapabiliriz ki yap�caz
    {
        TamamlananStandSayisi++;
        if (TamamlananStandSayisi == HedefStandSayisi)
        {
            Time.timeScale = 0;
            Paneller[4].SetActive(true);  // kazand�n paneli ��k�cak
            _bellekYonetimi.VeriKaydet_Int("SonLevel", _bellekYonetimi.VeriOku_I("SonLevel") + 1); //var olan levelin �st�ne 1 ekleme yapt�k �nemli kaydet ile say�y� kaydettik okuma ile say�y� okuduk ve �st�ne bir ekledik
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
