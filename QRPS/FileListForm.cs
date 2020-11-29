using QRPS.CommonLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QRPS
{
    public partial class FileListForm : Form
    {
        /// <summary>
        /// ログクラス
        /// </summary>
        private static Log _Log = new Log();

        public FileListForm()
        {
            InitializeComponent();
        }

        private void FileListForm_Load(object sender, EventArgs e)
        {
            // ファイル一覧取得
            _Log.WriteDebugLog("ファイル一覧取得");
            GetTargetFile();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            dgvFileNameList.Rows.Clear();
            GetTargetFile();
        }

        private void GetTargetFile()
        {
            String targetFldr = Config.GetIniFileString(Config.Section.System.ToString(), Config.SystemKey.PptxFldr.ToString());
            try
            {
                IEnumerable<string> files = Directory.EnumerateFiles(@targetFldr, "*" + Config.GetIniFileString(Config.Section.System.ToString(), Config.SystemKey.targetFileExtend.ToString()));


                foreach (string filepath in files)
                {
                    dgvFileNameList.Rows.Add(Path.GetFileName(filepath), filepath);
                }
                if (dgvFileNameList.RowCount == 0)
                {
                    // 対象フォルダが存在しないか、フォルダ内にファイルが存在しません。\r\n({0})
                    _Log.WriteErrorLog(string.Format(CommonLibrary.Utility.Message.GetMessage("E_0001"), targetFldr));
                    lblInfo.Text = string.Format(CommonLibrary.Utility.Message.GetMessage("E_0001"), targetFldr);
                    lblInfo.ForeColor = Color.Red;
                    lblInfo.Font = new Font(lblInfo.Font, FontStyle.Bold);
                }
                else
                {
                    // 対象フォルダのファイルを{0}件読み込みました。
                    _Log.WriteInfoLog(string.Format(CommonLibrary.Utility.Message.GetMessage("I_0001"), dgvFileNameList.RowCount));
                    lblInfo.Text = string.Format(CommonLibrary.Utility.Message.GetMessage("I_0001"), dgvFileNameList.RowCount);
                    lblInfo.ForeColor = Color.Black;
                    lblInfo.Font = new Font(lblInfo.Font, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                _Log.WriteErrorLog(ex.Message);
                // 対象フォルダが存在しません。\r\n({0})
                _Log.WriteErrorLog(string.Format(CommonLibrary.Utility.Message.GetMessage("E_0002"), targetFldr));
                lblInfo.Text = string.Format(CommonLibrary.Utility.Message.GetMessage("E_0002"), targetFldr);
                lblInfo.ForeColor = Color.Red;
                lblInfo.Font = new Font(lblInfo.Font, FontStyle.Bold);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formClosing_event(object sender, FormClosingEventArgs e)
        {
            // アプリケーションを終了します。
            DialogResult res = MessageBox.Show(string.Format(CommonLibrary.Utility.Message.GetMessage("I_0002")),
                                                    "質問",
                                                    MessageBoxButtons.OKCancel,
                                                    MessageBoxIcon.Exclamation);
            if (res == DialogResult.Cancel)
            {
                // 画面に戻る
                e.Cancel = true;
            }
        }
    }
}
