using System;
using System.Windows;

namespace IS3.Core
{
    public delegate void ConsoleDelegate(string str);

    public enum ErrorReportTarget {DebugConsole, MessageBox, DelegateConsole};

    // Summary:
    //     Error report class
    // Remarks:
    //     Available error report target include:
    //          DebugConsole
    //          MessageBox
    //          DelegateConsole: user defined function
    //
    public static class ErrorReport
    {
        public static ErrorReportTarget target = ErrorReportTarget.MessageBox;
        public static ConsoleDelegate consoleDelegate = null;

        public static void Report(string error)
        {
            if (target == ErrorReportTarget.DebugConsole)
                Console.Write(error);
            else if (target == ErrorReportTarget.MessageBox)
                MessageBox.Show(error, "Error");
            else if (target == ErrorReportTarget.DelegateConsole)
            {
                if (consoleDelegate != null)
                    consoleDelegate(error);
            }
        }
    }
}
