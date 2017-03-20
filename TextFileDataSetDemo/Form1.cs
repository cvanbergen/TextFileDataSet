using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PragmaticBlue.Data;

namespace DemoTextFileDataSet
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private TextFileDataSet MyTextFileDataSet;

		private void OpenBuilder_Click(object sender, EventArgs e)
		{
			// open the file 
			FileStream fileStream = new FileStream("DemoText.txt", FileMode.Open, FileAccess.Read);
			// create a new instance of MyTextFileDataSet
			MyTextFileDataSet = new TextFileDataSet();
			// create a new RegexColumnBuilder
			RegexColumnBuilder builder = new RegexColumnBuilder();
			builder.AddColumn("ID", ',', RegexColumnType.INTEGER);
			builder.AddColumn("Name", ',', RegexColumnType.STRING);
			builder.AddColumn("Date", ',', RegexColumnType.DATE);
			// add the RegexColumnBuilder to the TextFileDataSet
			MyTextFileDataSet.ColumnBuilder = builder;
			// set the optional table name - default is 'Table1' 
			MyTextFileDataSet.TableName = "DemoText";
			// fill the dataset
			MyTextFileDataSet.Fill(fileStream);
			// close the file
			fileStream.Close();
			// display the misreads
			ShowMisReads();
			// display the dataset
			ShowDataSet();
		}

		private void ShowDataSet()
		{
			this.dataGridView1.DataSource = MyTextFileDataSet;
			this.dataGridView1.DataMember = MyTextFileDataSet.TableName;
		}

		private void ShowMisReads()
		{
			this.listBox1.Items.Clear();
			foreach (string item in MyTextFileDataSet.MisReads)
				this.listBox1.Items.Add(item);
		}

		private void OpenExpression_Click(object sender, EventArgs e)
		{
			// open the file 
			FileStream fileStream = new FileStream("DemoText.txt", FileMode.Open, FileAccess.Read);
			// create a new instance of MyTextFileDataSet
			MyTextFileDataSet = new TextFileDataSet();
			// specify the regular expression for validating and recognising columns
			MyTextFileDataSet.ContentExpression = new Regex(this.Expression.Text);
			// fill the dataset
			MyTextFileDataSet.Fill(fileStream);
			// close the file
			fileStream.Close();
			// display the misreads
			ShowMisReads();
			// display the dataset
			ShowDataSet();
		}



	}
}