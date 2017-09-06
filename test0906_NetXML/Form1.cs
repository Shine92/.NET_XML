using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Xml;

namespace test0906_NetXML {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            WebClient objWebClient = new WebClient();
            objWebClient.DownloadFile("https://news.google.com/news/rss/?ned=tw&hl=zh-TW", @"D:\simon\googleNews.xml");  //把XML檔存到本機
            MessageBox.Show("Download OK!");
        }

        private void button2_Click(object sender, EventArgs e) {
            XmlDocument objDoc = new XmlDocument(); //讀XML檔
            objDoc.Load(@"D:\simon\googleNews.xml");
            XmlNode objNode = objDoc.SelectSingleNode("/rss/channel/item/title"); //找到XML的節點
            listBox1.Items.Add(objNode.InnerText); //InnerText只顯示節點的文字 InnerXML 子節點
        }

        private void button3_Click(object sender, EventArgs e) {
           XmlDocument objDoc = new XmlDocument();
            objDoc.Load(@"D:\simon\googleNews.xml");
            XmlNodeList objNodeList = objDoc.SelectNodes("/rss/channel/item"); //找到所有節點items
            foreach (XmlNode objNode in objNodeList) {
              string s = objNode.SelectSingleNode("./title").InnerText; //找到單項節點 title
                listBox1.Items.Add(s);
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            XmlDocument objDoc = new XmlDocument();
            objDoc.Load(@"D:\simon\googleNews.xml");
            XmlNode objNode = objDoc.SelectSingleNode("/rss");  //找到單項rss
            listBox1.Items.Add(objNode.Attributes["version"].Value); // 抓元素的屬性值
        }

        private void button5_Click(object sender, EventArgs e) {
            XmlWriter objWriter = XmlWriter.Create(@"D:\simon\createXML.xml");

            objWriter.WriteStartElement("booklist"); //booklist 寫入開始
            //for(){  考慮寫成迴圈
            objWriter.WriteStartElement("book"); //book 寫入開始
                objWriter.WriteAttributeString("id", "102"); //book 屬性值
                //objWriter.WriteElementString("title", "ASP.NET MVC5 ..."); //內容
                    objWriter.WriteStartElement("title");
                    objWriter.WriteAttributeString("lang","Zh-TW"); //同層寫入屬性 屬性值
                    objWriter.WriteString("ADO.NET...XML"); //內容
                    objWriter.WriteEndElement(); //title close
            objWriter.WriteEndElement(); //book close
            // }
            objWriter.WriteEndElement(); //booklist close
            objWriter.Close(); //寫完記得關閉

            MessageBox.Show("Create OK!");
        }
    }
}
