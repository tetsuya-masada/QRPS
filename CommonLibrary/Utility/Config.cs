using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace QRPS.CommonLibrary.Utility
{
    /// <summary>
    /// コンフィグクラス
    /// </summary>
    public static class Config
    {

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        #region メンバ変数

        #region Section

        /// <summary>
        /// Section
        /// </summary>
        public enum Section
        {
            System,

        }

        #endregion Section

        #region SystemKey

        /// <summary>
        /// SystemKey
        /// </summary>
        public enum SystemKey
        {
            PptxFldr,
            USBCOMName,
            targetFileExtend,
            startDigit,
            endDigit
        }

        #endregion SystemKey

        /// <summary>
        /// iniファイル情報
        /// </summary>
        private static Dictionary<Tuple<string, string>, string> _iniData = new Dictionary<Tuple<string, string>, string>();

        #endregion メンバ変数

        #region Public関数

        #region iniファイルを取得する

        /// <summary>
        /// iniファイルを取得する
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static void ReadIniFile()
        {
            // ファイルパスは実行ファイルと同じとする
            string iniFileName = ".\\" + Path.GetFileName(Environment.GetCommandLineArgs()[0]).Replace(".exe", ".ini");
            StringBuilder sb = new StringBuilder(1024);
            _iniData = new Dictionary<Tuple<string, string>, string>();

            string section = Section.System.ToString();
            // iniファイルを取得する
            foreach (string key in System.Enum.GetNames(typeof(SystemKey)))
            {
                GetPrivateProfileString(section, key, "", sb, Convert.ToUInt32(sb.Capacity), iniFileName);
                _iniData.Add(Tuple.Create(section, key), sb.ToString());
            }

            return;
        }

        #endregion iniファイルを取得する

        #region iniファイルの文字列を取得する

        /// <summary>
        /// iniファイルの文字列を取得する
        /// </summary>
        /// <param name="section">セクション</param>
        /// <param name="key">キー</param>
        /// <returns>Iniファイル設定文字列</returns>
        public static string GetIniFileString(string section, string key)
        {

            string resultVal = string.Empty;

            if (_iniData.TryGetValue(Tuple.Create(section, key), out string value))
            {
                resultVal = value;
            }

            return resultVal;
        }

        #endregion iniファイルの文字列を取得する

        #endregion Public関数
    }
}
