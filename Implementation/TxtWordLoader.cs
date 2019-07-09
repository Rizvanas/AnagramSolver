using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Domain;
using Interfaces;

namespace Implementation
{
    public class TxtWordLoader : IWordLoader
    {
        private readonly AppConfig _appConfig;

        public TxtWordLoader(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public IEnumerable<Word> Load(string filePath)
        {
            var lines = File.ReadLines(filePath);
            var words = new HashSet<Word>();
            var regex = new Regex("[.-]|[0-9]");
            var forbiddenTypes = new List<string> { "sutr", "dll", "akronim" };

            foreach (var line in lines)
            {
                var lineList = line.ToLower().Split('\t').ToList();

                var firstElem = lineList.ElementAtOrDefault(0);
                var secondElem = lineList.ElementAtOrDefault(1);
                var thirdElem = lineList.ElementAtOrDefault(2);

                if (firstElem != null && !regex.IsMatch(firstElem))
                    words.Add(new Word { Text = firstElem, Type = secondElem });

                if (thirdElem != null && !regex.IsMatch(thirdElem))
                    words.Add(new Word { Text = thirdElem, Type = secondElem });
            }

            return words
                .Where(w => !forbiddenTypes.Contains(w.Type))
                .ToHashSet();
        }

        public void BulkFillWordsTable(List<Word> words)
        {
            BulkCopyToDB(GetDataTable(words, "Words", new DataColumn[]
            {
                new DataColumn {
                    ColumnName = "WordId",
                    DataType = typeof(Int32),
                    ReadOnly = true,
                    Unique = true,
                    AutoIncrement = true
                },
                new DataColumn {
                    ColumnName ="Word",
                    DataType = typeof(String),
                    AutoIncrement = false,
                    ReadOnly = false,
                    Unique = false
                }
            }));
        }

        private DataTable GetDataTable(List<Word> words, string name, DataColumn[] columns)
        {
            var table = new DataTable(name);
            DataRow row;
            table.Columns.AddRange(columns);

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns["WordId"];
            table.PrimaryKey = PrimaryKeyColumns;

            foreach (var item in words.Select((value, i) => new { value, i }))
            {
                row = table.NewRow();
                row["WordId"] = item.i + 1;
                row["Word"] = item.value.Text;
                table.Rows.Add(row);
            }

            return table;
        }

        private void BulkCopyToDB(DataTable table)
        {
            using (SqlConnection sourceConnection =
                  new SqlConnection(_appConfig.GetConnectionString()))
            {
                sourceConnection.Open();

                using (SqlBulkCopy bulkCopy =
                    new SqlBulkCopy(_appConfig.GetConnectionString()))
                {
                    bulkCopy.DestinationTableName = "dbo.Words";
                    try
                    {
                        bulkCopy.WriteToServer(table);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
