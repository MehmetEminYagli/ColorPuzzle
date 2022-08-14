using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject HareketPozisyonu;
    public GameObject[] Soketler;
    public int BosSoket;
    public List<GameObject> _Cemberler = new(); //liste olu�turma 
    [SerializeField] private GameManager _GameManager;

    int CemberTamamlanmaSayisi;

    public GameObject EnYukaridakiCember()
    {
        return _Cemberler[^1]; //cemberler listesini kullanarak en �steki cemberi ald�k 
    }

    public GameObject MusaitSoket()
    {
        return Soketler[BosSoket];
    }
    public void SoketDegistirme(GameObject SilinecekObje)
    {
        _Cemberler.Remove(SilinecekObje);

        if (_Cemberler.Count != 0)
        {
            BosSoket--;
            _Cemberler[^1].GetComponent<cember>().hareketEdebilirMi = true; // en �stte giden obje bir alttaki cemberin hareket edebilirini true yap�yor
        }
        else
        {
            BosSoket = 0; //bos yere eleman� ekledikten sonra onu cembere kay�t ettik
        }
    }

    public void CemberKontrolEt()
    {
        if (_Cemberler.Count == 4)
        {
            string renkler = _Cemberler[0].GetComponent<cember>().Renk;
            foreach (var item in _Cemberler)
            {
                //do�rulama yapmam i�in k�yaslama yapmam�z gerekiyor ne ile k�yasl�caz diyorsan e�er en alttaki cemberin rengine g�re k�yasl�caz en alttan renk konrol�ne ba�la...
                if (renkler == item.GetComponent<cember>().Renk)
                    CemberTamamlanmaSayisi++;
            }
            if (CemberTamamlanmaSayisi == 4) //buraya istedi�in �eyi yazabilirsin tamamlan�nca effektmi ��ks�n rengimi de�i�sin falan filan i�te
            {
                _GameManager.StandTamamlandi();
                TamamlanmisStandlar();
            }
            else
            {
                CemberTamamlanmaSayisi = 0;
            }

        }
    }

    public void TamamlanmisStandlar()
    {
        foreach (var item in _Cemberler)
        {
            item.GetComponent<cember>().hareketEdebilirMi = false;

            Color32 color = item.GetComponent<MeshRenderer>().material.GetColor("_Color"); //tamamlanan cemberlerin renklerini ald�k ve �imdi de�i�tiricez
            color.a = 115;
            item.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
            //tamamland���nda stand�nda dokunulmaz olmas�n� istiyoruz bunuda tag�n� de�i�tirerek yapabiliriz neden ��nk� tag� ile yakal�yoruz onu
            gameObject.tag = "Tamamlanm��Stand";
        }
    }
}
