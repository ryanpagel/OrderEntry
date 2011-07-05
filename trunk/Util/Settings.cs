using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.IO;

namespace QuickBooks.Util
{
    public class Settings : QuickBooks.Util.ISettings
    {

        public Settings()
        {
            
        }

        bool _changesMade;
        
        [IncludeAttribute(false)]
        public bool ChangesMade
        {
            get { return _changesMade; }
        }

        string _taxableState;

        public string TaxableState
        {
            get { return _taxableState; }
            set { _taxableState = value; }
        }

        string _inStateTaxCodeName;

        public string InStateTaxCodeName
        {
            get { return _inStateTaxCodeName; }
            set { _inStateTaxCodeName = value; }
        }

        string _outOfStateTaxCodeName;

        public string OutOfStateTaxCodeName
        {
            get { return _outOfStateTaxCodeName; }
            set { _outOfStateTaxCodeName = value; }
        }

        double _taxableRate;

        public double TaxableRate
        {
            get { return _taxableRate; }
            set { _taxableRate = value; }
        }

        int _maxLogSizeBytes;

        public int MaxLogSizeBytes
        {
            get { return _maxLogSizeBytes; }
            set 
            {
                if (_maxLogSizeBytes != value)
                    _changesMade = true;
                _maxLogSizeBytes = value; 
            }
        }

        string _appRootPath;
        public string AppRootPath
        {
            get { return _appRootPath; }
            set 
            {
                
                if (_appRootPath != value)
                    _changesMade = true;
                _appRootPath = value; 
            }
        }

        string _quickBooksFilePath;

        public string QuickBooksFilePath
        {
            get { return _quickBooksFilePath; }
            set 
            {
                if (_quickBooksFilePath != value)
                    _changesMade = true;
                _quickBooksFilePath = value; 
            }
        }

        string _qbAppName;

        public string QbAppName
        {
            get { return _qbAppName; }
            set 
            {
                if (_qbAppName != value)
                    _changesMade = true;
                _qbAppName = value; 
            }
        }
        
        
        [IncludeAttribute(false)]
        public string LogFilePath
        {
            get
            {
                return _appRootPath + "\\Log\\QBLog.txt";
            }
        }

        [IncludeAttribute(false)]
        public string LogDirectory
        {
            get
            {
                return _appRootPath + "\\Log";
            }
        }

        [IncludeAttribute(false)]
        public string AppDataDirectory
        {
            get{return _appRootPath + "\\AppData";}
        }

        [IncludeAttribute(false)]
        public string ConfigFilePath
        {
            get { return AppDataDirectory + "\\app.cfg"; }
        }

        string _pendingOrdersPath;
        public string PendingOrdersPath
        {
            get { return _pendingOrdersPath; }
            set { _pendingOrdersPath = value; }
        }

        string _customerPrivateFieldsID;

        public string CustomerPrivateFieldsID
        {
            get { return _customerPrivateFieldsID; }
            set 
            {
                if (_customerPrivateFieldsID != value)
                    _changesMade = true;
                _customerPrivateFieldsID = value; 
            }
        }

        bool _isInitialStartup;

        public bool IsInitialStartup
        {
            get { return _isInitialStartup; }
            set { _isInitialStartup = value; }
        }

        public void NotifyIfChangesMades()
        {
            if (_changesMade)
            {
                _changesMade = false;

                if (SettingsReconfigured != null)
                    SettingsReconfigured(this);
            }
        }

       

        public event Action<Settings> SettingsReconfigured;

        public void Load(string filePath)
        {
            var xDoc = XDocument.Load(filePath);

            var props = typeof(Settings).GetProperties().Where(p => p.MemberType == System.Reflection.MemberTypes.Property).Select(p=>p);
            foreach (var item in xDoc.Root.Elements("Setting"))
            {
                Type t = typeof(string);
                
                if(item.Attribute("Type") != null)
                {
                    string sType = string.Empty;
                    sType = item.Attribute("Type").Value;
                    t = Type.GetType(sType);
                }
                
                string value = item.Attribute("Value").Value;
                Object oVal;
                if (value == "" && t.IsValueType)
                    oVal = Activator.CreateInstance(t);
                else oVal = Convert.ChangeType(value, t);

                var prop = props.Where(p => p.Name == item.Attribute("Name").Value).Select(p => p).Single();
                prop.SetValue(this, oVal, null);
                
            }
            
            if(_appRootPath == "")
                _appRootPath = Environment.CurrentDirectory;
            
            if (_pendingOrdersPath == "")
                _pendingOrdersPath = _appRootPath + "\\PendingOrders";

            _changesMade = false;
        }

        public void Save(string path)
        {
            XDocument xDoc = new XDocument(
                new XElement("Settings"));

            var props = typeof(Settings).GetProperties().Where(p => p.MemberType == System.Reflection.MemberTypes.Property).Select(p => p);
            
            foreach (var item in props)
            {
                bool include = true;
                var attr = item.GetCustomAttributes(typeof(IncludeAttribute), false).ToList();
                if (attr != null && attr.Count == 1)
                {
                    IncludeAttribute ia = (IncludeAttribute)attr[0];
                    include = ia.Include;
                }
                if (!include)
                    continue;

                string name = item.Name;
                string val = item.GetValue(this, null).ToString();
                string type = item.GetValue(this, null).GetType().ToString();

                xDoc.Root.Add(new XElement("Setting",
                             new XAttribute("Name", name),
                             new XAttribute("Value", val),
                             new XAttribute("Type", type)));

            }
            xDoc.Save(path);
        }

        public void Verify()
        {
            if (!File.Exists(_quickBooksFilePath))
            {
                throw new Exception("QuickBooks file path does not exist at " + _quickBooksFilePath);
            }

            if (!Directory.Exists(LogDirectory))
                throw new Exception("Log path does not exist at " + LogDirectory);

            if (!Directory.Exists(AppDataDirectory))
                throw new Exception("App data path does not exist at " + AppDataDirectory);

            if (_maxLogSizeBytes < 1000000)
                throw new Exception("Max Log Size must be at least 1,000,000 bytes of capacity");
        }


        public bool IsConnected { get; set; }
    }
}
