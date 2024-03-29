using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.item.sku.update
    /// </summary>
    public class ItemSkuUpdateRequest : ITopRequest<ItemSkuUpdateResponse>
    {
        /// <summary>
        /// sku所属商品的价格。当用户更新sku，使商品价格不属于sku价格之间的时候，用于修改商品的价格，使sku能够更新成功
        /// </summary>
        public string ItemPrice { get; set; }

        /// <summary>
        /// Sku文字的版本。可选值:zh_HK(繁体),zh_CN(简体);默认值:zh_CN
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// Sku所属商品数字id，可通过 taobao.item.get 获取
        /// </summary>
        public Nullable<long> NumIid { get; set; }

        /// <summary>
        /// Sku的商家外部id
        /// </summary>
        public string OuterId { get; set; }

        /// <summary>
        /// Sku的销售价格。精确到2位小数;单位:元。如:200.07，表示:200元7分。修改后的sku价格要保证商品的价格在所有sku价格所形成的价格区间内（例如：商品价格为6元，sku价格有5元、10元两种，如果要修改5元sku的价格，那么修改的范围只能是0-6元之间；如果要修改10元的sku，那么修改的范围只能是6到无穷大的区间中）
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Sku属性串。格式:pid:vid;pid:vid,如: 1627207:3232483;1630696:3284570,表示机身颜色:军绿色;手机套餐:一电一充。  如果包含自定义属性，则格式为pid:vid;pid2:vid2;$pText:vText , 其中$pText:vText为自定义属性。限制：其中$pText的’$’前缀不能少，且pText和vText文本中不可以存在 冒号:和分号;以及逗号，
        /// </summary>
        public string Properties { get; set; }

        /// <summary>
        /// Sku的库存数量。sku的总数量应该小于等于商品总数量(Item的NUM)，sku数量变化后item的总数量也会随着变化。取值范围:大于等于零的整数
        /// </summary>
        public Nullable<long> Quantity { get; set; }

        /// <summary>
        /// 产品的规格信息。
        /// </summary>
        public string SpecId { get; set; }

        private IDictionary<string, string> otherParameters;

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.item.sku.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("item_price", this.ItemPrice);
            parameters.Add("lang", this.Lang);
            parameters.Add("num_iid", this.NumIid);
            parameters.Add("outer_id", this.OuterId);
            parameters.Add("price", this.Price);
            parameters.Add("properties", this.Properties);
            parameters.Add("quantity", this.Quantity);
            parameters.Add("spec_id", this.SpecId);
            parameters.AddAll(this.otherParameters);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("num_iid", this.NumIid);
            RequestValidator.ValidateMinValue("num_iid", this.NumIid, 0);
            RequestValidator.ValidateRequired("properties", this.Properties);
            RequestValidator.ValidateMinValue("quantity", this.Quantity, 0);
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
