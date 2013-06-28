using System;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using NLog.Targets;
using NLog.Config;
using NLog.Web;

namespace AngService
{
    public class Log : ILog
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public event EventHandler OnLogItem;

        public Log()
        {
            var unit = ConfigurationManager.AppSettings["NLog"];
            var source = ConfigurationManager.AppSettings["NLogSource"];
            if (string.IsNullOrEmpty(source))
            {
                source = "AngApp";
            }

            var config = new LoggingConfiguration();
            //var ic = new InstallationContext
            //{
            //     IgnoreFailures = true,
            //     LogLevel = LogLevel.Trace
            //};

            //config.Install( ic );

            var layout = "${date:format=HH:mm:ss}|${level}|${stacktrace}|${message}";

            var fileTarget = new FileTarget
            {
                Name = "file",
                Layout = layout,
                FileName = "${basedir}/${shortdate}/${level}.log",
                CreateDirs = true
            };

            var traceTarget = new AspNetTraceTarget
            {
                Name = "aspnet",
                Layout = layout
            };

            var eventTarget = new EventLogTarget
            {
                Name = "eventlog",
                Layout = layout,
                Source = source,
                //Log = "WISE"
                Log = "Application"
            };

            switch (unit)
            {
                case "file":
                    config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));
                    config.AddTarget("file", fileTarget);
                    break;

                case "trace":
                    config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, traceTarget));
                    config.AddTarget("aspnet", traceTarget);
                    break;

                case "event":
                    config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, eventTarget));
                    config.AddTarget("eventlog", eventTarget);
                    break;
            }

            if (unit != null && unit != "")
            {
                LogManager.Configuration = config;
            }
        }

        public void WriteEntry(string message, LogEvent type)
        {
            switch (type)
            {
                case LogEvent.Trace:
                    Trace(message);
                    break;
                case LogEvent.Debug:
                    Debug(message);
                    break;
                case LogEvent.Info:
                    Info(message);
                    break;
                case LogEvent.Warn:
                    Warn(message);
                    break;
                case LogEvent.Error:
                    Error(message, null);
                    break;
                case LogEvent.Fatal:
                    Fatal(message, null);
                    break;
            }
            if (OnLogItem != null)
                OnLogItem.Invoke(String.Format("Trace: {0}", message), null);
        }

        public void WriteEntry(string message, Exception error, LogEvent type)
        {
            switch (type)
            {
                case LogEvent.Trace:
                    Trace(message, error);
                    break;
                case LogEvent.Debug:
                    Debug(message, error);
                    break;
                case LogEvent.Info:
                    Info(message, error);
                    break;
                case LogEvent.Warn:
                    Warn(message, error);
                    break;
                case LogEvent.Error:
                    Error(message, error);
                    break;
                case LogEvent.Fatal:
                    Fatal(message, error);
                    break;
            }
            if (OnLogItem != null)
                OnLogItem.Invoke(String.Format("Trace: {0}", message), null);

        }

        public void Trace(string message)
        {
            logger.Trace(String.Format("Trace: {0}", message));
            if (OnLogItem != null)
                OnLogItem.Invoke(String.Format("Trace: {0}", message), null);
        }

        public void Trace(string message, Exception error)
        {
            logger.TraceException(String.Format("Trace: {0}", message), error);
            if (OnLogItem != null)
                OnLogItem.Invoke(String.Format("Trace: {0}", message), null);
        }

        public void Debug(string message)
        {
            logger.Debug(String.Format("Debug: {0}", message));
            if (OnLogItem != null)
                OnLogItem.Invoke(String.Format("Debug: {0}", message), null);
        }

        public void Debug(string message, Exception error)
        {
            logger.DebugException(String.Format("Debug: {0}", message), error);
            if (OnLogItem != null)
                OnLogItem.Invoke(String.Format("Debug: {0}", message), null);
        }

        public void Info(string message)
        {
            logger.Info(string.Format("Info: {0}", message));
            if (OnLogItem != null)
                OnLogItem.Invoke(String.Format("Info: {0}", message), null);
        }

        public void Info(string message, Exception error)
        {
            logger.InfoException(String.Format("Info: {0}", message), error);
            if (OnLogItem != null)
                OnLogItem.Invoke(String.Format("Info: {0}", message), null);
        }

        public void Warn(string message)
        {
            logger.Warn(string.Format("Warning: {0}", message));
            if (OnLogItem != null)
                OnLogItem.Invoke(String.Format("Warn: {0}", message), null);
        }

        public void Warn(string message, Exception error)
        {
            //logger.WarnException(message, error);
            logger.Warn(string.Format("Warning: {0} ---> {1} --- end of inner exception", message, error));
            if (OnLogItem != null)
                OnLogItem.Invoke(String.Format("Warn: {0}", message), null);
        }

        public void Error(string message)
        {
            logger.Error(string.Format("Error: {0}", message));
            if (OnLogItem != null)
                OnLogItem.Invoke(String.Format("Error: {0}", message), null);
        }

        public void Error(string message, Exception error)
        {
            //logger.ErrorException(message, error);
            logger.Error(string.Format("Error: {0} ---> {1} --- end of inner exception", message, error));
            if (OnLogItem != null)
                OnLogItem.Invoke(String.Format("Error: {0} {1}", message, error.Message), null);
        }

        public void Fatal(string message)
        {
            logger.Fatal(String.Format("****FATAL****: {0}", message));
            if (OnLogItem != null)
                OnLogItem.Invoke(String.Format("****FATAL****: {0}", message), null);
        }

        public void Fatal(string message, Exception error)
        {
            logger.FatalException(String.Format("****FATAL****: {0} {1}", message, error.Message), error);
            if (OnLogItem != null)
                OnLogItem.Invoke(String.Format("****FATAL****: {0}", message), null);
        }
    }
}
