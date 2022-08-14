using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cember : MonoBehaviour
{

    public GameObject _AitOlduguStand;
    public GameObject _AitOlduguCemberSoketi; //terk etmi� oldu�u sokete geri d�nmek
    public bool hareketEdebilirMi;
    public string Renk;
    public GameManager _GameManager;


    GameObject HareketPozisyonu;
    GameObject GidecegiStand;

    bool Secildi, PozDegistir, SoketOtur, SoketeGeriGit;


    public void HareketEt(string islem , GameObject stand = null,GameObject soket = null , GameObject GidilecekObje = null)
    {
        switch (islem)
        {
            case "Secim":
                HareketPozisyonu = GidilecekObje;
                Secildi = true;
               
                break;

            case "PozisyonDegistir":
                GidecegiStand = stand;
                _AitOlduguCemberSoketi = soket;
                HareketPozisyonu = GidilecekObje;
                PozDegistir = true;
                break;

            case "SoketeGeriGit":
                SoketeGeriGit = true;
                break;
        }
    }

    void Update()
    {
        if (Secildi)
        {
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, .2f);
            if (Vector3.Distance(transform.position, HareketPozisyonu.transform.position)<.10)
            {
                Secildi = false;

            }
        }
        if (PozDegistir)
        {
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, .2f);
            if (Vector3.Distance(transform.position, HareketPozisyonu.transform.position) < .10)
            {
                PozDegistir = false;
                SoketOtur = true; // a�a��ya inme hareketi
            }
        }
        if (SoketOtur)
        {
            transform.position = Vector3.Lerp(transform.position, _AitOlduguCemberSoketi.transform.position, .2f);
            if (Vector3.Distance(transform.position, _AitOlduguCemberSoketi.transform.position) < .10)
            {
                transform.position = _AitOlduguCemberSoketi.transform.position;
                SoketOtur = false; // a�a��ya inme hareketi

                _AitOlduguStand = GidecegiStand;

                if (_AitOlduguStand.GetComponent<Stand>()._Cemberler.Count>1) //en az iki eleman var ise
                {
                    _AitOlduguStand.GetComponent<Stand>()._Cemberler[^2].GetComponent<cember>().hareketEdebilirMi = false; //yeni eklenen eleman�n alt�ndaki cemberin hareket etme yetenei�ini false yap�yoruz
                }
                _GameManager.HaraketVar = false;
            }
        }
        if (SoketeGeriGit)
        {
            transform.position = Vector3.Lerp(transform.position, _AitOlduguCemberSoketi.transform.position, .2f);
            if (Vector3.Distance(transform.position, _AitOlduguCemberSoketi.transform.position) < .10)
            {
                transform.position = _AitOlduguCemberSoketi.transform.position;
                SoketeGeriGit = false; // a�a��ya inme hareketi

                _GameManager.HaraketVar = false;
            }
        }

    }

  
}
