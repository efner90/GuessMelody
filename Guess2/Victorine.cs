using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace GuessMelody
{
    static class Victorine
    {
        public static List<string> musicList = new List<string>(); //общий список песен
        public static int gameDuration = 60;
        public static int musicDuration = 10; //врмя для воспроизв музыки
        public static bool startRandom = false; //с какого трека начинать
        public static string lastFolder = ""; //последняя папка
        public static bool allDirectories = false; //вложенные папки

        public static void ReadMusic()
        {
            string[] music_files = Directory.GetFiles(
                lastFolder, 
                "mp3",
                allDirectories ? //? == if
                SearchOption.AllDirectories : 
                SearchOption.TopDirectoryOnly); // : ==else
            musicList.Clear();
            musicList.AddRange(music_files);
        }

        public static string regKeyName = "SoftWare\\MyCompanyName\\GuessMelody";

        public static void WriteParam()
        {
            RegistryKey rk = null; //создаём класс для ключа реестра
            try
            {
                rk = Registry.CurrentUser.CreateSubKey(regKeyName);
                if (rk == null) return;
                rk.SetValue("LastFolder", lastFolder);
                rk.SetValue("Random", startRandom);
                rk.SetValue("GameDuration", gameDuration);
                rk.SetValue("MusicDuration", musicDuration);
                rk.SetValue("AllDirectoreies", allDirectories);
            }
            finally
            {
                if (rk != null) rk.Close();
            }
        }

        public static void ReadParam()
        {
            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(regKeyName); //считываем данные из ключа
                if (rk != null) //если не равен налл, читаем
                {
                    lastFolder = (string)rk.GetValue("LastFolder");
                    gameDuration = (int)rk.GetValue("GameDuration");
                    startRandom = Convert.ToBoolean(rk.GetValue("Random"));
                    musicDuration = (int)rk.GetValue("MusicDuration");
                    allDirectories = Convert.ToBoolean(rk.GetValue("AllDirectories"));
                }
            }
            finally
            {
                if(rk != null) rk.Close();
            }
        }

    }
}
