using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PragmaticBlue.Data
{
    /// <summary>
    /// Class for creating regular expressions for use in TextFileDataSet
    /// </summary>
    public class RegexColumnBuilder
    {
        /// <summary>
        /// Adds a column to the collection of columns from which
        /// the regular expression will be created
        /// </summary>
        /// <param name="aColumnName">name of the column</param>
        /// <param name="aSeparator">column separator</param>
        public void AddColumn(string aColumnName, char aSeparator)
        {
            AddColumn(aColumnName, string.Format("[^{0}]+", aSeparator), string.Empty);
            Separator = aSeparator.ToString();
        }

		/// <summary>
		/// Adds a column to the collection of columns from which
		/// the regular expression will be created
		/// </summary>
		/// <param name="aColumnName">name of the column</param>
		/// <param name="aSeparator">column separator</param>
		/// <param name="aColumnType">RegexColumnType of this column</param>
		public void AddColumn(string aColumnName, char aSeparator, RegexColumnType aColumnType)
		{
			AddColumn(aColumnName, string.Format("[^{0}]+", aSeparator), string.Empty, aColumnType);
			Separator = aSeparator.ToString();
		}

		/// <summary>
		/// Adds a column to the collection of columns from which
		/// the regular expression will be created
		/// </summary>
		/// <param name="aColumnName">name of the column</param>
		/// <param name="aLength">amount of characters this column has</param>
		public void AddColumn(string aColumnName, int aLength)
		{
			AddColumn(aColumnName, ".{" + aLength + "}", string.Empty);
		}

		/// <summary>
		/// Adds a column to the collection of columns from which
		/// the regular expression will be created
		/// </summary>
		/// <param name="aColumnName">name of the column</param>
		/// <param name="aLength">amount of characters this column has</param>
		/// <param name="aColumnType">RegexColumnType of this column</param>
		public void AddColumn(string aColumnName, int aLength, RegexColumnType aColumnType)
		{
			AddColumn(aColumnName, ".{" + aLength + "}", string.Empty, aColumnType);
		}

        /// <summary>
        /// Adds a column to the collection of columns from which
        /// the regular expression will be created
        /// </summary>
        /// <param name="aColumnName">name of the column</param>
        /// <param name="aColumnRegEx">regular expression for capturing the value of this column</param>
        public void AddColumn(string aColumnName, string aColumnRegEx)
        {
            AddColumn(aColumnName, aColumnRegEx, string.Empty);
        }

		/// <summary>
		/// Adds a column to the collection of columns from which
		/// the regular expression will be created
		/// </summary>
		/// <param name="aColumnName">name of the column</param>
		/// <param name="aColumnRegEx">regular expression for capturing the value of this column</param>
		/// <param name="aColumnType">RegexColumnType of this column</param>
		public void AddColumn(string aColumnName, string aColumnRegEx, RegexColumnType aColumnType)
		{
			AddColumn(aColumnName, aColumnRegEx, string.Empty, aColumnType);
		}

        /// <summary>
        /// Adds a column to the collection of columns from which
        /// the regular expression will be created
        /// </summary>
        /// <param name="aColumnName">name of the column</param>
        /// <param name="aColumnRegEx">regular expression for capturing the value of this column</param>
        /// <param name="aTrailingRegEx">regular expression for any data not te be captured for this column</param>
        public void AddColumn(string aColumnName, string aColumnRegEx, string aTrailingRegEx)
        {
            Columns.Add(new RegexColumn(aColumnName, aColumnRegEx, aTrailingRegEx));  
        }

		/// <summary>
		/// Adds a column to the collection of columns from which
		/// the regular expression will be created
		/// </summary>
		/// <param name="aColumnName">name of the column</param>
		/// <param name="aColumnRegEx">regular expression for capturing the value of this column</param>
		/// <param name="aTrailingRegEx">regular expression for any data not te be captured for this column</param>
		/// <param name="aColumnType">RegexColumnType of this column</param>
		public void AddColumn(string aColumnName, string aColumnRegEx, string aTrailingRegEx, RegexColumnType aColumnType)
		{
			Columns.Add(new RegexColumn(aColumnName, aColumnRegEx, aTrailingRegEx, aColumnType));
		}

        private List<RegexColumn> _Columns =  new List<RegexColumn>();
		/// <summary>
		/// Get the collection of created RegexColumns
		/// </summary>
        public List<RegexColumn> Columns
        {
            get { return _Columns; }
        }

		private string _Separator = string.Empty;
        /// <summary>
        /// A sepator string that will be appended after each
        /// column expression (including any trailing regular expression)
        /// </summary>
        public string Separator
        {
            get { return _Separator; }
            set { _Separator = value; }
        }
	
        private bool _StartAtBeginOfString = true;
        /// <summary>
        /// Indicates wether the regular expression will start 
        /// searching at each beginning of a line/string
        /// default is set to true
        /// </summary>
        public bool StartAtBeginOfString
        {
            get { return _StartAtBeginOfString; }
            set { _StartAtBeginOfString = value; }
        }

        private bool _EndAtEndOfString = true;
        /// <summary>
        /// Indicates wether the regular expression will end
        /// searching at each end of a line/string
        /// default is set to true
        /// </summary>
        public bool EndAtEndOfString
        {
            get { return _EndAtEndOfString; }
            set { _EndAtEndOfString = value; }
        }

        /// <summary>
        /// creates a regular expression string based on the added columns and 
        /// optional separator
        /// </summary>
        /// <returns>regular expression for use in TextFileDataSet</returns>
        public string CreateRegularExpressionString()
        {
            StringBuilder Result = new StringBuilder();
            if (StartAtBeginOfString) Result.Append("^");
            for (int i = 0; i<Columns.Count; i++)
            {
                Result.Append("(?<");
                Result.Append(Columns[i].ColumnName);
                Result.Append(">");
                Result.Append(Columns[i].RegEx);
                Result.Append(")");
                Result.Append(Columns[i].TrailingRegEx);
                if (i<Columns.Count-1)
                    Result.Append(Separator);
            }
            if (EndAtEndOfString) Result.Append("$");
            return Result.ToString();
        }

        /// <summary>
        /// creates a regular expression based on the added columns and 
        /// optional separator
        /// </summary>
        /// <returns></returns>
        public Regex CreateRegularExpression()
        {
            return new Regex(CreateRegularExpressionString());
        }

        /// <summary>
        /// creates a regular expression based on the added columns and 
        /// optional separator
        /// </summary>
        /// <param name="aOptions"></param>
        /// <returns></returns>
        public Regex CreateRegularExpression(RegexOptions aOptions)
        {
            return new Regex(CreateRegularExpressionString(), aOptions);
        }
    }

	/// <summary>
	/// Enumeration for certain types used in TextFileDataSet
	/// </summary>
	public enum RegexColumnType
	{
		/// <summary>
		/// Int32
		/// </summary>
		INTEGER,
		/// <summary>
		/// Double
		/// </summary>
		DOUBLE,
		/// <summary>
		/// String
		/// </summary>
		STRING,
		/// <summary>
		/// DateTime
		/// </summary>
		DATE
	}

	/// <summary>
	/// Class for defining a regular expression column
	/// </summary>
    public class RegexColumn
    {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="aColumnName">name of the column</param>
		/// <param name="aRegEx">regular expression for capturing the value of this column</param>
		/// <param name="aTrailingRegex">regular expression for any data not te be captured for this column</param>
        public RegexColumn(string aColumnName, string aRegEx, string aTrailingRegex)
        {
            Init(aColumnName, aRegEx, aTrailingRegex, RegexColumnType.STRING);
        }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="aColumnName">name of the column</param>
		/// <param name="aRegEx">regular expression for capturing the value of this column</param>
		/// <param name="aTrailingRegex">regular expression for any data not te be captured for this column</param>
		/// <param name="aColumnType">Type of this column</param>
		public RegexColumn(string aColumnName, string aRegEx, string aTrailingRegex, RegexColumnType aColumnType)
        {
            Init(aColumnName, aRegEx, aTrailingRegex, aColumnType);
        }

		private void Init(string aColumnName, string aRegEx, string aTrailingRegex, RegexColumnType aColumnType)
        {
            ColumnName = aColumnName;
            RegEx = aRegEx;
            TrailingRegEx = aTrailingRegex;
            ColumnType = aColumnType;
        }

        private string _ColumnName;
		/// <summary>
		/// Get or set the name of the column
		/// </summary>
        public string ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }

        private string _RegEx;
		/// <summary>
		/// Get or set the regular expression for capturing the value of this column
		/// </summary>
        public string RegEx
        {
            get { return _RegEx; }
            set { _RegEx = value; }
        }
	
	    private string  _TrailingRegEx;
		/// <summary>
		/// Get or set the regular expression for any data not te be captured for this column
		/// </summary>
	    public string  TrailingRegEx
	    {
		    get { return _TrailingRegEx;}
		    set { _TrailingRegEx = value;}
	    }

		private RegexColumnType _ColumnType;
		/// <summary>
		/// Get or set the Type of this column
		/// </summary>
		public RegexColumnType ColumnType
        {
            get { return _ColumnType; }
            set { _ColumnType = value; }
        }

		/// <summary>
		/// Get the System.Type of this RegexColumn
		/// </summary>
		public Type ColumnTypeAsType
		{
			get
			{
				if (_ColumnType == RegexColumnType.INTEGER) return typeof(int);
				if (_ColumnType == RegexColumnType.DOUBLE) return typeof(double);
				if (_ColumnType == RegexColumnType.DATE) return typeof(DateTime);
				return typeof(string);
			}
		}  
    }
}
