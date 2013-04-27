#region License, Terms and Conditions
//
// Jayrock - JSON and JSON-RPC for Microsoft .NET Framework and Mono
// Written by Atif Aziz (atif.aziz@skybow.com)
// Copyright (c) 2005 Atif Aziz. All rights reserved.
//
// This library is free software; you can redistribute it and/or modify it under
// the terms of the GNU Lesser General Public License as published by the Free
// Software Foundation; either version 2.1 of the License, or (at your option)
// any later version.
//
// This library is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
// FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
// details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this library; if not, write to the Free Software Foundation, Inc.,
// 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 
//
#endregion

namespace Jayrock.Json.Conversion.Converters
{
    #region Imports

    using System;
    using System.Collections;
    using System.Data;
    using System.Diagnostics;

    #endregion

    public sealed class DataRowExporter : ExporterBase
    {
        public DataRowExporter() :
            this(typeof(DataRow)) {}

        public DataRowExporter(Type inputType) : 
            base(inputType) {}

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            Debug.Assert(context != null);
            Debug.Assert(value != null);
            Debug.Assert(writer != null);

            ExportRow(context, (DataRow) value, writer);
        }

        internal static void ExportRow(ExportContext context, DataRow row, JsonWriter writer)
        {
            Debug.Assert(context != null);
            Debug.Assert(row != null);
            Debug.Assert(writer != null);

            writer.WriteStartObject();
    
            foreach (DataColumn column in row.Table.Columns)
            {
                writer.WriteMember(column.ColumnName);
                context.Export(row[column], writer);
            }
    
            writer.WriteEndObject();
        }
    }
}