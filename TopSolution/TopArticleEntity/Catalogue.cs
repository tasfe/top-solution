/*********************************************************
 * copyright learnren.com 版权所有 
 * 开发人员：ivan.yu
 * 创建时间：2013/4/27 13:45:42
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopEntity
{
    public class Catalogue
    {
        private int _Order;

        public int Order
        {
            get { return _Order; }
            set { _Order = value; }
        }

        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _TopKeywords;

        public string TopKeywords
        {
            get { return _TopKeywords; }
            set { _TopKeywords = value; }
        }
        

        private string _KeyWords;

        public string KeyWords
        {
            get { return _KeyWords; }
            set { _KeyWords = value; }
        }

        private string _Summary;

        public string Summary
        {
            get { return _Summary; }
            set { _Summary = value; }
        }
    }
}
