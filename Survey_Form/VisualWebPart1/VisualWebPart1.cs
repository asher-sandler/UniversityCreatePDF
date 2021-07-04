using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Survey_Form.VisualWebPart1
{
    [ToolboxItemAttribute(false)]
    public class VisualWebPart1 : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/Survey_Form/VisualWebPart1/VisualWebPart1UserControl.ascx";
        const string c_filePath = @"\\ekeksql00\SP_Resources$\HSS\default";

        const string c_ldap = "LDAP://DC=ekmd,DC=huji,DC=uni";
        private string _ldap;

        const string c_ccLdap = "LDAP://DC=hustaff,DC=huji,DC=local";
        private string _ccLdap;

        const string c_ccDomain = "CC";
        private string _ccDomain;

        const string c_domain = "EKMD";
        private string _domain;

        const string c_mailServer = "ekekcaa00.ekmd.huji.uni";
        private string _mailServer;

        const string c_fileName = "DynamicFormTest.xml";
        private string _fileName;

        const string c__headerTag = "Header";
        const string c__labelTag = "label";
        const string c__docHeader = "docHeader";
        const string c_DataTag = "data";
        const string c_TypeTag = "type";
        const string c_buttonTag = "Button";
        const string c_messageTag = "finalMessage";
        const int c_width = 200;
        const int c_height = 150;

        const int c_shortBoxWidth = 70;
        const int c_mediumBoxWidth = 200;
        const int c_longBoxWidth = 300;

        const bool DOWNLOAD_PDF = false;
        private bool download_pdf;

        const bool SEND_PDF_ATTACHMENT = false;
        private bool send_pdf_attachment;


        private const string c_fontName = "Arial";
        private string _fontName;

        private const string c__docHeaderColor = "#3560a0";
        private string __docHeaderColor;

        private const string c__doc_headerFontSize = "18px";
        private string __doc_headerFontSize;

        private const string c_sectionHeaderColor = "#3560a0";
        private string _sectionHeaderColor;

        private const string c_section_headerFontSize = "14px";
        private string _section_headerFontSize;

        private const string c__dataFontSize = "12px";
        private string __dataFontSize;

        private const string c_dataColor = "#545556";
        private string _dataColor;

        private const string c__configPath = "/rows/config";
        private string __configPath;

        private const string c__dataPath = "/rows/row";
        private string __dataPath;

        private const string c_controlPath = "/rows/row/control";
        private string _controlPath;

        private const bool c_adminMode = false;
        private bool _adminMode;



        private const float c_docHeaderFontSize = 16;
        private float _docHeaderFontSize;

        private const float c_textDataFontSize = 10;
        private const float c_labelFontSize = 10;
        private const float c_headerFontSize = 11;

        public enum c_textDirection { rtl = 0, ltr = 1 };
        private c_textDirection _textDirection;
        public enum c_textAlign { right = 0, left = 1 };
        private c_textAlign _textAlign;
        private bool c_addColumns = false;
        private bool c_addLists = false;


        //private string _applicantList;
        private string _filePath;

        private string __labelTag;
        private string __headerTag;
        private string __docHeader;
        private string _DataTag;
        private string _TypeTag;
        private string _buttonTag;
        private string _messageTag;



        private int _width;
        private int _height;

        private int _shortBoxWidth;
        private int _mediumBoxWidth;
        private int _longBoxWidth;

        private bool _addColumns;
        private bool _addLists;


        private float _textDataFontSize;
        private float _labelFontSize;
        private float _headerFontSize;


        public VisualWebPart1()
        {
            signature_button_text = SIGNATURE_BUTTON_TEXT;
            // _applicantList = c_applicantList;
            _filePath = c_filePath;
            _fileName = c_fileName;
            __headerTag = c__headerTag;
            __labelTag = c__labelTag;
            __docHeader = c__docHeader;

            _fontName = c_fontName;
            __docHeaderColor = c__docHeaderColor;
            __doc_headerFontSize = c__doc_headerFontSize;
            _sectionHeaderColor = c_sectionHeaderColor;
            _section_headerFontSize = c_section_headerFontSize;
            __dataFontSize = c__dataFontSize;
            _dataColor = c_dataColor;

            __configPath = c__configPath;
            __dataPath = c__dataPath;
            _DataTag = c_DataTag;
            _TypeTag = c_TypeTag;
            _buttonTag = c_buttonTag;
            _messageTag = c_messageTag;
            _controlPath = c_controlPath;
            _width = c_width;
            _height = c_height;
            _shortBoxWidth = c_shortBoxWidth;
            _mediumBoxWidth = c_mediumBoxWidth;
            _longBoxWidth = c_longBoxWidth;
            _addColumns = c_addColumns;
            _addLists = c_addLists;
            _docHeaderFontSize = c_docHeaderFontSize;
            _textDataFontSize = c_textDataFontSize;
            _labelFontSize = c_labelFontSize;
            _headerFontSize = c_headerFontSize;
            _adminMode = c_adminMode;

            _domain = c_domain;
            _ccDomain = c_ccDomain;
            _ldap = c_ldap;
            _ccLdap = c_ccLdap;
            _mailServer = c_mailServer;
            download_pdf = DOWNLOAD_PDF;
            send_pdf_attachment = SEND_PDF_ATTACHMENT;
            load_with_no_parameters = LOAD_WITH_NO_PARAMETERS;

            //start_time = "01/01/1900 00:00:00";
            //end_date_time = "01/01/1900 00:00:00"; 
            completed_form_message = "";
        }


        private const string SIGNATURE_BUTTON_TEXT = "";
        private string signature_button_text;

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
  System.Web.UI.WebControls.WebParts.WebDisplayName("Signature Button Text"),
  System.Web.UI.WebControls.WebParts.WebDescription(""),
  System.Web.UI.WebControls.WebParts.Personalizable(
  System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
  System.ComponentModel.Category("Display Text")]
        public string Signature_Button_Text
        {
            get
            {
                return signature_button_text;
            }
            set
            {
                signature_button_text = value;
            }
        }

        private const bool LOAD_WITH_NO_PARAMETERS = false;
        private bool load_with_no_parameters;

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
  System.Web.UI.WebControls.WebParts.WebDisplayName("Load form with no parameters"),
  System.Web.UI.WebControls.WebParts.WebDescription(""),
  System.Web.UI.WebControls.WebParts.Personalizable(
  System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
  System.ComponentModel.Category("Mode")]
        public bool Load_Form_With_No_Parameters_In_URL
        {
            get
            {
                return load_with_no_parameters;
            }
            set
            {
                load_with_no_parameters = value;
            }
        }

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
    System.Web.UI.WebControls.WebParts.WebDisplayName("Download PDF"),
    System.Web.UI.WebControls.WebParts.WebDescription(""),
    System.Web.UI.WebControls.WebParts.Personalizable(
    System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
    System.ComponentModel.Category("PDF File")]
        public bool Download_Pdf
        {
            get
            {
                return download_pdf;
            }
            set
            {
                download_pdf = value;
            }
        }
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
   System.Web.UI.WebControls.WebParts.WebDisplayName("Send PDF as attachment"),
   System.Web.UI.WebControls.WebParts.WebDescription(""),
   System.Web.UI.WebControls.WebParts.Personalizable(
   System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
   System.ComponentModel.Category("PDF File")]
        public bool Send_Pdf_Attachment
        {
            get
            {
                return send_pdf_attachment;
            }
            set
            {
                send_pdf_attachment = value;
            }
        }


        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
     System.Web.UI.WebControls.WebParts.WebDisplayName("Standart LDAP String"),
     System.Web.UI.WebControls.WebParts.WebDescription(""),
     System.Web.UI.WebControls.WebParts.Personalizable(
     System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
     System.ComponentModel.Category("Connection")]
        public string ldap
        {
            get
            {
                return _ldap;
            }
            set
            {
                _ldap = value;
            }
        }


        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
    System.Web.UI.WebControls.WebParts.WebDisplayName("Savion LDAP String"),
    System.Web.UI.WebControls.WebParts.WebDescription(""),
    System.Web.UI.WebControls.WebParts.Personalizable(
    System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
    System.ComponentModel.Category("Connection")]
        public string ccLdap
        {
            get
            {
                return _ccLdap;
            }
            set
            {
                _ccLdap = value;
            }
        }


        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
       System.Web.UI.WebControls.WebParts.WebDisplayName("Standart domain Name"),
       System.Web.UI.WebControls.WebParts.WebDescription(""),
       System.Web.UI.WebControls.WebParts.Personalizable(
       System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
       System.ComponentModel.Category("Connection")]
        public string domain
        {
            get
            {
                return _domain;
            }
            set
            {
                _domain = value;
            }
        }

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
      System.Web.UI.WebControls.WebParts.WebDisplayName("Savion domain Name"),
      System.Web.UI.WebControls.WebParts.WebDescription(""),
      System.Web.UI.WebControls.WebParts.Personalizable(
      System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
      System.ComponentModel.Category("Connection")]
        public string ccDomain
        {
            get
            {
                return _ccDomain;
            }
            set
            {
                _ccDomain = value;
            }
        }

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Mail Server"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("Connection")]
        public string mailServer
        {
            get
            {
                return _mailServer;
            }
            set
            {
                _mailServer = value;
            }
        }
        //adminMode
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Admin Mode"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("Mode")]
        public bool adminMode
        {
            get
            {
                return _adminMode;
            }
            set
            {
                _adminMode = value;
            }
        }

        protected DateTime start_time;

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("Start date time"),
         System.Web.UI.WebControls.WebParts.WebDescription("DD/MM/YYYY HH:MM:SS"),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("Start - End")]
        public DateTime Start_Date_Time
        {
            get
            {
                return start_time;
            }
            set
            {
                start_time = value;
            }
        }

              protected DateTime end_date_time;

              [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
               System.Web.UI.WebControls.WebParts.WebDisplayName("End date time"),
               System.Web.UI.WebControls.WebParts.WebDescription("DD/MM/YYYY HH:MM:SS"),
               System.Web.UI.WebControls.WebParts.Personalizable(
               System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
               System.ComponentModel.Category("Start - End")]
              public DateTime End_Date_Time
        {
                  get
                  {
                      return end_date_time;
                  }
                  set
                  {
                      end_date_time = value;
                  }
              }

        protected string start_time_message;

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("Form 'not started yet' message"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("Start - End")]
        public string Start_Date_Message
        {
            get
            {
                return start_time_message;
            }
            set
            {
                start_time_message = value;
            }
        }


        protected string end_time_message;

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("Form 'ended' message"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("Start - End")]
        public string End_Date_Message
        {
            get
            {
                return end_time_message;
            }
            set
            {
                end_time_message = value;
            }
        }

        protected string completed_form_message;

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("'Completed' form message"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("Start - End")]
        public string Completed_Form_Message
        {
            get
            {
                return completed_form_message;
            }
            set
            {
                completed_form_message = value;
            }
        }


        //_
        //Custom properties declaration 
        //Adding custom properties fields
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("Create lists?"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("Lists & Columns")]
        public bool addLists
        {
            get
            {
                return _addLists;
            }
            set
            {
                _addLists = value;
            }
        }

        //Custom properties declaration 
        //Adding custom properties fields
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("Create list columns?"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("Lists & Columns")]
        public bool addColumns
        {
            get
            {
                return _addColumns;
            }
            set
            {
                _addColumns = value;
            }
        }

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Document header font size:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("Look & Feel")]
        public float docHeaderFontSize
        {
            get
            {
                return _docHeaderFontSize;
            }
            set
            {
                _docHeaderFontSize = value;
            }
        }

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Paragraph header font size:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("Look & Feel")]
        public float headerFontSize
        {
            get
            {
                return _headerFontSize;
            }
            set
            {
                _headerFontSize = value;
            }
        }

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Document header font size:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("Look & Feel")]
        public float labelFontSize
        {
            get
            {
                return _labelFontSize;
            }
            set
            {
                _labelFontSize = value;
            }
        }
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
      System.Web.UI.WebControls.WebParts.WebDisplayName("Data font size:"),
      System.Web.UI.WebControls.WebParts.WebDescription(""),
      System.Web.UI.WebControls.WebParts.Personalizable(
      System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
      System.ComponentModel.Category("Look & Feel")]
        public float textDataFontSize
        {
            get
            {
                return _textDataFontSize;
            }
            set
            {
                _textDataFontSize = value;
            }
        }
        //Custom properties declaration 
        //Adding custom properties fields
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("'Short' Text box width:"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("Look & Feel")]
        public int shortBoxWidth
        {
            get
            {
                return _shortBoxWidth;
            }
            set
            {
                _shortBoxWidth = value;
            }
        }

        //Custom properties declaration 
        //Adding custom properties fields
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("'Medium' Text box width:"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("Look & Feel")]
        public int mediumBoxWidth
        {
            get
            {
                return _mediumBoxWidth;
            }
            set
            {
                _mediumBoxWidth = value;
            }
        }

        //Custom properties declaration 
        //Adding custom properties fields
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("'Long' Text box width:"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("Look & Feel")]
        public int longBoxWidth
        {
            get
            {
                return _longBoxWidth;
            }
            set
            {
                _longBoxWidth = value;
            }
        }


        //Custom properties declaration 
        //Adding custom properties fields
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("MultiLine Text box width:"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("Look & Feel")]
        public int MtWidth
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }


        //Custom properties declaration 
        //Adding custom properties fields
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("MultiLine Text box height:"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("Look & Feel")]
        public int MtHeight
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        //Custom properties declaration 
        //Adding custom properties fields
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("Select text alignment:"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("Look & Feel")]
        public c_textAlign textAlign
        {
            get
            {
                return _textAlign;
            }
            set
            {
                _textAlign = value;
            }
        }

        //Custom properties declaration 
        //Adding custom properties fields
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
         System.Web.UI.WebControls.WebParts.WebDisplayName("Select text direction:"),
         System.Web.UI.WebControls.WebParts.WebDescription(""),
         System.Web.UI.WebControls.WebParts.Personalizable(
         System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
         System.ComponentModel.Category("Look & Feel")]
        public c_textDirection textDirection
        {
            get
            {
                return _textDirection;
            }
            set
            {
                _textDirection = value;
            }
        }


        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("_document font name:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("Look & Feel")]
        public String fontName
        {
            get
            {
                return _fontName;
            }
            set
            {
                _fontName = value;
            }
        }



        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("_document header color (#xxxxxx):"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("Look & Feel")]
        public String _docHeaderColor
        {
            get
            {
                return __docHeaderColor;
            }
            set
            {
                __docHeaderColor = value;
            }
        }

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("_document header font size:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("Look & Feel")]
        public String _doc_headerFontSize
        {
            get
            {
                return __doc_headerFontSize;
            }
            set
            {
                __doc_headerFontSize = value;
            }
        }

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Section header color (#xxxxxx):"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("Look & Feel")]
        public String sectionHeaderColor
        {
            get
            {
                return _sectionHeaderColor;
            }
            set
            {
                _sectionHeaderColor = value;
            }
        }

        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Section header font size:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("Look & Feel")]
        public String section_headerFontSize
        {
            get
            {
                return _section_headerFontSize;
            }
            set
            {
                _section_headerFontSize = value;
            }
        }


        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Data font color:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("Look & Feel")]
        public String dataColor
        {
            get
            {
                return _dataColor;
            }
            set
            {
                _dataColor = value;
            }
        }

        /// <summary>
        /// Font size for data fields
        /// </summary>
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Data font size:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("Look & Feel")]
        public String _dataFontSize
        {
            get
            {
                return __dataFontSize;
            }
            set
            {
                __dataFontSize = value;
            }
        }



        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("XML 'config' path:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("XML Tags Config")]
        public String _configPath
        {
            get
            {
                return __configPath;
            }
            set
            {
                __configPath = value;
            }
        }


        /// <summary>
        /// XML path to config data
        /// </summary>
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("XML 'data' path:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("XML Tags Config")]
        public String _dataPath
        {
            get
            {
                return __dataPath;
            }
            set
            {
                __dataPath = value;
            }
        }


        /// <summary>
        /// XML path to control info
        /// </summary>
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("XML 'control' path:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("XML Tags Config")]
        public String controlPath
        {
            get
            {
                return _controlPath;
            }
            set
            {
                _controlPath = value;
            }
        }


        /// <summary>
        /// Document header XML tag name
        /// </summary>
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("_document header tag name:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("XML Tags Config")]
        public String _docHeader
        {
            get
            {
                return __docHeader;
            }
            set
            {
                __docHeader = value;
            }
        }


        /// <summary>
        /// Text header XML tag name
        /// </summary>
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Header tag name:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("XML Tags Config")]
        public String _headerTag
        {
            get
            {
                return __headerTag;
            }
            set
            {
                __headerTag = value;
            }
        }


        /// <summary>
        /// Label XML tag name
        /// </summary>
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Label tag name:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("XML Tags Config")]
        public String _labelTag
        {
            get
            {
                return __labelTag;
            }
            set
            {
                __labelTag = value;
            }
        }

        /// <summary>
        /// Data XML tag name
        /// </summary>
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Data tag name:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("XML Tags Config")]
        public String DataTag
        {
            get
            {
                return _DataTag;
            }
            set
            {
                _DataTag = value;
            }
        }


        /// <summary>
        /// Control Type XML tag name
        /// </summary>
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
       System.Web.UI.WebControls.WebParts.WebDisplayName("Data Type tag name:"),
       System.Web.UI.WebControls.WebParts.WebDescription(""),
       System.Web.UI.WebControls.WebParts.Personalizable(
       System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
       System.ComponentModel.Category("XML Tags Config")]
        public String TypeTag
        {
            get
            {
                return _TypeTag;
            }
            set
            {
                _TypeTag = value;
            }
        }

        /// <summary>
        /// Button XML tag name
        /// </summary>
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
      System.Web.UI.WebControls.WebParts.WebDisplayName("Button Tag tag name:"),
      System.Web.UI.WebControls.WebParts.WebDescription(""),
      System.Web.UI.WebControls.WebParts.Personalizable(
      System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
      System.ComponentModel.Category("XML Tags Config")]
        public String buttonTag
        {
            get
            {
                return _buttonTag;
            }
            set
            {
                _buttonTag = value;
            }
        }


        /// <summary>
        /// Message XML tag name
        /// </summary>
        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Message Tag tag name:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("XML Tags Config")]
        public String messageTag
        {
            get
            {
                return _messageTag;
            }
            set
            {
                _messageTag = value;
            }
        }

        /// <summary>
        /// Destiantion list name
        /// </summary>
       /* [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Enter destination list name:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("SP-Lists")]
        public String applicantsList
        {
            get
            {
                return _applicantList;
            }
            set
            {
                _applicantList = value;
            }
        }*/


        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Config file path:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("XML source - file location")]
        public string filePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
            }
        }


        [System.Web.UI.WebControls.WebParts.WebBrowsable(true),
        System.Web.UI.WebControls.WebParts.WebDisplayName("Config file name:"),
        System.Web.UI.WebControls.WebParts.WebDescription(""),
        System.Web.UI.WebControls.WebParts.Personalizable(
        System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared),
        System.ComponentModel.Category("XML source - file location")]
        public string fileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }
        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
