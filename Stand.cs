using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject HareketPozisyonu;
    public GameObject[] Soketler;
    public int BosSoket;
    public List<GameObject> _Cemberler = new(); //liste oluþturma 
    [SerializeField] private GameManager _GameManager;

    int CemberTamamlanmaSayisi;

    public GameObject EnYukaridakiCember()
    {
        return _Cemberler[^1]; //cemberler listesini kullanarak en üsteki cemberi aldýk 
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
            _Cemberler[^1].GetComponent<cember>().hareketEdebilirMi = true; // en üstte giden obje bir alttaki cemberin hareket edebilirini true yapýyor
        }
        else
        {
            BosSoket = 0; //bos yere elemaný ekledikten sonra onu cembere kayýt ettik
        }
    }

    public void CemberKontrolEt()
    {
        if (_Cemberler.Count == 4)
        {
            string renkler = _Cemberler[0].GetComponent<cember>().Renk;
            foreach (var item in _Cemberler)
            {
                //doðrulama yapmam için kýyaslama yapmamýz gerekiyor ne ile kýyaslýcaz diyorsan eðer en alttaki cemberin rengine göre kýyaslýcaz en alttan renk konrolüne baþla...
                if (renkler == item.GetComponent<cember>().Renk)
                    CemberTamamlanmaSayisi++;
            }
            if (CemberTamamlanmaSayisi == 4) //buraya istediðin þeyi yazabilirsin tamamlanýnca effektmi çýksýn rengimi deðiþsin falan filan iþte
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

            Color32 color = item.GetComponent<MeshRenderer>().material.GetColor("_Color"); //tamamlanan cemberlerin renklerini aldýk ve þimdi deðiþtiricez
            color.a = 115;
            item.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
            //tamamlandýðýnda standýnda dokunulmaz olmasýný istiyoruz bunuda tagýný deðiþtirerek yapabiliriz neden çünkü tagý ile yakalýyoruz onu
            gameObject.tag = "TamamlanmýþStand";
        }
    }
}
