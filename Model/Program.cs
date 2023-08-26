using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsComtrolProgram.Model
{
    public class Program
    {
        public string ProgramName { get; set; }
        public double TimeLife { get; set; }
        public double TimeWork { get; set; }

        public Program(string programName, double timeLife = 0, double timeWork = 0)
        {
            ProgramName = programName;
            TimeLife = timeLife;
            TimeWork = timeWork;
        }

        public static void BlockProcess(string procName)
        {
            var process = Process.GetProcessesByName(procName);
            if (process.Length > 0)
            {
                foreach (var item in process)
                {
                    item.CloseMainWindow();
                    item.WaitForExit(100);
                    if (!item.HasExited)
                    {
                        item.Kill();
                    }
                }
            }
        }
    }
}
