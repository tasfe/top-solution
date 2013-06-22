/*********************************************************
 * copyright learnren.com 版权所有
 * 开发人员：minyuan
 * 创建时间：2013/5/22 23:04:04
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSharing.DB4ODAL;

namespace TopLogic
{
    public class DataBaseManagerLogic : LogicBase<object>
    {
        public void BackupDataBase()
        {
            string dbBacupFilename = string.Format(@"{0}{1}.db", dbBackupDir, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            DB4ODALServerHelper.BackupDb(mainConn, dbBacupFilename);
        }
    }
}
