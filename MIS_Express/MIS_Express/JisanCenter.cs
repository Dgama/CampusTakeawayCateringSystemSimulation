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
    public partial class JisanCenter : Form
    {
        string name_id;
        string id;
        public JisanCenter(string name_id,string id)
        {
            this.name_id = name_id;//登陆的账号
            this.id = id;//对应所在点位的ID
            InitializeComponent();
            this.toolStripStatusLabel1.Text += name_id;
            this.Icon = new Icon(@"Pic&Ico\user2.ico");
            库存快递ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\inventory.ico");
            在途快递ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\onway.ico");
            快递信息修改ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\modify.ico");
            个人中心ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\user.ico");
            退出ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\logout.ico");

            panel1.BackgroundImage = Image.FromFile(@"Pic&Ico\bg.png");

            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            string sql_person = string.Format("select 姓名,性别,联系方式 from 商家登录信息表 where 账号 = '{0}'", name_id);
            SA = new SqlDataAdapter(sql_person, CN);
            SA.Fill(DS, "Sheet1$");
            label9.Text = name_id;
            label8.Text = DS.Tables["Sheet1$"].Rows[0][0].ToString();
            label7.Text = DS.Tables["Sheet1$"].Rows[0][1].ToString();
            label6.Text = DS.Tables["Sheet1$"].Rows[0][2].ToString();
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 帮助HToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void 库存快递ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(groupBox1);
            comboBox1.Items.Clear();
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection con = new SqlConnection(Loadstring);
            con.Open();
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            string Sql = "select 订单.订单编号,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,快递点and集散中心表.点位 as 当前地址,用户登录信息表.姓名 as 用户名,订单.重量,订单.物品类型,订单.重要物品,订单.揽件时间,当前地址表.到达时间,当前地址表.发出时间,地址.省,地址.市,地址.区,地址.详细地址"
            + "  from 订单, 寄件人, 收件人, 快递点and集散中心表, 用户登录信息表, 当前地址表,地址"
            + "  where 快递点and集散中心表.所属点ID = '" + id
            + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.地址表ID = 当前地址表.地址表ID and 当前地址表.所属点ID =快递点and集散中心表.所属点ID and 订单.用户账号 =用户登录信息表.用户账号 and 当前地址表.发出时间 is null and 当前地址表.到达时间 is not null and 快递点and集散中心表.类型 = 1 and 订单.是否完成=0 and 收件人.地址 = 地址.地址ID";
            SA = new SqlDataAdapter(Sql, con);
            SA.Fill(DS, "集散库存快递");
            this.dataGridView1.DataSource = DS.Tables["集散库存快递"];

            DataSet DSProvince = new DataSet();
            string sql_Province = "select 省 from 行政区 group by(省)";
            SA = new SqlDataAdapter(sql_Province, con);
            SA.Fill(DSProvince, "SheetProvince$");
            int provinceRows = DSProvince.Tables[0].Rows.Count;
            for (int i = 0; i < provinceRows; i = i + 1)
            {
                comboBox1.Items.Add(DSProvince.Tables["SheetProvince$"].Rows[i][0].ToString());
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void 在途快递ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(groupBox2);

            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection con = new SqlConnection(Loadstring);
            con.Open();
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            string Sql = "select 订单.订单编号,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,快递点and集散中心表.点位 as 当前地址,用户登录信息表.姓名 as 用户名,订单.重量,订单.物品类型,订单.重要物品,订单.揽件时间,当前地址表.到达时间,当前地址表.发出时间,地址.省,地址.市,地址.区,地址.详细地址"
            + "  from 订单, 寄件人, 收件人, 快递点and集散中心表, 用户登录信息表, 当前地址表,地址"
            + "  where 快递点and集散中心表.所属点ID = '"+ id
            + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.地址表ID = 当前地址表.地址表ID and 当前地址表.所属点ID =快递点and集散中心表.所属点ID and 订单.用户账号 =用户登录信息表.用户账号 and 当前地址表.发出时间 is null and 当前地址表.到达时间 is null and 快递点and集散中心表.类型 = 1 and 订单.是否完成=0 and 收件人.地址=地址.地址ID";
            SA = new SqlDataAdapter(Sql, con);
            SA.Fill(DS, "集散在途快递");
            con.Close();
            this.dataGridView2.DataSource = DS.Tables["集散在途快递"];

        }

        private void 快递信息修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(groupBox3);
        }

        private void label4_Click(object sender, EventArgs e)
        {

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
            ChangeCode changeCode = new ChangeCode(id,2);
            changeCode.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void JisanCenter_Load(object sender, EventArgs e)
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

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //HistoryAddress history = new HistoryAddress();
            //history.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //string basicTime = DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            string ddbh = this.dataGridView2.CurrentRow.Cells["订单编号"].Value.ToString();
            string basic_sql = "update 当前地址表 set 到达时间 = '{0}' where 当前地址表.地址表ID = (select 地址表ID from 订单 where 订单编号 = '{1}')";
            string sql = string.Format(basic_sql, DateTime.Now.ToString(), ddbh);

            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);
            CN.Open();
            SqlCommand comm = new SqlCommand(sql, CN);
            comm.ExecuteNonQuery();
            CN.Close();

            CN.Open();
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            string Sql = "select 订单.订单编号,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,快递点and集散中心表.点位 as 当前地址,用户登录信息表.姓名 as 用户名,订单.重量,订单.物品类型,订单.重要物品,订单.揽件时间,当前地址表.到达时间,当前地址表.发出时间"
            + "  from 订单, 寄件人, 收件人, 快递点and集散中心表, 用户登录信息表, 当前地址表"
            + "  where 快递点and集散中心表.所属点ID = '" + id
            + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.地址表ID = 当前地址表.地址表ID and 当前地址表.所属点ID =快递点and集散中心表.所属点ID and 订单.用户账号 =用户登录信息表.用户账号 and 当前地址表.发出时间 is null and 当前地址表.到达时间 is null and 快递点and集散中心表.类型 = 1 and 订单.是否完成=0";
            SA = new SqlDataAdapter(Sql, CN);
            SA.Fill(DS, "集散在途快递");
            CN.Close();
            this.dataGridView2.DataSource = DS.Tables["集散在途快递"];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
                SqlConnection con = new SqlConnection(Loadstring);
                con.Open();
                SqlDataAdapter SA;
                DataSet DS = new DataSet();
                string ddbh = this.dataGridView1.CurrentRow.Cells["订单编号"].Value.ToString();
                string next_id = "";
                string sql_next_id = string.Format("select 所属点ID from 快递点and集散中心表 where 省 = '{0}' and 市 = '{1}' and 区 = '{2}' and 点位 ='{3}'", comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text);
                SqlDataAdapter SA1;
                DataSet DS1 = new DataSet();
                SA1 = new SqlDataAdapter(sql_next_id, con);
                SA1.Fill(DS1, "sheet");
                next_id = DS1.Tables["sheet"].Rows[0][0].ToString();
                string basicTime = DateTime.Now.ToString();
                /*string Sql = "update 当前地址表 set 发出时间 = '"
                + DateTime.Now.Year.ToString() + "-"
                + DateTime.Now.Month.ToString() + "-"
                + DateTime.Now.Day.ToString() + " "
                + DateTime.Now.Hour.ToString() + ":"
                + DateTime.Now.Minute.ToString() + ":"
                + DateTime.Now.Second.ToString()
                + "' "
                 + "  where 当前地址表.地址表ID = (select 地址表ID from 订单 where 订单编号 = '"
                + ddbh
                + "') "
                + "insert into 当前地址表(地址表ID,所属点ID) values('"
                + "His" + basicTime + id + "To" + next_id
                + "','"
                +next_id
                + "') "
                + "  update 当前地址表 set 上一地址表ID = (select 地址表ID from 订单 where 订单编号 = '"
                + ddbh
                + "') "
                //+ "  where 当前地址表.地址表ID = (select 地址表ID from 订单 where 订单编号 = '"
                //+ ddbh
                //+ "') "
                + "  update 订单 set 地址表ID = '"
                + "His" + basicTime + id + "To" + next_id
                + "' ";
                SA = new SqlDataAdapter(Sql, con);
                SA.Fill(DS, "更新时间和地址");
                con.Close();
                this.dataGridView2.DataSource = DS.Tables["更新时间和地址"];
                //修改当前行单元格的内容}*/
                string Sql = "update 当前地址表 set 发出时间 = '"
            + DateTime.Now.Year.ToString() + "-"
            + DateTime.Now.Month.ToString() + "-"
            + DateTime.Now.Day.ToString() + " "
            + DateTime.Now.Hour.ToString() + ":"
            + DateTime.Now.Minute.ToString() + ":"
            + DateTime.Now.Second.ToString()
            + "' "
             + "  where 当前地址表.地址表ID = (select 地址表ID from 订单 where 订单编号 = '"
            + ddbh
            + "') "
            + "insert into 当前地址表(地址表ID,所属点ID) values('"
            + "His" + basicTime + id + "To" + next_id
            + "','"
            + next_id
            + "') "
            + "  update 当前地址表 set 上一地址表ID = (select 地址表ID from 订单 where 订单编号 = '"
            + ddbh
            + "') "
            + "  update 订单 set 地址表ID = '"
            + "His" + basicTime + id + "To" + next_id
            + "' where 订单编号 = '" + ddbh + "'";
                SA = new SqlDataAdapter(Sql, con);
                SA.Fill(DS, "更新时间和地址");
                con.Close();
                this.dataGridView1.DataSource = DS.Tables["更新时间和地址"];
                //修改当前行单元格的内容}
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);

            try
            {
                string sql_City = string.Format("select 市 from 行政区 where 省='{0}' group by(市)", comboBox1.Text);
                DataSet DSCity = new DataSet();
                SqlDataAdapter SA;
                SA = new SqlDataAdapter(sql_City, CN);
                SA.Fill(DSCity, "SheetCity$");
                int cityRows = DSCity.Tables[0].Rows.Count;
                for (int i = 0; i < cityRows; i = i + 1)
                {
                    comboBox2.Items.Add(DSCity.Tables["SheetCity$"].Rows[i][0].ToString());
                }
            }
            catch
            {
                MessageBox.Show("请先选择省份", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);

            try
            {
                string sql_SpotNear = string.Format("select 点位 from 快递点and集散中心表 where 省 = '{0}' and 市 = '{1}' and 区 = '{2}' and 类型 = 'false'", comboBox1.Text, comboBox2.Text, comboBox3.Text);
                string sql_SpotOther = string.Format("select 点位 from 快递点and集散中心表 where 省 = '{0}' and 市 = '{1}' and 区 = '{2}' and 类型 = 'true'", comboBox1.Text, comboBox2.Text, comboBox3.Text);
                DataSet DSSpotNear = new DataSet();
                DataSet DSSpotOther = new DataSet();
                SqlDataAdapter SAnear;
                SqlDataAdapter SAother;
                SAnear = new SqlDataAdapter(sql_SpotNear, CN);
                SAother = new SqlDataAdapter(sql_SpotOther, CN);
                SAnear.Fill(DSSpotNear, "SheetSpotNear$");
                SAother.Fill(DSSpotOther, "SheetSpotOther$");
                int NearRows = DSSpotNear.Tables[0].Rows.Count;
                int OtherRows = DSSpotOther.Tables[0].Rows.Count;
                for (int i = 0; i < NearRows; i = i + 1)
                {
                    comboBox4.Items.Add(DSSpotNear.Tables["SheetSpotNear$"].Rows[i][0].ToString());
                }
                for (int i = 0; i < OtherRows; i = i + 1)
                {
                    comboBox4.Items.Add(DSSpotOther.Tables["SheetSpotOther$"].Rows[i][0].ToString());
                }
            }
            catch
            {
                MessageBox.Show("请先选择省份", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);

            try
            {
                string sql_Area = string.Format("select 区 from 行政区 where 市='{0}' group by(区)", comboBox2.Text);
                DataSet DSArea = new DataSet();
                SqlDataAdapter SA;
                SA = new SqlDataAdapter(sql_Area, CN);
                SA.Fill(DSArea, "SheetArea$");
                int areaRows = DSArea.Tables[0].Rows.Count;
                for (int i = 0; i < areaRows; i = i + 1)
                {
                    comboBox3.Items.Add(DSArea.Tables["SheetArea$"].Rows[i][0].ToString());
                }
            }
            catch
            {
                MessageBox.Show("请先选择省份", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                    + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.地址表ID = 当前地址表.地址表ID and 当前地址表.所属点ID =快递点and集散中心表.所属点ID and 订单.用户账号 =用户登录信息表.用户账号 ";
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
            catch { MessageBox.Show("搜索条件有误", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
    }
}
