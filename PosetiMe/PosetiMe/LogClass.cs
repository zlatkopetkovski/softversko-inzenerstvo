using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace PosetiMe
{
    public static class LogClass
    {
        public static string sSource = "PosetiMe";
        public static string sLog = "Application";
        public static string StartUp = "D:/";

        public static void WriteInformationLog(String EventToWrite)
        {
            CheckIfLogExixts();
            EventLog.WriteEntry(sSource, EventToWrite, EventLogEntryType.Information);
           // File.WriteAllText(StartUp, DateTime.Now + "Info " + EventToWrite);
            File.AppendAllText(StartUp + @"/PosetiMelog.txt", DateTime.Now + " Information " + Environment.NewLine + EventToWrite + Environment.NewLine);

        }
        public static void WriteWarningLog(string EventToWrite)
        {
            CheckIfLogExixts();
            EventLog.WriteEntry(sSource, EventToWrite, EventLogEntryType.Warning);
            File.AppendAllText(StartUp + @"/PosetiMelog.txt", DateTime.Now + " Warning " + Environment.NewLine + EventToWrite + Environment.NewLine);
        }
        public static void WriteErrorLog(String EventToWrite)
        {
            CheckIfLogExixts();
            EventLog.WriteEntry(sSource, EventToWrite, EventLogEntryType.Error);
            File.AppendAllText(StartUp + @"/PosetiMelog.txt", DateTime.Now + " Error " + Environment.NewLine + EventToWrite + Environment.NewLine);
        }
        public static void CheckIfLogExixts()
        {
            if (!EventLog.SourceExists(sSource))
            {
                EventLog.CreateEventSource(sSource, sLog);
            }
        }
    }
}