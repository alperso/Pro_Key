using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HootKeys
{

    class globalKeyboardHook
    {
        #region Constant, Structure and Delegate Definitions

        public delegate int keyboardHookProc(int code, int wParam, ref keyboardHookStruct lParam);

        public struct keyboardHookStruct
        {
            public int vkCode;//ASCII KOD
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        //WH_KEYBOARD:GetMessage ve PeekMessage fonksiyonlarından 
        //WM_CHAR veya WM_KEYDOWN, WM_SYSKEYUP, WM_SYSKEYDOWN messajları döndüğü zaman bu hook türü gerçekleşir.

        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYDOWN = 0x104;
        const int WM_SYSKEYUP = 0x105;
        #endregion

        #region Instance Variables

        public List<Keys> HookedKeys = new List<Keys>();

        IntPtr hhook = IntPtr.Zero;
        #endregion

        #region Events

        public event KeyEventHandler KeyDown;

        public event KeyEventHandler KeyUp;
        #endregion

        #region Constructors and Destructors

        public globalKeyboardHook()
        {
            hook();
        }


        ~globalKeyboardHook()
        {
            unhook();
        }
        #endregion

        #region Public Methods

        public void hook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, hInstance, 0);//Hook işlemini başlatırız.
            //Fonksiyonun ilk parametresi hangi mesajı ele geçireceğimizi belirtmek için kullanırız.
            //Mesela klavye mesajlarını yakalamak için WH_KEYBOARD, mouse mesajlarını yakalamak için WH_MOUSE değerini kullanmalıyız.
            //İkinci parametres ise eğer mesaj ele geçirilirse hangi fonksiyonun çağrılacağını belirtir. Bu fonksiyonun prototipi aşağıdaki gibi olmalıdır.
        }


        public void unhook()
        {
            UnhookWindowsHookEx(hhook);
            //Bu fonskiyonu kullanarak hook işlemini sonlandırabiliriz. 
            //Fonksiyonun parametresi SetWindowsHookEx fonksiyonunun geri dönüş değerinden elde edilen handle değeridir.
        }


        public int hookProc(int code, int wParam, ref keyboardHookStruct lParam)//Hook yakalandıgınca calısacak fonksiyon
        {
            
            if (code >= 0)
            {
                Keys key = (Keys)lParam.vkCode;//GELEN HARFİN ASCII KODU harfe dönüştürülüyor. MESELA 68 D OLUYOR
                if (HookedKeys.Contains(key))
                {
                    KeyEventArgs kea = new KeyEventArgs(key);
                    if ((wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN) && (KeyDown != null))
                    {
                        KeyDown(this, kea);
                    }
                    else if ((wParam == WM_KEYUP || wParam == WM_SYSKEYUP) && (KeyUp != null))
                    {
                        KeyUp(this, kea);
                    }
                    if (kea.Handled)
                        return 1;
                }
            }
            return CallNextHookEx(hhook, code, wParam, ref lParam);
            //Bu fonksiyonu kullanarak eğer başka bir hook işlemi varsa onun çağrılması için kullanırız. 
            //Fonksiyonun ilk parametresi SetWindowsHookEx fonksiyonunun geri dönüş değerinden elde edilen handle değeridir. 
            //Diğer parametreler ise hook fonksiyonun parametreleridir(HookFunc(….)).
        }
        #endregion

        #region DLL imports
        //Hook işlemi için öncelikle hook fonksiyonunu içeren bir DLL hazırlanmalıdır. 

        //Nedeni ise daha önce de bahsettiğimiz gibi bir proses başka bir 
        //prosesin alanına erişemeyeceğinden bizim hazırladığımız hook fonksiyonu başka proses tarafından çağrılamayacaktır.
        //Ama hazırlamış olduğumuz DLL belleğe yüklendiğinde başka bir proses bizim hook fonksiyonumuzu çağırmak istediğinde 
        //DLL dosyamızın bir kopyası oluşturulacak ve bu DLL içinden ilgili hook fonksiyonumuz çağrılacaktır. 

        //Buradaki en önemli sorun şudur.

        //Madem her proses için DLL bellekte farklı adreslere yüklenebiliyorsa bizim hook fonksiyonun adresi nasıl tespit edilip fonksiyon çağrılacak? 
        //Bunu SetWindowsHookEx fonksiyonuna DLL in hInstance değeri ve hook fonksiyonumuzun adresi parametre geçirilirek bu problem halledilir. 



        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, keyboardHookProc callback, IntPtr hInstance, uint threadId);
        //Bu fonksiyon herhangi bir mesaj yakalandığında parametreleri sistem tarafından doldurularak çağrılır.
        //Fonksiyonun ilk parametresi yakalanan mesaj ile ilgili bilgi verir.  code

        //İkinci ve üçüncü parametreler ise mesaj ile ilgili ayrıntılı bilgiler verir. Genellikle yapı şeklindedirler.
        //Fonksiyonun üçüncü parametresi ayrıca bu fonksiyon hangi modül tarafından çağrıldı ise o modülün başlangıç adresini yani handle değerini belirtir.

        //Son parametre ise hangi pencerenin mesajını ele geçirmek istiyor isek o pencerenin bulunduğu threadin ID değeridir. 

        //Eğer sistem düzeyinde hook işlemi yapacaksak bu değer 0 olarak girilerek bütün pencerelerin mesajlarını yakalamak istediğimizi belirtiriz.
        
        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);
        //Bu fonskiyonu kullanarak hook işlemini sonlandırabiliriz. 
        //Fonksiyonun parametresi SetWindowsHookEx fonksiyonunun geri dönüş değerinden elde edilen handle değeridir.


        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref keyboardHookStruct lParam);
        //Bu fonksiyonu kullanarak eğer başka bir hook işlemi varsa onun çağrılması için kullanırız. 
        //Fonksiyonun ilk parametresi SetWindowsHookEx fonksiyonunun geri dönüş değerinden elde edilen handle değeridir. 
        //Diğer parametreler ise hook fonksiyonun parametreleridir(HookFunc(….)).


        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);
        #endregion
    }
}
