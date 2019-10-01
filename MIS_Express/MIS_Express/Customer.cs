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
    public partial class Customer : Form
    {
        string id;
        public Customer(string id)
        {
            this.id = id;
            InitializeComponent();
            this.toolStripStatusLabel1.Text += id;
            this.Icon = new Icon(@"Pic&Ico\user2.ico");
            全部快递ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\inventory.ico");
            指定条件快递ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\search.ico");
            快递信息修改ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\package.ico");
            个人中心ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\user.ico");
            寄件ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\Sending.ico");
            退出ToolStripMenuItem.Image = Image.FromFile(@"Pic&Ico\logout.ico");

            panel1.BackgroundImage = Image.FromFile(@"Pic&Ico\bg.png");

            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);
            CN.Open();
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            string sql_person = string.Format("select 姓名,性别,联系方式 from 用户登录信息表 where 用户账号 = '{0}'", id);
            SA = new SqlDataAdapter(sql_person, CN);
            SA.Fill(DS, "Sheet1$");
            label9.Text = id;
            label8.Text = DS.Tables["Sheet1$"].Rows[0][0].ToString();
            label7.Text = DS.Tables["Sheet1$"].Rows[0][1].ToString();
            label6.Text = DS.Tables["Sheet1$"].Rows[0][2].ToString();
        }

        private void 快递信息修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            string Sql = "select 订单.订单编号,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,用户登录信息表.姓名 as 用户名,订单.重量,订单.物品类型,订单.重要物品,订单.预计到达时间,订单.运费,订单.是否到货付款"
            + "  from 订单, 寄件人, 收件人, 用户登录信息表"
            + "  where 订单.用户账号 = '" + id
            + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.用户账号 =用户登录信息表.用户账号 and 订单.揽件时间 is NULL and 订单.是否完成 = 0 ";
            SA = new SqlDataAdapter(Sql, con);
            SA.Fill(DS, "待揽件快递");
            this.dataGridView2.DataSource = DS.Tables["待揽件快递"];


            string Loadstring1 = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection con1 = new SqlConnection(Loadstring1);
            con1.Open();
            SqlDataAdapter SA1;
            DataSet DS1 = new DataSet();
            string Sql1 = "select 订单.订单编号,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,快递点and集散中心表.点位 as 当前地址,用户登录信息表.姓名 as 用户名,订单.重量,订单.物品类型,订单.重要物品,订单.预计到达时间,订单.运费,订单.是否到货付款"
            + "  from 订单, 寄件人, 收件人, 快递点and集散中心表, 用户登录信息表, 当前地址表"
            + "  where 订单.用户账号 = '"+id
            + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.地址表ID = 当前地址表.地址表ID and 当前地址表.所属点ID =快递点and集散中心表.所属点ID and 订单.用户账号 =用户登录信息表.用户账号 and 订单.揽件时间 is not null and 订单.是否完成 = 0";
            SA1 = new SqlDataAdapter(Sql1, con1);
            SA1.Fill(DS1, "已寄出快递");
            this.dataGridView1.DataSource = DS1.Tables["已寄出快递"];

        }

        private void 指定条件快递ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(groupBox3);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            InfoChange infoChange = new InfoChange(id);
            infoChange.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeCode changeCode = new ChangeCode(id,1);
            changeCode.ShowDialog();
        }

        private void 个人中心ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(groupBox4);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void 寄件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(groupBox2);
            textBox2.Text = "";textBox3.Text = "";textBox4.Text = "";textBox5.Text = "";textBox6.Text = "";textBox7.Text = "";
            comboBox1.Items.Clear();comboBox2.Items.Clear();comboBox3.Items.Clear();comboBox4.Items.Clear();comboBox5.Items.Clear();comboBox6.Items.Clear();comboBox7.Items.Clear();
            checkBox1.Checked = false;checkBox2.Checked = false;checkBox3.Checked = false;
            radioButton3.Checked = false;radioButton4.Checked = false;
            
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);
            DataSet DSProvince = new DataSet();
            CN.Open();
            SqlDataAdapter SA;
            string sql_Province = "select 省 from 行政区 group by(省)";
            SA = new SqlDataAdapter(sql_Province, CN);
            SA.Fill(DSProvince, "SheetProvince$");
            int provinceRows = DSProvince.Tables[0].Rows.Count;
            for (int i = 0; i < provinceRows; i = i + 1)
            {
                comboBox1.Items.Add(DSProvince.Tables["SheetProvince$"].Rows[i][0].ToString());
                comboBox6.Items.Add(DSProvince.Tables["SheetProvince$"].Rows[i][0].ToString());
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Enabled = true;
            dateTimePicker2.Visible = true;

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Enabled = false;
            dateTimePicker2.Visible = false;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string ddbh = this.dataGridView1.CurrentRow.Cells["订单编号"].Value.ToString();
            HistoryAddress history = new HistoryAddress(ddbh);
            history.ShowDialog();
        }

        private void Customer_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Adbook adbook = new Adbook(id);
            adbook.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Adbook adbook = new Adbook(id);
            adbook.ShowDialog();
        }

        private void dataGridView3_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string ddbh = this.dataGridView3.CurrentRow.Cells["订单编号"].Value.ToString();
            HistoryAddress history = new HistoryAddress(ddbh);
            history.ShowDialog();
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);
            CN.Open();
            string title = "错误提示！";

            string receiverName = textBox3.Text;
            string receiverPhone = textBox4.Text;
            string receiverDetailAddress = textBox2.Text;
            string receiverProvince = comboBox1.Text;
            string receiverCity = comboBox2.Text;
            string receiverArea = comboBox3.Text;

            string senderName = textBox6.Text;
            string senderPhone = textBox5.Text;
            string senderDetailAddress = textBox7.Text;
            string senderProvince = comboBox6.Text;
            string senderCity = comboBox5.Text;
            string senderArea = comboBox4.Text;

            int payNow = 0;if (checkBox2.Checked) { payNow = 1; }
            float insurance = 0;if (checkBox1.Checked) { insurance = 9.9F; }
            int significance = 0;if (checkBox3.Checked) { significance = 1; }
            string sendSpot = comboBox7.Text;

            if (sendSpot == "请选择寄件点") {
                MessageBox.Show("请选择寄件点", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {
                if (receiverName == "" || receiverPhone == "" || receiverDetailAddress == "" || receiverProvince == "省" || receiverCity == "市" || receiverArea == "区"
                || senderName == "" || senderPhone == "" || senderDetailAddress == "" || senderProvince == "省" || senderCity == "市" || senderArea == "区"
                || sendSpot == "请选择寄件点")
                {
                    MessageBox.Show("信息不完整，请重新输入", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (radioButton4.Checked == false && radioButton3.Checked == false)
                    {
                        MessageBox.Show("请选择一种寄送方式", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        string sql_spotID = string.Format("select 所属点ID from 快递点and集散中心表 where 省 = '{0}' and 市 = '{1}'  and 点位 = '{2}'", comboBox6.Text, comboBox5.Text, comboBox7.Text);
                        SqlDataAdapter SA;
                        DataSet DS = new DataSet();
                        SA = new SqlDataAdapter(sql_spotID, CN);
                        SA.Fill(DS, "Sheet1$");
                        string sendSpotID = "";
                        sendSpotID = DS.Tables["Sheet1$"].Rows[0][0].ToString().Replace(" ", "");

                        string basicTime = DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        string senderAddID = "SA" + basicTime + id;
                        string receiverAddID = "RA" + basicTime + id;
                        string senderID = "Sr" + basicTime + id;
                        string receiverID = "Rr" + basicTime + id;
                        //string historyAddID = "His" + basicTime + sendSpotID;
                        string sql_insertReceiverAdd = string.Format("insert into 地址(地址ID,省,市,区,详细地址) values('{0}','{1}','{2}','{3}','{4}')", receiverAddID, receiverProvince, receiverCity, receiverArea, receiverDetailAddress);
                        string sql_insertSenderAdd = string.Format("insert into 地址(地址ID,省,市,区,详细地址) values('{0}','{1}','{2}','{3}','{4}')", senderAddID, senderProvince, senderCity, senderArea, senderDetailAddress);
                        string sql_insertReceiver = string.Format("insert into 收件人(收件人ID,姓名,地址,电话) values('{0}','{1}','{2}','{3}')", receiverID, receiverName, receiverAddID, textBox4.Text);
                        string sql_insertSender = string.Format("insert into 寄件人(寄件人ID,姓名,地址,电话) values('{0}','{1}','{2}','{3}')", senderID, senderName, senderAddID, textBox5.Text);
                        //string sql_insertHistoryAdd = string.Format("insert into 当前地址表(地址表ID,所属点ID) values('{0}','{1}')", historyAddID, sendSpotID);
                        SqlCommand commRA = new SqlCommand(sql_insertReceiverAdd, CN);
                        SqlCommand commSA = new SqlCommand(sql_insertSenderAdd, CN);
                        SqlCommand commRr = new SqlCommand(sql_insertReceiver, CN);
                        SqlCommand commSr = new SqlCommand(sql_insertSender, CN);
                        //SqlCommand commHis = new SqlCommand(sql_insertHistoryAdd, CN);
                        commRA.ExecuteNonQuery();
                        commSA.ExecuteNonQuery();
                        commRr.ExecuteNonQuery();
                        commSr.ExecuteNonQuery();
                        //commHis.ExecuteNonQuery();
                        if (radioButton4.Checked)
                        {//寄件点寄件
                            string orderID = "Sp" + basicTime + id + sendSpotID;
                            string sql_basicInsertOrderBySpot = "insert into 订单(订单编号,重要物品,是否到货付款,运费险,寄件人ID,收件人ID,用户账号,快递点,是否完成) values('{0}', {1}, {2}, {3}, '{4}', '{5}', '{6}', '{7}',{8})";
                            string sql_insertOrderBySpot = string.Format(sql_basicInsertOrderBySpot, orderID, significance, payNow, insurance, senderID, receiverID, id, sendSpotID,0);
                            SqlCommand commOrder = new SqlCommand(sql_insertOrderBySpot, CN);
                            commOrder.ExecuteNonQuery();
                            MessageBox.Show("您的寄件单已提交，请在3日内前往子营业点", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = "";
                            comboBox1.Items.Clear(); comboBox2.Items.Clear(); comboBox3.Items.Clear(); comboBox4.Items.Clear(); comboBox5.Items.Clear(); comboBox6.Items.Clear(); comboBox7.Items.Clear();
                            checkBox1.Checked = false; checkBox2.Checked = false; checkBox3.Checked = false;
                            radioButton3.Checked = false; radioButton4.Checked = false;
                        }
                        else
                        {
                            if (radioButton3.Checked)
                            {//上门取货寄件
                                string homeTime = dateTimePicker2.Value.ToString();
                                string orderID = "Ho" + basicTime + id + sendSpotID;
                                string sql_basicInsertOrderByHome = "insert into 订单(订单编号,重要物品,是否到货付款,运费险,寄件人ID,收件人ID,用户账号,快递点,上门取件预约时间,是否完成) values('{0}', {1}, {2}, {3}, '{4}', '{5}', '{6}', '{7}','{8}',{9})";
                                string sql_insertOrderByHome = string.Format(sql_basicInsertOrderByHome, orderID, significance, payNow, insurance, senderID, receiverID, id,  sendSpotID, homeTime,0);
                                SqlCommand commOrder = new SqlCommand(sql_insertOrderByHome, CN);
                                commOrder.ExecuteNonQuery();
                                MessageBox.Show("您的寄件单已提交，我们会在您的预约时间前后一小时到达", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = "";
                                comboBox1.Items.Clear(); comboBox2.Items.Clear(); comboBox3.Items.Clear(); comboBox4.Items.Clear(); comboBox5.Items.Clear(); comboBox6.Items.Clear(); comboBox7.Items.Clear();
                                checkBox1.Checked = false; checkBox2.Checked = false; checkBox3.Checked = false;
                                radioButton3.Checked = false; radioButton4.Checked = false;
                            }
                        }
                    }
                }
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

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Items.Clear();
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);

            try
            {
                string sql_City = string.Format("select 市 from 行政区 where 省='{0}' group by(市)", comboBox6.Text);
                DataSet DSCity = new DataSet();
                SqlDataAdapter SA;
                SA = new SqlDataAdapter(sql_City, CN);
                SA.Fill(DSCity, "SheetCity$");
                int cityRows = DSCity.Tables[0].Rows.Count;
                for (int i = 0; i < cityRows; i = i + 1)
                {
                    comboBox5.Items.Add(DSCity.Tables["SheetCity$"].Rows[i][0].ToString());
                }
            }
            catch
            {
                MessageBox.Show("请先选择省份", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);

            try
            {
                string sql_Area = string.Format("select 区 from 行政区 where 市='{0}' group by(区)", comboBox5.Text);
                DataSet DSArea = new DataSet();
                SqlDataAdapter SA;
                SA = new SqlDataAdapter(sql_Area, CN);
                SA.Fill(DSArea, "SheetArea$");
                int areaRows = DSArea.Tables[0].Rows.Count;
                for (int i = 0; i < areaRows; i = i + 1)
                {
                    comboBox4.Items.Add(DSArea.Tables["SheetArea$"].Rows[i][0].ToString());
                }
            }
            catch
            {
                MessageBox.Show("请先选择省份", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox7.Items.Clear();
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection CN = new SqlConnection(Loadstring);

            try
            {
                string sql_SpotNear = string.Format("select 点位 from 快递点and集散中心表 where 省 = '{0}' and 市 = '{1}' and 区 = '{2}' and 类型 = 'false'", comboBox6.Text,comboBox5.Text,comboBox4.Text);
                string sql_SpotOther = string.Format("select 点位 from 快递点and集散中心表 where 省 = '{0}' and 市 = '{1}' and 区 <> '{2}' and 类型 = 'false'", comboBox6.Text, comboBox5.Text, comboBox4.Text);
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
                    comboBox7.Items.Add(DSSpotNear.Tables["SheetSpotNear$"].Rows[i][0].ToString());
                }
                for (int i = 0; i < OtherRows; i = i + 1)
                {
                    comboBox7.Items.Add(DSSpotOther.Tables["SheetSpotOther$"].Rows[i][0].ToString());
                }
            }
            catch
            {
                MessageBox.Show("请先选择省份", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
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
                    string Sql = "select 订单.订单编号,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,快递点and集散中心表.点位 as 当前地址,用户登录信息表.姓名 as 用户名,订单.重量,订单.物品类型,订单.重要物品,订单.预计到达时间,订单.运费,订单.是否到货付款"
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
                    string Sql = "select 订单.订单编号,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,快递点and集散中心表.点位 as 当前地址,用户登录信息表.姓名 as 用户名,订单.重量,订单.物品类型,订单.重要物品,订单.预计到达时间,订单.运费,订单.是否到货付款"
                    + "  from 订单, 寄件人, 收件人, 快递点and集散中心表, 用户登录信息表, 当前地址表"
                    + "  where 收件人.姓名 = '" + textBox1.Text
                    + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.地址表ID = 当前地址表.地址表ID and 当前地址表.所属点ID =快递点and集散中心表.所属点ID and 订单.用户账号 =用户登录信息表.用户账号 and 订单.用户账号 = '" + "zsl            " + "'";
                    SA = new SqlDataAdapter(Sql, con);
                    SA.Fill(DS, "收件人查询");
                    con.Close();
                    this.dataGridView3.DataSource = DS.Tables["收件人查询"];
                }
                else
                {
                    string Loadstring1 = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
                    SqlConnection con1 = new SqlConnection(Loadstring1);
                    con1.Open();
                    SqlDataAdapter SA1;
                    DataSet DS1 = new DataSet();
                    string Sql1 = "select 订单.订单编号,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,快递点and集散中心表.点位 as 当前地址,用户登录信息表.姓名 as 用户名,订单.重量,订单.物品类型,订单.重要物品,订单.预计到达时间,订单.运费,订单.是否到货付款"
                    + "  from 订单, 寄件人, 收件人, 快递点and集散中心表, 用户登录信息表, 当前地址表"
                    + "  where 订单.上门取件预约时间='" + dateTimePicker1.Text
                    + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.地址表ID = 当前地址表.地址表ID and 当前地址表.所属点ID =快递点and集散中心表.所属点ID and 订单.用户账号 =用户登录信息表.用户账号 and 订单.用户账号 = '" + "zsl            " + "'";
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

        private void button6_Click(object sender, EventArgs e)
        {
            string Loadstring1 = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection con1 = new SqlConnection(Loadstring1);
            con1.Open();
            SqlDataAdapter SA1;
            DataSet DS1 = new DataSet();
            string ddbh = this.dataGridView2.CurrentRow.Cells["订单编号"].Value.ToString();
            string Sql1 = "update 订单 set 订单.是否完成=1 where 订单.订单编号='"
            +ddbh
            + "'";
            SA1 = new SqlDataAdapter(Sql1, con1);
            SA1.Fill(DS1, "快递单号删除");
            con1.Close();
            this.dataGridView2.DataSource = DS1.Tables["快递单号删除"];

        }
    }
}
