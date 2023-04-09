using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 记事本
{
    public partial class Form3 : Form
    {
        public int start = 0;
        public RichTextBox richText;
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(RichTextBox rtb)
        {
            richText = rtb;
        }

        private void button1_Click(object sender, EventArgs e)      //查找下一个
        {
            string str1;
            str1 = textBox1.Text.Trim();
            richText.SelectionColor = Color.Blue;        //显示为蓝色
            start = richText.Find(str1, start, RichTextBoxFinds.MatchCase); //查找下一个
            if(start == -1)
            {
                MessageBox.Show("已查找到文档的结尾", "查找结束对话框", MessageBoxButtons.OK);
                start = 0;
            }
            else
            {
                start = start + str1.Length;
            }
            richText.SelectionColor = Color.Red;        //显示为红色
            richText.Focus();
        }

        private void button2_Click(object sender, EventArgs e)      //替换
        {
            string str1, str2;
            str1 = textBox1.Text;
            str2 = textBox2.Text;
            richText.SelectionColor = Color.Blue;       //显示为蓝色
            start = richText.Find(str1, start, RichTextBoxFinds.MatchCase);
            if(start == -1)
            {
                MessageBox.Show("已替换到文档的结尾", "替换结束对话框", MessageBoxButtons.OK);
                start = 0;
            }
            else
            {
                start = start + str1.Length;
                richText.SelectedText = str2;
            }
            richText.SelectionColor = Color.Red;        //显示为红色
            richText.Focus();
        }

        private void button3_Click(object sender, EventArgs e)      //全部替换
        {
            string str1, str2;
            str1 = textBox1.Text;
            str2 = textBox2.Text;
            start = 0;
            start = richText.Find(str1, start, RichTextBoxFinds.MatchCase);
            while(start != -1)
            {
                richText.SelectedText = str2;
                start += str2.Length;
                start = richText.Find(str1, start, RichTextBoxFinds.MatchCase);
            }
            MessageBox.Show("已替换到文档的结尾", "替换结束对话框", MessageBoxButtons.OK);
            start = 0;
            richText.Focus();
        }

        private void button4_Click(object sender, EventArgs e)      //取消
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
