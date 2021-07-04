using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey_Form
{
    class Unique_Key
    {

        private string _value;
        private string _list;
        private string _column;

        public Unique_Key()
        {
            _value = "";
            _list = "";
            _column = "";
        }

        /// <summary>
        /// Get/Set parameter source name
        /// </summary>
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }

        /// <summary>
        /// Get/Set parameter SPList name
        /// </summary>
        public string List
        {
            get
            {
                return this._list;
            }
            set
            {
                this._list = value;
            }
        }

        /// <summary>
        /// Get/Set parameter SPListColumn name
        /// </summary>
        public string Column
        {
            get
            {
                return this._column;
            }
            set
            {
                this._column = value;
            }
        }
    }
}
