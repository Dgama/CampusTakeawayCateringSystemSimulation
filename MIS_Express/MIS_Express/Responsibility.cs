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
    public partial class Responsibility : Form
    {
        private string dz;
        public Responsibility(string dz)
        {
            this.dz = dz;
            InitializeComponent();
            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection con = new SqlConnection(Loadstring);
            con.Open();
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            string Sql = "select 订单.订单编号,订单.物品类型, 订单.重要物品,寄件人.姓名 as 寄件人,收件人.姓名 as 收件人,快递点and集散中心表.点位 as 当前地址,用户登录信息表.姓名 as 用户名,订单.重量 "
            + "  from 订单,寄件人,收件人,快递点and集散中心表,用户登录信息表,当前地址表"
            + "  where 订单.快递员ID ='" + dz
            + "' and 寄件人.寄件人ID = 订单.寄件人ID and 收件人.收件人ID = 订单.收件人ID and 订单.地址表ID = 当前地址表.地址表ID and 当前地址表.所属点ID =快递点and集散中心表.所属点ID and 订单.用户账号 =用户登录信息表.用户账号";
            SA = new SqlDataAdapter(Sql, con);
            SA.Fill(DS, "快递员信息");
            this.dataGridView1.DataSource = DS.Tables["快递员信息"];

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }
    }
}
