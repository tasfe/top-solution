/*********************************************************
 * copyright learnren.com 版权所有 
 * 开发人员：ivan.yu
 * 创建时间：2013/4/28 11:56:27
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopEntity;

namespace TopLogic
{
    public class SiteLogic : LogicBase<SiteConfig>
    {
        public override void Save(SiteConfig obj)
        {
            base.Save(obj);
            BasicCache.SiteConfig = obj;
        }
    }
}
