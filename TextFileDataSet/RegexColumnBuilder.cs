using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PragmaticBlue.Data
{
    /// <summary>
    ///     Class for creating regular expressions for use in TextFileDataSet
    /// </summary>
    public class RegexColumnBuilder
    {
        /// <summary>
        ///     Get the collection of created RegexColumns
        /// </summary>
        public List<RegexColumn> Columns { get; } = new List<RegexColumn>();

        /// <summary>
        ///     A sepator string that will be appended after each
        ///     column expression (including any trailing regular expression)
        /// </summary>
        public string Separator { get; set; } = string.Empty;

        /// <summary>
        ///     Indicates wether the regular expression will start
        ///     searching at each beginning of a line/string
        ///     default is set to true
        /// </summary>
        public bool StartAtBeginOfString { get; set; } = true;

        /// <summary>
        ///     Indicates wether the regular expression will end
        ///     searching at each end of a line/string
        ///     default is set to true
        /// </summary>
        public bool EndAtEndOfString { get; set; } = true;

        /// <summary>
        ///     Adds a column to the collection of columns from which
        ///     the regular expression will be created
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <param name="separator">column separator</param>
        public void AddColumn(string columnName, char separator)
        {
            AddColumn(columnName, $"[^{separator}]+", string.Empty);
            Separator = separator.ToString();
        }

        /// <summary>
        ///     Adds a column to the collection of columns from which
        ///     the regular expression will be created
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <param name="separator">column separator</param>
        /// <param name="columnType">RegexColumnType of this column</param>
        public void AddColumn(string columnName, char separator, RegexColumnType columnType)
        {
            AddColumn(columnName, $"[^{separator}]+", string.Empty, columnType);
            Separator = separator.ToString();
        }

        /// <summary>
        ///     Adds a column to the collection of columns from which
        ///     the regular expression will be created
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <param name="length">amount of characters this column has</param>
        public void AddColumn(string columnName, int length)
        {
            AddColumn(columnName, ".{" + length + "}", string.Empty);
        }

        /// <summary>
        ///     Adds a column to the collection of columns from which
        ///     the regular expression will be created
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <param name="length">amount of characters this column has</param>
        /// <param name="columnType">RegexColumnType of this column</param>
        public void AddColumn(string columnName, int length, RegexColumnType columnType)
        {
            AddColumn(columnName, ".{" + length + "}", string.Empty, columnType);
        }

        /// <summary>
        ///     Adds a column to the collection of columns from which
        ///     the regular expression will be created
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <param name="columnRegEx">regular expression for capturing the value of this column</param>
        public void AddColumn(string columnName, string columnRegEx)
        {
            AddColumn(columnName, columnRegEx, string.Empty);
        }

        /// <summary>
        ///     Adds a column to the collection of columns from which
        ///     the regular expression will be created
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <param name="columnRegEx">regular expression for capturing the value of this column</param>
        /// <param name="columnType">RegexColumnType of this column</param>
        public void AddColumn(string columnName, string columnRegEx, RegexColumnType columnType)
        {
            AddColumn(columnName, columnRegEx, string.Empty, columnType);
        }

        /// <summary>
        ///     Adds a column to the collection of columns from which
        ///     the regular expression will be created
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <param name="columnRegEx">regular expression for capturing the value of this column</param>
        /// <param name="trailingRegEx">regular expression for any data not te be captured for this column</param>
        public void AddColumn(string columnName, string columnRegEx, string trailingRegEx)
        {
            Columns.Add(new RegexColumn(columnName, columnRegEx, trailingRegEx));
        }

        /// <summary>
        ///     Adds a column to the collection of columns from which
        ///     the regular expression will be created
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <param name="columnRegEx">regular expression for capturing the value of this column</param>
        /// <param name="trailingRegEx">regular expression for any data not te be captured for this column</param>
        /// <param name="columnType">RegexColumnType of this column</param>
        public void AddColumn(string columnName, string columnRegEx, string trailingRegEx,
                              RegexColumnType columnType)
        {
            Columns.Add(new RegexColumn(columnName, columnRegEx, trailingRegEx, columnType));
        }

        /// <summary>
        ///     creates a regular expression string based on the added columns and
        ///     optional separator
        /// </summary>
        /// <returns>regular expression for use in TextFileDataSet</returns>
        public string CreateRegularExpressionString()
        {
            var result = new StringBuilder();
            if (StartAtBeginOfString) result.Append("^");
            for (var i = 0; i < Columns.Count; i++)
            {
                result.Append("(?<");
                result.Append(Columns[i].ColumnName);
                result.Append(">");
                result.Append(Columns[i].RegEx);
                result.Append(")");
                result.Append(Columns[i].TrailingRegEx);
                if (i < Columns.Count - 1)
                    result.Append(Separator);
            }
            if (EndAtEndOfString) result.Append("$");
            return result.ToString();
        }

        /// <summary>
        ///     creates a regular expression based on the added columns and
        ///     optional separator
        /// </summary>
        /// <returns></returns>
        public Regex CreateRegularExpression()
        {
            return new Regex(CreateRegularExpressionString());
        }

        /// <summary>
        ///     creates a regular expression based on the added columns and
        ///     optional separator
        /// </summary>
        /// <param name="aOptions"></param>
        /// <returns></returns>
        public Regex CreateRegularExpression(RegexOptions aOptions)
        {
            return new Regex(CreateRegularExpressionString(), aOptions);
        }
    }

    /// <summary>
    ///     Enumeration for certain types used in TextFileDataSet
    /// </summary>
    public enum RegexColumnType
    {
        /// <summary>
        ///     Int32
        /// </summary>
        INTEGER,

        /// <summary>
        ///     Double
        /// </summary>
        DOUBLE,

        /// <summary>
        ///     String
        /// </summary>
        STRING,

        /// <summary>
        ///     DateTime
        /// </summary>
        DATE
    }

    /// <summary>
    ///     Class for defining a regular expression column
    /// </summary>
    public class RegexColumn
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <param name="regEx">regular expression for capturing the value of this column</param>
        /// <param name="trailingRegex">regular expression for any data not te be captured for this column</param>
        public RegexColumn(string columnName, string regEx, string trailingRegex)
        {
            Init(columnName, regEx, trailingRegex, RegexColumnType.STRING);
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <param name="regEx">regular expression for capturing the value of this column</param>
        /// <param name="trailingRegex">regular expression for any data not te be captured for this column</param>
        /// <param name="columnType">Type of this column</param>
        public RegexColumn(string columnName, string regEx, string trailingRegex, RegexColumnType columnType)
        {
            Init(columnName, regEx, trailingRegex, columnType);
        }

        /// <summary>
        ///     Get or set the name of the column
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        ///     Get or set the regular expression for capturing the value of this column
        /// </summary>
        public string RegEx { get; set; }

        /// <summary>
        ///     Get or set the regular expression for any data not te be captured for this column
        /// </summary>
        public string TrailingRegEx { get; set; }

        /// <summary>
        ///     Get or set the Type of this column
        /// </summary>
        public RegexColumnType ColumnType { get; set; }

        /// <summary>
        ///     Get the System.Type of this RegexColumn
        /// </summary>
        public Type ColumnTypeAsType
        {
            get
            {
                switch (ColumnType)
                {
                    case RegexColumnType.INTEGER:
                        return typeof(int);
                    case RegexColumnType.DOUBLE:
                        return typeof(double);
                    case RegexColumnType.DATE:
                        return typeof(DateTime);
                }
                return typeof(string);
            }
        }

        private void Init(string columnName, string regEx, string trailingRegex, RegexColumnType columnType)
        {
            ColumnName = columnName;
            RegEx = regEx;
            TrailingRegEx = trailingRegex;
            ColumnType = columnType;
        }
    }
}