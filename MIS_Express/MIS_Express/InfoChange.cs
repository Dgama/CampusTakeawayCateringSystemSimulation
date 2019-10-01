using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MIS_Express
{
    public partial class InfoChange : Form
    {
        string id;
        public InfoChange(string id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void InfoChange_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(@"Pic&Ico\info.ico");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);
            CN.Open();

            string sql_update = string.Format("update 用户登录信息表 set 姓名 = '{0}',联系方式='{1}' where 用户账号 ='{2}'", textBox1.Text, textBox2.Text, id);
            SqlCommand comm = new SqlCommand(sql_update, CN);
            comm.ExecuteNonQuery();

            CN.Close();
            MessageBox.Show("更新信息成功", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Dispose();
        }
    }
}
