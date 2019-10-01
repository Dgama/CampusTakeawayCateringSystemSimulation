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
    
    public partial class ChangeCode : Form
    {
        string id;
        int category;
        public ChangeCode(string id, int category)
        {
            this.id = id;
            this.category = category;
            InitializeComponent();
            this.Icon = new Icon(@"Pic&Ico\info.ico");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ChangeCode_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);
            SqlDataAdapter SA;
            SqlDataAdapter SA2;
            DataSet DS = new DataSet();

            string password;
            string error = "系统出现异常";
            string title = "错误提示！";
            string Sql;
            string Sql2="";

            if (category == 0)
            {
                Sql = "select 密码 from 快递员 where 快递员ID = " + "'" + id + "'";
            }
            else
            {
                if (category == 1)
                {
                    Sql = "select 用户密码 from 用户登录信息表 where 用户账号 = " + "'" + id + "'";
                }
                else
                {
                    Sql = "select 密码 from 商家登录信息表 where 账号 = " + "'" + id + "'";
                    Sql2 = "select 类型 from 快递点and集散中心表 where 账号 =" + "'" + id + "'";
                }
            }
            SA = new SqlDataAdapter(Sql, CN);
            SA.Fill(DS, "Sheet1$");

            /*try
            {*/
                password = DS.Tables["Sheet1$"].Rows[0][0].ToString().Replace(" ", "");
                if (textBox1.Text == password)
                {
                    if (textBox2.Text == textBox3.Text)
                    {
                        if (category == 0)
                        {
                            CN.Open();
                            string sql_update = string.Format("update 快递员 set 密码 = '{0}' where 快递员ID ='{1}'", textBox2.Text, id);
                            SqlCommand comm = new SqlCommand(sql_update, CN);
                            comm.ExecuteNonQuery();
                            MessageBox.Show("密码修改成功", "消息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (category == 1)
                            {
                                string sql_update = string.Format("update 用户登录信息表 set 用户密码 = '{0}' where 用户账号 ='{1}'", textBox2.Text, id);
                                CN.Open();
                                SqlCommand comm = new SqlCommand(sql_update, CN);
                                comm.ExecuteNonQuery();
                                MessageBox.Show("密码修改成功", "消息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                string sql_update = string.Format("update 商家登录信息表 set 密码 = '{0}' where 账号 ='{1}'", textBox2.Text, id);
                                CN.Open();
                                SqlCommand comm = new SqlCommand(sql_update, CN);
                                comm.ExecuteNonQuery();
                                MessageBox.Show("密码修改成功", "消息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else {
                        MessageBox.Show("前后两次密码不一致", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else MessageBox.Show("密码错误", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            /*}
            catch
            {
                MessageBox.Show(error, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/
            CN.Close();
        }
    }
}
