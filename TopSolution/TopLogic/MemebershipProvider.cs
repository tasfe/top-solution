/*********************************************************
 * copyright learnren.com 版权所有 
 * 开发人员：ivan.yu
 * 创建时间：2013/5/3 17:13:17
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration.Common;
using System.Web.Security;
using TopEntity;


namespace TopLogic
{
    /// <summary>
    /// 自定义MemberShip
    /// </summary>
    public class MemebershipProvider : System.Web.Security.MembershipProvider
    {
        NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private string _ApplicationName;
        public override string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName = value;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                using (MembershipUserLogic logic = new MembershipUserLogic())
                {
                    TopUser user;
                    if (CheckUser(username, oldPassword, logic,out user))
                    {
                        user.Password = newPassword;
                        logic.Save(user);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorException("修改密码错误。", ex);
                return false;
            }
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            try
            {
                using (MembershipUserLogic logic = new MembershipUserLogic())
                {
                    TopUser user;
                    if (CheckUser(username, password, logic, out user))
                    {
                        user.PasswordQuestion1 = newPasswordQuestion;
                        user.PasswordAnswer = newPasswordAnswer;
                        logic.Save(user);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorException("修改密码提示问题及答案错误。", ex);
                return false;
            }
        }

        public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            try
            {
                using (MembershipUserLogic logic = new MembershipUserLogic())
                {
                    TopUser user = new TopUser();

                    user.UserName = username;
                    user.Password = password;
                    user.Email = email;
                    user.PasswordQuestion1 = passwordQuestion;
                    user.PasswordAnswer = passwordAnswer;
                    user.IsApproved = isApproved;

                    logic.Save(user);
                    status = System.Web.Security.MembershipCreateStatus.Success;
                    return user;
                }
            }
            catch (Exception ex)
            {
                log.ErrorException("创建用户错误。", ex);
                status = System.Web.Security.MembershipCreateStatus.ProviderError;
                return null;
            }
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            try
            {
                using (MembershipUserLogic logic = new MembershipUserLogic())
                {
                    TopUser user = logic.GetList(p => p.UserName == username).FirstOrDefault();

                    if (user != null)
                    {
                        logic.Delete(user);
                    }

                    return true; 
                }
            }
            catch (Exception ex)
            {
                log.ErrorException("删除用户错误。", ex); 
                return false;
            }
        }

        public override bool EnablePasswordReset
        {
            get { return true; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return true; }
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 5; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 5; }
        }

        public override System.Web.Security.MembershipPasswordFormat PasswordFormat
        {
            get { return System.Web.Security.MembershipPasswordFormat.Clear; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return ".{5,}"; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return false;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            try
            {
                using (MembershipUserLogic logic = new MembershipUserLogic())
                {
                    TopUser user = null;
                    return CheckUser(username, password, logic,out user);
                }
            }
            catch (Exception ex)
            {
                log.ErrorException("验证用户用户错误。", ex);
                return false;
            }
        }

        private bool CheckUser(string username,string password,MembershipUserLogic logic,out TopUser topuser)
        {
            TopUser user = logic.GetList(p => p.UserName == username).FirstOrDefault();
            topuser = user;
            if (user != null)
            {
                if (user.Password == password)
                {                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
