using Amazon_04_01_2024.Test_Data_Classes;
using ExcelDataReader;
using Amazon_04_01_2024.Test_Data_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon_04_01_2024.Utilities
{
    public class DataRead
    {
        public static List<AmazonText> ReadData(string excelFilePath, string sheetName)
        {
            List<AmazonText> excelDataList = new List<AmazonText>();
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    var dataTable = result.Tables[sheetName];
                    
                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            AmazonText excelData = new AmazonText
                            {
                              SearchText = GetValueOrDefault(row,"searchtext")
                            };

                            excelDataList.Add(excelData);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sheet '{sheetName}' not found in the Excel file.");
                    }
                }
            }
            return excelDataList;
        }

        static string GetValueOrDefault(DataRow row, string columnName)
        { 
            return row.Table.Columns.Contains(columnName) ? row[columnName]?.ToString() : null;
        }
    }
}
