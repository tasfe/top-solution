/*********************************************************
 * copyright learnren.com 版权所有
 * 开发人员：minyuan
 * 创建时间：2013/5/3 23:55:30
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace TopEntity
{
    public class TopUser : MembershipUser
    {
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private string _PasswordAnswer;

        public string PasswordAnswer
        {
            get { return _PasswordAnswer; }
            set { _PasswordAnswer = value; }
        }

        public override string PasswordQuestion
        {
            get
            {
                return _PasswordQuestion1;
            }
        }

        private string  _PasswordQuestion1;

        public string  PasswordQuestion1
        {
            get { return _PasswordQuestion1; }
            set { _PasswordQuestion1 = value; }
        }

        private string _UserName;

        public new string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        
    }
}
