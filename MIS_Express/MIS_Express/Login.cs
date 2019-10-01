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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.Icon = new Icon(@"Pic&Ico\login.ico");
            this.BackgroundImage = Image.FromFile(@"Pic&Ico\bg.png");
   //         pictureBox1.BackgroundImage = Image.FromFile(@"Pic&Ico\title.png");
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "请输入您的用户名")
            {
                textBox1.Text = "";
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "请输入您的密码")
            {
                textBox2.Text = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register form3 = new Register();
            form3.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Kuaidichaxun kuaidichaxun = new Kuaidichaxun();
            kuaidichaxun.ShowDialog();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);
            SqlDataAdapter SA;
            SqlDataAdapter SA2;
            SqlDataAdapter SA3;
            DataSet DS = new DataSet();

            string name = textBox1.Text;
            string password;
            string error = "账号、密码或登陆类型错误！";
            string title = "错误提示！";
            string Sql;
            string Sql2 = "";
            string jisan_or_kuaidiCenter;
            int int_jisan_or_kuaidiCenter;

            //提取数据库的密码（sql）
            if (comboBox1.SelectedIndex.ToString() == "0")
            {
                Sql = "select 密码 from 快递员 where 快递员ID = " + "'" + name + "'";
            }
            else
            {
                if (comboBox1.SelectedIndex.ToString() == "1")
                {
                    Sql = "select 用户密码 from 用户登录信息表 where 用户账号 = " + "'" + name + "'";
                }
                else
                {
                    Sql = "select 密码 from 商家登录信息表 where 账号 = " + "'" + name + "'";
                    Sql2 = "select 类型 from 快递点and集散中心表 where 账号 =" + "'" + name + "'";
                }
            }
            SA = new SqlDataAdapter(Sql, CN);
            SA.Fill(DS, "Sheet1$");
            string basic_sql = "select 所属点ID from 商家登录信息表 where 账号 = '{0}'";

            
            try{
                password = DS.Tables["Sheet1$"].Rows[0][0].ToString().Replace(" ", "");
                if (textBox2.Text == password)
                {
                    if (comboBox1.SelectedIndex.ToString() == "0")
                    {
                        Deliveryman deliveryman = new Deliveryman(textBox1.Text);
                        deliveryman.ShowDialog();
                        this.Dispose();
                    }
                    else
                    {
                        if (comboBox1.SelectedIndex.ToString() == "1")
                        {
                            Customer customer = new Customer(textBox1.Text);
                            customer.ShowDialog();
                            this.Dispose();
                        }
                        else
                        {
                            SA2 = new SqlDataAdapter(Sql2, CN);
                            SA2.Fill(DS, "Sheet2$");

                            string sqlMerchantID = string.Format(basic_sql, textBox1.Text);
                            SA3 = new SqlDataAdapter(sqlMerchantID, CN);
                            SA3.Fill(DS, "Sheet");

                            jisan_or_kuaidiCenter = DS.Tables["Sheet2$"].Rows[0][0].ToString().Replace(" ", "");
                            int_jisan_or_kuaidiCenter = jisan_or_kuaidiCenter.Length;

                        string MerchantID = DS.Tables["Sheet"].Rows[0][0].ToString();

                        if (int_jisan_or_kuaidiCenter == 5 && comboBox1.SelectedIndex.ToString() == "3")
                            {
                                //MessageBox.Show(MerchantID);
                                KuaidiCenter kuaidiCenter = new KuaidiCenter(textBox1.Text, MerchantID);
                                kuaidiCenter.ShowDialog();
                                this.Dispose();
                            }
                            else
                            {
                                if (int_jisan_or_kuaidiCenter == 4 && comboBox1.SelectedIndex.ToString() == "2")
                                {
                                    //MessageBox.Show(MerchantID);
                                    JisanCenter jisanCenter = new JisanCenter(textBox1.Text, MerchantID);
                                    jisanCenter.ShowDialog();
                                    this.Dispose();
                                }
                                else
                                {
                                    MessageBox.Show("账号与登陆身份不匹配", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                }
                else MessageBox.Show("密码错误", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {
                MessageBox.Show(error, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            CN.Close();
        }
    }
}
