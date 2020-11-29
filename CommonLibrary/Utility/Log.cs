using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QRPS.CommonLibrary.Utility
{
    /// <summary>
    /// ログクラス
    /// </summary>
    public class Log
    {

        #region メンバ変数

        /// <summary>
        /// ロガー
        /// </summary>
        private log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion メンバ変数

        #region public関数

        #region WriteLog

        /// <summary>
        /// Debugログ書き込み
        /// </summary>
        /// <param name="msg"></param>
        public void WriteDebugLog(string msg, [CallerFilePath] string filepath = "", [CallerMemberName] string name = "", [CallerLineNumber] int line = 0)
        {
            try
            {
                string strMsg = string.Format("{0}, {1}, {2}, {3}", Path.GetFileNameWithoutExtension(filepath), name, line, msg);
                _Logger.Debug(strMsg);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Infoログ書き込み
        /// </summary>
        /// <param name="msg"></param>
        public void WriteInfoLog(string msg, [CallerFilePath] string filepath = "", [CallerMemberName] string name = "", [CallerLineNumber] int line = 0)
        {
            try
            {
                string strMsg = string.Format("{0}, {1}, {2}, {3}", Path.GetFileNameWithoutExtension(filepath), name, line, msg);
                _Logger.Info(strMsg);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Warnログ書き込み
        /// </summary>
        /// <param name="msg"></param>
        public void WriteWarnLog(string msg, [CallerFilePath] string filepath = "", [CallerMemberName] string name = "", [CallerLineNumber] int line = 0)
        {
            try
            {
                string strMsg = string.Format("{0}, {1}, {2}, {3}", Path.GetFileNameWithoutExtension(filepath), name, line, msg);
                _Logger.Warn(strMsg);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Errorログ書き込み
        /// </summary>
        /// <param name="msg"></param>
        public void WriteErrorLog(string msg, [CallerFilePath] string filepath = "", [CallerMemberName] string name = "", [CallerLineNumber] int line = 0)
        {
            try
            {
                string strMsg = string.Format("{0}, {1}, {2}, {3}", Path.GetFileNameWithoutExtension(filepath), name, line, msg);
                _Logger.Error(strMsg);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Fatalログ書き込み
        /// </summary>
        /// <param name="msg"></param>
        public void WriteFatalLog(string msg, [CallerFilePath] string filepath = "", [CallerMemberName] string name = "", [CallerLineNumber] int line = 0)
        {
            try
            {
                string strMsg = string.Format("{0}, {1}, {2}, {3}", Path.GetFileNameWithoutExtension(filepath), name, line, msg);
                _Logger.Fatal(strMsg);
            }
            catch (Exception)
            {
            }
        }

        #endregion WriteLog

        #endregion public関数
    }
}
