using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessMelody
{
    static class Victorine
    {
        public static List<string> musicList = new List<string>(); //общий список песен
        public static int gameDuration = 60;
        public static int musicDuration = 10; //врмя для воспроизв музыки
        public static bool startRandom = false; //с какого трека начинать
        public static string lastFolder = ""; //последняя папка
        public static bool allDirectories = false; //волженные папки

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

    }
}
