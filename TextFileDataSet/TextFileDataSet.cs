using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace TextFileDataSet
{
    /// <summary>
    ///     Class for transforming a text-file into a dataset
    ///     based on one regular expression
    /// </summary>
    public class TextFileDataSet : DataSet
    {
        private const string NewName = "NewName";
        private const string DefaultGroup = "0";

        private Regex _contentRegex;

        private List<RegexColumn> _regexColumns;

        public TextFileDataSet()
        {
            UseFirstRowNamesAsColumnNames = false;
            TableName = "Table1";
        }

        /// <summary>
        ///     Set the RegexColumnBuilder
        ///     on setting this the columns and their RegexColumnTypes are set
        ///     as is the complete ContentExpression
        /// </summary>
        public RegexColumnBuilder ColumnBuilder
        {
            set
            {
                _regexColumns = value.Columns;
                ContentExpression = value.CreateRegularExpression();
            }
        }

        /// <summary>
        ///     Regular Expression that is used for validating textlines and
        ///     defining the column names of the dataset
        /// </summary>
        public Regex ContentExpression
        {
            get { return _contentRegex; }
            set
            {
                if ((_contentRegex == null) || !_contentRegex.ToString().Equals(value.ToString()))
                {
                    _contentRegex = value;
                    ContentExpressionHasChanged = true;
                }
            }
        }

        private bool ContentExpressionHasChanged { get; set; }

        /// <summary>
        ///     The regular expression used for handling a first row that could
        ///     contain column headers. If you do not have a first row with headers
        ///     use UseFirstRowNamesAsColumnNames=false, or if you do have a row
        ///     with headers but don't want to use them use: SkipFirstRow=true
        /// </summary>
        public Regex FirstRowExpression { get; set; }

        /// <summary>
        ///     When set to true the values in the first match made are
        ///     used as column names instead of the ones supplied in
        ///     te regular expression
        /// </summary>
        public bool UseFirstRowNamesAsColumnNames { get; set; }

        /// <summary>
        ///     When set to true the values in the first row are
        ///     discarded.
        /// </summary>
        public bool SkipFirstRow { get; set; }

        /// <summary>
        ///     The name the datatable in the dataset should get
        ///     or the name of the datatable to use when a dataset is
        ///     provided
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        ///     The text file to be read
        /// </summary>
        public Stream TextFile { get; set; }

        private DataTable DataTable
        {
            get
            {
                if (!Tables.Contains(TableName))
                {
                    var newTable = new DataTable(TableName);
                    Tables.Add(newTable);
                }
                return Tables[TableName];
            }
        }

        /// <summary>
        ///     Lines in the text file that did not match
        ///     the regular expression
        /// </summary>
        public List<string> MisReads { get; private set; }

        private void AddMisRead(string missRead)
        {
            if (MisReads == null)
                MisReads = new List<string>();
            MisReads.Add(missRead);
        }

        private void RemoveDataTable()
        {
            if (Tables.Contains(TableName))
                Tables.Remove(TableName);
        }

        private void BuildRegexSchemaIntoDataSet()
        {
            if ((ContentExpression == null) && !ContentExpressionHasChanged) return;
            RemoveDataTable();
            foreach (var sGroup in ContentExpression.GetGroupNames())
            {
                if (sGroup != DefaultGroup)
                {
                    var newDc = new DataColumn {DataType = typeof(string)};
                    if (_regexColumns != null)
                        foreach (var r in _regexColumns)
                            if (r.ColumnName == sGroup)
                            {
                                newDc.DataType = r.ColumnTypeAsType;
                                break;
                            }
                    newDc.ColumnName = sGroup;
                    DataTable.Columns.Add(newDc);
                }
            }
        }

        /// <summary>
        ///     Reads every line in the text file and tries to match
        ///     it with the given regular expression.
        ///     Every match will be placed as a new row in the
        ///     datatable
        /// </summary>
        /// <param name="textFile"></param>
        /// <param name="regularExpression"></param>
        /// <param name="tableName"></param>
        public void Fill(Stream textFile, Regex regularExpression, string tableName)
        {
            TableName = tableName;
            Fill(textFile, regularExpression);
        }

        /// <summary>
        ///     Reads every line in the text file and tries to match
        ///     it with the given regular expression.
        ///     Every match will be placed as a new row in the
        ///     datatable
        /// </summary>
        /// <param name="textFile"></param>
        /// <param name="regularExpression"></param>
        public void Fill(Stream textFile, Regex regularExpression)
        {
            ContentExpression = regularExpression;
            Fill(textFile);
        }

        /// <summary>
        ///     Reads every line in the text file and tries to match
        ///     it with the given regular expression.
        ///     Every match will be placed as a new row in the
        ///     datatable
        /// </summary>
        /// <param name="textFile"></param>
        public void Fill(Stream textFile)
        {
            TextFile = textFile;
            Fill();
        }

        /// <summary>
        ///     Reads every line in the text file and tries to match
        ///     it with the given regular expression.
        ///     Every match will be placed as a new row in the
        ///     datatable
        /// </summary>
        /// <returns></returns>
        public void Fill()
        {
            BuildRegexSchemaIntoDataSet();

            if (TextFile == null)
                throw new ApplicationException("No stream available to convert to a DataSet");

            TextFile.Seek(0, SeekOrigin.Begin);
            var sr = new StreamReader(TextFile);

            var readLine = sr.ReadLine();
            var isFirstLine = true;
            while (readLine != null)
            {
                if (isFirstLine && UseFirstRowNamesAsColumnNames && !SkipFirstRow)
                {
                    if (FirstRowExpression == null)
                        throw new TextFileDataSetException(
                            "FirstRowExpression is not set, but UseFirstRowNamesAsColumnNames is set to true");
                    if (!FirstRowExpression.IsMatch(readLine))
                        throw new TextFileDataSetException(
                            "The first row in the file does not match the FirstRowExpression");

                    var m = FirstRowExpression.Match(readLine);
                    foreach (var sGroup in FirstRowExpression.GetGroupNames())
                        if (sGroup != DefaultGroup)
                            DataTable.Columns[sGroup].ExtendedProperties.Add(NewName, m.Groups[sGroup].Value);
                }
                else if (!(isFirstLine && SkipFirstRow) && ContentExpression.IsMatch(readLine))
                {
                    var m = ContentExpression.Match(readLine);
                    var newRow = DataTable.NewRow();
                    foreach (var sGroup in ContentExpression.GetGroupNames())
                        if (sGroup != DefaultGroup)
                            if (newRow.Table.Columns[sGroup].DataType == typeof(int))
                                newRow[sGroup] = Convert.ToInt32(m.Groups[sGroup].Value);
                            else if (newRow.Table.Columns[sGroup].DataType == typeof(double))
                                newRow[sGroup] = Convert.ToDouble(m.Groups[sGroup].Value);
                            else if (newRow.Table.Columns[sGroup].DataType == typeof(DateTime))
                                newRow[sGroup] = Convert.ToDateTime(m.Groups[sGroup].Value);
                            else
                                newRow[sGroup] = m.Groups[sGroup].Value;
                    DataTable.Rows.Add(newRow);
                }
                else if (!(isFirstLine && SkipFirstRow))
                {
                    AddMisRead(readLine);
                }
                readLine = sr.ReadLine();
                isFirstLine = false;
            }
            if (!UseFirstRowNamesAsColumnNames) return;
            foreach (DataColumn column in DataTable.Columns)
                if (column.ExtendedProperties.ContainsKey(NewName))
                    column.ColumnName = column.ExtendedProperties[NewName].ToString();
        }
    }
}