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
            //buras� istedi�imiz anahtar� sorgulayabilece�imiz yer
            if (!PlayerPrefs.HasKey("SonLevel")) //SonLevel diye bir key varm� yokmu onu kontrol ediyoruz !=> yoksa demek e�er yoksa de�er atamas� yap�caz ki oyun ilk a��ld���nda hata vermesin
            {
                PlayerPrefs.SetInt("SonLevel",2);
                //bu kaydetmeleri nerede kontrol edicez diyorsan e�er oyun a��ld���nda kar��m�za ��kan ilk yer ile yani AnaMen�de Kontrol edicez
            }
        }
    }
}
