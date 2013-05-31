using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopArticleEntity
{
    public class Article
    {
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        

        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private int _ClickNum;

        public int ClickNum
        {
            get { return _ClickNum; }
            set { _ClickNum = value; }
        }

        private string _Content;

        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }


        private DateTime _CreateDate;

        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        private string _OrignSource;

        public string OrignSource
        {
            get { return _OrignSource; }
            set { _OrignSource = value; }
        }

        private string _OrignSourceUrl;

        public string OrignSourceUrl
        {
            get { return _OrignSourceUrl; }
            set { _OrignSourceUrl = value; }
        }
        

        private int _CatalogueId;

        public int CatalogueId
        {
            get { return _CatalogueId; }
            set { _CatalogueId = value; }
        }

        private string  _TopKeywords;

        public string  TopKeywords
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
