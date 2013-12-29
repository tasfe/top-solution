using System;
using System.Collections.Generic;
using System.Text;

namespace QQBuySdk
{
    class Program
    {
        static void Main(string[] args)
        {
            OpenApiOauth client = new OpenApiOauth("700130685", "1rwoZ0tGHnLoH5r7", 2827453269, "c839b268201fe4d1023a4d72ba565021");
            //获取发布类目测试
            IDictionary<string, string> txtParam = new Dictionary<string, string>();

            txtParam.Add("format", "json");
            txtParam.Add("charset", "utf-8");
            txtParam.Add("needRoot", "1");
		    txtParam.Add("randomValue", "2062974341");
		    txtParam.Add("timeStamp", "1376383357829");
		    txtParam.Add("status", "0");
		    txtParam.Add("qtType", "0");
		    txtParam.Add("chargeItemId", "2673");
		    txtParam.Add("itemDesc", "样品材质：材质棉+聚脂纤维        颜色描述：蓝色  绿色  卡其(样品为蓝色)");
		    txtParam.Add("spName", "华测检测技术股份有限公司");
		    txtParam.Add("qtCode", "CTI2013081310300753");
		    txtParam.Add("qtName", "成分 色牢度 洗缩率");
		    txtParam.Add("sellerUin", "2827453269");
		    txtParam.Add("itemId", "http://item.wanggou.com/558387A800000000040100002CCC7460");

            String response = client.InvokeOpenApi("http://api.paipai.com/qt/add.xhtml", txtParam, null);
            Console.WriteLine(response);
            Console.ReadKey();

            System.IO.File.WriteAllText(@"D:\addResponse.txt", response);      
        }
    }
}
