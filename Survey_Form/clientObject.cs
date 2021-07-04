using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey_Form
{

    class clientObject
    {
        string _controlId;
        string _clientEvent;
        string _eventFunc;
        private bool _required;
        private string _validCSS;
        private string _invalidCSS;

        public clientObject()
        {
        }
        public clientObject(string controlId, string clientEvent, string eventFunc)
        {
            _controlId = controlId;
            _clientEvent = clientEvent;
            _eventFunc = eventFunc;
            _validCSS = string.Empty;
            _invalidCSS = string.Empty;
        }


        public string controlId
        {
            get
            {
                return this._controlId;
            }
            set
            {
                this._controlId = value;
            }
        }

        public string clientEvent
        {
            get
            {
                return this._clientEvent;
            }
            set
            {
                this._clientEvent = value;
            }
        }

        public string eventFunc
        {
            get
            {
                return this._eventFunc;
            }
            set
            {
                this._eventFunc = value;
            }
        }

        public bool required
        {
            get
            {
                return this._required;
            }
            set
            {
                this._required = value;
            }
        }

        public string validCSS
        {
            get
            {
                return this._validCSS;
            }
            set
            {
                this._validCSS = value;
            }
        }
        public string invalidCSS
        {
            get
            {
                return this._invalidCSS;
            }
            set
            {
                this._invalidCSS = value;
            }
        }
    }
}
