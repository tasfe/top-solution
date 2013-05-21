using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopUtilityTool
{
    public static class TopUtility
    {
        /// <summary>
        /// 获取随机淘宝客关键字
        /// </summary>
        /// <param name="keywords">原始关键字字符串</param>
        /// <param name="spliter">分隔符</param>
        /// <returns></returns>
        public static string GetRandomKeyword(string keywords, params string[] spliter)
        {
            if (string.IsNullOrEmpty(keywords))
            {
                return "减肥";
            }

            string s = keywords;

            if (spliter == null || spliter.Length == 0)
            {
                spliter = new string[] { ",", "，" };
            }

            string[] keywordsArray = s.Split(spliter, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();

            int index = r.Next(keywordsArray.Length);

            return keywordsArray[index];
        }

        /// <summary>
        /// 获取强调关键词的内容。
        /// </summary>
        /// <param name="content">原内容</param>
        /// <param name="newKeywords">新关键词</param>
        /// <returns></returns>
        public static string GetStressedContent(string content, string newKeywords)
        {
            string result = content;
            char[] spliter = new char[] { ',', '，' };
            string[] newKeywordsArray = newKeywords.Split(spliter);

            for (int i = 0; i < newKeywordsArray.Length; i++)
            {
                result = result.Replace(newKeywordsArray[i], string.Format("<strong>{0}</strong>",newKeywordsArray[i]));
            }

            return result;
        }

        /// <summary>
        /// 将已经加强过的内容转换回没有加强过的内容
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="oldKeywords">旧的关键字</param>
        /// <returns></returns>
        public static string GetUnstressedContent(string content,string oldKeywords)
        {
            string result = content;
            char[] spliter = new char[] { ',', '，' };
            string[] oldKeywordsArray = oldKeywords.Split(spliter);

            for (int i = 0; i < oldKeywordsArray.Length; i++)
            {
                result = result.Replace(string.Format("<strong>{0}</strong>", oldKeywordsArray[i]), oldKeywordsArray[i]);
            }
            
            return result;
        }
    }
}
