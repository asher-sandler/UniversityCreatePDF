using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey_Form
{
    public class PDF_Obj
    {
        public string _pdf_log;
        public byte[] _pdf_bytes;
        public string _file_name;

         public PDF_Obj()
        {
            _pdf_log = "";
            _pdf_bytes = null;
            _file_name = "";
        }

         public string PDF_log
        {
            get
            {
                return this._pdf_log;
            }
            set
            {
                this._pdf_log = value;
            }
        }

         public byte[] PDF_bytes
        {
            get
            {
                return this._pdf_bytes;
            }
            set
            {
                this._pdf_bytes = value;
            }
        }

         public string File_Name
         {
             get
             {
                 return this._file_name;
             }
             set
             {
                 this._file_name = value;
             }
         }
    }
}
