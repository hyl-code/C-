using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressList
{
    public partial class Form1 : Form
    {
        DataSet DataSet1;
        string starpath = @"..\..\";                        //相对路径
        public static int allPersonNum;                     //通讯录中的总人数

        public Form1()
        {
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e) //加载窗体
        {
            toolStripStatusLabel1.Text = "登陆时间" + DateTime.Now.ToString();
            toolStripButton1_Click(null, null);                     //读取所有联系人信息
            /*读取组并显示在treeView1上*/
            treeView1.Nodes.Clear();
            TreeXML treeXML = new TreeXML();
            treeXML.XMLToTree(starpath + @"\List.xml", treeView1);  //调用 XMLToTree 方法
        }

        private void toolStripButton1_Click(object sender, EventArgs e) //读取所有通讯录信息
        {
            DataSet1 = new DataSet();                       //实例化 DataSet1
            DataSet1.ReadXml(starpath + @"\AddressList.xml", XmlReadMode.Auto);     //读取联络人信息
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = DataSet1;            //DataSet1 作为 dataGridView1 的数据源
            dataGridView1.DataMember = "PersonList";
            allPersonNum = dataGridView1.RowCount;          //获取总的联系人数
            toolStripStatusLabel2.Text = "共有" + allPersonNum.ToString() + "条记录";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(true);
            form3.ShowDialog();                                 //弹出 Form3 窗体
            toolStripButton1_Click(null, null);                 //重新读取
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Owner = this;
            form4.ShowDialog();                                 //弹出 Form4 窗体
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            /*获取选中的行的集合*/
            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;
            if (selectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("你确定要删除选定的数据吗?", "删除数据",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in selectedRows)            //迭代删除选中行
                    {
                        int i = Convert.ToInt32(row.Cells["ID"].Value);     //读取ID并转换为int类型
                        DataSet myDataSet = new DataSet();
                        myDataSet.ReadXml(starpath + @"\AddressList.xml");  //读取联系人到数据集中
                        myDataSet.Tables["PersonList"].Rows[i - 1].Delete();  //在数据集中删除
                        myDataSet.WriteXml(starpath + @"\AddressList.xml"); //保存删除后的XML文件
                        MessageBox.Show("删除成功！");
                        toolStripButton1_Click(null, null);                 //重新读取联系人
                    }
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            DataSet1 = new DataSet();
            DataSet1.ReadXml(starpath + @"\AddressList.xml", XmlReadMode.Auto);
            DataView dv = DataSet1.Tables["PersonList"].DefaultView;
            dv.RowFilter = "姓名='" + toolStripTextBox1.Text.Trim() + "'";       // 设置筛选条件
            dataGridView1.DataSource = dv.ToTable();                            // 显示搜索结果
        }

        private void toolStripButton6_Click(object sender, EventArgs e)//添加组
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();                                 //弹出添加修改分组窗体
            if (form2.groupName != "")
            {
                TreeNode tn = new TreeNode();
                tn.Text = form2.groupName;
                /*写入XML文件*/
                if (treeView1.SelectedNode.Parent == null)       //判断是否是父节点
                {
                    treeView1.Nodes.Add(tn);
                    TreeXML TreXml = new TreeXML();
                    TreXml.AddXmlNode(starpath + @"\List.xml", form2.groupName);
                }
                else
                {
                    treeView1.SelectedNode.Parent.Nodes.Add(tn);
                    TreeXML TreXml = new TreeXML();
                    TreXml.AddChildXmlNode(starpath + @"\List.xml", treeView1.SelectedNode.Parent.Text, form2.groupName);
                }
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)//删除组
        {
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("请选择一个节点", "提示信息", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
            }
            else
            {
                DialogResult dResult = MessageBox.Show("确定要删除此节点所有内容吗?", "删除节点",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (dResult)
                {
                    case DialogResult.Yes:
                        TreeXML TreXml = new TreeXML();
                        TreXml.DeleXml(starpath + @"\List.xml", treeView1.SelectedNode.Text);
                        treeView1.SelectedNode.Remove();            //删除节点
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        break;
                }
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)//修改组
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            if (form2.groupName != "")
            {
                if (treeView1.SelectedNode == null)
                {
                    MessageBox.Show("请选择一个节点", "提示信息", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    TreeXML TreeXml = new TreeXML();
                    TreeXml.AlterXml(starpath + @"\List.xml", treeView1.SelectedNode.Text, form2.groupName);
                    TreeXml.AlterGroup(starpath + @"\AddressList.xml", treeView1.SelectedNode.Text, form2.groupName);
                    treeView1.SelectedNode.Text = form2.groupName;
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeXML treeXML = new TreeXML();            //实例化一个 TreeXML 对象
            dataGridView1.DataSource = treeXML.GetPersonInfo(starpath + @"\AddressList.xml",
                treeView1.SelectedNode.Text);           //调用 GetPersonInfo 方法
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                /*获取所选择人的 ID */
                string PersonId = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                Form3 form3 = new Form3(PersonId);
                form3.ShowDialog();                             //显示联系人详细信息
            }
        }

        private void 添加下级分组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            if(form2.groupName != "")
            {
                TreeNode tn = new TreeNode();
                tn.Text = form2.groupName;
                treeView1.SelectedNode.Nodes.Add(tn);
                /*写入XML文件*/
                if(treeView1.SelectedNode != null)
                {
                    TreeXML TreXml = new TreeXML();
                    TreXml.AddChildXmlNode(starpath + @"\List.xml", treeView1.SelectedNode.Text, form2.groupName);
                }
            }
        }

        private void 关于通讯录AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此通讯录的版本为 V1.0！");
        }

        private void 退出系统EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
