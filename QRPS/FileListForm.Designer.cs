namespace QRPS
{
    partial class FileListForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvFileNameList = new System.Windows.Forms.DataGridView();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnDisConnect = new System.Windows.Forms.Button();
            this.lblConInf = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileNameList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFileNameList
            // 
            this.dgvFileNameList.AllowUserToAddRows = false;
            this.dgvFileNameList.AllowUserToDeleteRows = false;
            this.dgvFileNameList.AllowUserToOrderColumns = true;
            this.dgvFileNameList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFileNameList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.filePath});
            this.dgvFileNameList.Location = new System.Drawing.Point(35, 69);
            this.dgvFileNameList.Name = "dgvFileNameList";
            this.dgvFileNameList.ReadOnly = true;
            this.dgvFileNameList.RowTemplate.Height = 21;
            this.dgvFileNameList.Size = new System.Drawing.Size(244, 301);
            this.dgvFileNameList.TabIndex = 0;
            this.dgvFileNameList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFileNameList_CellClick);
            // 
            // FileName
            // 
            this.FileName.HeaderText = "ファイル名";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Width = 200;
            // 
            // filePath
            // 
            this.filePath.HeaderText = "ファイルパス";
            this.filePath.Name = "filePath";
            this.filePath.ReadOnly = true;
            this.filePath.Visible = false;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblInfo.Location = new System.Drawing.Point(32, 17);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(74, 15);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Infomation";
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(12, 400);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(126, 38);
            this.btnReload.TabIndex = 2;
            this.btnReload.Text = "再読み込み";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(222, 402);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(106, 36);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "終了";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(345, 79);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(154, 50);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "COM Connection";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cbPort
            // 
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Location = new System.Drawing.Point(345, 53);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(221, 20);
            this.cbPort.TabIndex = 5;
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(345, 231);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(173, 19);
            this.textBox1.TabIndex = 6;
            // 
            // btnDisConnect
            // 
            this.btnDisConnect.Location = new System.Drawing.Point(515, 79);
            this.btnDisConnect.Name = "btnDisConnect";
            this.btnDisConnect.Size = new System.Drawing.Size(154, 50);
            this.btnDisConnect.TabIndex = 4;
            this.btnDisConnect.Text = "COM DisConnection";
            this.btnDisConnect.UseVisualStyleBackColor = true;
            this.btnDisConnect.Click += new System.EventHandler(this.btnDisConnect_Click);
            // 
            // lblConInf
            // 
            this.lblConInf.AutoSize = true;
            this.lblConInf.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblConInf.Location = new System.Drawing.Point(32, 373);
            this.lblConInf.Name = "lblConInf";
            this.lblConInf.Size = new System.Drawing.Size(94, 14);
            this.lblConInf.TabIndex = 7;
            this.lblConInf.Text = "ConnectionInfo";
            // 
            // FileListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 450);
            this.Controls.Add(this.lblConInf);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cbPort);
            this.Controls.Add(this.btnDisConnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.dgvFileNameList);
            this.Name = "FileListForm";
            this.Text = "ファイルリスト";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formClosing_event);
            this.Load += new System.EventHandler(this.FileListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileNameList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFileNameList;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn filePath;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbPort;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnDisConnect;
        private System.Windows.Forms.Label lblConInf;
    }
}

