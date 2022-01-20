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
            Victorine.WriteParam();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide ();
        }
    }
}
