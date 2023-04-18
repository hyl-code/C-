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
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq.Expressions;

namespace BOOKSYS
{
    public partial class Form5 : Form
    {
        string strcon = BOOKSYS.Properties.Settings.Default.MBOOKConnectionString;  //获得连接字符串
        string FileNamePath = "";                                                   //保存图片路径
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“mBOOKDataSet4.TBook”中。您可以根据需要移动或移除它。
            this.tBookTableAdapter2.Fill(this.mBOOKDataSet4.TBook);
            // TODO: 这行代码将数据加载到表“mBOOKDataSet1.TBook”中。您可以根据需要移动或移除它。
            this.tBookTableAdapter1.Fill(this.mBOOKDataSet1.TBook);
        }

        private void button1_Click(object sender, EventArgs e)          //图书删除
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入图书书名！");
                return;
            }
            SqlConnection conn = new SqlConnection(strcon);
            string sqlStr = "Delete From [TBook] where [BookName]=@BookName";
            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            cmd.Parameters.Add("@BookName", SqlDbType.Char, 40).Value = textBox1.Text.Trim();
            try
            {
                conn.Open();
                int a = cmd.ExecuteNonQuery();                          //执行SQL语句
                this.tBookTableAdapter2.Fill(this.mBOOKDataSet4.TBook);
                if (a == 1)                                              //如果受影响的行数为1则删除成功
                {
                    MessageBox.Show("删除成功！");
                }
                else
                {
                    MessageBox.Show("数据库中没有此图书！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)          //图书修改
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("请输入图书书名！");
                return;
            }
            SqlConnection conn = new SqlConnection(strcon);
            string sqlStr = "update [TBook] set";
            if (textBox2.Text.Trim().ToString() != "")                   //如果姓名有输入
            {
                sqlStr += "[BookName]='" + textBox2.Text.Trim() + "',";
            }
            if (textBox3.Text.Trim() != "")                              //如果出生时间有输入
            {
                sqlStr += "[Author]='" + textBox3.Text.Trim() + "',";
            }
            if (textBox4.Text.Trim() != "")                              //如果出生时间有输入
            {
                sqlStr += "[Publisher]='" + textBox4.Text.Trim() + "',";
            }
            if (textBox5.Text.Trim() != "")                              //如果出生时间有输入
            {
                sqlStr += "[Price]='" + double.Parse(textBox5.Text.Trim()) + "',";
            }
            if (textBox6.Text.Trim() != "")                              //如果出生时间有输入
            {
                sqlStr += "[ISBN]='" + textBox6.Text.Trim() + "',";
            }
            if (textBox7.Text.Trim() != "")                              //如果出生时间有输入
            {
                sqlStr += "[Summary]='" + textBox7.Text.Trim() + "',";
            }
            if (FileNamePath != "")                                      //如果选择了照片
            {
                sqlStr += "[Photo]=@Photo,";
            }
            sqlStr += "[CNum]='" + numericUpDown1.Value + "'," + "[SNum]='" + numericUpDown2.Value + "'";
            sqlStr += " where BookName='" + textBox1.Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            if (FileNamePath != "")                                      //如果选择了照片
            {
                FileStream fs;
                fs = new FileStream(FileNamePath, FileMode.Open, FileAccess.Read);
                MemoryStream mem = new MemoryStream();
                byte[] data1 = new byte[fs.Length];
                fs.Read(data1, 0, (int)fs.Length);
                cmd.Parameters.Add("@Photo", SqlDbType.VarBinary);      //这里选择VarBinary类型
                cmd.Parameters["@Photo"].Value = data1;                 //把照片变化成字节数组
            }
            try
            {
                conn.Open();
                int b = cmd.ExecuteNonQuery();
                if (b == 1)
                {
                    MessageBox.Show("修改成功！");
                }
                else
                {
                    MessageBox.Show("数据库中没有此图书！");
                    this.tBookTableAdapter2.Fill(this.mBOOKDataSet4.TBook);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错，没有完成图书信息的修改！" + ex.Message);
            }
            finally
            {
                conn.Close();
                FileNamePath = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)          //图书添加
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || numericUpDown1.Value == 0)
            {
                MessageBox.Show("请输入完整！");
                return;     //如果没输入完整则返回
            }
            string sqlStr;
            SqlConnection conn = new SqlConnection(strcon);
            if (FileNamePath != "")      //如果选择了照片
            {
                sqlStr = "insert [TBook]([BookName],[Author],[Publisher],[Price],[ISBN],[CNum],[SNum],[Summary],[Photo])" +
                    "values(@BookName, @Author, @Publisher, @Price, @ISBN, @CNum, @SNum, @Summary, @Photo) ";     //设置Sql语句
            }
            else                        //如果没选照片
            {
                sqlStr = "insert [TBook]([BookName],[Author],[Publisher],[Price],[ISBN],[CNum],[SNum],[Summary])" +
                        "values(@BookName, @Author, @Publisher, @Price, @ISBN, @CNum, @SNum, @Summary)";     //设置Sql语句
            }
            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            cmd.Parameters.Add("@BookName", SqlDbType.Char, 40).Value = textBox2.Text.Trim();
            cmd.Parameters.Add("@Author", SqlDbType.Char, 16).Value = textBox3.Text.Trim();
            cmd.Parameters.Add("@Publisher", SqlDbType.Char, 30).Value = textBox4.Text.Trim();
            cmd.Parameters.Add("@Price", SqlDbType.Float).Value = double.Parse(textBox5.Text.Trim());
            cmd.Parameters.Add("@ISBN", SqlDbType.Char, 18).Value = textBox6.Text.Trim();
            cmd.Parameters.Add("CNum", SqlDbType.Int).Value = numericUpDown1.Value;
            cmd.Parameters.Add("SNum", SqlDbType.Int).Value = numericUpDown2.Value;
            cmd.Parameters.Add("Summary", SqlDbType.VarChar, 200).Value = textBox7.Text.Trim();
            if (FileNamePath != "")              //如果选择了照片
            {
                FileStream fs;                                      //以文件流方式读取照片
                fs = new FileStream(FileNamePath, FileMode.Open, FileAccess.Read);
                MemoryStream mem = new MemoryStream();              //实例化内存流对象mem
                byte[] data1 = new byte[fs.Length];                 //定义照片长度的数组
                fs.Read(data1, 0, (int)fs.Length);                  //把照片存到数组中
                cmd.Parameters.Add("@Photo", SqlDbType.VarBinary);  //这里选择VarBinary类型
                cmd.Parameters["@Photo"].Value = data1;             //给@Photo参数赋值
            }
            try
            {
                conn.Open();                                    //打开数据库连接
                cmd.ExecuteNonQuery();                          //执行SQL语句
                MessageBox.Show("添加成功！");
                this.tBookTableAdapter2.Fill(this.mBOOKDataSet4.TBook);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错！" + ex.Message);
            }
            finally
            {
                conn.Close();       //关闭数据库连接
                FileNamePath = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)          //图书查询
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("请输入图书书名！");
                return;
            }
            SqlConnection conn = new SqlConnection(strcon);
            string sqlStrSelect = "select [ISBN],[BookName],[Author],[Publisher],[Price],[CNum],[SNum],[Summary],[Photo] from [TBook] where [BookName]='" + textBox1
                .Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(sqlStrSelect, conn);
            try
            {
                conn.Open();                                            //打开数据库连接
                SqlDataReader sdr = cmd.ExecuteReader();
                MemoryStream memStream = null;                          //定义一个内存流
                if (sdr.HasRows)                                        //如果有记录
                {
                    sdr.Read();                                         //读取第一行记录
                    textBox2.Text = sdr["BookName"].ToString();         //读取书名
                    textBox3.Text = sdr["Author"].ToString();           //读取作者
                    textBox4.Text = sdr["Publisher"].ToString();        //读取出版社
                    textBox5.Text = sdr["Price"].ToString();            //读取定价
                    textBox6.Text = sdr["ISBN"].ToString();             //读取ISBN
                    textBox7.Text = sdr["Summary"].ToString();          //读取内容摘要
                    numericUpDown1.Value = int.Parse(sdr["CNum"].ToString());//读取复本量
                    numericUpDown2.Value = int.Parse(sdr["SNum"].ToString());//读取库存量
                    if (sdr["Photo"] != DBNull.Value)                   //如果有照片
                    {
                        if (pictureBox1.Image != null)                        //如果pictureBox1中有图片销毁
                        {
                            pictureBox1.Image.Dispose();
                        }
                        byte[] data = (byte[])sdr["Photo"];
                        memStream = new MemoryStream(data);             //字节流转换为内存流
                        pictureBox1.Image = Image.FromStream(memStream);//内存流转换为照片
                    }
                }
                else
                {
                    MessageBox.Show("没有此书籍！");
                }
                if (memStream != null)                                  //如果内存流不为空则关闭
                {
                    memStream.Close();
                }
                if (!sdr.IsClosed)
                {
                    sdr.Close();                                        //关闭sdr
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)          //选择照片
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();       //实例化打开文件对话框
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "jpg 图片|*.jpg|gif 图片|*.gif|所有文件(*.*)|*.*";//设置打开文件类型
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                FileNamePath = openFileDialog.FileName;                 //获取文件路径
                pictureBox1.Image = Image.FromFile(FileNamePath);       //将图片显示在pictureBox中
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                button4_Click(null, null);          //调用方法button4_Click，查询读者详细信息
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
