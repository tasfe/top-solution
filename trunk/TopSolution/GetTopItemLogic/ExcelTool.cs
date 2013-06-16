/*********************************************************
 * copyright learnren.com 版权所有
 * 开发人员：minyuan
 * 创建时间：2013/6/16 19:30:02
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace GetTopItemLogic
{
    public class ExcelTool
    {
        #region 采用openxml方式把excel转换成dataTable
        /// <summary>
        /// Excel流组织成Datatable
        /// </summary>
        /// <param name="stream">Excel文件流</param>
        /// <param name="sheetName">须要读取的Sheet</param>
        /// <returns>DataTable</returns>
        public DataTable ReadExcel(string sheetName, Stream stream)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(stream, false))
            {
                //打开Stream
                IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName);
                if (sheets.Count() == 0)
                {
                    //找出合适前提的sheet,没有则返回
                    return null;
                }
                WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);
                //获取Excel中共享数据
                SharedStringTable stringTable = document.WorkbookPart.SharedStringTablePart.SharedStringTable;
                IEnumerable<Row> rows = worksheetPart.Worksheet.Descendants<Row>();//获得Excel中得数据行
                DataTable dt = new DataTable("Excel");
                foreach (Row row in rows)
                {
                    if (row.RowIndex == 1)
                    {
                        //Excel第一行动列名
                        GetDataColumn(row, stringTable, ref dt);
                    }
                    else
                    {
                        GetDataRow(row, stringTable, ref dt);//Excel第二行同时为DataTable的第一行数据
                    }

                }
                return dt;
            }
        }
        /// <summary>
        /// 构建DataTable的列
        /// </summary>
        /// <param name="row">OpenXML定义的Row对象</param>
        /// <param name="stringTablePart"></param>
        /// <param name="dt">须要返回的DataTable对象</param>
        /// <returns></returns>
        public void GetDataColumn(Row row, SharedStringTable stringTable, ref DataTable dt)
        {
            DataColumn col = new DataColumn();
            Dictionary<string, int> columnCount = new Dictionary<string, int>();
            foreach (Cell cell in row)
            {
                string cellVal = GetValue(cell, stringTable);
                col = new DataColumn(cellVal);
                if (IsContainsColumn(dt, col.ColumnName))
                {
                    if (!columnCount.ContainsKey(col.ColumnName))
                        columnCount.Add(col.ColumnName, 0);
                    col.ColumnName = col.ColumnName + (columnCount[col.ColumnName]++);
                }
                dt.Columns.Add(col);
            }
        }
        /// <summary>
        /// 构建DataTable的每一行数据,并返回该Datatable
        /// </summary>
        /// <param name="row">OpenXML的行</param>
        /// <param name="stringTablePart"></param>
        /// <param name="dt">DataTable</param>
        private void GetDataRow(Row row, SharedStringTable stringTable, ref DataTable dt)
        {
            // 读取算法：按行一一读取单位格,若是整行均是空数据
            DataRow dr = dt.NewRow();
            int i = 0;
            int nullRowCount = i;
            foreach (Cell cell in row)
            {
                string cellVal = GetValue(cell, stringTable);
                if (cellVal == string.Empty)
                {
                    nullRowCount++;
                }
                dr[i] = cellVal;
                i++;
            }
            if (nullRowCount != i)
            {
                dt.Rows.Add(dr);
            }
        }
        /// <summary>
        /// 获取单位格的值
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="stringTablePart"></param>
        /// <returns></returns>
        private string GetValue(Cell cell, SharedStringTable stringTable)
        {
            //因为Excel的数据存储在SharedStringTable中,须要获取数据在SharedStringTable 中的索引
            string value = string.Empty;
            try
            {
                if (cell.ChildElements.Count == 0)
                    return value;
                value = double.Parse(cell.CellValue.InnerText).ToString();
                if ((cell.DataType != null) && (cell.DataType == CellValues.SharedString))
                {
                    value = stringTable.ChildElements[Int32.Parse(value)].InnerText;
                }
            }
            catch (Exception)
            {
                value = "N/A";
            }
            return value;
        }
        /// <summary>
        /// 判断网格是否存在列
        /// </summary>
        /// <param name="dt">网格</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public bool IsContainsColumn(DataTable dt, string columnName)
        {
            if (dt == null || columnName == null)
            {
                return false;
            }
            return dt.Columns.Contains(columnName);
        }

        #endregion 采用openxml方式把excel转换成dataTable
    }
}
