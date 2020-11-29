using QRPS.CommonLibrary.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace QRPS
{
    static class Program
    {
        /// <summary>
        /// ログクラス
        /// </summary>
        private static Log _Log = new Log();

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            _Log.WriteDebugLog("MainMenu起動");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // システム情報を取得する
            new Functions().GetSystemInfo();
            
            // Form起動
            Application.Run(new FileListForm());
        }
    }
}
