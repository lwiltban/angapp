using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngService
{
    public enum LogEvent
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }

    public interface ILog
    {
        void WriteEntry(string message, LogEvent type);
        void WriteEntry(string message, Exception error, LogEvent type);

        void Trace(string message);
        void Trace(string message, Exception error);

        void Debug(string message);
        void Debug(string message, Exception error);

        void Info(string message);
        void Info(string message, Exception error);

        void Warn(string message);
        void Warn(string message, Exception error);

        void Error(string message);
        void Error(string message, Exception error);

        void Fatal(string message);

        void Fatal(string message, Exception error);

        event EventHandler OnLogItem;

    }
}
