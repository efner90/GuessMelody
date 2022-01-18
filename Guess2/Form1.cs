using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guess2
{
    public partial class fMain : Form
    {
        fParams fp = new fParams(); //создаём экземпляр вне класса и кнопки

        public fMain()
        {
            InitializeComponent();
        }

        private void btnGame_Click(object sender, EventArgs e)
        {

        }

        private void btnParams_Click(object sender, EventArgs e)
        {
            fp.ShowDialog(); //метод Show вызывает окно и делает доступным сразу оба окна, 
                             //в отличии от ShowDialog
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
