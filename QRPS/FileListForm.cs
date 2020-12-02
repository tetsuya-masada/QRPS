using Microsoft.Office.Core;
using QRPS.CommonLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace QRPS
{
    public partial class FileListForm : Form
    {
        /// <summary>
        /// ログクラス
        /// </summary>
        private static Log _Log = new Log();

        /// <summary>
        /// Running File (PowerPoint)
        /// </summary>
        private Microsoft.Office.Interop.PowerPoint.Presentation ppt;

        public FileListForm()
        {
            InitializeComponent();
        }

        #region EventFunction
        /// <summary>
        /// form_Loadクラス
        /// </summary>
        private void FileListForm_Load(object sender, EventArgs e)
        {
            // ファイル一覧取得
            _Log.WriteDebugLog("ファイル一覧取得");
            GetTargetFile();

            // 指定COMポート接続
            ComConnection(Config.GetIniFileString(Config.Section.System.ToString(), Config.SystemKey.USBCOMName.ToString()));

            // USBシリアル用 COMポート取得
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports) { cbPort.Items.Add(port); }
            if (cbPort.Items.Count > 0)
                cbPort.SelectedIndex = 0;
        }

        /// <summary>
        /// 再読み込みボタン押下Event
        /// </summary>
        private void btnReload_Click(object sender, EventArgs e)
        {
            dgvFileNameList.Rows.Clear();
            GetTargetFile();
        }

        /// <summary>
        /// 閉じるボタン押下Event
        /// </summary>
        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// form_Closing Event
        /// </summary>
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

        /// <summary>
        /// DataGridView_CellClick Event
        /// </summary>
        private void dgvFileNameList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 実行中ファイルがある場合先に閉じる
            if (ppt != null)
            {
                try{ppt.Close();}
                catch (Exception ex){}
                finally{ppt = null;}
            }

            // ppt Setting
            var AppPpt = new Microsoft.Office.Interop.PowerPoint.Application();
            // ppt Open
            ppt = AppPpt.Presentations.Open(dgvFileNameList.Rows[e.RowIndex].Cells[1].Value.ToString(),
                MsoTriState.msoTrue, MsoTriState.msoTrue, MsoTriState.msoTrue);

            //SlideShow Setting
            Microsoft.Office.Interop.PowerPoint.SlideShowSettings settings;
            settings = ppt.SlideShowSettings;

            settings.Run();
        }

        #endregion EventFunction

        #region SubFunction

        /// <summary>
        /// 対象ファイル抽出
        /// </summary>
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

        /// <summary>
        /// COMポート接続
        /// </summary>
        private void ComConnection(string comName)
        {
            if (comName == "")
            {
                //COMポートがiniファイルに設定されていません。
                _Log.WriteErrorLog(string.Format(CommonLibrary.Utility.Message.GetMessage("E_0003")));
                lblConInf.Text = string.Format(CommonLibrary.Utility.Message.GetMessage("E_0003"));
                lblConInf.ForeColor = Color.Red;
            }
            else
            {
                try
                {
                    serialPort1.BaudRate = 115200;
                    serialPort1.Parity = Parity.None;
                    serialPort1.DataBits = 8;
                    serialPort1.StopBits = StopBits.One;
                    serialPort1.Handshake = Handshake.None;
                    serialPort1.ReadTimeout = 500;
                    serialPort1.WriteTimeout = 500;
                    serialPort1.PortName = comName;
                    serialPort1.Open();
                    // COMポート「" + comName + "」に接続しました。
                    _Log.WriteInfoLog(string.Format(CommonLibrary.Utility.Message.GetMessage("I_0003"), comName));
                    lblConInf.Text = string.Format(CommonLibrary.Utility.Message.GetMessage("I_0003"), comName);
                    lblConInf.ForeColor = Color.Black;
                }
                catch (Exception e)
                {
                    // COMポート「" + comName + "」に接続できませんでした。
                    _Log.WriteErrorLog(string.Format(CommonLibrary.Utility.Message.GetMessage("E_0004"), comName));
                    lblConInf.Text = string.Format(CommonLibrary.Utility.Message.GetMessage("E_0004"), comName);
                    lblConInf.ForeColor = Color.Red;
                }
            }
        }

        #endregion SubFunction

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ComConnection(cbPort.SelectedItem.ToString());
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] b = new byte[serialPort1.BytesToRead];
            serialPort1.Read(b, 0, b.Length);
            string str = System.Text.Encoding.ASCII.GetString(b);
            SrialPortDataReceived(str);
        }

        delegate void SetTextCallback(string text);
        private void SrialPortDataReceived (string text)
        {
            if (textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SrialPortDataReceived);
                BeginInvoke(d, new object[] { text });
            }
            else
            {
                textBox1.Text = text;
            }
        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
        }
    }
}
