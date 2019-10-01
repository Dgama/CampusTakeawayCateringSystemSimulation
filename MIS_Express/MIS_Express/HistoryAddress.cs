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
    public partial class HistoryAddress : Form
    {
        string id;
        public HistoryAddress(string id)
        {
            this.id = id;
            InitializeComponent();
            label1.Text = label1.Text + id;

            string Loadstring = "Server=DESKTOP-B174P17;DataBase=快递;Trusted_Connection=SSPI";
            SqlConnection con = new SqlConnection(Loadstring);
            con.Open();
            SqlDataAdapter SA;
            DataSet DS = new DataSet();
            string basic_sql = "select 快递点and集散中心表.点位 as 地址, 当前地址表.到达时间 from 当前地址表, 快递点and集散中心表 where 当前地址表.订单编号 = '{0}' and 当前地址表.所属点ID = 快递点and集散中心表.所属点ID order by 当前地址表.地址表ID ";
            string Sql = string.Format(basic_sql, id);
            SA = new SqlDataAdapter(Sql, con);
            SA.Fill(DS, "sheet");
            this.dataGridView1.DataSource = DS.Tables["sheet"];
        }

        private void HistoryAddress_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(@"Pic&Ico\location.ico");
        }
    }
}
