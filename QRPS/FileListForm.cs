using Microsoft.Office.Core;
using QRPS.CommonLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
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

            // USBシリアル COMポート一覧取得
            // Debug用
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
                                                    "Warning",
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
            // pptを開いて、スライドショー実行
            OpenPPT(e.RowIndex);
        }

        /// <summary>
        /// シリアル通信受信イベント
        /// </summary>
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] b = new byte[serialPort1.BytesToRead];
            serialPort1.Read(b, 0, b.Length);
            string str = System.Text.Encoding.ASCII.GetString(b);
            SrialPortDataReceived(str);
        }

        // デリゲータ
        delegate void SetTextCallback(string text);
        /// <summary>
        /// シリアル通信受信 -> form通信
        /// </summary>
        private void SrialPortDataReceived(string text)
        {
            if (textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SrialPortDataReceived);
                BeginInvoke(d, new object[] { text });
            }
            else
            {
                try
                {
                    textBox1.Text = text;
                    string partsNo = text.Substring(
                        int.Parse(Config.GetIniFileString(Config.Section.System.ToString(), Config.SystemKey.startDigit.ToString())),
                        int.Parse(Config.GetIniFileString(Config.Section.System.ToString(), Config.SystemKey.endDigit.ToString())) - int.Parse(Config.GetIniFileString(Config.Section.System.ToString(), Config.SystemKey.startDigit.ToString()))
                        );
                    // 検索対象は品番+拡張子
                    textBox2.Text = partsNo + Config.GetIniFileString(Config.Section.System.ToString(), Config.SystemKey.targetFileExtend.ToString());
                    DataGridViewRow dgvr = dgvFileNameList.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Cells["FileName"].Value.ToString() ==
                      partsNo + Config.GetIniFileString(Config.Section.System.ToString(), Config.SystemKey.targetFileExtend.ToString()));

                    // pptを開いて、スライドショー実行
                    OpenPPT(dgvr.Index);
                }
                catch(Exception ex)
                {
                    _Log.WriteErrorLog(ex.Message);
                    _Log.WriteErrorLog(ex.StackTrace);
                    // システムエラー
                    _Log.WriteErrorLog(string.Format(CommonLibrary.Utility.Message.GetMessage("E_9999")));
                }
            }
        }

        #endregion EventFunction

        #region SubFunction

        /// <summary>
        /// 対象powerpointを開く
        /// </summary>
        private void OpenPPT(int rowIndex)
        {
            // 実行中ファイルがある場合先に閉じる
            if (ppt != null)
            {
                try { ppt.Close(); }
                catch (Exception ex) {
                    _Log.WriteErrorLog(ex.Message);
                    _Log.WriteErrorLog(ex.StackTrace);
                }
                finally { ppt = null; }
            }

            // ppt Setting
            var AppPpt = new Microsoft.Office.Interop.PowerPoint.Application();
            // ppt Open
            try
            {
                ppt = AppPpt.Presentations.Open(dgvFileNameList.Rows[rowIndex].Cells[1].Value.ToString(),
                    MsoTriState.msoTrue, MsoTriState.msoTrue, MsoTriState.msoTrue);
                
                //SlideShow Setting
                Microsoft.Office.Interop.PowerPoint.SlideShowSettings settings;
                settings = ppt.SlideShowSettings;

                settings.Run();
            }
            catch(Exception ex)
            {
                _Log.WriteErrorLog(ex.Message);
                _Log.WriteErrorLog(ex.StackTrace);
                // 該当ファイルが存在しません\r\n({0})
                _Log.WriteErrorLog(string.Format(CommonLibrary.Utility.Message.GetMessage("E_0005"), dgvFileNameList.Rows[rowIndex].Cells[1].Value.ToString()));
                lblInfo.Text = string.Format(CommonLibrary.Utility.Message.GetMessage("E_0005"), dgvFileNameList.Rows[rowIndex].Cells[1].Value.ToString());
                lblInfo.ForeColor = Color.Red;
                lblInfo.Font = new Font(lblInfo.Font, FontStyle.Bold);
                MessageBox.Show(string.Format(CommonLibrary.Utility.Message.GetMessage("E_0005"), dgvFileNameList.Rows[rowIndex].Cells[1].Value.ToString()),
                                                    "Warning",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Exclamation);
                this.Close();
            }

        }

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
                    MessageBox.Show(string.Format(CommonLibrary.Utility.Message.GetMessage("E_0001"), targetFldr),
                                                        "Warning",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Exclamation);
                    this.Close();
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
                _Log.WriteErrorLog(ex.StackTrace);
                // 対象フォルダが存在しません。\r\n({0})
                _Log.WriteErrorLog(string.Format(CommonLibrary.Utility.Message.GetMessage("E_0002"), targetFldr));
                lblInfo.Text = string.Format(CommonLibrary.Utility.Message.GetMessage("E_0002"), targetFldr);
                lblInfo.ForeColor = Color.Red;
                lblInfo.Font = new Font(lblInfo.Font, FontStyle.Bold);
                MessageBox.Show(string.Format(CommonLibrary.Utility.Message.GetMessage("E_0002"), targetFldr),
                                                        "Warning",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Exclamation);
                this.Close();
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
                MessageBox.Show(string.Format(CommonLibrary.Utility.Message.GetMessage("E_0003")),
                                                    "Warning",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Exclamation);
                this.Close();
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
                catch (Exception ex)
                {
                    _Log.WriteErrorLog(ex.Message);
                    _Log.WriteErrorLog(ex.StackTrace);
                    // COMポート「" + comName + "」に接続できませんでした。
                    _Log.WriteErrorLog(string.Format(CommonLibrary.Utility.Message.GetMessage("E_0004"), comName));
                    lblConInf.Text = string.Format(CommonLibrary.Utility.Message.GetMessage("E_0004"), comName);
                    lblConInf.ForeColor = Color.Red;
                    MessageBox.Show(string.Format(CommonLibrary.Utility.Message.GetMessage("E_0004"), comName),
                                                    "Warning",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Exclamation);
                    this.Close();
                }
            }
        }

        #endregion SubFunction

        #region Debug用

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ComConnection(cbPort.SelectedItem.ToString());
        }

        

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
        }

        #endregion Debug用
    }
}
