namespace BloombergClient
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
            this.components = new System.ComponentModel.Container();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textBoxStock = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.symbolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subscribed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Bid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.midDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Open = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrevClose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ClosingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gamma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Theta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vega = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImpliedVol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockQuotesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new BloombergClient.DataSet1();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.checkBoxLog = new System.Windows.Forms.CheckBox();
            this.textBoxOption = new System.Windows.Forms.TextBox();
            this.buttonAddOption = new System.Windows.Forms.Button();
            this.buttonDeleteOption = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonTCPBloomberg = new System.Windows.Forms.RadioButton();
            this.radioButtonNativeBloomberg = new System.Windows.Forms.RadioButton();
            this.buttonAddFuture = new System.Windows.Forms.Button();
            this.buttonDeleteFuture = new System.Windows.Forms.Button();
            this.textBoxFuture = new System.Windows.Forms.TextBox();
            this.textBoxIndex = new System.Windows.Forms.TextBox();
            this.buttonDeleteIndex = new System.Windows.Forms.Button();
            this.buttonAddIndex = new System.Windows.Forms.Button();
            this.radioButtonTWS = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockQuotesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(1175, 11);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(146, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(1175, 41);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(146, 23);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(27, 13);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(112, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add Stock";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // textBoxStock
            // 
            this.textBoxStock.Location = new System.Drawing.Point(145, 15);
            this.textBoxStock.Name = "textBoxStock";
            this.textBoxStock.Size = new System.Drawing.Size(100, 20);
            this.textBoxStock.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.symbolDataGridViewTextBoxColumn,
            this.Subscribed,
            this.Bid,
            this.Ask,
            this.midDataGridViewTextBoxColumn,
            this.lastPriceDataGridViewTextBoxColumn,
            this.Open,
            this.PrevClose,
            this.closedDataGridViewCheckBoxColumn,
            this.ClosingPrice,
            this.Delta,
            this.Gamma,
            this.Theta,
            this.Vega,
            this.ImpliedVol,
            this.timeDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.stockQuotesBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 100);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1309, 237);
            this.dataGridView1.TabIndex = 4;
            // 
            // symbolDataGridViewTextBoxColumn
            // 
            this.symbolDataGridViewTextBoxColumn.DataPropertyName = "Symbol";
            this.symbolDataGridViewTextBoxColumn.HeaderText = "Symbol";
            this.symbolDataGridViewTextBoxColumn.Name = "symbolDataGridViewTextBoxColumn";
            // 
            // Subscribed
            // 
            this.Subscribed.DataPropertyName = "Subscribed";
            this.Subscribed.HeaderText = "Subscribed";
            this.Subscribed.Name = "Subscribed";
            // 
            // Bid
            // 
            this.Bid.DataPropertyName = "Bid";
            this.Bid.HeaderText = "Bid";
            this.Bid.Name = "Bid";
            // 
            // Ask
            // 
            this.Ask.DataPropertyName = "Ask";
            this.Ask.HeaderText = "Ask";
            this.Ask.Name = "Ask";
            // 
            // midDataGridViewTextBoxColumn
            // 
            this.midDataGridViewTextBoxColumn.DataPropertyName = "Mid";
            this.midDataGridViewTextBoxColumn.HeaderText = "Mid";
            this.midDataGridViewTextBoxColumn.Name = "midDataGridViewTextBoxColumn";
            // 
            // lastPriceDataGridViewTextBoxColumn
            // 
            this.lastPriceDataGridViewTextBoxColumn.DataPropertyName = "LastPrice";
            this.lastPriceDataGridViewTextBoxColumn.HeaderText = "LastPrice";
            this.lastPriceDataGridViewTextBoxColumn.Name = "lastPriceDataGridViewTextBoxColumn";
            // 
            // Open
            // 
            this.Open.DataPropertyName = "Open";
            this.Open.HeaderText = "Open";
            this.Open.Name = "Open";
            // 
            // PrevClose
            // 
            this.PrevClose.DataPropertyName = "PrevClose";
            this.PrevClose.HeaderText = "PrevClose";
            this.PrevClose.Name = "PrevClose";
            // 
            // closedDataGridViewCheckBoxColumn
            // 
            this.closedDataGridViewCheckBoxColumn.DataPropertyName = "Closed";
            this.closedDataGridViewCheckBoxColumn.HeaderText = "Closed";
            this.closedDataGridViewCheckBoxColumn.Name = "closedDataGridViewCheckBoxColumn";
            // 
            // ClosingPrice
            // 
            this.ClosingPrice.DataPropertyName = "ClosingPrice";
            this.ClosingPrice.HeaderText = "ClosingPrice";
            this.ClosingPrice.Name = "ClosingPrice";
            // 
            // Delta
            // 
            this.Delta.DataPropertyName = "Delta";
            this.Delta.HeaderText = "Delta";
            this.Delta.Name = "Delta";
            // 
            // Gamma
            // 
            this.Gamma.DataPropertyName = "Gamma";
            this.Gamma.HeaderText = "Gamma";
            this.Gamma.Name = "Gamma";
            // 
            // Theta
            // 
            this.Theta.DataPropertyName = "Theta";
            this.Theta.HeaderText = "Theta";
            this.Theta.Name = "Theta";
            // 
            // Vega
            // 
            this.Vega.DataPropertyName = "Vega";
            this.Vega.HeaderText = "Vega";
            this.Vega.Name = "Vega";
            // 
            // ImpliedVol
            // 
            this.ImpliedVol.DataPropertyName = "ImpliedVol";
            this.ImpliedVol.HeaderText = "ImpliedVol";
            this.ImpliedVol.Name = "ImpliedVol";
            // 
            // timeDataGridViewTextBoxColumn
            // 
            this.timeDataGridViewTextBoxColumn.DataPropertyName = "Time";
            this.timeDataGridViewTextBoxColumn.HeaderText = "Time";
            this.timeDataGridViewTextBoxColumn.Name = "timeDataGridViewTextBoxColumn";
            // 
            // stockQuotesBindingSource
            // 
            this.stockQuotesBindingSource.DataMember = "StockQuotes";
            this.stockQuotesBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(27, 45);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(112, 23);
            this.buttonDelete.TabIndex = 6;
            this.buttonDelete.Text = "Delete Stock";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.HorizontalScrollbar = true;
            this.listBoxLog.Location = new System.Drawing.Point(12, 343);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(1309, 199);
            this.listBoxLog.TabIndex = 7;
            // 
            // checkBoxLog
            // 
            this.checkBoxLog.AutoSize = true;
            this.checkBoxLog.Location = new System.Drawing.Point(1125, 12);
            this.checkBoxLog.Name = "checkBoxLog";
            this.checkBoxLog.Size = new System.Drawing.Size(44, 17);
            this.checkBoxLog.TabIndex = 8;
            this.checkBoxLog.Text = "Log";
            this.checkBoxLog.UseVisualStyleBackColor = true;
            // 
            // textBoxOption
            // 
            this.textBoxOption.Location = new System.Drawing.Point(378, 14);
            this.textBoxOption.Name = "textBoxOption";
            this.textBoxOption.Size = new System.Drawing.Size(141, 20);
            this.textBoxOption.TabIndex = 9;
            // 
            // buttonAddOption
            // 
            this.buttonAddOption.Location = new System.Drawing.Point(260, 12);
            this.buttonAddOption.Name = "buttonAddOption";
            this.buttonAddOption.Size = new System.Drawing.Size(112, 23);
            this.buttonAddOption.TabIndex = 10;
            this.buttonAddOption.Text = "Add Option";
            this.buttonAddOption.UseVisualStyleBackColor = true;
            this.buttonAddOption.Click += new System.EventHandler(this.buttonAddOption_Click);
            // 
            // buttonDeleteOption
            // 
            this.buttonDeleteOption.Location = new System.Drawing.Point(260, 45);
            this.buttonDeleteOption.Name = "buttonDeleteOption";
            this.buttonDeleteOption.Size = new System.Drawing.Size(112, 23);
            this.buttonDeleteOption.TabIndex = 11;
            this.buttonDeleteOption.Text = "Delete Option";
            this.buttonDeleteOption.UseVisualStyleBackColor = true;
            this.buttonDeleteOption.Click += new System.EventHandler(this.buttonDeleteOption_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonTWS);
            this.groupBox1.Controls.Add(this.radioButtonTCPBloomberg);
            this.groupBox1.Controls.Add(this.radioButtonNativeBloomberg);
            this.groupBox1.Location = new System.Drawing.Point(991, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(128, 83);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source";
            // 
            // radioButtonTCPBloomberg
            // 
            this.radioButtonTCPBloomberg.AutoSize = true;
            this.radioButtonTCPBloomberg.Location = new System.Drawing.Point(19, 38);
            this.radioButtonTCPBloomberg.Name = "radioButtonTCPBloomberg";
            this.radioButtonTCPBloomberg.Size = new System.Drawing.Size(99, 17);
            this.radioButtonTCPBloomberg.TabIndex = 2;
            this.radioButtonTCPBloomberg.Text = "Bloomberg TCP";
            this.radioButtonTCPBloomberg.UseVisualStyleBackColor = true;
            // 
            // radioButtonNativeBloomberg
            // 
            this.radioButtonNativeBloomberg.AutoSize = true;
            this.radioButtonNativeBloomberg.Location = new System.Drawing.Point(19, 19);
            this.radioButtonNativeBloomberg.Name = "radioButtonNativeBloomberg";
            this.radioButtonNativeBloomberg.Size = new System.Drawing.Size(109, 17);
            this.radioButtonNativeBloomberg.TabIndex = 0;
            this.radioButtonNativeBloomberg.Text = "Native Bloomberg";
            this.radioButtonNativeBloomberg.UseVisualStyleBackColor = true;
            // 
            // buttonAddFuture
            // 
            this.buttonAddFuture.Location = new System.Drawing.Point(538, 13);
            this.buttonAddFuture.Name = "buttonAddFuture";
            this.buttonAddFuture.Size = new System.Drawing.Size(112, 23);
            this.buttonAddFuture.TabIndex = 13;
            this.buttonAddFuture.Text = "Add Future";
            this.buttonAddFuture.UseVisualStyleBackColor = true;
            this.buttonAddFuture.Click += new System.EventHandler(this.buttonAddFuture_Click);
            // 
            // buttonDeleteFuture
            // 
            this.buttonDeleteFuture.Location = new System.Drawing.Point(538, 45);
            this.buttonDeleteFuture.Name = "buttonDeleteFuture";
            this.buttonDeleteFuture.Size = new System.Drawing.Size(112, 23);
            this.buttonDeleteFuture.TabIndex = 14;
            this.buttonDeleteFuture.Text = "Delete Future";
            this.buttonDeleteFuture.UseVisualStyleBackColor = true;
            this.buttonDeleteFuture.Click += new System.EventHandler(this.buttonDeleteFuture_Click);
            // 
            // textBoxFuture
            // 
            this.textBoxFuture.Location = new System.Drawing.Point(656, 14);
            this.textBoxFuture.Name = "textBoxFuture";
            this.textBoxFuture.Size = new System.Drawing.Size(97, 20);
            this.textBoxFuture.TabIndex = 15;
            // 
            // textBoxIndex
            // 
            this.textBoxIndex.Location = new System.Drawing.Point(890, 15);
            this.textBoxIndex.Name = "textBoxIndex";
            this.textBoxIndex.Size = new System.Drawing.Size(97, 20);
            this.textBoxIndex.TabIndex = 18;
            // 
            // buttonDeleteIndex
            // 
            this.buttonDeleteIndex.Location = new System.Drawing.Point(772, 46);
            this.buttonDeleteIndex.Name = "buttonDeleteIndex";
            this.buttonDeleteIndex.Size = new System.Drawing.Size(112, 23);
            this.buttonDeleteIndex.TabIndex = 17;
            this.buttonDeleteIndex.Text = "Delete Index";
            this.buttonDeleteIndex.UseVisualStyleBackColor = true;
            this.buttonDeleteIndex.Click += new System.EventHandler(this.buttonDeleteIndex_Click);
            // 
            // buttonAddIndex
            // 
            this.buttonAddIndex.Location = new System.Drawing.Point(772, 14);
            this.buttonAddIndex.Name = "buttonAddIndex";
            this.buttonAddIndex.Size = new System.Drawing.Size(112, 23);
            this.buttonAddIndex.TabIndex = 16;
            this.buttonAddIndex.Text = "Add Index";
            this.buttonAddIndex.UseVisualStyleBackColor = true;
            this.buttonAddIndex.Click += new System.EventHandler(this.buttonAddIndex_Click);
            // 
            // radioButtonTWS
            // 
            this.radioButtonTWS.AutoSize = true;
            this.radioButtonTWS.Checked = true;
            this.radioButtonTWS.Location = new System.Drawing.Point(19, 60);
            this.radioButtonTWS.Name = "radioButtonTWS";
            this.radioButtonTWS.Size = new System.Drawing.Size(50, 17);
            this.radioButtonTWS.TabIndex = 3;
            this.radioButtonTWS.Text = "TWS";
            this.radioButtonTWS.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 570);
            this.Controls.Add(this.textBoxIndex);
            this.Controls.Add(this.buttonDeleteIndex);
            this.Controls.Add(this.buttonAddIndex);
            this.Controls.Add(this.textBoxFuture);
            this.Controls.Add(this.buttonDeleteFuture);
            this.Controls.Add(this.buttonAddFuture);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonDeleteOption);
            this.Controls.Add(this.buttonAddOption);
            this.Controls.Add(this.textBoxOption);
            this.Controls.Add(this.checkBoxLog);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBoxStock);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "Form1";
            this.Text = "Bloomberg Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockQuotesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxStock;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.ListBox listBoxLog;
         private System.Windows.Forms.CheckBox checkBoxLog;
         private System.Windows.Forms.TextBox textBoxOption;
         private System.Windows.Forms.Button buttonAddOption;
         private System.Windows.Forms.Button buttonDeleteOption;
         private System.Windows.Forms.BindingSource stockQuotesBindingSource;
         private DataSet1 dataSet1;
         private System.Windows.Forms.GroupBox groupBox1;
         private System.Windows.Forms.RadioButton radioButtonNativeBloomberg;
         private System.Windows.Forms.RadioButton radioButtonTCPBloomberg;
         private System.Windows.Forms.Button buttonAddFuture;
         private System.Windows.Forms.Button buttonDeleteFuture;
         private System.Windows.Forms.TextBox textBoxFuture;
         private System.Windows.Forms.TextBox textBoxIndex;
         private System.Windows.Forms.Button buttonDeleteIndex;
         private System.Windows.Forms.Button buttonAddIndex;
         private System.Windows.Forms.DataGridViewTextBoxColumn symbolDataGridViewTextBoxColumn;
         private System.Windows.Forms.DataGridViewCheckBoxColumn Subscribed;
         private System.Windows.Forms.DataGridViewTextBoxColumn Bid;
         private System.Windows.Forms.DataGridViewTextBoxColumn Ask;
         private System.Windows.Forms.DataGridViewTextBoxColumn midDataGridViewTextBoxColumn;
         private System.Windows.Forms.DataGridViewTextBoxColumn lastPriceDataGridViewTextBoxColumn;
         private System.Windows.Forms.DataGridViewTextBoxColumn Open;
         private System.Windows.Forms.DataGridViewTextBoxColumn PrevClose;
         private System.Windows.Forms.DataGridViewCheckBoxColumn closedDataGridViewCheckBoxColumn;
         private System.Windows.Forms.DataGridViewTextBoxColumn ClosingPrice;
         private System.Windows.Forms.DataGridViewTextBoxColumn Delta;
         private System.Windows.Forms.DataGridViewTextBoxColumn Gamma;
         private System.Windows.Forms.DataGridViewTextBoxColumn Theta;
         private System.Windows.Forms.DataGridViewTextBoxColumn Vega;
         private System.Windows.Forms.DataGridViewTextBoxColumn ImpliedVol;
         private System.Windows.Forms.DataGridViewTextBoxColumn timeDataGridViewTextBoxColumn;
         private System.Windows.Forms.RadioButton radioButtonTWS;
    }
}

