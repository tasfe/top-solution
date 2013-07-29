/*********************************************************
 * copyright learnren.com 版权所有
 * 开发人员：minyuan
 * 创建时间：2013/7/29 23:00:50
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace TopUtilityTool
{
    public static class XMLTOOL
    {
        /// <summary>
        /// 获取节点的内容
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string GetNodeValue(XNode node)
        {
            switch (node.NodeType)
            {
                case System.Xml.XmlNodeType.Text:
                    return (node as XText).Value;
                default:
                    return node.ToString();
            }
        }

        /// <summary>
        /// 获取指定名字的第一个节点的串联文本内容
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static string GetFirstNodeValue(string path, string nodeName)
        {
            string result = null;

            if (File.Exists(path))
            {
                using (Stream stream = new FileStream(path, FileMode.Open))
                {
                    XDocument x = XDocument.Load(stream);
                    XElement element = x.Descendants(XName.Get(nodeName)).FirstOrDefault();
                    if (element != null)
                    {
                        result = element.Value;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取指定xml标签中指定路径的内容
        /// 
        /// html标签必须是闭合的符合xml规范否则返回null
        /// </summary>
        /// <param name="html">html</param>
        /// <param name="xmlPath">路径。形如1-2等，这里的编号是指相应的标签编号，从0开始</param>
        /// <returns></returns>
        public static string GetHtmlTextByXmlPath(string html, string xmlPath)
        {
            try
            {
                XDocument doc = null;
                doc = XDocument.Parse(html);
                if (doc != null)
                {
                    XElement element = doc.Root;
                    string[] paths = xmlPath.Split('-');

                    for (int i = 0; i < paths.Length; i++)
                    {
                        element = element.Elements().ToList()[int.Parse(paths[i])];
                    }

                    return element.Value;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
