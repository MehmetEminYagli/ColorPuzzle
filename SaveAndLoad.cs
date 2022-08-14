using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kaydet
{
    public class BellekYonetimi
    {
        public void VeriKaydet_String(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
        public void VeriKaydet_Int(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }
        public void VeriKaydet_Float(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        public string VeriOku_S(string key)
        {
            return PlayerPrefs.GetString(key);
        }
        public int VeriOku_I(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        public float VeriOku_F(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public void KontrolVeTanimlama()
        {
            //burasý istediðimiz anahtarý sorgulayabileceðimiz yer
            if (!PlayerPrefs.HasKey("SonLevel")) //SonLevel diye bir key varmý yokmu onu kontrol ediyoruz !=> yoksa demek eðer yoksa deðer atamasý yapýcaz ki oyun ilk açýldýðýnda hata vermesin
            {
                PlayerPrefs.SetInt("SonLevel",2);
                //bu kaydetmeleri nerede kontrol edicez diyorsan eðer oyun açýldýðýnda karþýmýza çýkan ilk yer ile yani AnaMenüde Kontrol edicez
            }
        }
    }
}
