using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.wangwang.eservice.groupmember.get
    /// </summary>
    public class WangwangEserviceGroupmemberGetRequest : ITopRequest<WangwangEserviceGroupmemberGetResponse>
    {
        /// <summary>
        /// 被查询用户组管理员ID：cntaobao+淘宝nick，例如cntaobaotest
        /// </summary>
        public string ManagerId { get; set; }

        private IDictionary<string, string> otherParameters;

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.wangwang.eservice.groupmember.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("manager_id", this.ManagerId);
            parameters.AddAll(this.otherParameters);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("manager_id", this.ManagerId);
            RequestValidator.ValidateMaxLength("manager_id", this.ManagerId, 128);
        }

        #endregion

        public void AddOtherParameter(string key, string value)
        {
            if (this.otherParameters == null)
            {
                this.otherParameters = new TopDictionary();
            }
            this.otherParameters.Add(key, value);
        }
    }
}
