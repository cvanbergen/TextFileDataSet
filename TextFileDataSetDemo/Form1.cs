using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TextFileDataSet;

namespace DemoTextFileDataSet
{
    public partial class Form1 : Form
    {
        private TextFileDataSet.TextFileDataSet _myTextFileDataSet;

        public Form1()
        {
            InitializeComponent();
        }

        private void OpenBuilder_Click(object sender, EventArgs e)
        {
            // open the file 
            var fileStream = new FileStream("DemoText.txt", FileMode.Open, FileAccess.Read);
            // create a new instance of MyTextFileDataSet
            _myTextFileDataSet = new TextFileDataSet.TextFileDataSet();
            // create a new RegexColumnBuilder
            var builder = new RegexColumnBuilder();
            builder.AddColumn("ID", ',', RegexColumnType.INTEGER);
            builder.AddColumn("Name", ',', RegexColumnType.STRING);
            builder.AddColumn("Date", ',', RegexColumnType.DATE);
            // add the RegexColumnBuilder to the TextFileDataSet
            _myTextFileDataSet.ColumnBuilder = builder;
            // set the optional table name - default is 'Table1' 
            _myTextFileDataSet.TableName = "DemoText";
            // fill the dataset
            _myTextFileDataSet.Fill(fileStream);
            // close the file
            fileStream.Close();
            // display the misreads
            ShowMisReads();
            // display the dataset
            ShowDataSet();
        }

        private void ShowDataSet()
        {
            dataGridView1.DataSource = _myTextFileDataSet;
            dataGridView1.DataMember = _myTextFileDataSet.TableName;
        }

        private void ShowMisReads()
        {
            listBox1.Items.Clear();
            foreach (var item in _myTextFileDataSet.MisReads)
                listBox1.Items.Add(item);
        }

        private void OpenExpression_Click(object sender, EventArgs e)
        {
            // open the file 
            var fileStream = new FileStream("DemoText.txt", FileMode.Open, FileAccess.Read);
            // create a new instance of MyTextFileDataSet
            _myTextFileDataSet = new TextFileDataSet.TextFileDataSet();
            // specify the regular expression for validating and recognising columns
            _myTextFileDataSet.ContentExpression = new Regex(Expression.Text);
            // fill the dataset
            _myTextFileDataSet.Fill(fileStream);
            // close the file
            fileStream.Close();
            // display the misreads
            ShowMisReads();
            // display the dataset
            ShowDataSet();
        }
    }
}