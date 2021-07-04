using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;



namespace Survey_Form
{
    class DynamicControl
    {
        protected Control _controlType;
        protected bool _controlRequired;
        protected string _id;
        protected string _dataSource;
        protected string _listSource;
        protected bool _printable;
        protected Object _data;
        protected string _formula;
        protected string _uploadFilePath;
        protected byte[] _fileBytes;
        protected string _regEx;
        protected string _rowNum;
        protected string _FilterList;
        protected string _filterColumn;
        protected string _displayColumn;
        protected string _filter;
        protected int _ddlOrder;
        protected string _ddlGroup;
        protected int _item;

        public DynamicControl()
        {
        }

        public DynamicControl(Control controlType, bool controlRequired, string id, string dataSource, bool printable, Object data, string rowNum)
        {
            _controlType = controlType;
            _controlRequired = controlRequired;
            _id = id;
            _dataSource = dataSource;
            _printable = printable;
            _data = data;
            _rowNum = rowNum;
            _regEx = string.Empty;
            
        }
        public DynamicControl(Control controlType, bool controlRequired, string id, string dataSource, bool printable, Object data)
        {
            _controlType = controlType;
            _controlRequired = controlRequired;
            _id = id;
            _dataSource = dataSource;
            _printable = printable;
            _data = data;
            _regEx = string.Empty;

        }

        public string UploadFilePath
        {
            get
            {
                return this._uploadFilePath;
            }
            set
            {
                this._uploadFilePath = value;
            }
        }



        //Get / Set control type
        public Control ControlType
        {
            get
            {
                return this._controlType;
            }
            set
            {
                this._controlType = value;
            }
        }

        public string ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public bool ControlRequired
        {
            get
            {
                return this._controlRequired;
            }
            set
            {
                this._controlRequired = value;
            }
        }

        public string DataSource
        {
            get
            {
                return this._dataSource;
            }
            set
            {
                this._dataSource = value;
            }
        }

        public string ListSource
        {
            get
            {
                return this._listSource;
            }
            set
            {
                this._listSource = value;
            }
        }

        //
        public bool Printable
        {
            get
            {
                return this._printable;
            }
            set
            {
                this._printable = value;
            }
        }
        public Object Data
        {
            get
            {
                return this._data;
            }
            set
            {
                this._data = value;
            }
        }
        public string Formula
        {
            get
            {
                return this._formula;
            }
            set
            {
                this._formula = value;
            }
        }

        //
        public byte[] FileBytes
        {
            get
            {
                return this._fileBytes;
            }
            set
            {
                this._fileBytes = value;
            }
        }

        public string RegEx
        {
            get
            {
                return this._regEx;
            }
            set
            {
                this._regEx = value;
            }
        }

        /// <summary>
        /// Dependent DropDownList Properties
        /// </summary>
        public string FilterList
        {
            get
            {
                return this._FilterList;
            }
            set
            {
                this._FilterList = value;
            }
        }

        public string FilterColumn
        {
            get
            {
                return this._filterColumn;
            }
            set
            {
                this._filterColumn = value;
            }
        }
        public string DisplayColumn
        {
            get
            {
                return this._displayColumn;
            }
            set
            {
                this._displayColumn = value;
            }
        }
        public string FilterControl
        {
            get
            {
                return this._filter;
            }
            set
            {
                this._filter = value;
            }
        }
        public int DdlOrder
        {
            get
            {
                return this._ddlOrder;
            }
            set
            {
                this._ddlOrder = value;
            }
        }

        public string DdlGroup
        {
            get
            {
                return this._ddlGroup;
            }
            set
            {
                this._ddlGroup = value;
            }
        }
        public string RowNum
        {
            get
            {
                return this._rowNum;
            }
            set
            {
                this._rowNum = value;
            }
        }

        /// <summary>
        /// Item num 
        /// </summary>
        public int Item
        {
            get
            {
                return this._item;
            }
            set
            {
                this._item = value;
            }
        }

    }
}
