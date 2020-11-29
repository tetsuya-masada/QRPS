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
            this.dgvFileNameList = new System.Windows.Forms.DataGridView();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnReload = new System.Windows.Forms.Button();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnStop = new System.Windows.Forms.Button();
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
            this.dgvFileNameList.Location = new System.Drawing.Point(29, 69);
            this.dgvFileNameList.Name = "dgvFileNameList";
            this.dgvFileNameList.ReadOnly = true;
            this.dgvFileNameList.RowTemplate.Height = 21;
            this.dgvFileNameList.Size = new System.Drawing.Size(244, 301);
            this.dgvFileNameList.TabIndex = 0;
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
            this.btnReload.Location = new System.Drawing.Point(34, 400);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(126, 38);
            this.btnReload.TabIndex = 2;
            this.btnReload.Text = "再読み込み";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
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
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(323, 401);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(106, 36);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "終了";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // FileListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 450);
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
    }
}

