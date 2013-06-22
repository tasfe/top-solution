/*********************************************************
 * copyright learnren.com 版权所有
 * 开发人员：minyuan
 * 创建时间：2013/6/21 20:50:01
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
    public class TopKeywordsLogic : LogicBase<TopKeywords>
    {
        /// <summary>
        /// 分析传入的以','分割的关键词字符串，并将分析出来的关键词存库，
        /// 如库中已经存在则增加一个引用计数，如过有删除的关键词，则减少一个引用计数
        /// 当oldKeywords为null时直接分析并加入新的关键词。
        /// </summary>
        /// <param name="keywords">新的关键词字符串</param>
        /// <param name="oldKeywords">原来的关键词字符串</param>
        public void AnalyzeAndSave(string keywords, string oldKeywords = null)
        {
            if (keywords == null)
            {
                logger.ErrorException("分析关键词失败", new NullReferenceException("关键词不能为null"));
                return;
            }

            if (oldKeywords == null)
            {
                string[] newkeywords = keywords.Split(',', '，');
                string[] oldkeywords = oldKeywords.Split(',', '，');
                // 比较差别，获取新增和移除的
                // 增加的项
                var added = from d in newkeywords where oldkeywords.Contains(d) == false select d;
                // 移除的项
                var deled = from d in oldkeywords where newkeywords.Contains(d) == false select d;

                foreach (var item in added)
                {
                    var nowkeyword = GetList(p => p.Keywords == item).FirstOrDefault();
                    if (nowkeyword != null)
                    {
                        nowkeyword.RefCount++;
                    }
                    else
                    {
                        nowkeyword = new TopKeywords();
                        nowkeyword.Id = GetNewIdentity();
                        nowkeyword.Keywords = item;
                        nowkeyword.RefCount = 1;
                    }
                    Save(nowkeyword);
                }

                foreach (var item in deled)
                {
                    var nowkeyword = GetList(p => p.Keywords == item).FirstOrDefault();
                    if (nowkeyword != null)
                    {
                        if (nowkeyword.RefCount > 0)
                        {
                            nowkeyword.RefCount--;
                            Save(nowkeyword);
                        }
                    }
                }
            }
            else
            {
                string[] newkeywords = keywords.Split(',', '，');

                foreach (var item in newkeywords)
                {
                    var nowkeyword = GetList(p => p.Keywords == item).FirstOrDefault();
                    if (nowkeyword != null)
                    {
                        nowkeyword.RefCount++;
                    }
                    else
                    {
                        nowkeyword = new TopKeywords();
                        nowkeyword.Id = GetNewIdentity();
                        nowkeyword.Keywords = item;
                        nowkeyword.RefCount = 1;
                    }
                    Save(nowkeyword);
                }
            }
        }
    }
}
