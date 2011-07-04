using System;
namespace QuickBooks.Util
{
    public interface ILogger
    {
        string GetLogText();
        void Log(string text);
        void LogException(string msg, Exception ex);
        void LogException(Exception ex);
    }
}
