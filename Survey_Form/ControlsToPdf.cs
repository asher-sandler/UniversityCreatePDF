using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.IO;
using System.Text;
using System.Web;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Collections;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Web.Hosting;





namespace Survey_Form
{
    class ControlsToPdf:UserControl
    {
        Document document;
        float _dataFontSize;
        float _labelFontSize;
        float _headerFontSize;
        float _doc_headerFontSize;

        string _dataPath;
        string _configPath;
        string _headerTag;
        string _labelTag;
        string _docHeader;
        string docName;
        ArrayList _repeatingTableIdsForm;
        ArrayList _repeatingTableIds;
        ArrayList _dynamicTextBoxIDs;
        ArrayList _dynamicDropDownControlIDs;
        ArrayList _dynamicLabelIDs;
        ArrayList _dynamicCheckBoxIds;
        ArrayList _dynamicRadioButtonIds;
        ArrayList _dynamicDatePickerIds;
        ArrayList _dynamicRepeatingTableIds;

        //DynamicControl dynamicControl;
        public ControlsToPdf(ArrayList repeatingTableIdsForm,
                             ArrayList repeatingTableIds,
                             ArrayList dynamicTextBoxIDs,
                             ArrayList dynamicDropDownControlIDs,
                             ArrayList dynamicLabelIDs,
                             ArrayList dynamicCheckBoxIds,
                             ArrayList dynamicRadioButtonIds,
                             ArrayList dynamicDatePickerIds,
                             ArrayList dynamicRepeatingTableIds, 
                             string dataPath,
                             string configPath,
                             string headerTag,
                             string labelTag,
                             
                             float dataFontSize,
                             float labelFontSize,
                             float headerFontSize,
                             float doc_headerFontSize)
        {
            document = new Document();
           
            _repeatingTableIdsForm = repeatingTableIdsForm;
            _repeatingTableIds = repeatingTableIds;
            _dynamicTextBoxIDs = dynamicTextBoxIDs;
            _dynamicDropDownControlIDs = dynamicDropDownControlIDs;
            _dynamicLabelIDs = dynamicLabelIDs;
            _dynamicCheckBoxIds = dynamicCheckBoxIds;
            _dynamicDatePickerIds = dynamicDatePickerIds;
            _dynamicRepeatingTableIds = dynamicRepeatingTableIds;
            _dynamicRadioButtonIds = dynamicRadioButtonIds;
            _dataPath = dataPath;
            _configPath = configPath;
            _headerTag = headerTag;
            _labelTag = labelTag;
            
            _dataFontSize = dataFontSize;
            _labelFontSize = labelFontSize;
            _headerFontSize = headerFontSize;
            _doc_headerFontSize = doc_headerFontSize;
        }


        public string DataPath
        {
            get
            {
                return this._dataPath;
            }
            set
            {
                this._dataPath = value;
            }
        }
        public string ConfigPath
        {
            get
            {
                return this._configPath;
            }
            set
            {
                this._configPath = value;
            }
        }
        public string HeaderTag
        {
            get
            {
                return this._headerTag;
            }
            set
            {
                this._headerTag = value;
            }
        }
        public string LabelTag
        {
            get
            {
                return this._labelTag;
            }
            set
            {
                this._labelTag = value;
            }
        }
        public string DocHeader
        {
            get
            {
                return this._docHeader;
            }
            set
            {
                this._docHeader = value;
            }
        }
        public float DataFontSize
        {
            get
            {
                return this._dataFontSize;
            }
            set
            {
                this._dataFontSize = value;
            }
        }
        public float LabelFontSize
        {
            get
            {
                return this._labelFontSize;
            }
            set
            {
                this._labelFontSize = value;
            }
        }
        public float HeaderFontSize
        {
            get
            {
                return this._headerFontSize;
            }
            set
            {
                this._headerFontSize = value;
            }
        }
        public float Doc_headerFontSize
        {
            get
            {
                return this._doc_headerFontSize;
            }
            set
            {
                this._doc_headerFontSize = value;
            }
        }


        public PDF_Obj loadXML(string xmlFPath, 
            string xmlFName, 
            string textFont, 
            string applicantsList, 
            string textDirection, 
            string itemUserName, 
           
           
            SPFolder destinationFolder,
            string destinationWeb,
            string destinationList,
            string destinationfileName,
            bool is_user_folder, 
            string docTypeList, 
            string docTypeColumn, 
            string docType
            )
        {

            //string printData = "";
            string url = destinationWeb;// SPContext.Current.Web.Url.ToString();
            string userName = "";
            string log = "";
            byte[] pdfBytes = {};
            PDF_Obj pdf_obj = new PDF_Obj();
            log += printInfoTitle("Generating PDF2");
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {


                    PdfPCell text;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlFPath + "\\" + xmlFName);
                   //log += "<br>loading xml at:" + xmlFPath + "\\" + xmlFName;
                    //log += "<br>applicants list: " + destinationList;
                    //log += "<br>fileName:" + destinationfileName;
                    using (SPSite site = new SPSite(url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;


                            //Fetching the current list
                            SPList oList = web.Lists[destinationList];
                            SPListItemCollection collListItems = oList.Items;
                            //messageLabel.Text += "found list <br>";
                            foreach (SPListItem oListItem in collListItems)
                            {
                              //  log += "<br>list title: " + oListItem["Title"];
                               // userName = oListItem.GetFormattedValue("userName");
                                if (oListItem["Title"].Equals(destinationfileName))
                                {
                                   
                                    log += printInfo("Found list item");
                                    //*********************************************************************************
                                    //Start new PDF Table
                                    //Reference a Unicode font to be sure that the symbols are present.
                                    document = new Document(PageSize.A4);
                                    MemoryStream memStream = new MemoryStream();
                                    PdfWriter writer = PdfWriter.GetInstance(document, memStream);
                                    writer.CompressionLevel = PdfStream.BEST_COMPRESSION;
                                    PdfPCell docTitle;
                                    document.Open();

                                    //Add a new page 
                                    document.NewPage();
                                    string fontName = "";
                                    if (textFont.CompareTo("Arial") == 0)
                                    {
                                        fontName = "ARIALUNI.TTF";
                                    }
                                    if (textFont.CompareTo("David") == 0)
                                    {
                                        fontName = "david.ttf";
                                    }
                                    if (textFont.CompareTo("TimesNewRoman") == 0)
                                    {
                                        fontName = "times.ttf";
                                    }
                                    if (textFont.CompareTo("Tahoma") == 0)
                                    {
                                        fontName = "tahoma.ttf";
                                    }


                                    BaseFont bfUniCode = BaseFont.CreateFont(@"c:\Windows\Fonts\" + fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                                    BaseColor Headercolor = new BaseColor(60, 69, 92); ;// new BaseColor(27, 72, 139);
                                    BaseColor _docHeadercolor = BaseColor.WHITE; //new CMYKColor(74, 67, 66, 84); ; //new BaseColor(46, 96, 171);
                                    BaseColor DataColor = new BaseColor(42, 49, 50);// new BaseColor(80, 80, 80);
                                    BaseColor LabelColor = new BaseColor(51, 107, 135);  //  new BaseColor(42, 49, 50);// BaseColor(130, 130, 130);

                                    //Create a font from the base font
                                    Font font = new Font(bfUniCode, 11, Font.NORMAL);

                                    //Use a table so that we can set the text direction
                                    PdfPTable table = new PdfPTable(4);
                                    if (textDirection.CompareTo("rtl") == 0)
                                    {
                                        table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                    }
                                    else
                                    {
                                        table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    }
                                    table.DefaultCell.Border = 0;
                                    //Ensure that wrapping is on, otherwise Right to Left text will not display
                                    table.DefaultCell.NoWrap = false;

                                    // Create a regex expression to detect hebrew or arabic code points
                                    // const string regex_match_arabic_hebrew = @"[\u0600-\u06FF,\u0590-\u05FF]+";

                                    //if (Regex.IsMatch(TextBox1.Text, regex_match_arabic_hebrew, RegexOptions.IgnoreCase))
                                    if (textDirection.CompareTo("rtl") == 0)
                                    {
                                        table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                    }
                                    else
                                    {
                                        table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    }
                                    //*********************************************************************************



                                    System.Web.UI.WebControls.Table t = new System.Web.UI.WebControls.Table();


                                    try
                                    {

                                        foreach (XmlNode node in xmlDoc.SelectNodes(_configPath))
                                        {
                                            foreach (XmlNode el in node.ChildNodes)
                                            {
                                                try
                                                {
                                                    // if (userName.CompareTo(oListItem.GetFormattedValue("userName")) == 0)
                                                    {
                                                        if (el.LocalName.ToLower().CompareTo("logo") == 0)
                                                        {
                                                            try
                                                            {
                                                                table = new PdfPTable(1);
                                                                table.DefaultCell.Border = 0;
                                                                String logoPath = el.InnerText;


                                                                // add header image; PdfPCell() overload sizes image to fit cell
                                                                iTextSharp.text.Image logoImage = iTextSharp.text.Image.GetInstance(logoPath);
                                                                logoImage.BackgroundColor = BaseColor.WHITE;

                                                                logoImage.ScaleAbsolute(485f, 50f);

                                                                text = new PdfPCell(logoImage, false);
                                                                text.Border = 0;
                                                                table.AddCell(text);
                                                                document.Add(table);
                                                            }
                                                            catch (Exception ex) {

                                                                log += exceptionMessageBuilder("Add Logo", ex.Message);
                                                            }
                                                        }
                                                        if (el.LocalName.ToLower().CompareTo("docheader") == 0)
                                                        {

                                                            table = new PdfPTable(1);
                                                            table.DefaultCell.Border = 0;

                                                            if (textDirection.CompareTo("rtl") == 0)
                                                            {
                                                                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                table.HorizontalAlignment = 2;
                                                            }
                                                            else
                                                            {
                                                                table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                table.HorizontalAlignment = 0;
                                                            }
                                                           
                                                            bool simpleHeader = true;
                                                            string docHeaderText = string.Empty;
                                                            foreach (XmlNode cel in el.ChildNodes)
                                                            {
                                                               

                                                                if (cel.LocalName.Equals("text"))
                                                                {
                                                                    docHeaderText = cel.InnerText;
                                                                    simpleHeader = false;
                                                                }

                                                            }
                                                            if (simpleHeader)
                                                            {
                                                                docHeaderText = el.InnerText;
                                                            }

                                                            font = new Font(bfUniCode, _doc_headerFontSize, iTextSharp.text.Font.NORMAL, _docHeadercolor);
                                                            
                                                           docTitle = new PdfPCell(new Phrase(docHeaderText, font));
                                                          
                                                            docTitle.Border = 0;
                                                            docTitle.BackgroundColor = new BaseColor(60, 69, 92); // new CMYKColor(67, 36, 0, 53);// BaseColor(130, 130, 130);
                                                            
                                                          
                                                            docTitle.PaddingBottom = 10;
                                                            docTitle.VerticalAlignment = Element.ALIGN_MIDDLE;
                                                            docTitle.HorizontalAlignment = Element.ALIGN_CENTER;
                                                            table.WidthPercentage = 100;
                                                            table.AddCell(docTitle);
                                                            document.Add(table);

                                                            table = new PdfPTable(4);
                                                            if (textDirection.CompareTo("rtl") == 0)
                                                            {
                                                                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                            }
                                                            else
                                                            {
                                                                table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                            }
                                                            table.DefaultCell.Border = 0;
                                                            table.SpacingBefore = 10;
                                                            table.DefaultCell.NoWrap = false;

                                                        }

                                                        if (el.LocalName.CompareTo("fileName") == 0)
                                                        {
                                                            docName = el.InnerText;
                                                        
                                                        }


                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    log += exceptionMessageBuilder("Load Configurations", ex.Message);
                                                   
                                                }


                                            }
                                        }


                                        foreach (XmlNode node in xmlDoc.SelectNodes(_dataPath))
                                        {
                                            ArrayList PDF_Cells = new ArrayList();
                                            ArrayList PDF_CellWidtds = new ArrayList();

                                            table = new PdfPTable(node.ChildNodes.Count + 1);
                                            if (textDirection.CompareTo("rtl") == 0)
                                            {
                                                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                table.HorizontalAlignment = 2;
                                            }
                                            else
                                            {
                                                table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                table.HorizontalAlignment = 0;
                                            }
                                            table.DefaultCell.Border = 0;
                                            // table.SpacingBefore= 4;
                                            table.DefaultCell.NoWrap = false;


                                            float[] widths;// = new float[rowControls];
                                            float[] widthsRtl;// = new float[rowControls];
                                            int counter = 0;
                                            bool setWidth = true;
                                            // widths[0]=1f;
                                            foreach (XmlNode el in node.ChildNodes)
                                            {

                                                //if (userName.CompareTo(oListItem.GetFormattedValue("userName")) == 0)
                                                {

                                                    try
                                                    {
                                                        if (el.LocalName.CompareTo(_headerTag) == 0)
                                                        {

                                                            setWidth = false;
                                                            document.Add(table);

                                                            font = new Font(bfUniCode, _headerFontSize, iTextSharp.text.Font.NORMAL, Headercolor);

                                                            table = new PdfPTable(1);
                                                            table.DefaultCell.Border = 0;
                                                            table.DefaultCell.BorderColor = BaseColor.WHITE;
                                                            if (textDirection.CompareTo("rtl") == 0)
                                                            {
                                                                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                table.HorizontalAlignment = 2;
                                                            }
                                                            else
                                                            {
                                                                table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                table.HorizontalAlignment = 0;
                                                            }

                                                            docTitle = new PdfPCell(new Phrase(el.InnerText, font));
                                                            //docTitle.BorderWidthRight = 0;
                                                            //docTitle.BorderWidthLeft = 0;
                                                            //docTitle.BorderWidthTop = 0;
                                                            //docTitle.BorderColorBottom = new BaseColor(42, 49, 50);
                                                            docTitle.Border = 0;
                                                            //docTitle.BackgroundColor = new BaseColor(42, 49, 50);
                                                            docTitle.PaddingBottom = 7;
                                                            docTitle.PaddingTop = 10;
                                                            

                                                            table.AddCell(docTitle);
                                                            table.WidthPercentage = 100;
                                                            document.Add(table);
                                                            document.Add(new Paragraph(""));
                                                            table = new PdfPTable(1);
                                                          

                                                        }

                                                        if (el.LocalName.ToLower().CompareTo("label") == 0)
                                                        {


                                                            string print = "yes";
                                                            string labelText = "";
                                                            float labelWidth = 0f;
                                                            foreach (XmlNode cel in el.ChildNodes)
                                                            {
                                                                if (cel.LocalName.CompareTo("text") == 0)
                                                                {
                                                                    labelText = cel.InnerText;
                                                                }

                                                                if (cel.LocalName.CompareTo("print") == 0)
                                                                {
                                                                    if (cel.InnerText.Length > 0)
                                                                    {
                                                                        print = cel.InnerText;
                                                                    }

                                                                }
                                                                if (cel.LocalName.ToLower().Equals("width"))
                                                                {
                                                                    labelWidth = (float)Convert.ToDouble(cel.InnerText);
                                                                }

                                                            }

                                                            if (print.ToLower().CompareTo("yes") == 0)
                                                            {
                                                                font = new Font(bfUniCode, _labelFontSize, iTextSharp.text.Font.NORMAL, LabelColor);
                                                                text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(labelText.Trim()), font));
                                                                text.VerticalAlignment = Element.ALIGN_TOP;
                                                                text.NoWrap = false;
                                                                text.Border = 0;
                                                                PDF_Cells.Add(text);
                                                                text.PaddingTop = 5;
                                                                PDF_CellWidtds.Add(labelWidth);
                                                                counter++;
                                                                log += string.Format("cell:{0}__width:{1} ", counter, labelWidth);
                                                            }
                                                        }

                                                        if (el.LocalName.CompareTo("spaceRow") == 0)
                                                        {
                                                            try
                                                            {
                                                                setWidth = false;
                                                                table.CompleteRow();
                                                                text = new PdfPCell(new Phrase(" "));
                                                                text.Border = 0;

                                                                table.AddCell(text);
                                                                table.CompleteRow();
                                                            }
                                                            catch (Exception ex) { log += exceptionMessageBuilder("Add space row", ex.Message); }

                                                        }
                                                        if (el.LocalName.CompareTo("control") == 0)
                                                        {

                                                            string controlId = "";
                                                            string controlType = "";
                                                            string controlData = "";
                                                            string controlList = "";
                                                            float width = 0f;
                                                            bool printControl = true;
                                                            bool visibleContorl = true;
                                                            //controlPath

                                                            foreach (XmlNode cel in el.ChildNodes)
                                                            {

                                                                if (cel.LocalName.ToLower().Equals("data"))
                                                                {
                                                                    controlData = cel.InnerText;
                                                                    controlId = cel.InnerText;
                                                                }

                                                                if (cel.LocalName.ToLower().Equals("type"))
                                                                {
                                                                    controlType = cel.InnerText;
                                                                }
                                                                if (cel.LocalName.ToLower().Equals("list"))
                                                                {
                                                                    controlList = cel.InnerText;
                                                                }
                                                                if (cel.LocalName.ToLower().Equals("width"))
                                                                {
                                                                    width = (float)Convert.ToDouble(cel.InnerText);
                                                                }
                                                                if (cel.LocalName.ToLower().CompareTo("print") == 0)
                                                                {
                                                                    if (cel.InnerText.Length > 0)
                                                                    {
                                                                        printControl = (cel.InnerText.ToLower().Equals("yes")) ? true : false;

                                                                    }
                                                                }
                                                                if (cel.LocalName.ToLower().CompareTo("visible") == 0)
                                                                {
                                                                    if (cel.InnerText.Length > 0)
                                                                    {
                                                                        visibleContorl = (cel.InnerText.ToLower().Equals("yes")) ? true : false;

                                                                    }
                                                                }

                                                            }

                                                            log += string.Format("<br />Print [{0}] control: {1}", controlId, printControl);
                                                            log += print_OK_Step("COntrol type: " + controlType);

                                                            if (controlType.ToLower().Equals("datepicker") && printControl)
                                                            {
                                                                // widths[counter] = width;

                                                                log += "<br>adding date picker..";
                                                                try
                                                                {

                                                                    log += "<br>datepicker data: <" + oListItem.GetFormattedValue(controlId) + ">";
                                                                    //log += "<br>Text box data: <" + (String)ctrl.Data + ">";
                                                                    font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);

                                                                    text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId).Split(' ')[0]), font));
                                                                    text.Border = 0;
                                                                    text.PaddingTop = 5;
                                                                    if (textDirection.CompareTo("rtl") == 0)
                                                                    {
                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                        table.HorizontalAlignment = 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                        table.HorizontalAlignment = 0;
                                                                    }
                                                                    //table.AddCell(text);
                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;
                                                                    // widths[counter] = width;

                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print Datepicker", ex.Message);
                                                                }
                                                            }
                                                            if (controlType.ToLower().CompareTo("textbox") == 0 && printControl)
                                                            {
                                                                //widths[counter] = width;

                                                                //log += "<br>adding text box..";
                                                                try
                                                                {
                                                                    font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                    text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font));
                                                                    if (textDirection.CompareTo("rtl") == 0)
                                                                    {
                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                        table.HorizontalAlignment = 2;

                                                                    }
                                                                    else
                                                                    {
                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                        table.HorizontalAlignment = 0;
                                                                    }
                                                                    text.Border = 0;
                                                                    text.PaddingTop = 5;
                                                                    text.VerticalAlignment = 1;
                                                                    //table.AddCell(text);
                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;
                                                                    // widths[counter] = width;

                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print TextBox", ex.Message);
                                                                }
                                                            }
                                                            if (controlType.ToLower().CompareTo("label") == 0 && printControl)
                                                            {
                                                                //log += "<br>adding text box..";
                                                                // widths[counter] = width;

                                                                try
                                                                {

                                                                    //  foreach (DynamicControl ctrl in _dynamicTextBoxIDs)
                                                                    {
                                                                        //if (ctrl.ID.CompareTo(controlId) == 0)
                                                                        {
                                                                            //log += "<br>Text box data: <" + (String)ctrl.Data + ">";
                                                                            font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                            text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font));
                                                                            if (textDirection.CompareTo("rtl") == 0)
                                                                            {
                                                                                // text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                                table.HorizontalAlignment = 2;
                                                                            }
                                                                            else
                                                                            {
                                                                                // text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                                table.HorizontalAlignment = 0;
                                                                            }
                                                                            text.Border = 0;
                                                                            text.PaddingTop = 5;
                                                                            //table.AddCell(text);
                                                                            PDF_Cells.Add(text);
                                                                            PDF_CellWidtds.Add(width);
                                                                            counter++;
                                                                            // widths[counter] = width;
                                                                        }
                                                                    }
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print Label", ex.Message);
                                                                }
                                                            }
                                                            if (controlType.ToLower().Equals("dependentdropdownlist") && printControl)
                                                            {
                                                                //widths[counter] = width;

                                                                try
                                                                {
                                                                    log += "<br>**********Adding dependent DDL***********";
                                                                    //foreach (DynamicControl ctrl in _dynamicDropDownControlIDs)
                                                                    //   {
                                                                    // if (ctrl.ID.CompareTo(controlId) == 0)
                                                                    //{
                                                                    font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                    text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font));
                                                                    text.Border = 0;
                                                                    text.PaddingTop = 5;
                                                                    if (textDirection.CompareTo("rtl") == 0)
                                                                    {
                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                        table.HorizontalAlignment = 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        //text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                        table.HorizontalAlignment = 0;
                                                                    }
                                                                    //table.AddCell(text);
                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;
                                                                    // widths[counter] = width;
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print Dependent DropDownList", ex.Message);
                                                                }

                                                            }

                                                            if (controlType.ToLower().CompareTo("multilinetextbox") == 0 && printControl)
                                                            {
                                                                // widths[counter] = width;

                                                                try
                                                                {
                                                                    font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                    Paragraph ph = new Paragraph(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font);
                                                                    ph.Alignment = (textDirection.Equals("rtl")) ? Element.ALIGN_RIGHT : Element.ALIGN_LEFT;
                                                                    text = new PdfPCell(ph);
                                                                    if (textDirection.CompareTo("rtl") == 0)
                                                                    {

                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                        table.HorizontalAlignment = 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                        table.HorizontalAlignment = 0;
                                                                    }


                                                                    text.Border = 0;
                                                                    text.PaddingTop = 5;
                                                                    //table.AddCell(text);
                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;
                                                                    //widths[counter] = width;
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print MultiLineTextBox", ex.Message);
                                                                }

                                                            }


                                                            /*  if (controlType.CompareTo("Label") == 0)
                                                              {
                                                                  //widths[counter] = width;
                                                                
                                                                  font = new Font(bfUniCode, _labelFontSize, 1, DataColor);
                                                                  text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font));
                                                                  text.Border = 0;
                                                                  if (textDirection.Equals("rtl"))
                                                                  {
                                                                      text.HorizontalAlignment = Element.ALIGN_RIGHT;
                                                                  }
                                                                 // table.AddCell(text);
                                                                  PDF_Cells.Add(text);
                                                                  PDF_CellWidtds.Add(width);
                                                                  counter++;
                                                                  //widths[counter] = width;
                                                                 //break;
                                                              }*/
                                                            if (controlType.ToLower().CompareTo("calculatedvalue") == 0 && printControl)
                                                            {
                                                                font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font));
                                                                text.Border = 0;
                                                                if (textDirection.CompareTo("rtl") == 0)
                                                                {
                                                                    //text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                    table.HorizontalAlignment = 2;
                                                                }
                                                                else
                                                                {
                                                                    // text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                    table.HorizontalAlignment = 0;
                                                                }
                                                                text.PaddingTop = 5;
                                                                //table.AddCell(text);
                                                                PDF_Cells.Add(text);
                                                                PDF_CellWidtds.Add(width);
                                                                counter++;
                                                            }
                                                            if ((controlType.ToLower().Equals("textlabel") || controlType.ToLower().Equals("parameter") || controlType.ToLower().Equals("url_parameter")) && printControl)
                                                            {
                                                                // widths[counter] = width;

                                                                try
                                                                {
                                                                    string temp = "";
                                                                    //check if controlId content is dateTime
                                                                    if (oListItem.Fields[controlId].FieldValueType.ToString().Equals("System.DateTime"))
                                                                    {
                                                                        //logLabel.Text += "<br>Created type for: " + controlId + " is: " + oListItem.Fields[controlId].FieldValueType;
                                                                        DateTime d = Convert.ToDateTime(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)));

                                                                        temp = d.ToUniversalTime().ToString();
                                                                    }
                                                                    else
                                                                    {
                                                                        temp = HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId));
                                                                    }
                                                                    font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                    text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(temp), font));
                                                                    text.Border = 0;
                                                                    text.PaddingTop = 5;
                                                                    if (textDirection.CompareTo("rtl") == 0)
                                                                    {
                                                                        //text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                        table.HorizontalAlignment = 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        //text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                        table.HorizontalAlignment = 0;
                                                                    }
                                                                    //table.AddCell(text);
                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;
                                                                    //widths[counter] = width;
                                                                }

                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print TextLabel", ex.Message);
                                                                }

                                                            }
                                                         
                                                            if (controlType.ToLower().Equals("signature"))
                                                            {
                                                                try
                                                                {
                                                                    log += print_OK_Step("Adding signature Image: ");
                                                                   // table = new PdfPTable(1);
                                                                    //table.DefaultCell.Border = 1;
                                                                   // String logoPath =((TextBox)FindControl("Signature")).Text;
                                                                    String logoPath = HttpUtility.HtmlDecode(oListItem.GetFormattedValue("Signature").ToString());
                                                                   // log += print_OK_Step("signature Image path: " + logoPath);
                                                                    var base64Data = Regex.Match(logoPath, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;

                                                                    byte[] imageBytes = Convert.FromBase64String(base64Data);
                                                                     
                                                                    iTextSharp.text.Image signatureImage = iTextSharp.text.Image.GetInstance(imageBytes);
                                                                    signatureImage.BackgroundColor = BaseColor.WHITE;
                                                                    signatureImage.ScalePercent(40f);
                                                                    //signatureImage.ScaleAbsolute(485f, 250f);

                                                                    text = new PdfPCell(signatureImage, false);
                                                                    text.Border = 0;


                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;

                                                                   // table.AddCell(text);
                                                                   // document.Add(table);
                                                                }
                                                                catch (Exception ex)
                                                                {

                                                                    log += exceptionMessageBuilder("Add signature", ex.Message);
                                                                }
                                                            }

                                                            if (controlType.CompareTo("CheckBox") == 0 && printControl)
                                                            {

                                                                try
                                                                {
                                                                    if (printControl && !visibleContorl)
                                                                    {
                                                                        counter++;
                                                                        //  widths[counter] = width;

                                                                        string pathUnchecked = @"\\ekeksql00\SP_Resources$\HSS\images\unchecked.jpg";


                                                                        iTextSharp.text.Image checkBoxImage = iTextSharp.text.Image.GetInstance(pathUnchecked);
                                                                        checkBoxImage.BackgroundColor = BaseColor.WHITE;

                                                                        checkBoxImage.ScaleAbsolute(10f, 10f);
                                                                        text = new PdfPCell(checkBoxImage, false);
                                                                        if (textDirection.CompareTo("rtl") == 0)
                                                                        {
                                                                            // text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                            table.HorizontalAlignment = 2;
                                                                        }
                                                                        else
                                                                        {
                                                                            //text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                            table.HorizontalAlignment = 0;
                                                                        }
                                                                        text.VerticalAlignment = 1;
                                                                        text.Border = 0;
                                                                        text.PaddingTop = 5;
                                                                        //table.AddCell(text);
                                                                        PDF_Cells.Add(text);
                                                                        PDF_CellWidtds.Add(width);
                                                                    }
                                                                    else
                                                                    {
                                                                        foreach (DynamicControl ctrl in _dynamicCheckBoxIds)
                                                                        {
                                                                            if (ctrl.ID.CompareTo(controlId) == 0)
                                                                            {
                                                                                //widths[counter] = width;

                                                                                String pathChecked = @"\\ekeksql00\SP_Resources$\HSS\images\checked.jpg";
                                                                                String pathUnchecked = @"\\ekeksql00\SP_Resources$\HSS\images\unchecked.jpg";
                                                                                String pathCheckBox = ((bool)oListItem[controlId]) ? pathChecked : pathUnchecked;


                                                                                // add header image; PdfPCell() overload sizes image to fit cell
                                                                                iTextSharp.text.Image checkBoxImage = iTextSharp.text.Image.GetInstance(pathCheckBox);
                                                                                checkBoxImage.BackgroundColor = BaseColor.WHITE;

                                                                                checkBoxImage.ScaleAbsolute(10f, 10f);
                                                                                text = new PdfPCell(checkBoxImage, false);
                                                                                text.Border = 0;
                                                                                text.PaddingTop = 5;
                                                                                if (textDirection.CompareTo("rtl") == 0)
                                                                                {
                                                                                    //    text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                                    table.HorizontalAlignment = 2;
                                                                                }
                                                                                else
                                                                                {
                                                                                    //    text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                                    table.HorizontalAlignment = 0;
                                                                                }
                                                                                //table.AddCell(text);
                                                                                PDF_Cells.Add(text);
                                                                                PDF_CellWidtds.Add(width);
                                                                                counter++;
                                                                                //widths[counter] = width;
                                                                                break;
                                                                            }

                                                                        }
                                                                    }

                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print CheckBox", ex.Message);
                                                                }


                                                            }

                                                            if (controlType.ToLower().Equals("radiobuttonlist") && printControl)
                                                            {
                                                                // widths[counter] = width;

                                                                try
                                                                {
                                                                    font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                    text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font));
                                                                    text.Border = 0;
                                                                    text.PaddingTop = 5;
                                                                    if (textDirection.CompareTo("rtl") == 0)
                                                                    {
                                                                        //text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                        table.HorizontalAlignment = 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                        table.HorizontalAlignment = 0;
                                                                    }
                                                                    //table.AddCell(text);
                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;
                                                                    //widths[counter] = width;
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print RadioButtonList", ex.Message);
                                                                }
                                                            }

                                                            if (controlType.Equals("DropDownList") && printControl)
                                                            {


                                                                try
                                                                {
                                                                    font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                    text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font));
                                                                    text.Border = 0;
                                                                    text.PaddingTop = 5;
                                                                    if (textDirection.CompareTo("rtl") == 0)
                                                                    {
                                                                        //  table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                        table.HorizontalAlignment = 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        // table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                        table.HorizontalAlignment = 0;
                                                                    }
                                                                    // table.AddCell(text);
                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;
                                                                    //widths[counter] = width;
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print DropDownList", ex.Message);
                                                                }

                                                            }
                                                            log += string.Format(" cell:{0}__width:{1} ", counter, width);
                                                        }

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        log += exceptionMessageBuilder("Print Controls", ex.Message);
                                                    }


                                                }

                                                //counter++;
                                            }
                                            // table.AddCell(new PdfPCell()); //for the empty cell
                                            if (PDF_Cells.Count > 0)
                                            {
                                                log += printInfo("Num. of controls in row: " + (PDF_CellWidtds.Count));
                                                log += printInfo("Num. of cells_ in row: " + PDF_Cells.Count);
                                                int cell_counter = PDF_Cells.Count;
                                                table = new PdfPTable(PDF_Cells.Count);
                                                table.DefaultCell.Border = 0;
                                                table.CompleteRow();
                                                //table.WidthPercentage = 100;
                                                
                                                if (textDirection.Equals("rtl"))
                                                {
                                                    table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                    table.HorizontalAlignment = Element.ALIGN_RIGHT;

                                                }
                                                else
                                                {
                                                    table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                    table.HorizontalAlignment = Element.ALIGN_LEFT;

                                                }


                                                foreach (PdfPCell pdf_cell in PDF_Cells)
                                                {

                                                    table.AddCell(pdf_cell);
                                                }
                                                table.DefaultCell.NoWrap = false;


                                                widths = new float[PDF_CellWidtds.Count];
                                                widthsRtl = new float[PDF_CellWidtds.Count];
                                                int a = 0;
                                                float total = 0f;
                                                foreach (float c_width in PDF_CellWidtds)
                                                {
                                                    total += c_width;
                                                    widths[a] = c_width;
                                                    a++;
                                                }
                                                if (textDirection.Equals("rtl"))
                                                {
                                                    for (int b = 0; b < widths.Length; b++)
                                                    {
                                                        widthsRtl[widths.Length - b - 1] = widths[b];
                                                    }
                                                }

                                                if (total > 0f)
                                                {
                                                    if (textDirection.Equals("rtl"))
                                                    {
                                                        table.SetWidths(widthsRtl);

                                                    }
                                                    else
                                                    {
                                                        table.SetWidths(widths);

                                                    }
                                                   
                                                }
                                            }
                                            document.Add(table);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        log += exceptionMessageBuilder("Print Controls - Layout Settings", ex.ToString());
                                    }



                                    Rectangle page = document.PageSize;

                                    PdfPTable head = new PdfPTable(1);
                                    head.TotalWidth = 49;

                                    font = new Font(bfUniCode, _dataFontSize, 1, DataColor);
                                   


                                    head.WriteSelectedRows(0, -1, 10, 48, writer.DirectContent);
                                    
                                    document.Close();

                                    log += "<br>PDF1";
                                    try
                                    {

                                        

                                        pdfBytes = memStream.ToArray();
                                        string libURLName = userName.Replace("\\", "");
                                        web.Files.Add(url + "/" + destinationFolder + "/" + destinationfileName + ".pdf", pdfBytes, true);
                                        web.Update();
                                        //log += "is_user_folder: " + is_user_folder;
                                        //if (is_user_folder)
                                        //{
                                        //    SPFolder oFolder = web.Folders[url + "/" + destinationFolder];
                                        //    SPFileCollection collFiles = oFolder.Files;

                                        //    SPList list = web.Lists[docTypeList];
                                        //    SPListItemCollection listCol = list.Items;
                                        //    int ValueId = 0;
                                        //    foreach (SPItem item in listCol)
                                        //    {
                                        //        if (item["Title"].Equals(docType))
                                        //        {
                                        //            ValueId = item.ID;
                                        //        }
                                        //    }
                                        //    try
                                        //    {
                                        //        log += "Browsing existing files<br>";
                                        //        foreach (SPFile oFile in collFiles)
                                        //        {
                                        //            log += "<br>" + oFile.Name;

                                        //            if (oFile.Name.CompareTo(destinationfileName + ".pdf") == 0)
                                        //            {
                                        //                log += "<br>found file";
                                        //                oFile.Item[docTypeColumn] = new SPFieldLookupValue(ValueId, docType);
                                        //                oFile.Item.Update();
                                        //                oFile.Update();
                                        //                //log += "<br> file type: " + oFile.Item.GetFormattedValue("Document Type");
                                        //                break;
                                        //            }

                                        //        }
                                        //    }
                                        //    catch (Exception ex)
                                        //    {
                                        //        log += exceptionMessageBuilder("Set doc type", ex.Message);
                                        //    }

                                        //}


                                        //log += "<br>Success!";
                                        log += print_OK_Step("Success saving file.");
                                        pdf_obj._pdf_bytes = pdfBytes;
                                        pdf_obj.File_Name = destinationfileName;
                                        pdf_obj.PDF_log = log;
                                    }

                                    catch (Exception ex)
                                    {
                                        log += exceptionMessageBuilder("Save PDF File", ex.Message);
                                    }

                                    


                                }
                            }
                        }

                    }



                });
            }
            catch (Exception ex) { log += exceptionMessageBuilder("Controls to PDF - Main", ex.Message); }
           // return printData;
           
            return pdf_obj;
            //return  pdfBytes;
        }


        public PDF_Obj loadXML(string fPath,
           string fName,
           string textFont,
           string applicantsList,
           string textDirection,
           string itemUserName,
           
           
           string destinationWeb,
           string destinationFolder,
           string destinationList,
           string fileName,
           bool is_user_folder, 
           string docTypeList, 
           string docTypeColumn, 
           string docType
            )
        {


            //string printData = "";
            string url = destinationWeb;// SPContext.Current.Web.Url.ToString();
            string userName = "";
            string log = "";
            byte[] pdfBytes = { };
            PDF_Obj pdf_obj = new PDF_Obj();
            log += printInfoTitle("Generating PDF1");
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    log +=printInfo("trying to create file at: " + url + "/" + destinationFolder);
                    log += printInfo("destination web: " + destinationWeb);
                    log += printInfo("destination folder: " + destinationFolder);


                    PdfPCell text;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(fPath + "\\" + fName);
                    //log += "<br>loading xml at:" + fPath + "\\" + fName;
                    //log += "<br>applicants list: " + destinationList;
                    //log += "<br>fileName:" + fileName;
                    using (SPSite site = new SPSite(destinationWeb))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;


                            //Fetching the current list
                            SPList oList = web.Lists[destinationList];
                            SPListItemCollection collListItems = oList.Items;
                            //messageLabel.Text += "found list <br>";
                            foreach (SPListItem oListItem in collListItems)
                            {
                                if (oListItem["Title"].Equals(fileName))
                                {

                                    log += printInfo("Found list item");
                                    //*********************************************************************************
                                    //Start new PDF Table
                                    //Reference a Unicode font to be sure that the symbols are present.
                                    document = new Document(PageSize.A4);
                                    MemoryStream memStream = new MemoryStream();
                                    PdfWriter writer = PdfWriter.GetInstance(document, memStream);
                                    writer.CompressionLevel = PdfStream.BEST_COMPRESSION;
                                    PdfPCell docTitle;
                                    document.Open();

                                    //Add a new page 
                                    document.NewPage();
                                    string fontName = "";
                                    if (textFont.CompareTo("Arial") == 0)
                                    {
                                        fontName = "ARIALUNI.TTF";
                                    }
                                    if (textFont.CompareTo("David") == 0)
                                    {
                                        fontName = "david.ttf";
                                    }
                                    if (textFont.CompareTo("TimesNewRoman") == 0)
                                    {
                                        fontName = "times.ttf";
                                    }
                                    if (textFont.CompareTo("Tahoma") == 0)
                                    {
                                        fontName = "tahoma.ttf";
                                    }
                                    if (textFont.CompareTo("OpenSans") == 0)
                                    {
                                        fontName = "OpenSansHebrew-Regular.ttf";
                                    }

                                    BaseFont bfUniCode = BaseFont.CreateFont(@"c:\Windows\Fonts\" + fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                                    BaseColor Headercolor = new BaseColor(60, 69, 92); ;// new BaseColor(27, 72, 139);
                                    BaseColor _docHeadercolor = BaseColor.WHITE; //new CMYKColor(74, 67, 66, 84); ; //new BaseColor(46, 96, 171);
                                    BaseColor DataColor = new BaseColor(42, 49, 50);// new BaseColor(80, 80, 80);
                                    BaseColor LabelColor = new BaseColor(51, 107, 135);  //  new BaseColor(42, 49, 50);// BaseColor(130, 130, 130);

                                    //Create a font from the base font
                                    Font font = new Font(bfUniCode, 11, Font.NORMAL);

                                    //Use a table so that we can set the text direction
                                    PdfPTable table = new PdfPTable(4);
                                    if (textDirection.CompareTo("rtl") == 0)
                                    {
                                        table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                    }
                                    else
                                    {
                                        table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    }
                                    table.DefaultCell.Border = 0;
                                    //Ensure that wrapping is on, otherwise Right to Left text will not display
                                    table.DefaultCell.NoWrap = false;

                                    // Create a regex expression to detect hebrew or arabic code points
                                    // const string regex_match_arabic_hebrew = @"[\u0600-\u06FF,\u0590-\u05FF]+";

                                    //if (Regex.IsMatch(TextBox1.Text, regex_match_arabic_hebrew, RegexOptions.IgnoreCase))
                                    if (textDirection.CompareTo("rtl") == 0)
                                    {
                                        table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                    }
                                    else
                                    {
                                        table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    }
                                    //*********************************************************************************



                                    System.Web.UI.WebControls.Table t = new System.Web.UI.WebControls.Table();


                                    try
                                    {

                                        foreach (XmlNode node in xmlDoc.SelectNodes(_configPath))
                                        {
                                            foreach (XmlNode el in node.ChildNodes)
                                            {
                                                try
                                                {
                                                    // if (userName.CompareTo(oListItem.GetFormattedValue("userName")) == 0)
                                                    {
                                                        if (el.LocalName.ToLower().CompareTo("logo") == 0)
                                                        {
                                                            try
                                                            {
                                                                table = new PdfPTable(1);
                                                                table.DefaultCell.Border = 0;
                                                                String logoPath = el.InnerText;


                                                                // add header image; PdfPCell() overload sizes image to fit cell
                                                                iTextSharp.text.Image logoImage = iTextSharp.text.Image.GetInstance(logoPath);
                                                                logoImage.BackgroundColor = BaseColor.WHITE;

                                                                logoImage.ScaleAbsolute(485f, 50f);

                                                                text = new PdfPCell(logoImage, false);
                                                                text.Border = 0;
                                                                table.AddCell(text);
                                                                document.Add(table);
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                                log += exceptionMessageBuilder("Add Logo", ex.Message);
                                                            }
                                                        }
                                                        if (el.LocalName.ToLower().CompareTo("docheader") == 0)
                                                        {

                                                            table = new PdfPTable(1);
                                                            table.DefaultCell.Border = 0;

                                                            if (textDirection.CompareTo("rtl") == 0)
                                                            {
                                                                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                table.HorizontalAlignment = 2;
                                                            }
                                                            else
                                                            {
                                                                table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                table.HorizontalAlignment = 0;
                                                            }

                                                            bool simpleHeader = true;
                                                            string docHeaderText = string.Empty;
                                                            foreach (XmlNode cel in el.ChildNodes)
                                                            {


                                                                if (cel.LocalName.Equals("text"))
                                                                {
                                                                    docHeaderText = cel.InnerText;
                                                                    simpleHeader = false;
                                                                }

                                                            }
                                                            if (simpleHeader)
                                                            {
                                                                docHeaderText = el.InnerText;
                                                            }

                                                            font = new Font(bfUniCode, _doc_headerFontSize, iTextSharp.text.Font.NORMAL, _docHeadercolor);

                                                            docTitle = new PdfPCell(new Phrase(docHeaderText, font));

                                                            docTitle.Border = 0;
                                                            docTitle.BackgroundColor = new BaseColor(60, 69, 92); // new CMYKColor(67, 36, 0, 53);// BaseColor(130, 130, 130);


                                                            docTitle.PaddingBottom = 10;
                                                            docTitle.VerticalAlignment = Element.ALIGN_MIDDLE;
                                                            docTitle.HorizontalAlignment = Element.ALIGN_CENTER;
                                                            table.WidthPercentage = 100;
                                                            table.AddCell(docTitle);
                                                            document.Add(table);

                                                            table = new PdfPTable(4);
                                                            if (textDirection.CompareTo("rtl") == 0)
                                                            {
                                                                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                            }
                                                            else
                                                            {
                                                                table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                            }
                                                            table.DefaultCell.Border = 0;
                                                            table.SpacingBefore = 10;
                                                            table.DefaultCell.NoWrap = false;

                                                        }

                                                        if (el.LocalName.CompareTo("fileName") == 0)
                                                        {
                                                            docName = el.InnerText;

                                                        }


                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    log += exceptionMessageBuilder("Load Configurations", ex.Message);

                                                }


                                            }
                                        }


                                        foreach (XmlNode node in xmlDoc.SelectNodes(_dataPath))
                                        {
                                            ArrayList PDF_Cells = new ArrayList();
                                            ArrayList PDF_CellWidtds = new ArrayList();

                                            table = new PdfPTable(node.ChildNodes.Count + 1);
                                            if (textDirection.CompareTo("rtl") == 0)
                                            {
                                                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                table.HorizontalAlignment = 2;
                                            }
                                            else
                                            {
                                                table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                table.HorizontalAlignment = 0;
                                            }
                                            table.DefaultCell.Border = 0;
                                            // table.SpacingBefore= 4;
                                            table.DefaultCell.NoWrap = false;


                                            float[] widths;// = new float[rowControls];
                                            float[] widthsRtl;// = new float[rowControls];
                                            int counter = 0;
                                            bool setWidth = true;
                                            // widths[0]=1f;
                                            foreach (XmlNode el in node.ChildNodes)
                                            {

                                                //if (userName.CompareTo(oListItem.GetFormattedValue("userName")) == 0)
                                                {

                                                    try
                                                    {
                                                        if (el.LocalName.CompareTo(_headerTag) == 0)
                                                        {

                                                            setWidth = false;
                                                            document.Add(table);

                                                            font = new Font(bfUniCode, _headerFontSize, iTextSharp.text.Font.NORMAL, Headercolor);

                                                            table = new PdfPTable(1);
                                                            table.DefaultCell.Border = 0;
                                                            table.DefaultCell.BorderColor = BaseColor.WHITE;
                                                            if (textDirection.CompareTo("rtl") == 0)
                                                            {
                                                                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                table.HorizontalAlignment = 2;
                                                            }
                                                            else
                                                            {
                                                                table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                table.HorizontalAlignment = 0;
                                                            }

                                                            docTitle = new PdfPCell(new Phrase(el.InnerText, font));
                                                            docTitle.Border = 0;
                                                           // docTitle.BorderWidthRight = 0;
                                                           // docTitle.BorderWidthLeft = 0;
                                                           // docTitle.BorderWidthTop = 0;
                                                           // docTitle.BorderColorBottom = new BaseColor(42, 49, 50);
                                                            //docTitle.Border = 0;
                                                            //docTitle.BackgroundColor = new BaseColor(42, 49, 50);
                                                            docTitle.PaddingBottom = 7;
                                                            docTitle.PaddingTop = 10;


                                                            table.AddCell(docTitle);
                                                            table.WidthPercentage = 100;
                                                            document.Add(table);
                                                            document.Add(new Paragraph(""));
                                                            table = new PdfPTable(1);


                                                        }

                                                        if (el.LocalName.ToLower().CompareTo("label") == 0)
                                                        {


                                                            string print = "yes";
                                                            string labelText = "";
                                                            float labelWidth = 0f;
                                                            foreach (XmlNode cel in el.ChildNodes)
                                                            {
                                                                if (cel.LocalName.CompareTo("text") == 0)
                                                                {
                                                                    labelText = cel.InnerText;
                                                                }

                                                                if (cel.LocalName.CompareTo("print") == 0)
                                                                {
                                                                    if (cel.InnerText.Length > 0)
                                                                    {
                                                                        print = cel.InnerText;
                                                                    }

                                                                }
                                                                if (cel.LocalName.ToLower().Equals("width"))
                                                                {
                                                                    labelWidth = (float)Convert.ToDouble(cel.InnerText);
                                                                }

                                                            }

                                                            if (print.ToLower().CompareTo("yes") == 0)
                                                            {
                                                                font = new Font(bfUniCode, _labelFontSize, iTextSharp.text.Font.NORMAL, LabelColor);
                                                                text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(labelText.Trim()), font));
                                                                text.VerticalAlignment = Element.ALIGN_TOP;
                                                                text.NoWrap = false;
                                                                text.Border = 0;
                                                                text.PaddingTop = 5;
                                                                PDF_Cells.Add(text);
                                                                PDF_CellWidtds.Add(labelWidth);
                                                                counter++;
                                                                log += string.Format("cell:{0}__width:{1} ", counter, labelWidth);
                                                            }
                                                        }

                                                        if (el.LocalName.CompareTo("spaceRow") == 0)
                                                        {
                                                            try
                                                            {
                                                                setWidth = false;
                                                                table.CompleteRow();
                                                                text = new PdfPCell(new Phrase(" "));
                                                                text.Border = 0;

                                                                table.AddCell(text);
                                                                table.CompleteRow();
                                                            }
                                                            catch (Exception ex) { log += exceptionMessageBuilder("Add space row", ex.Message); }

                                                        }
                                                        if (el.LocalName.CompareTo("control") == 0)
                                                        {

                                                            string controlId = "";
                                                            string controlType = "";
                                                            string controlData = "";
                                                            string controlList = "";
                                                            float width = 0f;
                                                            bool printControl = true;
                                                            bool visibleContorl = true;
                                                            //controlPath

                                                            foreach (XmlNode cel in el.ChildNodes)
                                                            {

                                                                if (cel.LocalName.ToLower().Equals("data"))
                                                                {
                                                                    controlData = cel.InnerText;
                                                                    controlId = cel.InnerText;
                                                                }

                                                                if (cel.LocalName.ToLower().Equals("type"))
                                                                {
                                                                    controlType = cel.InnerText;
                                                                }
                                                                if (cel.LocalName.ToLower().Equals("list"))
                                                                {
                                                                    controlList = cel.InnerText;
                                                                }
                                                                if (cel.LocalName.ToLower().Equals("width"))
                                                                {
                                                                    width = (float)Convert.ToDouble(cel.InnerText);
                                                                }
                                                                if (cel.LocalName.ToLower().CompareTo("print") == 0)
                                                                {
                                                                    if (cel.InnerText.Length > 0)
                                                                    {
                                                                        printControl = (cel.InnerText.ToLower().Equals("yes")) ? true : false;

                                                                    }
                                                                }
                                                                if (cel.LocalName.ToLower().CompareTo("visible") == 0)
                                                                {
                                                                    if (cel.InnerText.Length > 0)
                                                                    {
                                                                        visibleContorl = (cel.InnerText.ToLower().Equals("yes")) ? true : false;

                                                                    }
                                                                }

                                                            }

                                                            log += string.Format("<br />    {0}] control: {1}", controlId, printControl);

                                                            if (controlType.ToLower().Equals("datepicker") && printControl)
                                                            {
                                                                // widths[counter] = width;

                                                                log += "<br>adding date picker..";
                                                                try
                                                                {

                                                                    log += "<br>datepicker data: <" + oListItem.GetFormattedValue(controlId) + ">";
                                                                    //log += "<br>Text box data: <" + (String)ctrl.Data + ">";
                                                                    font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);

                                                                    text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId).Split(' ')[0]), font));
                                                                    text.PaddingTop = 5;
                                                                    text.Border = 0;
                                                                    if (textDirection.CompareTo("rtl") == 0)
                                                                    {
                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                        table.HorizontalAlignment = 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                        table.HorizontalAlignment = 0;
                                                                    }
                                                                    //table.AddCell(text);
                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;
                                                                    // widths[counter] = width;

                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print Datepicker", ex.Message);
                                                                }
                                                            }
                                                            if (controlType.ToLower().CompareTo("textbox") == 0 && printControl)
                                                            {
                                                                //widths[counter] = width;

                                                                //log += "<br>adding text box..";
                                                                try
                                                                {
                                                                    font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                    text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font));
                                                                    if (textDirection.CompareTo("rtl") == 0)
                                                                    {
                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                        table.HorizontalAlignment = 2;

                                                                    }
                                                                    else
                                                                    {
                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                        table.HorizontalAlignment = 0;
                                                                    }
                                                                    text.Border = 0;
                                                                    text.PaddingTop = 5;
                                                                    text.VerticalAlignment = 1;
                                                                    //table.AddCell(text);
                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;
                                                                    // widths[counter] = width;

                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print TextBox", ex.Message);
                                                                }
                                                            }
                                                          
                                                                if (controlType.ToLower().CompareTo("label") == 0 && printControl)
                                                            {
                                                                //log += "<br>adding text box..";
                                                                // widths[counter] = width;

                                                                try
                                                                {

                                                                    //  foreach (DynamicControl ctrl in _dynamicTextBoxIDs)
                                                                    {
                                                                        //if (ctrl.ID.CompareTo(controlId) == 0)
                                                                        {
                                                                            //log += "<br>Text box data: <" + (String)ctrl.Data + ">";
                                                                            font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                            text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font));
                                                                            if (textDirection.CompareTo("rtl") == 0)
                                                                            {
                                                                                // text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                                table.HorizontalAlignment = 2;
                                                                            }
                                                                            else
                                                                            {
                                                                                // text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                                table.HorizontalAlignment = 0;
                                                                            }
                                                                            text.Border = 0;
                                                                            text.PaddingTop = 5;
                                                                            //table.AddCell(text);
                                                                            PDF_Cells.Add(text);
                                                                            PDF_CellWidtds.Add(width);
                                                                            counter++;
                                                                            // widths[counter] = width;
                                                                        }
                                                                    }
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print Label", ex.Message);
                                                                }
                                                            }
                                                            if (controlType.ToLower().Equals("dependentdropdownlist") && printControl)
                                                            {
                                                                //widths[counter] = width;

                                                                try
                                                                {
                                                                    log += "<br>**********Adding dependent DDL***********";
                                                                    //foreach (DynamicControl ctrl in _dynamicDropDownControlIDs)
                                                                    //   {
                                                                    // if (ctrl.ID.CompareTo(controlId) == 0)
                                                                    //{
                                                                    font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                    text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font));
                                                                    text.Border = 0;
                                                                    text.PaddingTop = 5;
                                                                    if (textDirection.CompareTo("rtl") == 0)
                                                                    {
                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                        table.HorizontalAlignment = 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        //text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                        table.HorizontalAlignment = 0;
                                                                    }
                                                                    //table.AddCell(text);
                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;
                                                                    // widths[counter] = width;
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print Dependent DropDownList", ex.Message);
                                                                }

                                                            }

                                                            if (controlType.ToLower().CompareTo("multilinetextbox") == 0 && printControl)
                                                            {
                                                                // widths[counter] = width;

                                                                try
                                                                {
                                                                    font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                    Paragraph ph = new Paragraph(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font);
                                                                    ph.Alignment = (textDirection.Equals("rtl")) ? Element.ALIGN_RIGHT : Element.ALIGN_LEFT;
                                                                    text = new PdfPCell(ph);
                                                                    if (textDirection.CompareTo("rtl") == 0)
                                                                    {

                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                        table.HorizontalAlignment = 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                        table.HorizontalAlignment = 0;
                                                                    }


                                                                    text.Border = 0;
                                                                    text.PaddingTop = 5;
                                                                    //table.AddCell(text);
                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;
                                                                    //widths[counter] = width;
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print MultiLineTextBox", ex.Message);
                                                                }

                                                            }


                                                            /*  if (controlType.CompareTo("Label") == 0)
                                                              {
                                                                  //widths[counter] = width;
                                                                
                                                                  font = new Font(bfUniCode, _labelFontSize, 1, DataColor);
                                                                  text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font));
                                                                  text.Border = 0;
                                                                  if (textDirection.Equals("rtl"))
                                                                  {
                                                                      text.HorizontalAlignment = Element.ALIGN_RIGHT;
                                                                  }
                                                                 // table.AddCell(text);
                                                                  PDF_Cells.Add(text);
                                                                  PDF_CellWidtds.Add(width);
                                                                  counter++;
                                                                  //widths[counter] = width;
                                                                 //break;
                                                              }*/
                                                            if (controlType.ToLower().CompareTo("calculatedvalue") == 0 && printControl)
                                                            {
                                                                font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font));
                                                                text.Border = 0;
                                                                if (textDirection.CompareTo("rtl") == 0)
                                                                {
                                                                    //text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                    table.HorizontalAlignment = 2;
                                                                }
                                                                else
                                                                {
                                                                    // text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                    table.HorizontalAlignment = 0;
                                                                }
                                                                text.PaddingTop = 5;
                                                                //table.AddCell(text);
                                                                PDF_Cells.Add(text);
                                                                PDF_CellWidtds.Add(width);
                                                                counter++;
                                                            }
                                                            if ((controlType.ToLower().Equals("textlabel")|| controlType.ToLower().Equals("parameter") || controlType.ToLower().Equals("url_parameter"))  && printControl)
                                                            {
                                                                // widths[counter] = width;

                                                                try
                                                                {
                                                                    string temp = "";
                                                                    //check if controlId content is dateTime
                                                                    if (oListItem.Fields[controlId].FieldValueType.ToString().Equals("System.DateTime"))
                                                                    {
                                                                        //logLabel.Text += "<br>Created type for: " + controlId + " is: " + oListItem.Fields[controlId].FieldValueType;
                                                                        DateTime d = Convert.ToDateTime(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)));

                                                                        temp = d.ToUniversalTime().ToString();
                                                                    }
                                                                    else
                                                                    {
                                                                        temp = HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId));
                                                                    }
                                                                    font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                    text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(temp), font));
                                                                    text.Border = 0;
                                                                    text.PaddingTop = 5;
                                                                    if (textDirection.CompareTo("rtl") == 0)
                                                                    {
                                                                        //text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                        table.HorizontalAlignment = 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        //text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                        table.HorizontalAlignment = 0;
                                                                    }
                                                                    //table.AddCell(text);
                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;
                                                                    //widths[counter] = width;
                                                                }

                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print TextLabel", ex.Message);
                                                                }

                                                            }
                                                            if (controlType.ToLower().Equals("signature"))
                                                            {
                                                                try
                                                                {
                                                                    log += print_OK_Step("Adding signature Image: ");
                                                                    // table = new PdfPTable(1);
                                                                    //table.DefaultCell.Border = 1;
                                                                    // String logoPath =((TextBox)FindControl("Signature")).Text;
                                                                    String logoPath = HttpUtility.HtmlDecode(oListItem.GetFormattedValue("Signature").ToString());
                                                                    //log += print_OK_Step("signature Image path: " + logoPath);
                                                                    var base64Data = Regex.Match(logoPath, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;

                                                                    byte[] imageBytes = Convert.FromBase64String(base64Data);

                                                                    iTextSharp.text.Image signatureImage = iTextSharp.text.Image.GetInstance(imageBytes);
                                                                    signatureImage.BackgroundColor = BaseColor.WHITE;
                                                                    signatureImage.ScalePercent(40f);
                                                                    //signatureImage.ScaleAbsolute(485f, 250f);

                                                                    text = new PdfPCell(signatureImage, false);
                                                                    text.Border = 0;


                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;

                                                                    // table.AddCell(text);
                                                                    // document.Add(table);
                                                                }
                                                                catch (Exception ex)
                                                                {

                                                                    log += exceptionMessageBuilder("Add signature", ex.Message);
                                                                }
                                                            }
                                                            if (controlType.CompareTo("CheckBox") == 0 && printControl)
                                                            {

                                                                try
                                                                {
                                                                    if (printControl && !visibleContorl)
                                                                    {
                                                                        counter++;
                                                                        //  widths[counter] = width;

                                                                        string pathUnchecked = @"\\ekeksql00\SP_Resources$\HSS\images\unchecked.jpg";


                                                                        iTextSharp.text.Image checkBoxImage = iTextSharp.text.Image.GetInstance(pathUnchecked);
                                                                        checkBoxImage.BackgroundColor = BaseColor.WHITE;

                                                                        checkBoxImage.ScaleAbsolute(10f, 10f);
                                                                        text = new PdfPCell(checkBoxImage, false);
                                                                        if (textDirection.CompareTo("rtl") == 0)
                                                                        {
                                                                            // text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                            table.HorizontalAlignment = 2;
                                                                        }
                                                                        else
                                                                        {
                                                                            //text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                            table.HorizontalAlignment = 0;
                                                                        }
                                                                        text.VerticalAlignment = 1;
                                                                        text.Border = 0;
                                                                        text.PaddingTop = 5;
                                                                        //table.AddCell(text);
                                                                        PDF_Cells.Add(text);
                                                                        PDF_CellWidtds.Add(width);
                                                                    }
                                                                    else
                                                                    {
                                                                        foreach (DynamicControl ctrl in _dynamicCheckBoxIds)
                                                                        {
                                                                            if (ctrl.ID.CompareTo(controlId) == 0)
                                                                            {
                                                                                //widths[counter] = width;

                                                                                String pathChecked = @"\\ekeksql00\SP_Resources$\HSS\images\checked.jpg";
                                                                                String pathUnchecked = @"\\ekeksql00\SP_Resources$\HSS\images\unchecked.jpg";
                                                                                String pathCheckBox = ((bool)oListItem[controlId]) ? pathChecked : pathUnchecked;


                                                                                // add header image; PdfPCell() overload sizes image to fit cell
                                                                                iTextSharp.text.Image checkBoxImage = iTextSharp.text.Image.GetInstance(pathCheckBox);
                                                                                checkBoxImage.BackgroundColor = BaseColor.WHITE;

                                                                                checkBoxImage.ScaleAbsolute(10f, 10f);
                                                                                text = new PdfPCell(checkBoxImage, false);
                                                                                text.Border = 0;
                                                                                text.PaddingTop = 5;
                                                                                if (textDirection.CompareTo("rtl") == 0)
                                                                                {
                                                                                    //    text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                                    table.HorizontalAlignment = 2;
                                                                                }
                                                                                else
                                                                                {
                                                                                    //    text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                                    table.HorizontalAlignment = 0;
                                                                                }
                                                                                //table.AddCell(text);
                                                                                PDF_Cells.Add(text);
                                                                                PDF_CellWidtds.Add(width);
                                                                                counter++;
                                                                                //widths[counter] = width;
                                                                                break;
                                                                            }

                                                                        }
                                                                    }

                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print CheckBox", ex.Message);
                                                                }


                                                            }

                                                            if (controlType.ToLower().Equals("radiobuttonlist") && printControl)
                                                            {
                                                                // widths[counter] = width;

                                                                try
                                                                {
                                                                    font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                    text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font));
                                                                    text.Border = 0;
                                                                    text.PaddingTop = 5;
                                                                    if (textDirection.CompareTo("rtl") == 0)
                                                                    {
                                                                        //text.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                        table.HorizontalAlignment = 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        // text.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                        table.HorizontalAlignment = 0;
                                                                    }
                                                                    //table.AddCell(text);
                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;
                                                                    //widths[counter] = width;
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print RadioButtonList", ex.Message);
                                                                }
                                                            }

                                                            if (controlType.Equals("DropDownList") && printControl)
                                                            {


                                                                try
                                                                {
                                                                    font = new Font(bfUniCode, _dataFontSize, iTextSharp.text.Font.NORMAL, DataColor);
                                                                    text = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(oListItem.GetFormattedValue(controlId)), font));
                                                                    text.Border = 0;
                                                                    text.PaddingTop = 5;
                                                                    if (textDirection.CompareTo("rtl") == 0)
                                                                    {
                                                                        //  table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                                        table.HorizontalAlignment = 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        // table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                                        table.HorizontalAlignment = 0;
                                                                    }
                                                                    // table.AddCell(text);
                                                                    PDF_Cells.Add(text);
                                                                    PDF_CellWidtds.Add(width);
                                                                    counter++;
                                                                    //widths[counter] = width;
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    log += exceptionMessageBuilder("Print DropDownList", ex.Message);
                                                                }

                                                            }
                                                            log += string.Format("cell:{0}__width:{1} ", counter, width);
                                                        }

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        log += exceptionMessageBuilder("Print Controls", ex.Message);
                                                    }


                                                }

                                                //counter++;
                                            }
                                            // table.AddCell(new PdfPCell()); //for the empty cell
                                            if (PDF_Cells.Count > 0)
                                            {
                                                log += printInfo("Num. of controls in row: " + (PDF_CellWidtds.Count));
                                                log += printInfo("Num. of cells_ in row: " + PDF_Cells.Count);
                                                int cell_counter = PDF_Cells.Count;
                                                table = new PdfPTable(PDF_Cells.Count);
                                                table.DefaultCell.Border = 0;
                                                table.CompleteRow();                                               
                                                //table.WidthPercentage = 100;

                                                if (textDirection.Equals("rtl"))
                                                {
                                                    table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                                                    table.HorizontalAlignment = Element.ALIGN_RIGHT;
                                                }
                                                else
                                                {
                                                    table.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                                    table.HorizontalAlignment = Element.ALIGN_LEFT;
                                                }


                                                foreach (PdfPCell pdf_cell in PDF_Cells)
                                                {

                                                    table.AddCell(pdf_cell);
                                                }
                                                table.DefaultCell.NoWrap = false;


                                                widths = new float[PDF_CellWidtds.Count];
                                                widthsRtl = new float[PDF_CellWidtds.Count];
                                                int a = 0;
                                                float total = 0f;
                                                foreach (float c_width in PDF_CellWidtds)
                                                {
                                                    total += c_width;
                                                    widths[a] = c_width;
                                                    a++;
                                                }
                                                if (textDirection.Equals("rtl"))
                                                {
                                                    for (int b = 0; b < widths.Length; b++)
                                                    {
                                                        widthsRtl[widths.Length - b - 1] = widths[b];
                                                    }
                                                }

                                                if (total > 0f)
                                                {
                                                    if (textDirection.Equals("rtl"))
                                                    {
                                                        table.SetWidths(widthsRtl);

                                                    }
                                                    else
                                                    {
                                                        table.SetWidths(widths);

                                                    }
                                                }
                                            }
                                            document.Add(table);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        log += exceptionMessageBuilder("Print Controls - Layout Settings", ex.ToString());
                                    }



                                    Rectangle page = document.PageSize;

                                    PdfPTable head = new PdfPTable(1);
                                    head.TotalWidth = 49;

                                    font = new Font(bfUniCode, _dataFontSize, 1, DataColor);



                                    head.WriteSelectedRows(0, -1, 10, 48, writer.DirectContent);
                                    log += "<br>Closing PDF document";
                                    document.Close();
                                  
                                    try
                                    {
                                        log += "<br>trying to create file at: "+ url + "/" + destinationFolder + "/" + fileName + ".pdf";

                                        pdfBytes = memStream.ToArray();
                                        log += "<br>File size: " + pdfBytes.Length;
                                        string libURLName = userName.Replace("\\", "");
                                        //folder.Files.Add(url + "/" + folder + "/" + _upload.FileName, _upload.FileBytes, true);
                                        web.Files.Add(url + "/" + destinationFolder + "/" + fileName + ".pdf", pdfBytes, true);
                                        web.Update();

                                        log += "is_user_folder: " + is_user_folder;
                                        log += "folder to browse: " + url + "/" + destinationFolder;
                                        if (is_user_folder)
                                        {
                                            SPFolder oFolder = web.Folders[url + "/" + destinationFolder];
                                            SPFileCollection collFiles = oFolder.Files;

                                            SPList list = web.Lists[docTypeList];
                                            SPListItemCollection listCol = list.Items;
                                            int ValueId = 0;
                                            foreach (SPItem item in listCol)
                                            {
                                                if (item["Title"].Equals(docType))
                                                {
                                                    ValueId = item.ID;
                                                }
                                            }
                                            try
                                            {

                                                foreach (SPFile oFile in collFiles)
                                                {
                                                    log += "<br>" + oFile.Name;

                                                    if (oFile.Name.CompareTo(fileName + ".pdf") == 0)
                                                    {
                                                        log += "<br>found file";
                                                        oFile.Item[docTypeColumn] = new SPFieldLookupValue(ValueId, docType);
                                                        oFile.Item.Update();
                                                        oFile.Update();
                                                        //log += "<br> file type: " + oFile.Item.GetFormattedValue("Document Type");
                                                        break;
                                                    }

                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                log += exceptionMessageBuilder("Set doc type", ex.Message);
                                            }
                                        }




                                        log += print_OK_Step("Success creating PDF");




                                    }

                                    catch (Exception ex)
                                    {
                                        log += exceptionMessageBuilder("Add PDF File", ex.Message);

                                    }

                                   
                                   // printData = "";


                                }
                            }
                        }

                    }



                });
            }
            catch (Exception ex) {
                log += "<br>" + ex.Message;
                pdf_obj.PDF_log = log;
            }
            // return printData;
            pdf_obj.PDF_bytes = pdfBytes;
            pdf_obj.File_Name = fileName;
            pdf_obj.PDF_log = log;
            return pdf_obj;
            //return pdfBytes;
        }

        /// <summary>
        /// Generates standart format for exception 
        /// </summary>
        /// <param name="_location"></param>
        /// <param name="_exMessage"></param>
        /// <returns></returns>
        protected string exceptionMessageBuilder(string _location, string _exMessage)
        {
            string __message = string.Empty;

            __message += "<div style = \"color:red;\">";
            __message += "<h3>EXCEPTION THROWN</h3>";
            __message += "At: " + _location + "<br />Message: " + _exMessage;
            __message += "</div>";
            return __message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_title"></param>
        /// <returns></returns>
        protected string printInfoTitle(string _title)
        {
            string __message = string.Empty;

            __message += "<div style = \"color:#3d557c;\">";
            __message += "<h3>" + _title + "</h3>";
            __message += "</div>";
            return __message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_content"></param>
        /// <returns></returns>
        protected string printInfo(string _content)
        {
            string __message = string.Empty;

            __message += "<div style = \"color:#3d557c;\">";
            __message += _content;
            __message += "</div>";
            return __message;
        }

        protected string print_OK_Step(string _content)
        {
            string __message = string.Empty;

            __message += "<div style = \"color:#4d875f;\">";
            __message += _content;
            __message += "</div>";
            return __message;
        }

    }

}
