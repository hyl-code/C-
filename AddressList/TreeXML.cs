using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using System.Windows.Forms;

namespace AddressList
{
    internal class TreeXML
    {
        TreeView thetreeview;               //定义TreeView成员变量
        XmlDocument xmldocument;            //定义XmlDocument成员变量
        
        public TreeXML()
        {
            xmldocument = new XmlDocument();
        }
        ~TreeXML()
        {
            //析构函数
        }

        /*添加组名，XMLFilePath为XML文件路径，NodeName为所添加分组的组名*/
        public void AddXmlNode(string XMLFilePath, string NodeName)
        {
            xmldocument.Load(XMLFilePath);
            XmlNode root = xmldocument.SelectSingleNode("List");    //查找ParentName
            XmlElement xel = xmldocument.CreateElement("Node");         //创建一个<Node>节点
            xel.SetAttribute("Text", NodeName);                         //设置节点的串联值
            xel.InnerText = "";
            root.AppendChild(xel);                                      //添加到节点中
            xmldocument.Save(XMLFilePath);                              //将XML文档保存在指定的文件中
        }

        public void AddChildXmlNode(string XMLFilePath, string ParentName, string NodeName)
        {
            xmldocument.Load(XMLFilePath);
            foreach(XmlElement xe in xmldocument.DocumentElement)
            {
                if (xe.GetAttribute("Text") == ParentName)
                {
                    XmlElement xel = xmldocument.CreateElement("Node");         //创建一个<Node>节点
                    xel.SetAttribute("Text", NodeName);                         //设置节点的串联值
                    xel.InnerText = "";
                    xe.AppendChild(xel);                                      //添加到节点中
                    xmldocument.Save(XMLFilePath);                              //将XML文档保存在指定的文件中
                }
            }
        }

        /*读取分组的XML文件并显示在TreeView 控件上*/
        public void XMLToTree(string XMLFilePath, TreeView TheTreeView)
        {
            thetreeview = TheTreeView;
            xmldocument.Load(XMLFilePath);                                      //读取XML文件
            XmlNode root = xmldocument.SelectSingleNode("List");                //选择匹配 List 的第 1 个节点
            RecursionTree(root, TheTreeView.Nodes);
        }

        public void RecursionTree(XmlNode xmlNode, TreeNodeCollection nodes)
        {
            foreach (XmlNode subXmlnod in xmlNode.ChildNodes)                       //遍历此所有子节点
            {
                if (subXmlnod.Name == "Node")                                    //子节点的限定名为 "组名"
                {
                    TreeNode trerotnod = new TreeNode();                        //实例化一个树节点
                    trerotnod.Text = subXmlnod.Attributes["Text"].Value;        //将子节点串联值作为树节点名称
                    nodes.Add(trerotnod);                                       //添加此树节点
                    RecursionTree(subXmlnod, trerotnod.Nodes);
                }
            }
        }

        /*删除分组，其中NodeName为所要删除的组名*/
        public void DeleXml(string XMLFilePath, string NodeName)
        {
            xmldocument.Load(XMLFilePath);
            /*获取List 节点下的所有子节点*/
            XmlNodeList xnl = xmldocument.SelectSingleNode("List").ChildNodes;
            Del(xnl, XMLFilePath, NodeName);
        }

        public void Del(XmlNodeList xnl, string XMLFilePath, string NodeName)
        {
            foreach (XmlNode xd in xnl)                      //遍历
            {
                XmlElement xe = (XmlElement)xd;            //转换为 XmlElement 类型
                if (xe.GetAttribute("Text") == NodeName)
                {
                    xe.ParentNode.RemoveChild(xe);          //删除此节点以及此节点下的所有节点
                    xmldocument.Save(XMLFilePath);          //保存
                }
                else
                {
                    Del(xd.ChildNodes, XMLFilePath, NodeName);
                }
            }
        }

        /*更改分组名称，OldNodeName 为原组名，NewNodeName 为更改后的组名*/
        public void AlterXml(string XMLFilePath, string OldNodeName, string NewNodeName)
        {
            xmldocument.Load(XMLFilePath);
            XmlNodeList xnl = xmldocument.SelectSingleNode("List").ChildNodes;
            Alter(xnl, XMLFilePath, OldNodeName, NewNodeName);
        }

        public void Alter(XmlNodeList xnl, string XMLFilePath,string OldNodeName, string NewNodeName)
        {
            foreach (XmlNode xd in xnl)                              //遍历所有子节点
            {
                XmlElement xe = (XmlElement)xd;                    //将子节点类型转换为 XmlElement 类型
                if (xe.GetAttribute("Text") == OldNodeName)          //如果为要修改的节点
                {
                    xe.SetAttribute("Text", NewNodeName);           //修改
                    xmldocument.Save(XMLFilePath);                  //保存
                }
                else
                {
                    Alter(xd.ChildNodes, XMLFilePath, OldNodeName, NewNodeName);
                }
            }
        }

        public void AlterGroup(string XMLFilePath, string OldNodeName, string NewNodeName)
        {
            xmldocument.Load(XMLFilePath);
            XmlNodeList xnl = xmldocument.SelectSingleNode("CONTENTS").ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("所在分组") == OldNodeName) //“所在分组”属性值如为指定的分组
                {
                    xe.SetAttribute("所在分组", NewNodeName);
                    xmldocument.Save(XMLFilePath);
                }
            }
        }

        /*获得所选中的分组中的所有联系人信息表，NodeName为组名*/
        public DataTable GetPersonInfo(string XMLFilePath, string NodeName)
        {
            xmldocument.Load(XMLFilePath);
            XmlNodeList xnl = xmldocument.SelectSingleNode("CONTENTS").ChildNodes;
            DataTable dt = new DataTable();             //实例化一个DataTable对象 dt
            dt.Columns.Add("ID", typeof(string));       //添加列名为 "ID"
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("出生时间", typeof(string));
            dt.Columns.Add("电话", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("QQ", typeof(string));
            dt.Columns.Add("性别", typeof(string));
            dt.Columns.Add("地址", typeof(string));
            dt.Columns.Add("备注", typeof(string));
            foreach(XmlNode xd in xnl)                  //遍历所有子节点
            {
                XmlElement xe = (XmlElement) xd;        //将子节点类型转换为XmIElement类型
                if (xe.GetAttribute("所在分组") == NodeName)//“所在分组”属性值如为指定的分组
                {
                    DataRow myRow = dt.NewRow();        //新建一行
                    myRow["ID"] = xe.ChildNodes.Item(0).InnerText;
                    myRow["姓名"] = xe.ChildNodes.Item(1).InnerText;
                    myRow["出生时间"] = xe.ChildNodes.Item(2).InnerText;
                    myRow["电话"] = xe.ChildNodes.Item(3).InnerText;
                    myRow["Email"] = xe.ChildNodes.Item(4).InnerText;
                    myRow["QQ"] = xe.ChildNodes.Item(5).InnerText;
                    myRow["性别"] = xe.ChildNodes.Item(6).InnerText;
                    myRow["地址"] = xe.ChildNodes.Item(7).InnerText;
                    myRow["备注"] = xe.ChildNodes.Item(8).InnerText;
                    dt.Rows.Add(myRow);
                }
            }
            return dt;                                  //返回表dt
        }
    }
}
