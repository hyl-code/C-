using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AddressList
{
    public partial class Form4 : Form
    {
        DataSet DataSet1;
        string starpath = @"..\..\";    //相对路径

        public Form4()
        {
            InitializeComponent();
            ReadList();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void ReadList()                          //读取所有分组
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(starpath + @"List.xml");
            XmlNodeList xnl = xmlDocument.SelectSingleNode("List").ChildNodes;
            Recursion(xnl);
        }

        private void Recursion(XmlNodeList xnl)
        {
            foreach (XmlNode xd in xnl)
            {
                XmlElement xe = (XmlElement)xd;
                XmlNodeList nodes = xe.ChildNodes;
                Recursion(nodes);
                comboBox1.Items.Add(xe.Attributes["Text"].Value);      //加载分组
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)this.Owner;
            DataSet1= new DataSet();
            DataSet1.ReadXml(starpath + @"\AddressList.xml", XmlReadMode.Auto);
            DataView dv = DataSet1.Tables["PersonList"].DefaultView;
            dv.RowFilter = "姓名='" + textBox1.Text.Trim() + "' AND QQ='" + textBox2.Text.Trim() + 
                "' AND Email='" + textBox3.Text.Trim() + "' AND 电话='" + textBox4.Text.Trim() + "'"; //设置筛选条件
            form1.dataGridView1.DataSource = dv.ToTable();
            form1.Refresh();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
