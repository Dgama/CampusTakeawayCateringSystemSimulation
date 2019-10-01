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
    public partial class Deliveryman : Form
    {
        string id;
        public Deliveryman(string id)
        {
            this.id = id;
            InitializeComponent();
            this.toolStripStatusLabel1.Text += id;
            this.Icon = new Icon(@"Pic&Ico\user2.ico");
            全部快递ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\inventory.ico");
            指定条件快递ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\search.ico");
            快递信息修改ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\delivery.ico");
            个人中心ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\user.ico");
            待揽件ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\order.ico");
            退出ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\logout.ico");

            panel1.BackgroundImage = Image.FromFile(@"Pic&Ico\bg.png");


            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            string sql_person = string.Format("select 姓名,性别,电话 from 快递员 where 快递员ID = '{0}'", id);
            SA = new SqlDataAdapter(sql_person, CN);
            SA.Fill(DS, "Sheet1$");
            label9.Text = id;
            label8.Text = DS.Tables["Sheet1$"].Rows[0][0].ToString();
            if (DS.Tables["Sheet1$"].Rows[0][1].ToString().Replace(" ","").Length == 4) { label7.Text = "男"; } else {label7.Text = "女";}
            label6.Text = DS.Tables["Sheet1$"].Rows[0][2].ToString();
        }

        private void 全部快递ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(groupBox1);
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection con = new SqlConnection(Loadstring);
            con.Open();
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            string Sql = "select 订单.订单编号, 寄件人.姓名 as 寄件人, 寄件人.电话 as 电话, A.详细地址 as 收件地址,B.详细地址 as 揽件地址, 订单.重要物品, 订单.上门取件预约时间,订单.运费险,A.省,A.市,A.区,A.详细地址"
            + "  from 订单,寄件人,收件人,地址 A,地址 B "
            + "  where 订单.快递员ID = '" + id 
            + "' and  订单.寄件人ID = 寄件人.寄件人ID and 订单.收件人ID =收件人.收件人ID and A.地址ID = 收件人.地址 and B.地址ID = 寄件人.地址 and 订单.是否完成 =0";
            SA = new SqlDataAdapter(Sql, con);
            SA.Fill(DS, "全部快递包裹");
            this.dataGridView1.DataSource = DS.Tables["全部快递包裹"];

        }

        private void 指定条件快递ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(groupBox3);
        }

        private void 个人中心ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(groupBox4);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            InfoChange infoChange = new InfoChange();
            infoChange.ShowDialog();
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeCode changeCode = new ChangeCode(id,0);
            changeCode.ShowDialog();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void 待揽件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(groupBox2);
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection con = new SqlConnection(Loadstring);
            con.Open();
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            string Sql = "select 订单.订单编号, 寄件人.姓名 as 寄件人, 寄件人.电话 as 电话, A.详细地址 as 收件地址,B.详细地址 as 揽件地址, 订单.重要物品, 订单.上门取件预约时间,订单.运费险"
            + "  from 订单,寄件人,收件人,地址 A,地址 B "
            + "  where 订单.揽件人ID = '" + id
            + "' and 订单.揽件时间 is null and 订单.寄件人ID = 寄件人.寄件人ID and 订单.收件人ID =收件人.收件人ID and A.地址ID = 收件人.地址 and B.地址ID = 寄件人.地址 and 订单.是否完成=0";
            SA = new SqlDataAdapter(Sql, con);
            SA.Fill(DS, "待揽件包裹");
            this.dataGridView2.DataSource = DS.Tables["待揽件包裹"];
        }

        private void Deliveryman_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //HistoryAddress history = new HistoryAddress();
            //history.ShowDialog();
        }

        private void dataGridView3_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //HistoryAddress history = new HistoryAddress();
            //history.ShowDialog();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked && textBox1.Text != "")
                {
                    string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
                    SqlConnection con = new SqlConnection(Loadstring);
                    con.Open();
                    SqlDataAdapter SA;
                    DataSet DS = new DataSet();
                    string Sql = "select 订单.订单编号,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,快递点and集散中心表.点位 as 当前地址,用户登录信息表.姓名 as 用户名,订单.重量,订单.物品类型,订单.重要物品,订单.预计到达时间,订单.运费,订单.是否到货付款, 订单.是否完成"
                    + "  from 订单, 寄件人, 收件人, 快递点and集散中心表, 用户登录信息表, 当前地址表"
                    + "  where 订单.订单编号 = '" + textBox1.Text
                    + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.地址表ID = 当前地址表.地址表ID and 当前地址表.所属点ID =快递点and集散中心表.所属点ID and 订单.用户账号 =用户登录信息表.用户账号  ";
                    SA = new SqlDataAdapter(Sql, con);
                    SA.Fill(DS, "快递单号查询");
                    con.Close();
                    this.dataGridView3.DataSource = DS.Tables["快递单号查询"];
                }
                else if (radioButton2.Checked && textBox1.Text != "")
                {
                    string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
                    SqlConnection con = new SqlConnection(Loadstring);
                    con.Open();
                    SqlDataAdapter SA;
                    DataSet DS = new DataSet();
                    string Sql = "select 订单.订单编号,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,快递点and集散中心表.点位 as 当前地址,用户登录信息表.姓名 as 用户名,订单.重量,订单.物品类型,订单.重要物品,订单.预计到达时间,订单.运费,订单.是否到货付款, 订单.是否完成"
                    + "  from 订单, 寄件人, 收件人, 快递点and集散中心表, 用户登录信息表, 当前地址表"
                    + "  where 收件人.收件人ID = '" + textBox1.Text
                    + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.地址表ID = 当前地址表.地址表ID and 当前地址表.所属点ID =快递点and集散中心表.所属点ID and 订单.用户账号 =用户登录信息表.用户账号 ";
                    SA = new SqlDataAdapter(Sql, con);
                    SA.Fill(DS, "收件人查询");
                    con.Close();
                    this.dataGridView3.DataSource = DS.Tables["收件人查询"];
                }
                else if (radioButton3.Checked && textBox1.Text != "")
                {
                    string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
                    SqlConnection con = new SqlConnection(Loadstring);
                    con.Open();
                    SqlDataAdapter SA;
                    DataSet DS = new DataSet();
                    string Sql = "select 订单.订单编号,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,快递点and集散中心表.点位 as 当前地址,用户登录信息表.姓名 as 用户名,订单.重量,订单.物品类型,订单.重要物品,订单.预计到达时间,订单.运费,订单.是否到货付款, 订单.是否完成"
                    + "  from 订单, 寄件人, 收件人, 快递点and集散中心表, 用户登录信息表, 当前地址表"
                    + "  where 寄件人.寄件人ID='" + textBox1.Text
                    + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.地址表ID = 当前地址表.地址表ID and 当前地址表.所属点ID =快递点and集散中心表.所属点ID and 订单.用户账号 =用户登录信息表.用户账号 ";
                    SA = new SqlDataAdapter(Sql, con);
                    SA.Fill(DS, "寄件人查询");
                    con.Close();
                    this.dataGridView3.DataSource = DS.Tables["寄件人查询"];
                }
                else if (radioButton4.Checked && textBox1.Text != "")
                {
                    string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
                    SqlConnection con = new SqlConnection(Loadstring);
                    con.Open();
                    SqlDataAdapter SA;
                    DataSet DS = new DataSet();
                    string Sql = "select 订单.订单编号,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,快递点and集散中心表.点位 as 当前地址,用户登录信息表.姓名 as 用户名,订单.重量,订单.物品类型,订单.重要物品,订单.预计到达时间,订单.运费,订单.是否到货付款, 订单.是否完成"
                    + "  from 订单, 寄件人, 收件人, 快递点and集散中心表, 用户登录信息表, 当前地址表"
                    + "  where 用户登录信息表.用户账号='" + textBox1.Text
                    + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.地址表ID = 当前地址表.地址表ID and 当前地址表.所属点ID =快递点and集散中心表.所属点ID and 订单.用户账号 =用户登录信息表.用户账号 ";
                    SA = new SqlDataAdapter(Sql, con);
                    SA.Fill(DS, "用户账号查询");
                    con.Close();
                    this.dataGridView3.DataSource = DS.Tables["用户账号查询"];
                }
                else
                {
                    string Loadstring1 = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
                    SqlConnection con1 = new SqlConnection(Loadstring1);
                    con1.Open();
                    SqlDataAdapter SA1;
                    DataSet DS1 = new DataSet();
                    string Sql1 = "select 订单.订单编号,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,快递点and集散中心表.点位 as 当前地址,用户登录信息表.姓名 as 用户名,订单.重量,订单.物品类型,订单.重要物品,订单.预计到达时间,订单.运费,订单.是否到货付款, 订单.是否完成"
                    + "  from 订单, 寄件人, 收件人, 快递点and集散中心表, 用户登录信息表, 当前地址表"
                    + "  where 订单.上门取件预约时间='" + dateTimePicker1.Text
                    + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.地址表ID = 当前地址表.地址表ID and 当前地址表.所属点ID =快递点and集散中心表.所属点ID and 订单.用户账号 =用户登录信息表.用户账号 ";
                    SA1 = new SqlDataAdapter(Sql1, con1);
                    SA1.Fill(DS1, "快递单号查询");
                    con1.Close();
                    this.dataGridView3.DataSource = DS1.Tables["快递单号查询"];
                    //MessageBox.Show(Sql1);
                }
            }
            catch {
                MessageBox.Show("查询条件下无内容", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.dataGridView3.SelectedRows.Count > 0)
            {
                //if (result == DialogResult.Yes)
                //{
                string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
                SqlConnection con = new SqlConnection(Loadstring);
                con.Open();
                SqlDataAdapter SA;
                DataSet DS = new DataSet();
                string str = this.dataGridView3.CurrentRow.Cells["订单编号"].Value.ToString();
                //MessageBox.Show(str);
                string Sql = "update 订单 set 是否完成= 1 where 订单编号 = '"
                + str
                + "' ";
                SA = new SqlDataAdapter(Sql, con);
                SA.Fill(DS, "用户账号修改");
                con.Close();
                this.dataGridView3.DataSource = DS.Tables["用户账号修改"];
                //修改当前行单元格的内容
                //}
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                //if (result == DialogResult.Yes)
                //{
                string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
                SqlConnection con = new SqlConnection(Loadstring);
                con.Open();
                SqlDataAdapter SA;
                DataSet DS = new DataSet();
                string str = this.dataGridView1.CurrentRow.Cells["订单编号"].Value.ToString();
                //MessageBox.Show(str);
                string Sql = "update 订单 set 是否完成= 1 where 订单编号 = '"
                + str
                + "' ";
                SA = new SqlDataAdapter(Sql, con);
                SA.Fill(DS, "用户账号修改");
                con.Close();
                this.dataGridView1.DataSource = DS.Tables["用户账号修改"];
                //修改当前行单元格的内容
                //}
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.SelectedRows.Count > 0)
            {
                //if (result == DialogResult.Yes)
                //{
                string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
                SqlConnection con = new SqlConnection(Loadstring);
                con.Open();
                SqlDataAdapter SA;
                DataSet DS = new DataSet();
                string ddbh = this.dataGridView2.CurrentRow.Cells["订单编号"].Value.ToString();
                //MessageBox.Show(str);

                string sql_sendSpotID = string.Format("select 所属点ID from 快递员 where 快递员ID = '{0}'",id);
                SA = new SqlDataAdapter(sql_sendSpotID, con);
                SA.Fill(DS, "SpotID");
                string sendSpotID = DS.Tables["SpotID"].Rows[0][0].ToString();
                //MessageBox.Show(sendSpotID);

                string basic_time = DateTime.Now.ToString();
                string basicTime = DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                string historyAddID = "His" + basicTime + id;
                string sql_insertHistoryAdd = string.Format("insert into 当前地址表(地址表ID,所属点ID,订单编号,到达时间) values('{0}','{1}','{2}','{3}')", historyAddID, sendSpotID,  ddbh, basic_time);
                string basic_sql = "update 订单 set 运费 ={0},揽件时间='{1}',重量={2},物品类型='{3}' ,地址表ID ='{4}' where 订单.订单编号 ='{5}'";
                
                /*try{*/
                    string Sql = string.Format(basic_sql, float.Parse(textBox3.Text), basic_time, float.Parse(textBox2.Text), comboBox1.SelectedItem.ToString(), historyAddID, ddbh);
                    //SA = new SqlDataAdapter(Sql, con);
                    SqlCommand comm1 = new SqlCommand(sql_insertHistoryAdd, con);
                    SqlCommand comm = new SqlCommand(Sql, con);
                    comm1.ExecuteNonQuery();
                    comm.ExecuteNonQuery();
                    MessageBox.Show("成功揽件", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.Close();
                /*}
                catch {
                    MessageBox.Show("请输入完整信息", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }*/

                //MessageBox.Show(Sql);
                SqlDataAdapter SA_new;
                DataSet DS_new = new DataSet();
                string Sql_new = "select 订单.订单编号, 寄件人.姓名 as 寄件人, 寄件人.寄件人ID as 电话, A.详细地址 as 收件地址,B.详细地址 as 揽件地址, 订单.重要物品, 订单.上门取件预约时间,订单.运费险"
                + "  from 订单,寄件人,收件人,地址 A,地址 B "
                + "  where 订单.揽件人ID = '" + id
                + "' and 订单.揽件时间 is null and 订单.寄件人ID = 寄件人.寄件人ID and 订单.收件人ID =收件人.收件人ID and A.地址ID = 收件人.地址 and B.地址ID = 寄件人.地址 and 订单.是否完成=0";
                SA_new = new SqlDataAdapter(Sql_new, con);
                SA_new.Fill(DS_new, "new");
                this.dataGridView2.DataSource = DS_new.Tables["new"];

                //修改当前行单元格的内容
            }
            //}

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string Loadstring1 = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection con1 = new SqlConnection(Loadstring1);
            con1.Open();
            SqlDataAdapter SA1;
            DataSet DS1 = new DataSet();
            string ddbh = this.dataGridView3.CurrentRow.Cells["订单编号"].Value.ToString();
            string Sql1 = "update 订单 set 订单.是否完成=1 where 订单.订单编号='"
            + ddbh
            + "'";
            SA1 = new SqlDataAdapter(Sql1, con1);
            SA1.Fill(DS1, "快递单号删除");
            con1.Close();
            this.dataGridView2.DataSource = DS1.Tables["快递单号删除"];

            SqlDataAdapter SA_new;
            DataSet DS_new = new DataSet();
            string Sql_new = "select 订单.订单编号, 寄件人.姓名 as 寄件人, 寄件人.寄件人ID as 电话, A.详细地址 as 收件地址,B.详细地址 as 揽件地址, 订单.重要物品, 订单.上门取件预约时间,订单.运费险"
            + "  from 订单,寄件人,收件人,地址 A,地址 B "
            + "  where 订单.揽件人ID = '" + id
            + "' and 订单.揽件时间 is null and 订单.寄件人ID = 寄件人.寄件人ID and 订单.收件人ID =收件人.收件人ID and A.地址ID = 收件人.地址 and B.地址ID = 寄件人.地址 and 订单.是否完成=0";
            SA_new = new SqlDataAdapter(Sql_new, con1);
            SA_new.Fill(DS_new, "new");
            this.dataGridView2.DataSource = DS_new.Tables["new"];
        }
    }
}
