using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRPS.CommonLibrary.Utility
{
    /// <summary>
    /// 共通関数クラス
    /// </summary>
    public class Functions
    {
        #region システム情報取得

        /// <summary>
        /// システム情報取得
        /// </summary>
        public void GetSystemInfo()
        {
            Config.ReadIniFile();
        }

        #endregion システム情報取得
    }
}
