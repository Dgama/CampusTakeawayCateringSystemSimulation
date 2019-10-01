using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIS_Express
{
    public partial class Navi : Form
    {
        public Navi()
        {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile(@"Pic&Ico\bg.png");
            this.Icon = new Icon(@"Pic&Ico\gps.ico");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            Deliveryman deliveryman = new Deliveryman();
            deliveryman.ShowDialog();
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            Customer customer = new Customer();
            customer.ShowDialog();
            */
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*
            JisanCenter jisanCenter = new JisanCenter();
            jisanCenter.ShowDialog();
            */
        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*
            KuaidiCenter kuaidiCenter = new KuaidiCenter();
            kuaidiCenter.ShowDialog();
            */
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Kuaidichaxun kuaidichaxun = new Kuaidichaxun();
            kuaidichaxun.ShowDialog();
        }
    }
}
