using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey_Form
{
    class Parameter
    {
        private string __sourceName;
        private string __bindName;
        private string __type;
        private string __source;
        private string __nullBehavior;
        private string __value;

        public Parameter()
        {
            __sourceName = string.Empty;
            __bindName = string.Empty;
            __type = string.Empty;
            __source = string.Empty;
            __nullBehavior = string.Empty;
            __value = string.Empty;
        }

        
        /// <summary>
        /// Get/Set parameter source name
        /// </summary>
        public string SourceName
        {
            get
            {
                return this.__sourceName;
            }
            set
            {
                this.__sourceName = value;
            }
        }

        /// <summary>
        /// Get/Set parameter bind name
        /// </summary>
        public string BindName
        {
            get
            {
                return this.__bindName;
            }
            set
            {
                this.__bindName = value;
            }
        }

        /// <summary>
        /// Get/Set parameter type
        /// </summary>
        public string Type
        {
            get
            {
                return this.__type;
            }
            set
            {
                this.__type = value;
            }
        }

        /// <summary>
        /// Get/Set parameter source ('viewstate' or URL)
        /// </summary>
        public string Source
        {
            get
            {
                return this.__source;
            }
            set
            {
                this.__source = value;
            }
        }

        /// <summary>
        /// Get/Set parameter null behavior (bindnull, error)
        /// </summary>
        public string NullBehavior
        {
            get
            {
                return this.__nullBehavior;
            }
            set
            {
                this.__nullBehavior = value;
            }
        }

        /// <summary>
        /// Get/Set parameter value
        /// </summary>
        public string Value
        {
            get
            {
                return this.__value;
            }
            set
            {
                this.__value = value;
            }
        }
    }
}
