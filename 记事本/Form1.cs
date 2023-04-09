using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace 记事本
{
    public partial class Form1 : Form
    {
        private static string openfilepath = "";        //保存所打开文件的路径
        public Form1()
        {
            InitializeComponent();
            this.Text = "记事本";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            place();
        }

        public void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText == "")
            {
                复制ToolStripMenuItem.Enabled = false;
                toolStripButton5.Enabled = false;
                剪切ToolStripMenuItem.Enabled = false;
                toolStripButton4.Enabled = false;
                删除toolStripMenuItem.Enabled = false;
            }
            else
            {
                复制ToolStripMenuItem.Enabled = true;
                toolStripButton5.Enabled = true;
                剪切ToolStripMenuItem.Enabled = true;
                toolStripButton4.Enabled = true;
                删除toolStripMenuItem.Enabled = true;
            }
        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //新建文件
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Modified)
            {
                /*提示保存对话框*/
                DialogResult dResult = MessageBox.Show("文件" + this.Text + "的内容已改变，需要保存吗？",
                    "保存文件", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (dResult)
                {
                    case DialogResult.Yes:
                        另存为ToolStripMenuItem_Click(null, null);
                        richTextBox1.Clear();
                        this.Text = "无标题 - 记事本";
                        break;
                    case DialogResult.No:
                        richTextBox1.Clear();
                        this.Text = "无标题 - 记事本";
                        break;
                    case DialogResult.Cancel:
                        break;
                }
            }
            else
            {
                richTextBox1.Clear();
                this.Text = "无标题 - 记事本";
                richTextBox1.Modified = false;
            }
        }

        //打开新窗口
        private void 新窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1= new Form1();
            form1.Show();
        }

        //打开文件
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "文本文件(*.txt) | *.txt | 所有文件(*.*) | *.*";   //设置文件类型
            openFileDialog1.FilterIndex = 1;            //设置默认文件类型的显示顺序
            openFileDialog1.RestoreDirectory = true;    //打开对话框是否记忆上次打开的目录
            StreamReader sr = null;                     //定义 StreamReader 对象
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    openfilepath = openFileDialog1.FileName;                //获取打开的文件路径
                    string name = openfilepath.Substring(openfilepath.LastIndexOf('\\') + 1);
                    Text = name;                                            //文件名作为标题
                    sr = new StreamReader(openfilepath, Encoding.Default);  //实例化sr
                    richTextBox1.Text = sr.ReadToEnd();                      //读取所有文件内容
                }
                catch
                {
                    MessageBox.Show("打开文件时出错。", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Close();             //关闭对象sr
                        sr.Dispose();           //释放对象sr资源
                    }
                }
            }
        }

        //保存文件
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter sw = null;
            if(openfilepath == "")
            {
                另存为ToolStripMenuItem_Click(null, null);     //调用另存为方法
                return;
            }
            try
            {
                sw = new StreamWriter(openfilepath, false, Encoding.Default);
                sw.Write(richTextBox1.Text);
                Text  = sw.ToString();
                toolStripStatusLabel1.Text = "保存成功";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            finally
            {
                if(sw != null)
                {
                    sw.Close();     //关闭StreamWriter
                    sw.Dispose();   //释放资源
                }
            }
        }

        //另存为文件
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "文本文件(*.txt) | *.txt | 所有文件(*.*) | *.*";   //设置文件类型
            saveFileDialog1.FilterIndex = 1;            //设置默认文件类型的显示顺序
            saveFileDialog1.RestoreDirectory = true;    //保存对话框是否记忆上次打开的目录
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openfilepath = saveFileDialog1.FileName.ToString(); //获取文件路径
                FileStream fs;
                try
                {
                    fs = File.Create(openfilepath);
                    int index = openfilepath.LastIndexOf("\\");
                    Text = openfilepath.Substring(index + 1);
                }
                catch
                {
                    MessageBox.Show("建立文件时出错。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                byte[] content = Encoding.Default.GetBytes(richTextBox1.Text);
                try
                {
                    fs.Write(content, 0, content.Length);
                    fs.Flush();
                    toolStripStatusLabel1.Text = "保存成功";
                }
                catch
                {
                    MessageBox.Show("写入文件时出错。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        //打印文件
        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        //退出程序
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Modified)
            {
                /*提示保存对话框*/
                DialogResult dResult = MessageBox.Show("文件" + this.Text + "的内容已改变，需要保存吗？",
                    "保存文件", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if(dResult == DialogResult.Yes )
                {
                    另存为ToolStripMenuItem_Click(null, null);
                }
            }
            Application.Exit();
        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        //撤销操作
        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void 重做ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        //复制选中文本
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        //剪切选中文本
        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            richTextBox1.Cut();
        }

        //粘贴复制文本
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void 删除toolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        //查找关键词
        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 ff = new Form2();
            ff.richTextBox = richTextBox1;
            ff.ShowDialog();
        }

        //查找关键词并替换
        private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 fc = new Form3();
            fc.richText = richTextBox1;
            fc.ShowDialog();
        }

        //全选文本框文本
        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        //显示日期,时间
        private void 日期DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(richTextBox1.SelectionLength > 0)
            {
                richTextBox1.SelectedText = DateTime.Now.Hour.ToString() + ":" + 
                    DateTime.Now.Minute.ToString() + " " + DateTime.Now.Year.ToString() + "/"
                    + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString();
            }
            else
            {
                richTextBox1.SelectedText += DateTime.Now.Hour.ToString() + ":" +
                    DateTime.Now.Minute.ToString() + " " + DateTime.Now.Year.ToString() + "/"
                    + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString();
            }
        }

        private void 格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //是否自动换行操作
        private void 自动换行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(richTextBox1.WordWrap == true)
            {
                richTextBox1.WordWrap = false;
                自动换行ToolStripMenuItem.Checked = false;
                richTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            }
            else
            {
                richTextBox1.WordWrap = true;
                自动换行ToolStripMenuItem.Checked = true;
                richTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            }
        }

        //设置所选择的文本字体
        private void 字体FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fontDialog1.ShowDialog() == DialogResult.OK)
            {
                Font font = fontDialog1.Font;
                richTextBox1.SelectionFont = font;
            }
        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(statusStrip1.Visible == true)
            {
                statusStrip1.Visible = false;
                状态栏ToolStripMenuItem.Checked = false;
                richTextBox1.Height += 22;
            }
            else
            {
                statusStrip1.Visible = true;
                状态栏ToolStripMenuItem.Checked = true;
                richTextBox1.Height -= 22;
            }
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 关于记事本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此记事本的版本为 V1.0！");
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 页面设计ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            place();
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            place();
        }

        private void place()            //计算行数与列数
        {
            string str = richTextBox1.Text;
            int m = richTextBox1.SelectionStart;
            int Ln = 0;
            int Col = 0;
            for(int i = m - 1; i >= 0; i--)
            {
                if (str[i] == '\n')
                {
                    Ln++;
                }
                if(Ln < 1)
                {
                    Col++;
                }
            }
            Ln += 1;
            Col += 1;
            toolStripStatusLabel1.Text = "行：" + Ln.ToString() + "，" + "列：" + Col.ToString();
        }
    }
}