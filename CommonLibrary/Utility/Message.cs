using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QRPS.CommonLibrary.Utility
{
    /// <summary>
    /// メッセージクラス
    /// </summary>
    public static class Message
    {
        #region 定数

        /// <summary>
        /// メッセージ情報を格納するxmlファイルパス
        /// </summary>
        private static string XmlFilePath = @"XML\\Message.xml";

        /// <summary>
        /// ノード名(Messages)
        /// </summary>
        private static string XmlElementMessages = "Messages";

        /// <summary>
        /// ノード名(ID)
        /// </summary>
        private static string XmlElementID = "ID";

        /// <summary>
        /// ノード名(Message)
        /// </summary>
        private static string XmlElementMessage = "Message";

        #endregion

        #region メンバ変数

        /// <summary>
        /// メッセージ引き当て用ディクショナリ(キー：ID、値：メッセージ)
        /// </summary>
        static Dictionary<string, string> _Dictionary = null;

        #endregion

        #region public関数

        #region 指定したIDのメッセージを取得する

        /// <summary>
        /// 指定したIDのメッセージを取得する
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>メッセージ</returns>
        public static string GetMessage(string id)
        {
            // ディクショナリが未作成の場合は作成する
            if (_Dictionary == null)
            {
                CreateDictionary();
            }

            // 指定したIDのメッセージを返却する(存在しないIDの場合は空文字列を返却する)
            return (_Dictionary.ContainsKey(id)) ? _Dictionary[id] : string.Empty;
        }
        #endregion 指定したIDのメッセージを取得する

        #endregion public関数

        #region private関数

        #region メッセージ引き当て用ディクショナリを作成する

        /// <summary>
        /// メッセージ引き当て用ディクショナリを作成する
        /// </summary>
        private static void CreateDictionary()
        {
            // xmlファイルを読み込みする
            XElement xml = XElement.Load(XmlFilePath);

            // Messagesタグの情報を取得する
            IEnumerable<XElement> infos = from item in xml.Elements(XmlElementMessages) select item;

            // ディクショナリを生成する
            _Dictionary = new Dictionary<string, string>();

            // Messagesタグの数分ループしてディクショナリに設定する
            foreach (XElement info in infos)
            {
                _Dictionary.Add(info.Element(XmlElementID).Value, info.Element(XmlElementMessage).Value);
            }
        }

        #endregion メッセージ引き当て用ディクショナリを作成する

        #endregion private関数
    }
}
