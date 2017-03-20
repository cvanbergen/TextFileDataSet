namespace DemoTextFileDataSet
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.OpenBuilder = new System.Windows.Forms.Button();
			this.OpenExpression = new System.Windows.Forms.Button();
			this.Expression = new System.Windows.Forms.TextBox();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(16, 87);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(538, 174);
			this.dataGridView1.TabIndex = 0;
			// 
			// OpenBuilder
			// 
			this.OpenBuilder.Location = new System.Drawing.Point(16, 12);
			this.OpenBuilder.Name = "OpenBuilder";
			this.OpenBuilder.Size = new System.Drawing.Size(199, 23);
			this.OpenBuilder.TabIndex = 1;
			this.OpenBuilder.Text = "Open File With RegexColumnBuilder";
			this.OpenBuilder.UseVisualStyleBackColor = true;
			this.OpenBuilder.Click += new System.EventHandler(this.OpenBuilder_Click);
			// 
			// OpenExpression
			// 
			this.OpenExpression.Location = new System.Drawing.Point(16, 41);
			this.OpenExpression.Name = "OpenExpression";
			this.OpenExpression.Size = new System.Drawing.Size(199, 23);
			this.OpenExpression.TabIndex = 2;
			this.OpenExpression.Text = "Open File with Regular Expression";
			this.OpenExpression.UseVisualStyleBackColor = true;
			this.OpenExpression.Click += new System.EventHandler(this.OpenExpression_Click);
			// 
			// Expression
			// 
			this.Expression.Location = new System.Drawing.Point(230, 43);
			this.Expression.Name = "Expression";
			this.Expression.Size = new System.Drawing.Size(302, 20);
			this.Expression.TabIndex = 3;
			this.Expression.Text = "^(?<ID>[^,]+),(?<Name>[^,]+),(?<Date>[^,]+)$";
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(16, 281);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(537, 82);
			this.listBox1.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(15, 265);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Misreads";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(18, 71);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "DataSet";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(572, 382);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.Expression);
			this.Controls.Add(this.OpenExpression);
			this.Controls.Add(this.OpenBuilder);
			this.Controls.Add(this.dataGridView1);
			this.Name = "Form1";
			this.Text = "Demo TextFileDataSet";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button OpenBuilder;
		private System.Windows.Forms.Button OpenExpression;
		private System.Windows.Forms.TextBox Expression;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}

