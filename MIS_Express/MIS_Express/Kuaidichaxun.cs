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
    public partial class Kuaidichaxun : Form
    {
        public Kuaidichaxun()
        {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile(@"Pic&Ico\bg.png");
        }

        private void Kuaidichaxun_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(@"Pic&Ico\search.ico");
        }

        private void dataGridView3_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string ddbh = this.dataGridView3.CurrentRow.Cells["订单编号"].Value.ToString();
            HistoryAddress history = new HistoryAddress(ddbh);
            history.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection con = new SqlConnection(Loadstring);
            con.Open();
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            string Sql = "select 订单.订单编号,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,快递点and集散中心表.点位 as 当前地址,用户登录信息表.姓名 as 用户名,订单.重量,订单.物品类型,订单.重要物品,订单.预计到达时间,订单.运费,订单.是否到货付款"
            + "  from 订单, 寄件人, 收件人, 快递点and集散中心表, 用户登录信息表, 当前地址表"
            + "  where 订单.订单编号 = '" + textBox1.Text
            + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.地址表ID = 当前地址表.地址表ID and 当前地址表.所属点ID =快递点and集散中心表.所属点ID and 订单.用户账号 =用户登录信息表.用户账号";
            SA = new SqlDataAdapter(Sql, con);
            SA.Fill(DS, "查询快递信息");
            con.Close();
            this.dataGridView3.DataSource = DS.Tables["查询快递信息"];

        }
    }
}
