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
        

        public fGame()
        {
            InitializeComponent();
        }

        void MakeMusic()
        {
            try
            {
                int musicCount = Victorine.musicList.Count() - 1;
                int count = rnd.Next(0, musicCount);

                if (count >= 0)
                {
                    
                    //int count = rnd.Next(0, musicCount);
                    WMP.URL = Victorine.musicList[count];
                    Victorine.musicList.RemoveAt(count);
                }
                else
                {
                    WMP.Ctlcontrols.stop();
                }
            } catch
            {
                WMP.Ctlcontrols.stop();
            }




        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            MakeMusic();
        }

        private void fGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            WMP.Ctlcontrols.stop();
        }
    }
}
