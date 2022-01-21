using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessMelody
{
    public partial class fParams : Form
    {
        public fParams()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Victorine.allDirectories = cbFolders.Checked; //проверяем чекбокс внутренние папки
            Victorine.gameDuration = Convert.ToInt32(cbGameDuration.Text); //прод игры
            Victorine.musicDuration = Convert.ToInt32(cbMusicDuration.Text); //прод песни
            Victorine.startRandom = cbRandomPlace.Checked; //рандомный старт
            Victorine.WriteParam(); //и записали
            this.Hide();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog(); //открывать проводник
            fbd.ShowDialog(); //показываем
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string[] musicList = System.IO.Directory.GetFiles
                    (fbd.SelectedPath, //выбираем путь
                    "*.mp3", //фильтр по формату
                    cbFolders.Checked ? //проверка на проставленность "?"
                    SearchOption.AllDirectories : //в положительном
                    SearchOption.TopDirectoryOnly); //":", в отрицательном
                Victorine.lastFolder = fbd.SelectedPath; //сохраняем последнюю папку
                listBox1.Items.Clear();
                listBox1.Items.AddRange(musicList); //добавляем
                Victorine.musicList.Clear();
                Victorine.musicList.AddRange(musicList); //добавляем в экземпляр класса
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear ();
        }

        void setParams()
        {
            //возвращаем что было
            cbFolders.Checked = Victorine.allDirectories;
            cbGameDuration.Text = Victorine.gameDuration.ToString();
            cbMusicDuration.Text = Victorine.musicDuration.ToString();
            cbRandomPlace.Checked = Victorine.startRandom;
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            setParams();
            this.Hide ();
        }

        private void fParams_Load(object sender, EventArgs e)
        {
            setParams (); //прогружаем настройки что были
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Victorine.musicList.ToArray()); //добавляем
        }

        private void cbFolders_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbRandomPlace_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
