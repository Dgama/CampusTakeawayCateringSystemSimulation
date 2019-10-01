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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            this.Icon = new Icon(@"Pic&Ico\info.ico");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            CN.Open();

            string title = "错误提示！";

            string realName = textBox1.Text;
            string sex="";
            if (radioButton1.Checked){ sex = "男";}
            if (radioButton2.Checked) { sex = "女"; }
            string phone = textBox2.Text;
            string name = textBox3.Text;
            string code = textBox4.Text;
            string reCode = textBox5.Text;

            string sql_nameCheck = "select count(*) from 用户登录信息表 where 用户账号 = '" + name + "'";
            string nameCheckOK;

            if (name == "" || code == "" || realName =="" || sex =="" || phone =="") {
                MessageBox.Show("信息不完整，请重新输入", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {
                if (code != reCode)
                {
                    MessageBox.Show("两次密码不一致，请重新输入", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else {
                    SA = new SqlDataAdapter(sql_nameCheck, CN);
                    SA.Fill(DS, "Sheet_nameCheck$");
                    nameCheckOK = DS.Tables["Sheet_nameCheck$"].Rows[0][0].ToString().Replace(" ", "");
                    if (nameCheckOK == "1")
                    {
                        MessageBox.Show("用户名已存在，请重新输入", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else {
                        string sql_insertName = string.Format("insert into 用户登录信息表(用户账号,用户密码,姓名,性别,联系方式) values ('{0}','{1}','{2}','{3}','{4}')", name, code, realName, sex, phone);
                        SqlCommand comm = new SqlCommand(sql_insertName, CN);
                        comm.ExecuteNonQuery();
                        MessageBox.Show("注册成功，请返回主界面登陆", "注册成功", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Dispose();
                    }
                }
            }
        }
    }
}
