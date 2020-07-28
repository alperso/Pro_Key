using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;//Şifreleme kütüphanesi
namespace Pro_Key
{
    class Class2
    {
        private const string AES_IV = @"!&+QWSDF!123126+";//128 BİT KARSILIGI
        private string a_e_s = @"QQ()saw++,,12alB";//QQsaw!257()&&ert///BU BENİM ANAHTARIM BU ANAHTARLA ŞİFRELEDİGİM HER AES ACABİLİRİM.

        public string AESsifrele(string metin)
        {

            AesCryptoServiceProvider aesSaglayıcı = new AesCryptoServiceProvider();

            aesSaglayıcı.BlockSize = 128;//128 KARAKTER 128 BİT UZUNLUGUNDA OLACAK istersek 256 yapabilirz internnetten bakıp
            aesSaglayıcı.KeySize = 128;

            aesSaglayıcı.IV = Encoding.UTF8.GetBytes(AES_IV);
            aesSaglayıcı.Key = Encoding.UTF8.GetBytes(a_e_s);
            aesSaglayıcı.Mode = CipherMode.CBC;
            aesSaglayıcı.Padding = PaddingMode.PKCS7;

            byte[] kaynak = Encoding.Unicode.GetBytes(metin);


            using (ICryptoTransform sifrele = aesSaglayıcı.CreateEncryptor())
            {
                byte[] hedef = sifrele.TransformFinalBlock(kaynak, 0, kaynak.Length);
                return Convert.ToBase64String(hedef);
            }

        }
    }
}
