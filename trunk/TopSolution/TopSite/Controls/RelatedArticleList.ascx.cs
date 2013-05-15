using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TopLogic;

namespace TopSite.Controls
{
    public partial class RelatedArticleList : System.Web.UI.UserControl
    {
        /// <summary>
        /// 当前的逻辑处理对象
        /// </summary>
        public ArticleLogic CurArticleLogic { get; set; }
        /// <summary>
        /// 当前的关键词
        /// </summary>
        public string CurArticleKeywords { get; set; }

        private int _ShowNums = 10;

        /// <summary>
        /// 要显示的相关文件的数量，默认为10
        /// </summary>
        public int ShowNums
        {
            get { return _ShowNums = 10; }
            set { _ShowNums  = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRelatedArticleList();
            }
        }

        /// <summary>
        /// 获取相关文档并显示
        /// </summary>
        private void BindRelatedArticleList()
        {
            if (CurArticleLogic != null)
            {
                if (ShowNums > 0)
                {
                    this.RepeaterArticleList.DataSource = CurArticleLogic.GetRelatedArticleList(CurArticleKeywords, ShowNums);
                    this.RepeaterArticleList.DataBind();
                }
            }
        }
    }
}