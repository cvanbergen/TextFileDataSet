# TextFileDataSet
Converting text files or csv-files to a System.Data.DataTable based on regular expressions

[![Build status](https://ci.appveyor.com/api/projects/status/12wo95w2s0xwjm9t?svg=true)](https://ci.appveyor.com/project/cvanbergen/textfiledataset)
[![cvanbergen MyGet Build Status](https://www.myget.org/BuildSource/Badge/cvanbergen?identifier=e79c68c2-c119-4b8f-b93c-0d00d621138a)](https://www.myget.org/)

## Introduction
(Originally posted on 18 Feb 2008)
This code allows you to generate and fill a DataTable from a text file. You are able to:
* define your own column names or have them created automatically
* validate data with regular expressions
* use RegexColumnBuilder to construct the DataTable with no regular expression knowledge
* specify columntypes for the resulting DataTable, not just the type string
* process any kind of text file, not just delimited files
* have a collection of misread lines of data
* process large files
### Background
Every now and then I need to read some text file for importing data. Most of the times what I really need is a dataset with that data. But how to convert that text file to a dataset quickly? There are several articles on codeproject that describe how to convert a csv file or a text file to a database or dataset (e.g. [FinalCSVReader](http://www.codeproject.com/cs/database/FinalCSVReader.asp), [DataSetFrmDelimTxt](http://www.codeproject.com/cs/database/DataSetFrmDelimTxt.asp)). But none of them were flexible enough, or offered data-validation.
### Using the code
#### Getting started
To get started right away let us assume that we have a delimited file birthday.txt with the following content from which we want to create a DataSet:
```
    1,Chris,12-07-1972
    2,Dave,03-01-1974
    3,John,03-19-1980,Drummer
    4,Mark,12-02-1980
    5,Eric,09-18-1981
```
A quick way to get the DataSet would be:
```csharp
    // open the file
    FileStream fileStream = new FileStream("birthday.txt", FileMode.Open, FileAccess.Read);
    // create an instance of MyTextFileDataSet
    TextFileDataSet MyTextFileDataSet = new TextFileDataSet();
    // specify the regular expression for validating and recognising columns
    MyTextFileDataSet.ContentExpression = new Regex("^(?<ID>[^,]+),(?<Name>[^,]+),(?<Date>[^,]+)$");
    // fill the dataset
    MyTextFileDataSet.Fill(fileStream);
    // close the file
    fileStream .Close();
``` 
The resulting MyTextFileDataSet will contain one DataTable with three DataColumns: ID, Name and Date. Ofcourse it will be filled with four rows of data. Four? Yes, four rows. Row number 3 is not valid according to the regular expression. You can find this row in the property MyTextFileDataSet.MisReads. This property is of type List<string>.
Wondering where the column names came from, just take a look at the used regular expression:
```
    ^(?<ID>[^,]+),(?<Name>[^,]+),(?<Date>[^,]+)$
```    
You can see that the names for the DataColumns are provided. If you did not specify the names but made your expression something like
```
    ^([^,]+),([^,]+),([^,]+)$
```
The names of the columns would be 1,2 and 3.
#### Without regular expressions
If you are not familiar with regular expressions another approach is implemented to define the columns.This is achieved by using the RegexColumnBuilder. Take a look at this sample:
```csharp
    RegexColumnBuilder MyColumBuilder = new RegexColumnBuilder();
    MyColumBuilder.AddColumn("ID", ',');
    MyColumBuilder.AddColumn("DATE", ',');
    MyColumBuilder.AddColumn("NAME", ',');
    Regex MyRegex = MyColumBuilder.CreateRegularExpression();
```    
The MyRegex can be used to put in the MyTextFileDataSet.RegularExpression, or just place the complete RegexColumnBuilder in the TextFileDataSet.
```csharp
    MyTextFileDataSet.ColumnBuilder = MyColumnBuilder;
```
#### Specify column type
In the previous sample a column is defined with only a suitable delimiter. At this time there are four defined types available: INT, DOUBLE, DATE and STRING. The way to use these is:
```csharp
    MyColumBuilder.AddColumn("ID", ',', RegexColumnType.INTEGER);
    MyColumBuilder.AddColumn("DATE", ',', RegexColumnType.DATE);
```
To have the specified types present in the resulting dataset one must place the MyColumnBuilder in the MyTextFileDataSet.ColumnBuilder property, otherwise the specifications are always of type string.
#### Large files
TextFileDataSet can handle large files because internally it is using System.IO.Stream as way of input. This also has the extra benefit of not having to have the file on disk per se.
### Points of Interest
The TextFileDataSet inherits the System.Data.DataSet, so anything you can do with a System.Data.DataSet you can do with TextFileDataSet.
