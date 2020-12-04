using System.ComponentModel;
using System.Configuration.Install;
using System.Collections;
using System.IO;

namespace QRPS
{
    [RunInstaller(true)]
    public class InstallerCustomAction : Installer
    {
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);

            string appPath = Path.GetDirectoryName(this.Context.Parameters["AssemblyPath"]);
            System.Windows.Forms.MessageBox.Show("プロパティファイルに設定値を登録してください。");
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = appPath + "\\QRPS.ini";
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
            proc.WaitForExit();
            // ファイルを閉じるのを待つ
            //System.Diagnostics.Process p = System.Diagnostics.Process.Start(appPath+"\\QRPS.ini");
        }
    }
}
