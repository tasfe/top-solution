/*********************************************************
 * copyright learnren.com 版权所有
 * 开发人员：minyuan
 * 创建时间：2013/6/21 20:45:12
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TopEntity
{
    [Serializable]
    public class TopKeywords
    {
        private long _Id;
        [XmlAnyElement("id")]
        public long Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Keywords;
        [XmlAnyElement("keywords")]
        public string Keywords
        {
            get { return _Keywords; }
            set { _Keywords = value; }
        }

        private int _RefCount;
        /// <summary>
        /// 被引用的次数
        /// </summary>
        public int RefCount
        {
            get { return _RefCount; }
            set { _RefCount = value; }
        }

        private DateTime _LastGetTime;
        /// <summary>
        /// 最后一次从网络抓取相关广告的时间
        /// </summary>
        public DateTime LastGetTime
        {
            get { return _LastGetTime; }
            set { _LastGetTime = value; }
        }

    }
}
