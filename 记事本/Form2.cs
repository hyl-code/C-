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
    public partial class Form2 : Form
    {
        public RichTextBox richTextBox;
        public int start = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)      //查找下一个
        {
            richTextBox.SelectionColor = Color.Blue;        //显示为蓝色
            string str;
            str = textBox1.Text;
            if(checkBox1.Checked)
            {
                if(radioButton2.Checked)
                {
                    checkUp(str);
                }
                else
                {
                    checkDown(str);
                }
            }
            else
            {
                if(radioButton1.Checked)
                {
                    uncheckDown(str);
                }
                else
                {
                    uncheckUp(str);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void checkDown(string ss)            //区分大小写向下查找
        {
            int c = 0;
            int b = 0;
            try
            {
                c = richTextBox.SelectionStart;
                b = richTextBox.Text.IndexOf(ss, c + ss.Length, StringComparison.CurrentCulture);
                richTextBox.SelectionLength = ss.Length;
                richTextBox.SelectionColor = Color.Red;     //显示红色
            }
            catch
            {
                MessageBox.Show("已查找到文档的结尾", "查找结束对话框", MessageBoxButtons.OK);
                textBox1.SelectionStart = c;
                textBox1.SelectionLength = ss.Length;
            }
        }

        public void checkUp(string ss)              //区分大小写向上查找
        {
            int c = 0;
            int b = 0;
            try
            {
                c = richTextBox.SelectionStart;
                b = richTextBox.Text.LastIndexOf(ss, c - ss.Length, StringComparison.InvariantCulture);
                richTextBox.SelectionStart = b;
                richTextBox.SelectionLength = ss.Length;
                richTextBox.SelectionColor = Color.Red;         //显示为红色
            }
            catch
            {
                MessageBox.Show("已查找到文档的开头", "查找结束对话框", MessageBoxButtons.OK);
                richTextBox.SelectionStart = c;
                richTextBox.SelectionLength = ss.Length;
            }
        }

        public void uncheckDown(string ss)
        {
            int c = 0;
            int b = 0;
            try
            {
                c = richTextBox.SelectionStart;
                b = richTextBox.Text.IndexOf(ss, c + ss.Length, StringComparison.CurrentCultureIgnoreCase);
                richTextBox.SelectionStart = b;
                richTextBox.SelectionLength = ss.Length;
                richTextBox.SelectionColor = Color.Red;         //显示为红色
            }
            catch
            {
                MessageBox.Show("已查找到文档的结尾", "查找结束对话框", MessageBoxButtons.OK);
                textBox1.SelectionStart = c;
                textBox1.SelectionLength = ss.Length;
            }
        }

        public void uncheckUp(string ss)
        {
            int c = 0;
            int b = 0;
            try
            {
                c = richTextBox.SelectionStart;
                b = richTextBox.Text.LastIndexOf(ss, c - ss.Length, StringComparison.InvariantCultureIgnoreCase);
                richTextBox.SelectionStart = b;
                richTextBox.SelectionLength = ss.Length;
                richTextBox.SelectionColor = Color.Red;         //显示为红色
            }
            catch
            {
                MessageBox.Show("已查找到文档的开头", "查找结束对话框", MessageBoxButtons.OK);
                richTextBox.SelectionStart = c;
                richTextBox.SelectionLength = ss.Length;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
