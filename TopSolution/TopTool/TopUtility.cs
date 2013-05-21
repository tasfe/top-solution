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
                spliter = new string[] { ",","，" };
            }

            string[] keywordsArry = s.Split(spliter, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();

            int index = r.Next(keywordsArry.Length);

            return keywordsArry[index];
        }
    }
}
