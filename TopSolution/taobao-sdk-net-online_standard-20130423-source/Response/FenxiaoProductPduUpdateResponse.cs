using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// FenxiaoProductPduUpdateResponse.
    /// </summary>
    public class FenxiaoProductPduUpdateResponse : TopResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [XmlElement("is_success")]
        public bool IsSuccess { get; set; }
    }
}
