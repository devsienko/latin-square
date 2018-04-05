using System;
using System.Diagnostics;

namespace Grid.Manager
{
    public class WorkerInvoker
    {
        public static string[] Invoke(string rowData, int index)
        {
            string[] result = null;
            var worker = new Process();
            var rootPath = @"C:\Users\daniil_\YandexDisk\Политех\Последняя сессия\Grid\";
            worker.StartInfo.FileName = rootPath + @"Grid.Worker\bin\Debug\Grid.Worker.exe";
            worker.StartInfo.Arguments = string.Format(@"{0} ""{1}"" {2}", rowData.Length, rowData, index);
            worker.StartInfo.UseShellExecute = false;
            worker.StartInfo.RedirectStandardOutput = true;
            worker.Start();
            var data = worker.StandardOutput.ReadToEnd();
            worker.WaitForExit();
            data = data.Substring(0, data.Length - 2);//removing new line
            result = data.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None
            );
            return result;
        }
    }
}
