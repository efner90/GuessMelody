using System;
using WMPLib;
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
    public partial class fGame : Form
    {
        static Random rnd = new Random();
        int musicDuration = Victorine.musicDuration;
        

        public fGame()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Воспросизведение музыки
        /// </summary>
        void MakeMusic()
        {
            try
            {
                int musicCount = Victorine.musicList.Count() - 1;
                int count = rnd.Next(0, musicCount);

                if (count >= 0)
                {
                    musicDuration = Victorine.musicDuration;
                    //int count = rnd.Next(0, musicCount);
                    WMP.URL = Victorine.musicList[count];
                    Victorine.musicList.RemoveAt(count);
                    lblMusicCount.Text = Victorine.musicList.Count.ToString(); //передаём список оставшихся песен
                }
                else
                {
                    EndGame();
                }
            } catch
            {
                EndGame(); ;
            }




        }
        private void btnNext_Click(object sender, EventArgs e) //кнопка следующее
        {
            timer1.Start();
            MakeMusic();
        }

        private void fGame_FormClosed(object sender, FormClosedEventArgs e) //закрытие окна игры
        {
            timer1.Stop();
            WMP.Ctlcontrols.stop();
        }        

        private void lblMusicCount_Click(object sender, EventArgs e) 
        {

        }
        /// <summary>
        /// завершает игру, останавливает таймер и музыку
        /// </summary>
        void EndGame()
        {
            timer1.Stop();
            WMP.Ctlcontrols.stop();
        }
        private void timer1_Tick(object sender, EventArgs e) //таймер
        {
            progressBar1.Value++;
            musicDuration--;
            lblMusicDuration.Text = musicDuration.ToString();
            if (progressBar1.Value == progressBar1.Maximum)
            {
                EndGame();
                return;
            }
            if (musicDuration == 0) MakeMusic();
        }

        private void btnPause_Click(object sender, EventArgs e) //пауза
        {
            GamePause();
        }

        private void btnContinue_Click(object sender, EventArgs e)  //продолжить
        {
            GamePlay();
        }

        private void fGame_Load_1(object sender, EventArgs e)   //загрузка формы игры
        {
            lblMusicCount.Text = Victorine.musicList.Count.ToString();
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = Victorine.gameDuration;
            lblMusicDuration.Text = musicDuration.ToString();

        }
        /// <summary>
        ///Ставит воспроизведение музыки на паузу
        /// </summary>
        void GamePause()
        {
            timer1.Stop();
            WMP.Ctlcontrols.pause();
        }
        /// <summary>
        /// Продолжает воспроизведение музыки
        /// </summary>
        void GamePlay()
        {
            timer1.Start();
            WMP.Ctlcontrols.play();
        }
        private void fGame_KeyDown(object sender, KeyEventArgs e) //нажатие в форме
        {
            if (e.KeyData == Keys.A)
            {
                GamePause();
                if (MessageBox.Show("Правильный ответ", "Игрок 1", MessageBoxButtons.YesNo)
                    == DialogResult.Yes)
                {
                    lblCounterOne.Text =Convert.ToString(Convert.ToInt32(lblCounterOne.Text) +1);
                    //прибавляем балл в слуаче правильного ответа
                    MakeMusic();
                    //музон продолжается
                }
                GamePlay();
            }
            if (e.KeyData == Keys.L)
            {
                GamePause();
                if (MessageBox.Show("Правильный ответ", "Игрок 2", MessageBoxButtons.YesNo)
                    == DialogResult.Yes)
                {
                    lblCounterTwo.Text = Convert.ToString(Convert.ToInt32(lblCounterOne.Text) + 1);
                    //прибавляем балл в слуаче правильного ответа
                    MakeMusic();
                    //музон продолжается
                }
                GamePlay();
            }
        }
    }
}
