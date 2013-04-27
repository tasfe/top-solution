using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.inventory.authorize.removeall
    /// </summary>
    public class InventoryAuthorizeRemoveallRequest : ITopRequest<InventoryAuthorizeRemoveallResponse>
    {
        /// <summary>
        /// 商品编码列表，用”,”隔开.每次请求的商品列表不超过50个
        /// </summary>
        public string ScItemIdList { get; set; }

        /// <summary>
        /// 移除授权的目标用户昵称列表，用”,”隔开
        /// </summary>
        public string UserNickList { get; set; }

        private IDictionary<string, string> otherParameters;

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.inventory.authorize.removeall";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("sc_item_id_list", this.ScItemIdList);
            parameters.Add("user_nick_list", this.UserNickList);
            parameters.AddAll(this.otherParameters);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("sc_item_id_list", this.ScItemIdList);
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
