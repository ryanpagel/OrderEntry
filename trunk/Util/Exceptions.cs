using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickBooks.DataAccess;
using System.Windows.Forms;

namespace QuickBooks.Util
{
    [global::System.Serializable]
    public class QuickBooksException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public QuickBooksException() { }
        public QuickBooksException(string message) : base(message) { }
        public QuickBooksException(string message, Exception inner) : base(message, inner) { }
        protected QuickBooksException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        bool _hasBeenLogged;

        public bool HasBeenLogged
        {
            get { return _hasBeenLogged; }
            set { _hasBeenLogged = value; }
        }

        string _displayMessage = string.Empty;

        public string DisplayMessage
        {
            get { return _displayMessage; }
            set { _displayMessage = value; }
        }

        MessageBoxButtons _buttons;

        public MessageBoxButtons Buttons
        {
            get { return _buttons; }
            set { _buttons = value; }
        }

        MessageBoxIcon _icon;

        public MessageBoxIcon Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        string _caption;

        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        public QuickBooksException(string displayMessage, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            _displayMessage = displayMessage;
            _caption = caption;
            _buttons = buttons;
            _icon = icon;
        }
    }
}
