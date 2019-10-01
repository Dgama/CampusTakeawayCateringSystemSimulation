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
    public partial class Adbook : Form
    {
        string id;
        public Adbook(string id)
        {
            this.id = id;
            InitializeComponent();
            this.Icon = new Icon(@"Pic&Ico\checklist.ico");
            this.BackgroundImage = Image.FromFile(@"Pic&Ico\bg.png");
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI;";
            SqlConnection con = new SqlConnection(Loadstring);
            con.Open();
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            string Sql = "select  地址ID, 省, 区, 市 from 地址, 用户登录信息表 where 用户登录信息表.用户账号 = '" + id
            + "' and (地址.地址ID = 用户登录信息表.地址1 or 地址.地址ID = 用户登录信息表.地址2 or 地址.地址ID = 用户登录信息表.地址3 or 地址.地址ID = 用户登录信息表.地址4 or 地址.地址ID = 用户登录信息表.地址5)";
            SA = new SqlDataAdapter(Sql, con);
            SA.Fill(DS, "地址");
            this.dataGridView1.DataSource = DS.Tables["地址"];

        }

        private void adbook_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI;";
            SqlConnection con = new SqlConnection(Loadstring);
            con.Open();
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            string dz = this.dataGridView1.CurrentRow.Cells["地址表"].Value.ToString();
            string Sql = "update 地址 set 省='" + comboBox1.SelectedItem.ToString()
            + "',市='" + comboBox2.SelectedItem.ToString()
            + "',区='" + comboBox3.SelectedItem.ToString()
            + "',详细地址='" + textBox2.Text
            + "' where 地址ID = '" + dz + "'";
            SA = new SqlDataAdapter(Sql, con);
            SA.Fill(DS, "顾客地址修改");
            this.dataGridView1.DataSource = DS.Tables["顾客地址修改"];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI;";
            SqlConnection con = new SqlConnection(Loadstring);
            con.Open();
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            string dzid = this.dataGridView1.CurrentRow.Cells["地址表"].Value.ToString();
            string Sql = "insert into 地址 values ('" + dzid + "','"
            + "','" + comboBox1.SelectedItem.ToString()
            + "','" + comboBox2.SelectedItem.ToString()
            + "','" + comboBox3.SelectedItem.ToString()
            + "','" + textBox2.Text
            + "')  update 用户登录信息表 set 地址5=地址4, 地址4=地址3, 地址3=地址2, 地址2=地址1, 地址1='"
            + dzid + "' where 用户账号='"
            + id + "'";
            SA = new SqlDataAdapter(Sql, con);
            SA.Fill(DS, "顾客地址增加");
            this.dataGridView1.DataSource = DS.Tables["顾客地址增加"];
        }
    }
}
