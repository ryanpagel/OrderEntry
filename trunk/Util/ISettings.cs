using System;
namespace QuickBooks.Util
{
    public interface ISettings
    {
        string AppDataDirectory { get; }
        string AppRootPath { get; set; }
        bool ChangesMade { get; }
        string ConfigFilePath { get; }
        string CustomerPrivateFieldsID { get; set; }
        string InStateTaxCodeName { get; set; }
        bool IsInitialStartup { get; set; }
        void Load(string filePath);
        string LogDirectory { get; }
        string LogFilePath { get; }
        int MaxLogSizeBytes { get; set; }
        void NotifyIfChangesMades();
        string OutOfStateTaxCodeName { get; set; }
        string PendingOrdersPath { get; set; }
        string QbAppName { get; set; }
        string QuickBooksFilePath { get; set; }
        void Save(string path);
        event Action<Settings> SettingsReconfigured;
        double TaxableRate { get; set; }
        string TaxableState { get; set; }
        bool IsConnected { get; set; }
        void Verify();
    }
}
