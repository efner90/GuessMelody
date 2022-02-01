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
        bool[] players = new bool[2];
        

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
                    lblMusicCount.Text = Victorine.musicList.Count.ToString();
                    //передаём список оставшихся песен
                    players[0] = false;
                    players[1] = false;
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
            if (!timer1.Enabled) return; //если таймер не включён то просто возврат, иначе код ниже
            if (players[0]==false && e.KeyData == Keys.A) 
                //выбрана кнопка А, есчли ещё не отжимал(фолс) идём дальше и ставим тру,
                //блокировав выбирать снова
            {
                GamePause();
                fMessage fm = new fMessage();
                fm.lblMessage.Text = "Игрок 1";
                players[0] = true;
                if (fm.ShowDialog() == DialogResult.Yes) //открываем форму сообщения
                {
                    lblCounterOne.Text =Convert.ToString(Convert.ToInt32(lblCounterOne.Text) +1);
                    //прибавляем балл в слуаче правильного ответа
                    MakeMusic();
                    //музон продолжается
                }
                GamePlay();
            }
            if (players[1] == false && e.KeyData == Keys.L) //выбрана кнопка L
            {
                GamePause();
                fMessage fm = new fMessage();
                fm.lblMessage.Text = "Игрок 1";
                players[1] = true;
                if (fm.ShowDialog() == DialogResult.Yes)                    
                {
                    lblCounterTwo.Text = Convert.ToString(Convert.ToInt32(lblCounterOne.Text) + 1);
                    //прибавляем балл в слуаче правильного ответа
                    MakeMusic();
                    //музон продолжается
                }
                GamePlay();
            }
        }

        private void WMP_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            if (Victorine.startRandom) //проверяем галку рандома
                if (WMP.openState == WMPOpenState.wmposMediaOpen)
                    WMP.Ctlcontrols.currentPosition =
                        rnd.Next(0, (int)WMP.currentMedia.duration / 2); //указываем место

        }
    }
}
