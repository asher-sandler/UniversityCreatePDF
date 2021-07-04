#region Reference declaration
using System;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Web.Hosting;
using System.Data;
//using System.Drawing;
using System.Collections.Generic;
using NCalc;
using System.DirectoryServices;
//using System.DirectoryServices.AccountManagement;
using System.Text;
using System.Text.RegularExpressions;
//using Microsoft.SharePoint.Client;
using SP = Microsoft.SharePoint.Client;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
#endregion


namespace Survey_Form.VisualWebPart1
{
    public partial class VisualWebPart1UserControl : UserControl
    {
        const string PARAMETERS_PATH = "/rows/parameters/parameter";
        const string UNIQUE_KEY_PATH = "/rows/unique_key";

        const string URL_UNIQUE_KEY = "ukey";
        const string URL_ITEM_ID = "itemid";
        const string URL_DESTINATION_KEY = "dest";

        StateBag vstate;

        ArrayList repeatingTableIdsForm;
        ArrayList repeatingTableIds;
        ArrayList dependentDynamicDropDownControlIDs;
        ArrayList dynamicTextBoxIDs;
        ArrayList dynamicDropDownControlIDs;
        ArrayList dynamicLabelIDs;
        ArrayList dynamicCheckBoxIds;
        ArrayList dynamicRadioButtonIds;
        ArrayList dynamicDatePickerIds;
        ArrayList dynamicRepeatingTableIds;
        ArrayList dynamicFormulaIds;
        ArrayList dynamicLabelIds;
        ArrayList dynamicTextLabelIds;
        ArrayList dynamicAttachmentIds;
        ArrayList clientControls;
        ArrayList ParametersList;
        ArrayList URL_Parameters_List;
        clientObject cObj;

        Unique_Key u_key;
        const string attachIconPath = "/Style Library/Images/DynamicForm/paperclipSmall.png";
        bool added_captcha = false;
        private VisualWebPart1 WebPart { get; set; }
        string _url = string.Empty;
        bool autoPostBack = false;
        // string applicantsList;
        string fileName;
        string filePath;
        string tooltip = string.Empty;
        string item_num = "0";
        string _docHeader;
        string _labelTag;
        string TypeTag;
        string fontName;
        string _docHeaderColor;
        string _doc_headerFontSize;
        string sectionHeaderColor;
        string section_headerFontSize;
        string _dataFontSize;
        string _configPath;
        string _dataPath;
        string dataColor;
        string _headerTag;
        string dataTag;
        string textDirection;
        string textAlign;
        string controlPath;
        string buttonTag;
        string messageTag;
        string finalMessage;
        string missingDataMessage;
        string logoImage;
        string unique_key_list;
        string unique_key_column;
        string unique_key_value;
        string unique_item_id;
        string url_destination=string.Empty;
        string unique_key_response_list;
        string status_column = "";

        string messageLabelId;
        string messageContainerID;
        string messageImageID;

        string captchaMessageLabelId;
        string captchaMessageContainerID;
        string captchaMessageImageID;
        int MtWidth;
        int MtHeight;
        int shortBoxWidth;
        int mediumBoxWidth;
        int longBoxWidth;
        bool adminMode;


        string filterColumn = string.Empty;
        string lookupColumn = string.Empty;
        string ddlGroup = string.Empty;
        int ddlOrder = -1;
        string mainFilter = string.Empty;
        string parentControl = string.Empty;
        bool lastDdl = false;
        string listColumn = string.Empty;
        string filter = string.Empty;
        string displayColumn = string.Empty;
        string addButtonTemplate = string.Empty;
        string addButtonPeriodControl = string.Empty;
        string sourceurlkey = string.Empty;

        bool addColumns;
        bool addLists;
        //string userName;
        string labelStyle = "";
        string headerStyle = "";
        string dataStyle = "";
        string buttonStyle = "";
        string multiLineDataStyle = "";
        string _docHeaderStyle = "";
        string MessageLabelStyle = "";
        string TextLabelStyle = "";
        string checkBoxStyle = "";
        string dropDownListStyle = string.Empty;
        string RadioButtonListStyle = string.Empty;
        string RadioButtonListItemStyle = string.Empty;
        string MessageContainerSuccessStyle = string.Empty;
        string MessageContainerFailedStyle = string.Empty;
        string MessageImageStyle = string.Empty;
        string messageTextStyle = string.Empty;
        string messageImageSuccessStyle = string.Empty;
        string messageImageFailedStyle = string.Empty;
        string tableStyle = string.Empty;
        string datePickerStyle = string.Empty;
        string docName = string.Empty;
        string addRowText = string.Empty;
        string print = string.Empty;
        string redirectPage = string.Empty;


        float docHeaderFontSize;
        float textDataFontSize;
        float labelFontSize;
        float headerFontSize;

        const string defaultWidth = "150";
        string cellWidth = defaultWidth;
        string[] controlTypes = { "" };
        string[] colData = { "" };
        string[] colTitle = { "" };
        string[] widthList = { "" };
        string calcFormula = string.Empty;

        string docTypeList = string.Empty;
        string docTypeColumn = string.Empty;
        string docType = string.Empty;
        string destinationFolder = string.Empty;
       
        bool is_user_folder = false;
        string destinationWeb = string.Empty;
        string destinationList = string.Empty;
        int destination_items_num = 1;
        string meta_source_list = string.Empty;
        string data_source_list = string.Empty;
        string pdfMailBodyHtml = string.Empty;
        string pdfMailSubject = string.Empty;
        string pdfEmailField = string.Empty;
        string pdfEmailFieldValue = string.Empty;
        string pdfDownloadButtonText = string.Empty;
        string ticketNum = string.Empty;
        bool createSubFolder = false;
        string renameFileName = string.Empty;
        string maxFileSize = "1048576";
        bool createPDF = true;
        int rowNum = 0;
        string description = string.Empty;
        string adData = string.Empty;
        //AD variables
        string firstName;
        string lastName;
        string email;
        string homePhone;
        string workPhone;
        string mobilePhone;
        string employeeId;
        string firstNameHe;
        string lastNameHe;
        string ldap;
        string domain;
        string mailServer;
        string ad_description;
        string regEx = string.Empty;
        string userDomain = string.Empty;
        string userName = string.Empty;
        string fullUserName = string.Empty;
        bool isAzureUser;

        /// <summary>Our sample property.</summary>
        /// <remarks>
        /// This property is really interesting.
        /// </remarks>
        /// <value>Some nice text.</value>
        /// <example>This is an example how to use it:</example>
        /// <para></para>


        //


        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                this.WebPart = this.Parent as VisualWebPart1;
                if (this.WebPart.adminMode)
                {
                    debugLabel.Visible = true;
                }
                else
                {
                    debugLabel.Visible = false;
                }
                bool can_load_form = false;
                //bool start_end_open = true;
                debugLabel.Text = "Starting";
                try
                {
                    debugLabel.Text += printInfo("Start date: " + this.WebPart.Start_Date_Time.ToString());
                    debugLabel.Text += printInfo("End date: " + this.WebPart.End_Date_Time.ToString());

                    if (this.WebPart.Start_Date_Time != null && this.WebPart.Start_Date_Time.ToString().Trim().Length > 0)
                    {
                        if (DateTime.Compare(DateTime.Now, this.WebPart.Start_Date_Time) < 0)
                        {
                            Start_End_Label.Visible = true;
                            Start_End_Label.Text = this.WebPart.Start_Date_Message;

                        }

                    }
                    if (DateTime.Compare(DateTime.Now, this.WebPart.End_Date_Time) > 0)
                    {
                        Start_End_Label.Visible = true;
                        Start_End_Label.Text = this.WebPart.End_Date_Message;
                    }
                }
                catch (Exception ex)
                {
                    debugLabel.Text += exceptionMessageBuilder("Check Start - End dates", ex.Message);
                }





                if (DateTime.Compare(DateTime.Now, this.WebPart.Start_Date_Time) >= 0 && DateTime.Compare(DateTime.Now, this.WebPart.End_Date_Time) < 0)
                //if (!start_end_open)
                {

                    Start_End_Label.Visible = false;
                    added_captcha = false;
                    _url = SPContext.Current.Web.Url.ToString();

                    //applicantsList = this.WebPart.applicantsList;
                    ldap = this.WebPart.ldap;
                    domain = this.WebPart.domain;
                    mailServer = this.WebPart.mailServer;
                    fileName = this.WebPart.fileName;
                    filePath = this.WebPart.filePath;
                    _headerTag = this.WebPart._headerTag;
                    _docHeader = this.WebPart._docHeader;
                    _labelTag = this.WebPart._labelTag;
                    dataTag = this.WebPart.DataTag;
                    TypeTag = this.WebPart.TypeTag;
                    messageTag = this.WebPart.messageTag;

                    fontName = this.WebPart.fontName;
                    _docHeaderColor = this.WebPart._docHeaderColor;
                    _doc_headerFontSize = this.WebPart._doc_headerFontSize;
                    sectionHeaderColor = this.WebPart.sectionHeaderColor;
                    section_headerFontSize = this.WebPart.section_headerFontSize;
                    _dataFontSize = this.WebPart._dataFontSize;
                    dataColor = this.WebPart.dataColor;
                    _configPath = this.WebPart._configPath;
                    _dataPath = this.WebPart._dataPath;
                    textDirection = this.WebPart.textDirection.ToString();
                    textAlign = this.WebPart.textAlign.ToString();
                    controlPath = this.WebPart.controlPath;
                    buttonTag = this.WebPart.buttonTag;
                    MtWidth = this.WebPart.MtWidth;
                    MtHeight = this.WebPart.MtHeight;
                    addColumns = this.WebPart.addColumns;
                    addLists = this.WebPart.addLists;

                    //userName = this.Context.User.Identity.Name.ToString();
                    GetUser();
                    GetDomain();
                    if (isAzureUser)
                    {
                        fullUserName = userName + "@" + userDomain;
                    }
                    else
                    {
                        fullUserName = userDomain + "\\" + userName;
                    }


                    shortBoxWidth = this.WebPart.shortBoxWidth;
                    mediumBoxWidth = this.WebPart.mediumBoxWidth;
                    longBoxWidth = this.WebPart.longBoxWidth;
                    docHeaderFontSize = this.WebPart.docHeaderFontSize;
                    textDataFontSize = this.WebPart.textDataFontSize;
                    labelFontSize = this.WebPart.labelFontSize;
                    headerFontSize = this.WebPart.headerFontSize;
                    adminMode = this.WebPart.adminMode;

                    finalMessage = "";
                    missingDataMessage = "";
                    messageLabelId = "";
                    messageContainerID = string.Empty;
                    messageImageID = string.Empty;
                    captchaMessageLabelId = string.Empty;
                    captchaMessageContainerID = string.Empty;
                    captchaMessageImageID = string.Empty;

                    repeatingTableIdsForm = new ArrayList();
                    repeatingTableIds = new ArrayList();
                    dependentDynamicDropDownControlIDs = new ArrayList();
                    dynamicTextBoxIDs = new ArrayList();
                    dynamicDropDownControlIDs = new ArrayList();
                    dynamicLabelIDs = new ArrayList();
                    dynamicCheckBoxIds = new ArrayList();
                    dynamicRadioButtonIds = new ArrayList();
                    dynamicDatePickerIds = new ArrayList();
                    dynamicRepeatingTableIds = new ArrayList();
                    dynamicFormulaIds = new ArrayList();
                    dynamicLabelIds = new ArrayList();
                    dynamicTextLabelIds = new ArrayList();
                    dynamicAttachmentIds = new ArrayList();
                    clientControls = new ArrayList();
                    ParametersList = new ArrayList();
                    URL_Parameters_List = new ArrayList();
                    u_key = new Unique_Key();

                    labelStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicLabel" : "DynamicLabelHe";
                    headerStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicHeader" : "DynamicHeaderHe";
                    dataStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicTextBox" : "DynamicTextBoxHe";
                    buttonStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicButton" : "DynamicButtonHe";
                    multiLineDataStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicMultiLineTextBox" : "DynamicMultiLineTextBoxHe";
                    _docHeaderStyle = (textDirection.CompareTo("ltr") == 0) ? "Dynamic_docHeader" : "Dynamic_docHeaderHe";
                    MessageLabelStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicMessageLabel" : "DynamicMessageLabelHe";
                    TextLabelStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicTextLabel" : "DynamicTextLabelHe";
                    checkBoxStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicCheckBox" : "DynamicCheckBoxHe";
                    RadioButtonListStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicRadioButtonListStyle" : "DynamicRadioButtonListStyleHe";
                    RadioButtonListItemStyle = (textDirection.CompareTo("ltr") == 0) ? "rblItem" : "rblItemHe";
                    dropDownListStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicDropDownList" : "DynamicDropDownListHe";
                    tableStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicTable" : "DynamicTableHe";
                    datePickerStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicDatePickerAnonymous" : "DynamicDatePickerAnonymousHe";
                    MessageContainerSuccessStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicMessageContainerSuccessEn" : "DynamicMessageContainerSuccessHe";
                    MessageContainerFailedStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicMessageContainerFailedEn" : "DynamicMessageContainerFailedHe";
                    MessageImageStyle = (textDirection.CompareTo("ltr") == 0) ? "DinamicMessageImageEn" : "DinamicMessageImageHe";
                    messageTextStyle = (textDirection.CompareTo("ltr") == 0) ? "DynamicMessageTextEn" : "DynamicMessageTextHe";
                    messageImageSuccessStyle = (textDirection.CompareTo("ltr") == 0) ? "DinamicMessageImageSuccessEn" : "DinamicMessageImageSuccessHe";
                    messageImageFailedStyle = (textDirection.CompareTo("ltr") == 0) ? "DinamicMessageImageFailedEn" : "DinamicMessageImageFailedHe";
                    //
                    //

                    debugLabel.Text += printInfo("Version 2.7 - 30/6/2020");
                   // long dt_ticks = DateTime.Now.Ticks;
                    //debugLabel.Text += printInfo("Date time ticks: " + dt_ticks);
                    // debugLabel.Text += printInfo("date time: " + new DateTime(dt_ticks));
                    try
                    {
                        if (this.WebPart.Send_Pdf_Attachment)
                        {
                            debugLabel.Text += printInfo(string.Format("To send PDF as attachment use the following tags in config part of XML:"));
                            debugLabel.Text += printInfo(HttpUtility.HtmlEncode(string.Format("<pdfMailBodyHtml><![CDATA[ Content]]></pdfMailBodyHtml>")));
                            debugLabel.Text += printInfo(HttpUtility.HtmlEncode(string.Format("<pdfMailSubject>[Subject Text]</pdfMailSubject>")));
                            debugLabel.Text += printInfo(HttpUtility.HtmlEncode(string.Format("<pdfEmailField>[Email column name]</pdfEmailField>")));
                        }

                        
                        debugLabel.Text += printInfoTitle("Web part Configurations");
                        debugLabel.Text += printInfo("<b>XML file path:</b> " + filePath);
                        debugLabel.Text += printInfo("<b>XML file name: </b>" + fileName);
                        debugLabel.Text += printInfo("<b>Create lists: </b>" + this.WebPart.addLists.ToString());
                        debugLabel.Text += printInfo("<b>Create destination list columns: </b>" + this.WebPart.addColumns.ToString());
                        debugLabel.Text += printInfo("---------------------------------<br />");
                        SPSecurity.RunWithElevatedPrivileges(delegate ()
                        {
                            try
                            {
                                loadAD();

                            }
                            catch (Exception ex)
                            {
                                debugLabel.Text += exceptionMessageBuilder("Load data from AD", ex.Message);
                            }
                            loadXML_Config(filePath, fileName);
                            //signature signature_img
                           
                            try
                            {
                                
                                can_load_form = SetParametersFromURL();
                                LoadParameters();
                                if(url_destination.Trim().Length>0)
                                {
                                    destinationFolder = url_destination;
                                }
                                debugLabel.Text += printInfo("<b>PDF destination folder:</b> " + destinationFolder);
                            }
                            catch (Exception ex)
                            {
                                debugLabel.Text += exceptionMessageBuilder("'Page Load' - load parameters from URL ", ex.Message);
                            }
                           
                            if (survey_done() && can_load_form)
                            {
                                debugLabel.Text += printInfo("Was form completed? Yes");
                                Start_End_Message_Label.Text = this.WebPart.Completed_Form_Message;
                            }
                            else
                            {
                                if (can_load_form || this.WebPart.Load_Form_With_No_Parameters_In_URL)
                                {
                                    loadXML(filePath, fileName);
                                    try
                                    {
                                        ((System.Web.UI.HtmlControls.HtmlButton)FindControl("export_png")).Attributes.Add("onclick", "load_png('" + FindControl("signature_img").ClientID + "','" + FindControl("Signature").ClientID + "')");
                                    }
                                    catch { }
                                    debugLabel.Text += printInfo("Was form completed?  No");
                                }
                                else
                                {
                                    debugLabel.Text += printInfoTitle("Encrypted parameters expected in URL. Failed to load form");
                                }
                                
                            }
                           
                            if (IsPostBack)
                            {
                                try
                                {
                                    debugLabel.Text += printInfoTitle("Updating signature image: " + ViewState["sig"].ToString());
                                    ((Image)FindControl("signature_img")).Attributes["src"] = ViewState["sig"].ToString();
                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("IsPostBack", ex.Message);
                                }
                            }
                           

                            validate();

                        });
                    }
                    catch (Exception ex)
                    {
                        debugLabel.Text += exceptionMessageBuilder("'Page Load' event ", ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("'Page Load - Main' event ", ex.Message);
            }
        }


        protected void GetDomain()
        {
            try
            {
                int userType = this.Context.User.Identity.Name.Split('|').Length;
                switch (userType)
                {
                    case 1:
                        userDomain = this.Context.User.Identity.Name.Split('\\')[0]; // AD 
                        isAzureUser = false;
                        break;
                    case 2:
                        userDomain = this.Context.User.Identity.Name.Split('|')[1].Split('\\')[0];
                        isAzureUser = false;
                        break;
                    case 3:
                        userDomain = this.Context.User.Identity.Name.Split('|')[2].Split('@')[1];
                        isAzureUser = true;
                        break;
                }
            }
            catch
            {
                debugLabel.Text += printInfo("No user logged in.</ br>For full functionality please log in.");
            }
        }


protected void GetUser()
        {
            try
            {
                int userType = this.Context.User.Identity.Name.Split('|').Length;
                switch (userType)
                {
                    case 1:
                        userName = this.Context.User.Identity.Name.Split('\\')[1]; // AD SAMAccountName
                        break;
                    case 2:
                        userName = this.Context.User.Identity.Name.Split('|')[1].Split('\\')[1]; // AD SAMAccountName
                        break;
                    case 3:
                        userName = this.Context.User.Identity.Name.Split('|')[2].Split('@')[0];
                        break;
                }
            }
            catch {
                debugLabel.Text += printInfo("No user logged in.</ br>For full functionality please log in.");
            }

        }






/// <summary>
/// Check if survey was already filled for particular user
/// </summary>
/// <returns></returns>
private bool survey_done()
        {
            debugLabel.Text += printInfo("running response check");
            bool done = true;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {

                    debugLabel.Text += printInfo("checking site: " + SPContext.Current.Web.Url);
                    debugLabel.Text += printInfo("checking list: " + unique_key_response_list);
                    debugLabel.Text += printInfo("looking for: (" + unique_item_id + " , " + unique_key_value + ")");
                    using (SPSite site = new SPSite(destinationWeb))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPListItemCollection survey_responses = null;
                            SPList survey_responses_list = web.Lists[unique_key_response_list];
                            survey_responses = survey_responses_list.GetItems(new SPQuery()
                            {
                                //Modify query to load by unique key and item id
                               Query = @"<Where>"
                                                + "<And><Eq>"
                                                + "<FieldRef Name='Title' />"
                                                + "<Value Type='Text'>" + unique_key_value + "</Value>"
                                                + "</Eq>"
                                                + "<Eq>"
                                                + "<FieldRef Name='item_id' />"
                                                + "<Value Type='Text'>" + unique_item_id + "</Value>"
                                                + "</Eq>"
                                                + "</And>"
                                                + "</Where>"

                                             
                            });
                            debugLabel.Text += printInfo("found items: " + survey_responses.Count);
                            if (survey_responses.Count == 0)
                            {
                                done = false;
                            }
                        }

                    }
                });

            }
            catch (Exception ex)
            {
                exceptionMessageBuilder("Check if form was competed", ex.Message);
                done = true;
            }
            return done;
        }


        /// <summary>
        /// Update parameters with URL key values
        /// Has the following names reserved: destinationList, 
        /// </summary>
        /// <returns>TRUE on success, FALSE otherwise</returns>
        private bool SetParametersFromURL()
        {
            bool valid_url = false;
            try
            {
                if (!this.WebPart.Load_Form_With_No_Parameters_In_URL) { 
                // string url = SPContext.Current.Web.Url.ToString();
                string requestString = "";
                string param_part_of_url = "";
                Encoding enc = Encoding.GetEncoding("utf-8");
                requestString = HttpContext.Current.Request.Url.ToString();
                debugLabel.Text += printInfoTitle("Loading URL parameters");
                //debugLabel.Text += printInfo("Request: " + requestString);
                string[] allKeys = null;
                try
                {
                    // param_part_of_url = Decrypt(HttpUtility.UrlDecode(requestString.Split('?')[1]));
                    param_part_of_url = requestString.Split('?')[1];
                    //if (param_part_of_url.Length == 0)
                    //{
                    //    return false; // no parametes were passed to the form
                    //}

                    debugLabel.Text += "<br/>Url param: " + HttpUtility.UrlDecode(param_part_of_url);
                    foreach (string url_param in param_part_of_url.Split('&'))
                    {
                        debugLabel.Text += "<br/>Url param: " + HttpUtility.UrlDecode(url_param);
                        if (url_param.Split('=')[0].ToLower().Equals("fp"))
                        {
                            debugLabel.Text += printInfo("Decripted part: " + Decrypt(HttpUtility.UrlDecode(url_param.Replace("fp=", ""))));
                            allKeys = Decrypt(HttpUtility.UrlDecode(url_param.Replace("fp=", ""))).Split('&');

                        }
                    }

                    // allKeys = param_part_of_url.Split('&');
                    //debugLabel.Text += "<br />Request param string:" + requestString.Split('?');
                }
                catch (Exception ex)
                {
                    debugLabel.Text += exceptionMessageBuilder("Decript URL", ex.Message);
                }

                foreach (string key in allKeys)
                {

                    string key_name = key.Split('=')[0].ToLower();

                    debugLabel.Text += printInfo("URL Key: " + key_name);
                    switch (key_name)
                    {
                        case URL_ITEM_ID:
                            unique_item_id = key.Split('=')[1];
                            debugLabel.Text += printInfo("Item ID: " + unique_item_id);
                            break;

                        case URL_UNIQUE_KEY:
                            unique_key_value = key.Split('=')[1];
                            debugLabel.Text += printInfo("unique key value: " + unique_key_value);
                            break;
                        case URL_DESTINATION_KEY:

                            url_destination = key.Split('=')[1];
                            debugLabel.Text += printInfo("destination value: " + url_destination);

                            break;
                        default:
                            Parameter p = new Parameter();
                            p.BindName = key_name;
                            p.Value = key.Split('=')[1];
                            URL_Parameters_List.Add(p);
                            debugLabel.Text += printInfo("Additional params: " + p.BindName + ": " + p.Value);
                            break;
                    }

                }
                if (unique_key_value.Length > 0 && unique_item_id.Length > 0)
                {
                    valid_url = true;
                }
                debugLabel.Text += "<br />---------------------------------";
            }
            }
            catch (Exception ex)
            {
                if (!this.WebPart.Load_Form_With_No_Parameters_In_URL)
                {
                    debugLabel.Text += exceptionMessageBuilder("Load URL parameters", ex.ToString());
                    return false;
                }
            }
            return valid_url;
        }

        /// <summary>
        /// Set parameters from SPList
        /// </summary>
        private void Set_Parameters_From_List()
        {
            try {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    string url = SPContext.Current.Web.Url.ToString();

                    using (SPSite site = new SPSite(destinationWeb))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList data_list = web.Lists[data_source_list];
                            SPListItem data_item = data_list.Items.GetItemById(Convert.ToInt32(unique_item_id));
                            foreach (Parameter paramObj in ParametersList)
                            {
                                switch(paramObj.BindName)
                                {
                                    case "USER":
                                        paramObj.Value = fullUserName.Replace("@", "").Replace("\\", "");
                                        break;
                                    default:
                                        paramObj.Value = HttpUtility.HtmlDecode(data_item.GetFormattedValue(paramObj.SourceName));
                                        break;
                                     
                                }
                                
                                debugLabel.Text +=printInfo("List Param: " + paramObj.BindName + " value: " + paramObj.Value);

                            }
                        }
                    }
                });
            }
            catch(Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Load SPList parameters", ex.ToString());
            }
        }


        private string Encrypt(string clearText)
        {

            string EncryptionKey = "3emtRR4323$Twe34";

            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

            using (Aes encryptor = Aes.Create())
            {

                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                encryptor.Key = pdb.GetBytes(32);

                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {

                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {

                        cs.Write(clearBytes, 0, clearBytes.Length);

                        cs.Close();

                    }

                    clearText = Convert.ToBase64String(ms.ToArray());

                }

            }

            return clearText;

        }

        private string Decrypt(string cipherText)
        {

            string EncryptionKey = "3emtRR4323$Twe34";

            cipherText = cipherText.Replace(" ", "+");

            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes encryptor = Aes.Create())
            {

                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                encryptor.Key = pdb.GetBytes(32);

                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {

                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {

                        cs.Write(cipherBytes, 0, cipherBytes.Length);

                        cs.Close();

                    }

                    cipherText = Encoding.Unicode.GetString(ms.ToArray());

                }

            }

            return cipherText;

        }



        /// <summary>
        /// Load list of parameters from XML
        /// </summary>
        private void LoadParameters()
        {

            try
            {
                //  ArrayList paramList = new ArrayList();
                debugLabel.Text += printInfoTitle("LOADING PARAMETERS");
                //debugLabel.Text += printInfo("Reserved parameter names: <br />'start_form': start date time. <br />'end_form': end date time.");
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(this.WebPart.filePath + "\\" + this.WebPart.fileName);
                debugLabel.Text += "<br>Got config file";
                int count = 1;
                Parameter paramObj;
                foreach (XmlNode parameter in xmlDoc.SelectNodes(PARAMETERS_PATH))
                {
                    paramObj = new Parameter();
                    debugLabel.Text += "<br />Param " + count + ":";
                    foreach (XmlNode param_attribute in parameter.ChildNodes)
                    {
                        switch (param_attribute.LocalName.ToLower())
                        {
                            case "name_source":
                                paramObj.SourceName = param_attribute.InnerText;
                                debugLabel.Text += "<br />source name: " + paramObj.SourceName;
                                break;
                            case "name_bind":
                                paramObj.BindName = param_attribute.InnerText;
                                debugLabel.Text += "<br />bind name: " + paramObj.BindName;
                                break;
                            case "type":
                                paramObj.Type = param_attribute.InnerText;
                                break;
                            case "source":
                                paramObj.Source = param_attribute.InnerText;
                                debugLabel.Text += "<br />source: " + paramObj.Source;
                                break;
                            case "nabehavior":
                                paramObj.NullBehavior = param_attribute.InnerText;
                                break;

                        }
                    }
                    if (paramObj.BindName.Trim().Length > 0)
                    {
                        this.ParametersList.Add(paramObj);
                    }
                    debugLabel.Text += "<br />----------------------------";
                    count++;

                }
                Set_Parameters_From_List();

                debugLabel.Text += "<br /><b> Done loading parameters. Num of parameters: " + ParametersList.Count + "</b>";
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("LoadParameters", ex.Message);
            }
            // return paramList;
        }

        /// <summary>
        /// Generate title for sub folder (to store the PDF)
        /// </summary>
        /// <param name="item">SPListItem</param>
        /// <returns>Sub folder title</returns>
        protected string createSubFolderTitle(SPListItem item)
        {
            string subFolderTitle = description;
            string[] temp = description.Split('-');

            foreach (string str in temp)
            {
                if (str.Contains("["))
                {
                    try
                    {

                        subFolderTitle = subFolderTitle.Replace(str, item.GetFormattedValue(HttpUtility.HtmlDecode(str.Replace("[", "").Replace("]", "").Trim())));
                    }
                    catch (Exception ex)
                    {
                        debugLabel.Text += exceptionMessageBuilder("Create Sub Folder", ex.Message);
                    }
                }

            }
            return subFolderTitle;
        }


        //Converts string to formula and evaluates it
        /// <summary>
        /// Converts string expression to formula format and calculates the result 
        /// </summary>
        /// <param name="controlId"></param>
        /// <param name="function">Fuction expression</param>
        /// <returns>Formula evaluation result</returns>
        protected double EvalFormula(string controlId, string function)
        {
            char[] operatorsList = { '+', '-', '*', '/', '(', ')' };

            string newFunction = (function.Length == 0) ? "0" : function;

            string[] funcVars = newFunction.Split(operatorsList);

            foreach (string funcVar in funcVars)
            {
                try
                {
                    TextBox txtName = FindControl(funcVar) as TextBox;
                    string temp = (txtName.Text.Length == 0) ? "0" : txtName.Text.Trim();
                    newFunction = newFunction.Replace(funcVar, temp);
                }
                catch (Exception ex)
                {
                    debugLabel.Text += exceptionMessageBuilder("Evaluating formula", ex.Message);
                }
            }
            Expression func = new Expression(newFunction);
            return Convert.ToDouble(func.Evaluate());

        }


        /// <summary>
        /// Creates SPList
        /// </summary>
        /// <param name="listName">List name</param>
        protected void createList(string listName)
        {
            
            ArrayList adminGroups = new ArrayList();
            //string url = SPContext.Current.Web.Url.ToString();

            bool listExist = false;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {

                    using (SPSite site = new SPSite(destinationWeb))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;

                            try
                            {
                                SPList oList = web.Lists[listName];
                                listExist = true;
                            }
                            catch (IndexOutOfRangeException indEx)
                            {
                                debugLabel.Text += exceptionMessageBuilder("List '" + listName + "' not found", indEx.Message);
                            }
                            catch
                            {
                                // other error thrown  
                            }

                            if (!listExist)
                            {

                                SPListCollection lists = web.Lists;
                                //create new list
                                lists.Add(listName, "For 'Dynamic Form' ", SPListTemplateType.GenericList);
                                //add default '-' item
                                SPList list = web.Lists[listName];
                                //hide from quickLaunch
                                list.OnQuickLaunch = false;
                                SPListItem newItem = list.Items.Add();
                                newItem["Title"] = "-";
                                newItem.Update();


                                SPRoleAssignmentCollection roles = list.RoleAssignments;
                                foreach (SPRoleAssignment role in roles)
                                {
                                    if (role.Member.ToString().ToLower().Contains("admin"))
                                    {
                                        adminGroups.Add(role.Member.ToString());

                                    }
                                }
                                //break role inheritance
                                list.BreakRoleInheritance(true);
                                List<string> principals = new List<string>(new string[] { "My Intranet Owners", "My Management Team", "System Account" }); ;


                                //remove all existing permissions
                                for (int i = list.RoleAssignments.Count - 1; i >= 0; i--)
                                {
                                    if (principals.IndexOf(list.RoleAssignments[i].Member.Name) == -1)
                                        list.RoleAssignments.Remove(i);
                                }

                                SPRoleDefinition contribute = web.RoleDefinitions["Contribute"];
                                SPUser adminAccount;
                                SPRoleAssignment adminRole;


                                foreach (string member in adminGroups)
                                {
                                    adminAccount = web.EnsureUser(member);
                                    adminRole = new SPRoleAssignment(adminAccount);
                                    adminRole.RoleDefinitionBindings.Add(contribute);
                                    list.RoleAssignments.Add(adminRole);

                                }
                                list.Update();
                            }

                        }
                    }
                });
                try
                {
                    ((TextBox)FindControl("signature_img")).Attributes.Add("src", ((TextBox)FindControl("Signature")).Text);
                }
                catch { }
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Create List '" + listName + "'", ex.Message);
            }
        }

        

        private void loadXML_Config(string fPath, string fName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fPath + "\\" + fName);
                debugLabel.Text += print_OK_Step("<b>Found XML file.</b>");
                foreach (XmlNode node in xmlDoc.SelectNodes(_configPath))
                {
                    foreach (XmlNode el in node.ChildNodes)
                    {
                        try
                        {

                            //missingDataMessage
                            if (el.LocalName.ToLower().CompareTo("missingdatamessage") == 0)
                            {
                                missingDataMessage = el.InnerText;
                            }
                            if (el.LocalName.CompareTo(messageTag) == 0)
                            {
                                finalMessage = el.InnerText;
                            }
                            if (el.LocalName.ToLower().Equals("doctypelist"))
                            {
                                docTypeList = el.InnerText;
                            }
                            if (el.LocalName.ToLower().Equals("doctypecolumn"))
                            {
                                docTypeColumn = el.InnerText;
                            }
                            if (el.LocalName.ToLower().Equals("doctype"))
                            {
                                docType = el.InnerText;
                            }
                            //
                            if (el.LocalName.ToLower().Equals("createpdf"))
                            {
                                createPDF = el.InnerText.ToLower().Equals("yes") ? true : false;
                            }

                            if (el.LocalName.ToLower().Equals("destination"))
                            {
                                foreach (XmlNode teg in el.ChildNodes)
                                {
                                    switch(teg.LocalName.ToLower())
                                    {
                                        case "library":
                                            destinationFolder = teg.InnerText;
                                            break;
                                        case "list":
                                            destinationList = teg.InnerText;
                                            break;
                                        case "web":
                                            destinationWeb = teg.InnerText;
                                            break;
                                        case "user_folder":
                                            is_user_folder = teg.InnerText.ToLower().Equals("yes") ? true : false;
                                            break;
                                       
                                      
                                        case "itemns_num":
                                            try
                                            {
                                                destination_items_num = Convert.ToInt32(teg.InnerText);
                                            }
                                            catch { destination_items_num = 1; }
                                            break;

                                    }
                                   
                                }
                            }
                         /*   if (el.LocalName.ToLower().Equals("destinationfolder"))
                            {
                                destinationFolder = el.InnerText;
                            }
                            if (el.LocalName.ToLower().Equals("destinationlist"))
                            {
                                destinationList = el.InnerText;
                            }*/

                            if (el.LocalName.ToLower().Equals("metasourcelist"))
                            {
                                meta_source_list = el.InnerText;
                            }

                            if (el.LocalName.ToLower().Equals("datasourcelist"))
                            {
                                data_source_list = el.InnerText;
                                debugLabel.Text += printInfo("Loaded data source list: " + data_source_list);
                            }
                            if (el.LocalName.ToLower().Equals("statuscolumn"))
                            {
                                status_column = el.InnerText;
                                debugLabel.Text += printInfo("Loaded status column: " + status_column);
                            }

                            if (el.LocalName.ToLower().Equals("pdfmailbodyhtml"))
                            {
                                pdfMailBodyHtml = el.InnerText;
                            }
                            if (el.LocalName.ToLower().Equals("pdfmailsubject"))
                            {
                                pdfMailSubject = el.InnerText;
                            }
                            if (el.LocalName.ToLower().Equals("pdfemailfield"))
                            {
                                pdfEmailField = el.InnerText;
                            }
                            if (el.LocalName.ToLower().Equals("pdfdownloadbuttontext"))
                            {
                                pdfDownloadButtonText = el.InnerText;
                            }


                            //
                            //ticketNum
                            if (el.LocalName.ToLower().Equals("ticketnum"))
                            {
                                ticketNum = el.InnerText;
                            }
                            if (el.LocalName.ToLower().Equals("description"))
                            {
                                description = el.InnerText;
                            }

                            //Create sub folder for pdf file
                            //used to store uploaded files
                            if (el.LocalName.ToLower().Equals("createsubfolder"))
                            {
                                createSubFolder = el.InnerText.ToLower().Equals("yes") ? true : false;
                            }

                            //redirectPage
                            if (el.LocalName.ToLower().Equals("redirectpage"))
                            {
                                redirectPage = el.InnerText;
                            }
                            if (el.LocalName.ToLower().Equals("logo"))
                            {
                                logoImage = el.InnerText;
                            }


                        }
                        catch (Exception ex)
                        {
                            debugLabel.Text += exceptionMessageBuilder("Load XML config", ex.Message);
                        }


                    }
                }
                debugLabel.Text += print_OK_Step("Done loading config part.");
                debugLabel.Text += printInfoTitle("XML Configurations");
                debugLabel.Text += printInfo("<b>Destination web:</b> " + destinationWeb);
                debugLabel.Text += printInfo("<b>Destiantion list:</b> " + destinationList);
                //debugLabel.Text += printInfo("<b>PDF destiantion folder:</b> " + destinationFolder);
                debugLabel.Text += printInfo("<b>Create sub folder:</b> " + createSubFolder.ToString());
                debugLabel.Text += printInfo("<b>Create PDF:</b> " + createPDF.ToString());
                debugLabel.Text += printInfo("<b>'Success' redirection page:</b> " + redirectPage);
                debugLabel.Text += printInfo("---------------------------------<br />");

                try
                {
                    foreach (XmlNode node in xmlDoc.SelectNodes(UNIQUE_KEY_PATH))
                    {
                        foreach (XmlNode el in node.ChildNodes)
                        {
                            if (el.LocalName.ToLower().Equals("list"))
                            {
                                unique_key_list = el.InnerText;
                            }
                            if (el.LocalName.ToLower().Equals("column"))
                            {
                                unique_key_column = el.InnerText;
                            }
                            if (el.LocalName.ToLower().Equals("response_list"))
                            {
                                unique_key_response_list = el.InnerText;
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    debugLabel.Text += exceptionMessageBuilder("Load unique key settings", ex.Message);
                }
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Load XML Config", ex.Message);
            }
        }

        /// <summary>
        /// Load Dynamic form from XML
        /// </summary>
        /// <param name="fPath">XML file path</param>
        /// <param name="fName">XML file name</param>
        /// <returns></returns>
        protected string loadXML(string fPath, string fName)
        {
            debugLabel.Text += print_OK_Step("Loading XML file");
            string str = "";

            string url = SPContext.Current.Web.Url.ToString();
            string userName = this.Context.User.Identity.Name.ToString();
            string temp = "";

            UpdatePanel formPanel = new UpdatePanel();
            formPanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(fPath + "\\" + fName);
                    debugLabel.Text += print_OK_Step("<b>Found XML file.</b>");

                    using (SPSite site = new SPSite(destinationWeb))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;

                            //foreach (SPListItem oListItem in collListItems)
                            {
                                Table t = new Table();
                                t.CssClass = tableStyle;
                                try
                                {
                                    foreach (XmlNode node in xmlDoc.SelectNodes(_configPath))
                                    {
                                        foreach (XmlNode el in node.ChildNodes)
                                        {
                                            try
                                            {
                                                //  if (userName.CompareTo(oListItem.GetFormattedValue("userName")) == 0)
                                                {
                                                    if (el.LocalName.CompareTo(_docHeader) == 0)
                                                    {
                                                        bool showHeader = true;
                                                        bool simpleHeader = true;
                                                        string docHeaderText = string.Empty;
                                                        TableRow tr1 = new TableRow();
                                                        tr1.Attributes.Add("direction", textDirection);
                                                        TableCell tcFormHeaderLabel1 = new TableCell();
                                                        Label DocHeaderLabel = new Label();

                                                        foreach (XmlNode cel in el.ChildNodes)
                                                        {
                                                            if (cel.LocalName.ToLower().Equals("visible"))
                                                            {
                                                                showHeader = (cel.InnerText.Equals("yes")) ? true : false;
                                                            }


                                                            if (cel.LocalName.Equals("text"))
                                                            {
                                                                docHeaderText = cel.InnerText;
                                                                simpleHeader = false;
                                                            }

                                                        }

                                                        if (showHeader)
                                                        {
                                                            DocHeaderLabel.Text = docHeaderText;
                                                            tcFormHeaderLabel1.Controls.Add(DocHeaderLabel);
                                                            tr1.Cells.Add(tcFormHeaderLabel1);

                                                            t.Rows.Add(tr1);
                                                            formPanel.ContentTemplateContainer.Controls.Add(t);
                                                            tr1 = new TableRow();
                                                            tr1.Attributes.Add("direction", textDirection);
                                                        }
                                                        DocHeaderLabel.CssClass = _docHeaderStyle;
                                                        if (simpleHeader)
                                                        {
                                                            DocHeaderLabel.Text = el.InnerText;
                                                            tcFormHeaderLabel1.Controls.Add(DocHeaderLabel);
                                                            tr1.Cells.Add(tcFormHeaderLabel1);
                                                            t.Rows.Add(tr1);


                                                            formPanel.ContentTemplateContainer.Controls.Add(t);
                                                        }



                                                        t = new Table();
                                                        t.CssClass = tableStyle;
                                                    }
                                                    //missingDataMessage
                                                /*    if (el.LocalName.ToLower().CompareTo("missingdatamessage") == 0)
                                                    {
                                                        missingDataMessage = el.InnerText;
                                                    }
                                                    if (el.LocalName.CompareTo(messageTag) == 0)
                                                    {
                                                        finalMessage = el.InnerText;
                                                    }
                                                    if (el.LocalName.ToLower().Equals("doctypelist"))
                                                    {
                                                        docTypeList = el.InnerText;
                                                    }
                                                    if (el.LocalName.ToLower().Equals("doctypecolumn"))
                                                    {
                                                        docTypeColumn = el.InnerText;
                                                    }
                                                    if (el.LocalName.ToLower().Equals("doctype"))
                                                    {
                                                        docType = el.InnerText;
                                                    }
                                                    //
                                                    if (el.LocalName.ToLower().Equals("createpdf"))
                                                    {
                                                        createPDF = el.InnerText.ToLower().Equals("yes") ? true : false;
                                                    }
                                                    if (el.LocalName.ToLower().Equals("destinationfolder"))
                                                    {
                                                        destinationFolder = el.InnerText;
                                                    }
                                                    if (el.LocalName.ToLower().Equals("destinationlist"))
                                                    {
                                                        destinationList = el.InnerText;
                                                    }

                                                    if (el.LocalName.ToLower().Equals("metasourcelist"))
                                                    {
                                                        meta_source_list = el.InnerText;
                                                    }

                                                    if (el.LocalName.ToLower().Equals("datasourcelist"))
                                                    {
                                                        data_source_list = el.InnerText;
                                                        debugLabel.Text += "Loaded data source list: " + data_source_list;
                                                    }

                                                    if (el.LocalName.ToLower().Equals("pdfmailbodyhtml"))
                                                    {
                                                        pdfMailBodyHtml = el.InnerText;
                                                    }
                                                    if (el.LocalName.ToLower().Equals("pdfmailsubject"))
                                                    {
                                                        pdfMailSubject = el.InnerText;
                                                    }
                                                    if (el.LocalName.ToLower().Equals("pdfemailfield"))
                                                    {
                                                        pdfEmailField = el.InnerText;
                                                    }
                                                    if (el.LocalName.ToLower().Equals("pdfdownloadbuttontext"))
                                                    {
                                                        pdfDownloadButtonText = el.InnerText;
                                                    }


                                                    //
                                                    //ticketNum
                                                    if (el.LocalName.ToLower().Equals("ticketnum"))
                                                    {
                                                        ticketNum = el.InnerText;
                                                    }
                                                    if (el.LocalName.ToLower().Equals("description"))
                                                    {
                                                        description = el.InnerText;
                                                    }

                                                    //Create sub folder for pdf file
                                                    //used to store uploaded files
                                                    if (el.LocalName.ToLower().Equals("createsubfolder"))
                                                    {
                                                        createSubFolder = el.InnerText.ToLower().Equals("yes") ? true : false;
                                                    }

                                                    //redirectPage
                                                    if (el.LocalName.ToLower().Equals("redirectpage"))
                                                    {
                                                        redirectPage = el.InnerText;
                                                    }
                                                    if (el.LocalName.ToLower().Equals("logo"))
                                                    {
                                                        logoImage = el.InnerText;
                                                    }*/

                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                debugLabel.Text += exceptionMessageBuilder("Load XML config", ex.Message);
                                            }


                                        }
                                    }
                                 /*   debugLabel.Text += print_OK_Step("Done loading config part.");
                                    debugLabel.Text += printInfoTitle("XML COnfigurations");
                                    debugLabel.Text += printInfo("<b>Destiantion list:</b> " + destinationList);
                                    debugLabel.Text += printInfo("<b>PDF destiantion folder:</b> " + destinationFolder);
                                    debugLabel.Text += printInfo("<b>Create sub folder:</b> " + createSubFolder.ToString());
                                    debugLabel.Text += printInfo("<b>Create PDF:</b> " + createPDF.ToString());
                                    debugLabel.Text += printInfo("<b>'Success' redirection page:</b> " + redirectPage);
                                    debugLabel.Text += printInfo("---------------------------------<br />");

                                    try
                                    {
                                        foreach (XmlNode node in xmlDoc.SelectNodes(UNIQUE_KEY_PATH))
                                        {
                                            foreach (XmlNode el in node.ChildNodes)
                                            {
                                                if (el.LocalName.ToLower().Equals("list"))
                                                {
                                                    unique_key_list = el.InnerText;
                                                }
                                                if (el.LocalName.ToLower().Equals("column"))
                                                {
                                                    unique_key_column = el.InnerText;
                                                }
                                                if (el.LocalName.ToLower().Equals("response_list"))
                                                {
                                                    unique_key_response_list = el.InnerText;
                                                }
                                                
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        debugLabel.Text += exceptionMessageBuilder("Load unique key settings", ex.Message);
                                    }*/

                                    SPList oList = web.Lists[destinationList];
                                    SPListItemCollection collListItems = oList.Items;

                                    //debugLabel.Text += "<br> Destination List: '" + destinationList + "'";
                                    foreach (XmlNode node in xmlDoc.SelectNodes(_dataPath))
                                    {



                                        TableRow tr = new TableRow();
                                        tr.Attributes.Add("direction", textDirection);
                                        bool existColumn = false;
                                        t = new Table();
                                        t.CssClass = tableStyle;
                                        tr = new TableRow();
                                        tr.Attributes.Add("direction", textDirection);
                                        //bool show = true;
                                        string headerText = string.Empty;

                                        foreach (XmlNode el in node.ChildNodes)
                                        {
                                            // if (userName.CompareTo(oListItem.GetFormattedValue("userName")) == 0)
                                            {

                                                try
                                                {
                                                    // Add save button
                                                    // DateTime deadline = Convert.ToDateTime(oListItem.GetFormattedValue("deadline"));


                                                    if (el.LocalName.CompareTo(buttonTag) == 0)
                                                    {
                                                        formPanel.ContentTemplateContainer.Controls.Add(t);
                                                        Controls.Add(formPanel);
                                                        formPanel = new UpdatePanel();
                                                        t = new Table();
                                                        t.CssClass = tableStyle;
                                                        // if (DateTime.Today.Date <= deadline)
                                                        {
                                                            Button saveButton = new Button();
                                                            saveButton.PostBackUrl = "";
                                                            // saveButton.OnClientClick = "javascript:SP.UI.ModalDialog.showWaitScreenWithNoClose('Please Wait', 'Please wait as this may take a few minutes...', 76, 330);SP.UI.ModalDialog.close(SP.UI.DialogResult.OK);";
                                                            saveButton.Attributes.Add("onclick", "sendClick('" + modalError.ClientID + "','Please wait');");

                                                            TableCell tcHeaderLabel = new TableCell();
                                                            saveButton.CssClass = buttonStyle;
                                                            saveButton.Text = el.InnerText;
                                                            saveButton.Click += new EventHandler(Button1_Click);
                                                            tcHeaderLabel.Controls.Add(saveButton);
                                                            tr.Cells.Add(tcHeaderLabel);
                                                            t.Rows.Add(tr);
                                                            Controls.Add(t);
                                                        }
                                                        tr = new TableRow();
                                                        tr.Attributes.Add("direction", textDirection);
                                                        t = new Table();
                                                        t.CssClass = tableStyle;


                                                    }
                                                    if (el.LocalName.CompareTo(_headerTag) == 0)
                                                    {
                                                        //1
                                                        formPanel.ContentTemplateContainer.Controls.Add(t);
                                                        t = new Table();
                                                        t.CssClass = tableStyle;

                                                        TableCell tcHeaderLabel = new TableCell();
                                                        Label HeaderLabel = new Label();
                                                        HeaderLabel.CssClass = headerStyle;

                                                        HeaderLabel.Text = el.InnerText;
                                                        tcHeaderLabel.Controls.Add(HeaderLabel);
                                                        tr.Cells.Add(tcHeaderLabel);

                                                        t.Rows.Add(tr);
                                                        formPanel.ContentTemplateContainer.Controls.Add(t);
                                                        tr = new TableRow();
                                                        tr.Attributes.Add("direction", textDirection);

                                                        t = new Table();
                                                        t.CssClass = tableStyle;


                                                    }

                                                    if (el.LocalName.ToLower().CompareTo(_labelTag) == 0)
                                                    {
                                                        string text = "";
                                                        bool _visibleLabel = true;
                                                        bool _printLabel = true;
                                                        //string width = "";
                                                        foreach (XmlNode cel in el.ChildNodes)
                                                        {
                                                            if (cel.LocalName.ToLower().CompareTo("text") == 0)
                                                            {
                                                                text = cel.InnerText;
                                                            }
                                                            if (cel.LocalName.ToLower().CompareTo("width") == 0)
                                                            {
                                                                cellWidth = cel.InnerText;
                                                            }
                                                            if (cel.LocalName.ToLower().CompareTo("visible") == 0)
                                                            {
                                                                _visibleLabel = cel.InnerText.ToLower().Equals("yes") ? true : false;
                                                            }
                                                            if (cel.LocalName.ToLower().CompareTo("print") == 0)
                                                            {
                                                                _printLabel = cel.InnerText.ToLower().Equals("yes") ? true : false;
                                                            }


                                                        }
                                                        if (_visibleLabel)
                                                        {
                                                            TableCell tcLabel = new TableCell();
                                                            Label label = new Label();
                                                            label.CssClass = labelStyle;
                                                            //tr.CssClass = labelStyle;
                                                            if (text.Trim().Length == 0)
                                                            {
                                                                label.Text = "<br>";
                                                            }
                                                            else
                                                            {
                                                                label.Text = text;
                                                            }
                                                            tcLabel.Attributes.Add("style", "width:" + cellWidth + "px;direction:" + textDirection + ";");
                                                            tcLabel.Controls.Add(label);
                                                            if (tcLabel.Controls.Count > 0)
                                                            {
                                                                tr.Cells.Add(tcLabel);
                                                                tcLabel = new TableCell();
                                                                tcLabel.Attributes.Add("style", "width:10px;direction:" + textDirection + ";");

                                                                tr.Cells.Add(tcLabel);
                                                            }
                                                        }

                                                    }

                                                    // 1 -------------------------------------------------------------------------------------------------------------------

                                                    string controlId = string.Empty;
                                                    string controlType = string.Empty;
                                                    string controlData = string.Empty;
                                                    string controlList = string.Empty;
                                                    string direction = string.Empty;
                                                    string clientData = string.Empty;

                                                    bool printControl = true;
                                                    bool required = false;
                                                    bool visible = true;
                                                    string fileName = string.Empty;
                                                    int colNum = 0;
                                                    regEx = string.Empty;
                                                    adData = string.Empty;
                                                    tooltip = string.Empty;
                                                    item_num = "0";
                                                    int maxrowsnum = 0;

                                                    string[] requiredFields = { "" };

                                                    string repeatTableId = "";

                                                    cellWidth = defaultWidth;
                                                    calcFormula = "";
                                                    //controlPath
                                                    foreach (XmlNode cel in el.ChildNodes)
                                                    {
                                                        //-------------------------------------------------------------------------------
                                                        // dataTag = 'list column name' and 'control' name on page
                                                        // typeTag = 'control type', i.e. Label, TextBox, CheckBox, DropDownList, 
                                                        // list - 'list name' data source for controls 'dropDownList' and 'radioButtonList'
                                                        if (cel.LocalName.ToLower().CompareTo(dataTag) == 0 || cel.LocalName.ToLower().CompareTo("formcontrol") == 0)
                                                        {
                                                            controlData = cel.InnerText;
                                                            controlId = cel.InnerText;
                                                        }
                                                        //required
                                                        if (cel.LocalName.ToLower().CompareTo("required") == 0)
                                                        {
                                                            required = (cel.InnerText.ToLower().CompareTo("yes") == 0) ? true : false;

                                                        }
                                                        if (cel.LocalName.ToLower().CompareTo("visible") == 0)
                                                        {
                                                            visible = (cel.InnerText.ToLower().CompareTo("yes") == 0) ? true : false;

                                                        }

                                                        if (cel.LocalName.ToLower().CompareTo(TypeTag) == 0)
                                                        {
                                                            controlType = cel.InnerText;
                                                        }
                                                        if (cel.LocalName.ToLower().CompareTo("list") == 0)
                                                        {
                                                            controlList = cel.InnerText;
                                                        }
                                                        if (cel.LocalName.ToLower().Equals("tooltip"))
                                                        {
                                                            tooltip = cel.InnerText;
                                                        }

                                                        if (cel.LocalName.ToLower().Equals("item"))
                                                        {
                                                            item_num = cel.InnerText;
                                                        }

                                                        if (cel.LocalName.ToLower().CompareTo("width") == 0)
                                                        {

                                                            cellWidth = cel.InnerText;
                                                            widthList = cel.InnerText.Split(';');

                                                        }

                                                        if (cel.LocalName.ToLower().CompareTo("formula") == 0)
                                                        {
                                                            calcFormula = cel.InnerText;
                                                        }
                                                        //-------------------------------------------------------------------------------

                                                        //-------------------------------------------------------------------------------
                                                        // Local PC and IP - Client data
                                                        if (cel.LocalName.ToLower().CompareTo("calientData") == 0)
                                                        {
                                                            try
                                                            {
                                                                if (cel.InnerText.ToLower().CompareTo("ip") == 0)
                                                                {
                                                                    //get client IP
                                                                    clientData = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                                                                    if (clientData == string.Empty)
                                                                    {
                                                                        clientData = Context.Request.ServerVariables["REMOTE_ADDR"];
                                                                    }
                                                                }
                                                                if (cel.InnerText.ToLower().CompareTo("mac") == 0)
                                                                {
                                                                    //get client IP
                                                                    clientData = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR "];
                                                                }
                                                                if (cel.InnerText.ToLower().CompareTo("os") == 0)
                                                                {
                                                                    //get client IP
                                                                    clientData = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR "];
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                debugLabel.Text += ex.Message;
                                                            }

                                                        }

                                                        if (cel.LocalName.ToLower().Equals("regex"))
                                                        {
                                                            regEx = cel.InnerText;
                                                        }
                                                        if (cel.LocalName.ToLower().Equals("addata"))
                                                        {
                                                            try
                                                            {
                                                                //employeeId

                                                                if (cel.InnerText.ToLower().CompareTo("employeeid") == 0)
                                                                {
                                                                    adData = employeeId;
                                                                }
                                                                if (cel.InnerText.ToLower().CompareTo("email") == 0)
                                                                {
                                                                    adData = email;
                                                                }

                                                                if (cel.InnerText.ToLower().CompareTo("name") == 0)
                                                                {
                                                                    adData = firstName;
                                                                }
                                                                if (cel.InnerText.ToLower().CompareTo("surname") == 0)
                                                                {
                                                                    adData = lastName;
                                                                }
                                                                if (cel.InnerText.ToLower().CompareTo("namehe") == 0)
                                                                {
                                                                    adData = firstNameHe;
                                                                }
                                                                if (cel.InnerText.ToLower().CompareTo("surnamehe") == 0)
                                                                {
                                                                    adData = lastNameHe;
                                                                }
                                                                if (cel.InnerText.ToLower().CompareTo("hphone") == 0)
                                                                {
                                                                    adData = homePhone;
                                                                }
                                                                if (cel.InnerText.ToLower().CompareTo("wphone") == 0)
                                                                {
                                                                    adData = workPhone;

                                                                }
                                                                if (cel.InnerText.ToLower().CompareTo("mphone") == 0)
                                                                {
                                                                    adData = mobilePhone;
                                                                }
                                                                //
                                                                if (cel.InnerText.ToLower().CompareTo("description") == 0)
                                                                {
                                                                    adData = ad_description;
                                                                }

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                debugLabel.Text += ex.Message;
                                                            }

                                                        }
                                                        //-------------------------------------------------------------------------------

                                                        //-------------------------------------------------------------------------------
                                                        // Repeating table properties

                                                        //get column
                                                        if (cel.LocalName.ToLower().CompareTo("colnum") == 0)
                                                        {
                                                            try
                                                            {
                                                                colNum = Convert.ToInt32(cel.InnerText);
                                                            }
                                                            catch
                                                            {
                                                                colNum = 0;
                                                            }
                                                        }
                                                        if (cel.LocalName.ToLower().CompareTo("rownum") == 0)
                                                        {
                                                            try
                                                            {
                                                                rowNum = Convert.ToInt32(cel.InnerText);
                                                            }
                                                            catch
                                                            {
                                                                rowNum = 0;
                                                            }
                                                        }


                                                        if (cel.LocalName.ToLower().CompareTo("coltitle") == 0)
                                                        {
                                                            colTitle = cel.InnerText.Split(';');
                                                        }
                                                        if (cel.LocalName.ToLower().CompareTo("coldata") == 0)
                                                        {
                                                            colData = cel.InnerText.Split(';');
                                                        }
                                                        if (cel.LocalName.ToLower().CompareTo("tableid") == 0)
                                                        {
                                                            repeatTableId = cel.InnerText;
                                                        }
                                                        if (cel.LocalName.ToLower().CompareTo("controltypes") == 0)
                                                        {
                                                            controlTypes = cel.InnerText.Split(';');
                                                        }
                                                        if (cel.LocalName.ToLower().CompareTo("maxrowsnum") == 0)
                                                        {
                                                            maxrowsnum = Convert.ToInt32(cel.InnerText);
                                                        }
                                                        //requiredFields
                                                        //required
                                                        if (cel.LocalName.ToLower().CompareTo("required") == 0)
                                                        {
                                                            requiredFields = cel.InnerText.Split(';');
                                                        }
                                                        //addRowText
                                                        if (cel.LocalName.ToLower().CompareTo("addrowtext") == 0)
                                                        {
                                                            addRowText = cel.InnerText;
                                                        }

                                                        //fileName for pdf file 
                                                        if (cel.LocalName.ToLower().CompareTo("filename") == 0)
                                                        {
                                                            fileName = cel.InnerText;
                                                        }

                                                        //file name for uploaded document
                                                        if (cel.LocalName.ToLower().CompareTo("rename") == 0)
                                                        {
                                                            renameFileName = cel.InnerText;
                                                        }
                                                        if (cel.LocalName.ToLower().CompareTo("maxsize") == 0)
                                                        {
                                                            if (cel.InnerText.Length > 0)
                                                            {
                                                                maxFileSize = cel.InnerText;
                                                            }
                                                        }

                                                        if (cel.LocalName.ToLower().Equals("autopostback"))
                                                        {
                                                            autoPostBack = cel.InnerText.ToLower().Equals("yes") ? true : false;
                                                        }
                                                        //-------------------------------------------------------------------------------
                                                        //Dependent drop down list properties
                                                        //--- dependent dropDownList filter properties.
                                                        //list column - used for dropDownList binding data
                                                        if (cel.LocalName.ToLower().Equals("listcolumn"))
                                                        {
                                                            listColumn = cel.InnerText;
                                                        }

                                                        //control (list item - field value) used as the filter for dependent dropDownList 
                                                        if (cel.LocalName.ToLower().Equals("filter"))
                                                        {
                                                            filter = cel.InnerText;
                                                        }

                                                        if (cel.LocalName.ToLower().Equals("filtercolumn"))
                                                        {
                                                            filterColumn = cel.InnerText;
                                                        }
                                                        //
                                                        if (cel.LocalName.ToLower().Equals("lookupcolumn"))
                                                        {
                                                            lookupColumn = cel.InnerText;
                                                        }
                                                        if (cel.LocalName.ToLower().Equals("displaycolumn"))
                                                        {
                                                            displayColumn = cel.InnerText;
                                                        }
                                                        if (cel.LocalName.ToLower().Equals("ddlorder"))
                                                        {
                                                            ddlOrder = (int)Convert.ToInt32(cel.InnerText);
                                                        }
                                                        if (cel.LocalName.ToLower().Equals("ddlgroup"))
                                                        {
                                                            ddlGroup = cel.InnerText;
                                                        }

                                                        //parentControl
                                                        if (cel.LocalName.ToLower().Equals("parentcontrol"))
                                                        {
                                                            parentControl = cel.InnerText;
                                                        }
                                                        //template
                                                        if (cel.LocalName.ToLower().Equals("template"))
                                                        {
                                                            addButtonTemplate = cel.InnerText;
                                                        }
                                                        if (cel.LocalName.ToLower().Equals("periodcontrol"))
                                                        {
                                                            addButtonPeriodControl = cel.InnerText;
                                                        }

                                                        //last DDL
                                                        if (cel.LocalName.ToLower().Equals("lastddl"))
                                                        {
                                                            lastDdl = (cel.InnerText.ToLower().Equals("yes")) ? true : false;
                                                        }

                                                        if (cel.LocalName.ToLower().Equals("sourceurlkey"))
                                                        {
                                                            sourceurlkey = cel.InnerText;
                                                        }
                                                        //-------------------------------------------------------------------------------
                                                        //-------------------------------------------------------------------------------
                                                        // Radio button list direction - horizontal/vertical
                                                        if (cel.LocalName.ToLower().CompareTo("direction") == 0)
                                                        {
                                                            direction = cel.InnerText;
                                                        }
                                                        //-------------------------------------------------------------------------------
                                                        if (cel.LocalName.ToLower().CompareTo("print") == 0)
                                                        {
                                                            printControl = (cel.InnerText.ToLower().Equals("yes")) ? true : false;
                                                        }

                                                    }

                                                    TableCell tcData = new TableCell();
                                                    if (controlType.ToLower().CompareTo("table") == 0)
                                                    {
                                                        tcData.Attributes.Add("style", "padding:4px;direction:" + textDirection + ";");
                                                    }
                                                    else
                                                    {
                                                        tcData.Attributes.Add("style", "width:" + cellWidth + "px;padding:4px;direction:" + textDirection + ";");
                                                    }


                                                    //-------------------------------------------------------------------------------
                                                    // Control: DatePicker
                                                    //-------------------------------------------------------------------------------
                                                    if (controlType.ToLower().CompareTo("datepicker") == 0 && visible)
                                                    {
                                                        if (addColumns)
                                                        {

                                                            for (int a = 0; a < oList.Fields.Count; a++)
                                                            {
                                                                if (oList.Fields[a].Title.CompareTo(controlId) == 0)
                                                                    existColumn = true;
                                                            }
                                                            if (!existColumn)
                                                            {
                                                                oList.Fields.Add(controlId, SPFieldType.DateTime, false);
                                                                oList.Update();
                                                                SPFieldDateTime dt = (SPFieldDateTime)oList.Fields[controlId];
                                                                dt.DisplayFormat = SPDateTimeFieldFormatType.DateOnly;
                                                                dt.Update();
                                                                oList.Update();

                                                            }

                                                            existColumn = false;
                                                        }
                                                        System.DateTime tempDate = DateTime.Today.Date;
                                                        try
                                                        {
                                                            //if (userName.CompareTo(oListItem.GetFormattedValue("userName")) == 0)
                                                            //  {
                                                            //       tempDate = (System.DateTime)oListItem[controlId];
                                                            //   }
                                                        }
                                                        catch { }
                                                        tcData.Controls.Add(addDatePicker(controlId, datePickerStyle, tempDate, printControl, required));
                                                    }
                                                    //-------------------------------------------------------------------------------
                                                    // Control: calculated value
                                                    //-------------------------------------------------------------------------------
                                                    if (controlType.ToLower().CompareTo("calculatedvalue") == 0 && visible)
                                                    {

                                                        try
                                                        {

                                                            tcData.Attributes.Add("style", "width:" + cellWidth + "px;direction:" + textDirection + ";");
                                                            tcData.Controls.Add(addFormula(controlId, dataStyle, temp, printControl, required, calcFormula,Convert.ToInt32(item_num)));
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            debugLabel.Text += "<br>add formula control: " + ex.Message;
                                                        }
                                                    }

                                                    //-------------------------------------------------------------------------------
                                                    // Control: Attachment
                                                    //-------------------------------------------------------------------------------
                                                    if (controlType.ToLower().CompareTo("attachment") == 0 && visible)
                                                    {
                                                        try
                                                        {
                                                            //bool existing = false;
                                                            string uploadPath = string.Empty;



                                                            Table tb = new Table();
                                                            tb.CssClass = tableStyle;
                                                            TableRow tbr = new TableRow();
                                                            TableCell tbc = new TableCell();
                                                            FileUpload upload = new FileUpload();

                                                            upload.CssClass = "DynamicButton";
                                                            Button uploadFileToSession = new Button();


                                                            uploadFileToSession.ID = controlId + "Attach";
                                                            uploadFileToSession.CssClass = "DynamicButton";
                                                            uploadFileToSession.Text = "Upload";
                                                            uploadFileToSession.Click += new EventHandler(this.uploadToSession);

                                                            uploadFileToSession.Attributes.Add("controlId", controlId);
                                                            uploadFileToSession.Attributes.Add("fileName", renameFileName);
                                                            uploadFileToSession.Attributes.Add("fileSize", maxFileSize.ToString());
                                                            System.Web.UI.WebControls.Image attachIcon = new System.Web.UI.WebControls.Image();
                                                            attachIcon.ImageUrl = attachIconPath;


                                                            Label attachmentIcon = new Label();
                                                            attachmentIcon.Text = "<img src=\"" + attachIconPath + "\"></img>";

                                                            Label filePathdisplay = new Label();
                                                            filePathdisplay.ID = controlId + "Label";
                                                            filePathdisplay.CssClass = MessageLabelStyle;
                                                            if (!upload.HasFile)
                                                            {
                                                                if (direction.Equals("ltr"))
                                                                {
                                                                    filePathdisplay.Text = "No file selected";
                                                                }
                                                                else
                                                                {
                                                                    filePathdisplay.Text = "לא נבחר קובץ";
                                                                }

                                                            }
                                                            else
                                                            {

                                                                filePathdisplay.Text = string.Empty;
                                                            }

                                                            Label uploadError = new Label();
                                                            uploadError.ID = controlId + "Error";
                                                            uploadError.CssClass = "DynamicMessageLabelError";
                                                            upload.EnableViewState = true;
                                                            if (required)
                                                            {
                                                                cObj = new clientObject(controlId, "onclick", "validateRequiredAttachment('controlId',ViewState[" + controlId + "fileBytes]'," + dataStyle + "','" + dataStyle + "Red');");
                                                                upload.CssClass = dataStyle + "Red";
                                                                cObj.required = required;
                                                                clientControls.Add(cObj);
                                                            }
                                                            else
                                                            {
                                                                upload.CssClass = dataStyle;
                                                            }
                                                            upload.ID = controlId;


                                                            DynamicControl uploadControl = new DynamicControl();
                                                            uploadControl.ControlRequired = required;
                                                            uploadControl.ID = controlId;
                                                            dynamicAttachmentIds.Add(uploadControl);
                                                            tbc.Controls.Add(upload);
                                                            tbr.Cells.Add(tbc);
                                                            tb.Rows.Add(tbr);
                                                            tbr = new TableRow();
                                                            tbc = new TableCell();
                                                            tbc.Controls.Add(uploadFileToSession);
                                                            tbr.Cells.Add(tbc);
                                                            tb.Rows.Add(tbr);
                                                            tbr = new TableRow();
                                                            tbc = new TableCell();
                                                            tbc.Controls.Add(uploadError);
                                                            tbr.Cells.Add(tbc);
                                                            tb.Rows.Add(tbr);
                                                            Table upTabel = new Table();
                                                            upTabel.CssClass = tableStyle;
                                                            TableRow upTbr = new TableRow();
                                                            TableCell upTbc = new TableCell();
                                                            upTbc.Attributes.Add("style", "direction:" + textDirection + ";");
                                                            upTbc.Controls.Add(attachIcon);
                                                            upTbc.Controls.Add(filePathdisplay);
                                                            upTbr.Cells.Add(upTbc);
                                                            upTabel.Rows.Add(upTbr);
                                                            formPanel.ContentTemplateContainer.Controls.Add(upTabel);
                                                            if (formPanel.ContentTemplateContainer.Controls.Count > 0)
                                                            {
                                                                Controls.Add(formPanel);
                                                            }
                                                            formPanel = new UpdatePanel();
                                                            Controls.Add(tb);





                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            debugLabel.Text += "<br>attachment :<br>" + ex.Message;
                                                        }
                                                    }

                                                    //---------------------------------------------------------------------------------
                                                    // Control: DependentDropDownList
                                                    if (controlType.ToLower().CompareTo("dependentdropdownlist") == 0 && visible)
                                                    {
                                                        string tempFilter = string.Empty;


                                                        tcData.Attributes.Add("style", "width:" + cellWidth + "px;direction:" + textDirection + ";");
                                                        tcData.Controls.Add(addDependentDropDownList(controlId, dropDownListStyle, controlList, printControl, required, false, 0, controlList, true, "", filter, filterColumn, displayColumn, tempFilter, autoPostBack, ddlGroup, ddlOrder, Convert.ToInt32(item_num)));
                                                    }

                                                    //---------------------------------------------------------------------------------

                                                    //-------------------------------------------------------------------------------
                                                    // Control: TextBox
                                                    //-------------------------------------------------------------------------------

                                                    if (controlType.ToLower().CompareTo("download") == 0 && visible)
                                                    {
                                                        try
                                                        {
                                                            SPFile file = null;
                                                            string sourceURL = "";
                                                            SPListItemCollection docTypesCollection = null;
                                                            foreach (Parameter param in ParametersList)
                                                            {

                                                                if (param.BindName.ToLower().Equals("sourceurlkey"))
                                                                {

                                                                    SPList doc_types = web.Lists[docTypeList];
                                                                    foreach (SPList list in web.Lists)
                                                                    {
                                                                        debugLabel.Text += "<br />List: " + list.Title + " | " + list.DefaultDisplayFormUrl.Split('/')[0];

                                                                    }
                                                                    SPDocumentLibrary source_lib = web.Lists[param.Value] as SPDocumentLibrary;
                                                                    debugLabel.Text += print_OK_Step("found source parameter");
                                                                    debugLabel.Text += print_OK_Step("Folder items count: " + source_lib.ItemCount);
                                                                    //Get all items marked with as 'filter' from DocType list
                                                                    docTypesCollection = doc_types.GetItems(new SPQuery()
                                                                    {
                                                                        Query = @"<Where>
                                                                              <Eq>
                                                                              <FieldRef Name='" + filterColumn + "' />"
                                                                                   + "<Value Type='Text'>" + filter + "</Value>"
                                                                                + " </Eq> "
                                                                             + " </Where>"
                                                                    });
                                                                    debugLabel.Text += print_OK_Step("loaded doc types");
                                                                    string docType = ((SPListItem)docTypesCollection[0]).GetFormattedValue("Title").ToString();

                                                                    debugLabel.Text += print_OK_Step("Checking files");
                                                                    debugLabel.Text += print_OK_Step("lookup column: " + lookupColumn);
                                                                    debugLabel.Text += print_OK_Step("docType: " + docType);
                                                                    foreach (SPListItem item in source_lib.Items)
                                                                    {
                                                                        debugLabel.Text += print_OK_Step("browsing items");

                                                                        SPFieldLookupValue lucolumn = new SPFieldLookupValue(item[lookupColumn].ToString());
                                                                        if (lucolumn.LookupValue.Equals(docType))
                                                                        {
                                                                            debugLabel.Text += print_OK_Step("found file to download");
                                                                            file = item.File;
                                                                            sourceURL = web.Url + "//";
                                                                            break;
                                                                        }
                                                                    }

                                                                }
                                                            }
                                                            //UpdatePanel up = new UpdatePanel();
                                                            //  up.ID = controlId + "_Panel";
                                                            tcData = new TableCell();

                                                            Button download_file = new Button();
                                                            try
                                                            {
                                                                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                                                                scriptManager.RegisterPostBackControl(download_file);
                                                            }
                                                            catch { }

                                                            byte[] data = file.OpenBinary();
                                                            debugLabel.Text += print_OK_Step("file size (bytes): " + data.Length);
                                                            // download_file.PostBackUrl = "";
                                                            download_file.Text = "download";
                                                            download_file.CausesValidation = false;
                                                            download_file.ID = controlId + "_button";
                                                            download_file.Click += new EventHandler(Download_file_Button_Click);
                                                            download_file.Visible = true;
                                                            download_file.Attributes.Add("file_bytes", Convert.ToBase64String(data));
                                                            download_file.Attributes.Add("file_name", file.Name);
                                                            download_file.Attributes.Add("file_url", sourceURL + file.Url.Split('/')[0]);
                                                            // download_file.Attributes.Add("runat", "server");
                                                            //download_file.Attributes.Add("OnClick", "Download_files('" + controlId + "'," + data + ",'" + file.Name + "');");
                                                            // up.ContentTemplateContainer.Controls.Add(download_file);
                                                            // tcData.Controls.Add(up);
                                                            tcData.Controls.Add(download_file);

                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            debugLabel.Text += exceptionMessageBuilder("Add download control", ex.Message);
                                                        }
                                                    }

                                                    //-------------------------------------------------------------------------------
                                                    // Control: TextBox
                                                    //-------------------------------------------------------------------------------

                                                    if (controlType.ToLower().CompareTo("textbox") == 0 && visible)
                                                    {

                                                        tcData.Attributes.Add("style", "width:" + cellWidth + "px;direction:" + textDirection + ";");
                                                        tcData.Controls.Add(addTextBox(controlId, dataStyle, "", printControl, required, regEx, Convert.ToInt32(item_num)));
                                                        tcData.CssClass = "Dynamictooltip";
                                                        HtmlGenericControl _span = new HtmlGenericControl("span");
                                                        _span.Attributes["class"] = "Dynamictooltiptext";
                                                        _span.InnerHtml = tooltip;

                                                        if (tooltip.Length > 0)
                                                        {
                                                            tcData.Controls.Add(_span);
                                                        }
                                                    }

                                                    //-------------------------------------------------------------------------------
                                                    // Control: Parameter (parameter label)
                                                    //-------------------------------------------------------------------------------

                                                    if (controlType.ToLower().CompareTo("parameter") == 0 && visible)
                                                    {
                                                        temp = "";
                                                        foreach (Parameter param in ParametersList)
                                                        {
                                                            if (controlId.ToLower().Equals(param.BindName.ToLower()))
                                                            {
                                                                temp = param.Value;
                                                                break;
                                                            }
                                                        }
                                                        tcData.Attributes.Add("style", "width:" + cellWidth + "px;direction:" + textDirection + ";");
                                                        tcData.Controls.Add(addTextLabel(controlId, TextLabelStyle, printControl, temp, Convert.ToInt32(item_num)));
                                                        // tcData.Controls.Add(addTextBox(controlId, dataStyle, "", printControl, required, regEx))
                                                        tcData.CssClass = "Dynamictooltip";
                                                        HtmlGenericControl _span = new HtmlGenericControl("span");
                                                        _span.Attributes["class"] = "Dynamictooltiptext";
                                                        _span.InnerHtml = tooltip;

                                                        if (tooltip.Length > 0)
                                                        {
                                                            tcData.Controls.Add(_span);
                                                        }
                                                    }

                                                    if (controlType.ToLower().CompareTo("url_parameter") == 0 && visible)
                                                    {
                                                        temp = "";
                                                        foreach (Parameter param in URL_Parameters_List)
                                                        {
                                                            if (controlId.ToLower().Equals(param.BindName.ToLower()))
                                                            {
                                                                temp = param.Value;
                                                                break;
                                                            }
                                                        }
                                                        tcData.Attributes.Add("style", "width:" + cellWidth + "px;direction:" + textDirection + ";");
                                                        tcData.Controls.Add(addTextLabel(controlId, TextLabelStyle, printControl, temp, Convert.ToInt32(item_num)));
                                                        // tcData.Controls.Add(addTextBox(controlId, dataStyle, "", printControl, required, regEx))
                                                        tcData.CssClass = "Dynamictooltip";
                                                        HtmlGenericControl _span = new HtmlGenericControl("span");
                                                        _span.Attributes["class"] = "Dynamictooltiptext";
                                                        _span.InnerHtml = tooltip;

                                                        if (tooltip.Length > 0)
                                                        {
                                                            tcData.Controls.Add(_span);
                                                        }
                                                    }
                                                    //-------------------------------------------------------------------------------
                                                    // Control: Captcha texp box (depricated)
                                                    //-------------------------------------------------------------------------------
                                                    /*    if (controlType.ToLower().CompareTo("captchatextbox") == 0 && visible)
                                                        {

                                                            tcData.Attributes.Add("style", "width:" + cellWidth + "px;direction:" + textDirection + ";");
                                                            tcData.Controls.Add(addTextBox("CaptchaTextBox", dataStyle, "", printControl, required, ""));
                                                        }*/

                                                    //-------------------------------------------------------------------------------
                                                    // Control: MultiLineTextBox
                                                    //-------------------------------------------------------------------------------
                                                    if (controlType.ToLower().CompareTo("multilinetextbox") == 0 && visible)
                                                    {

                                                        tcData.Attributes.Add("style", "width:" + cellWidth + "px;direction:" + textDirection + ";");
                                                        tcData.Controls.Add(addMultiLineTextBox(controlId, multiLineDataStyle, "", printControl, required, regEx, Convert.ToInt32(item_num)));
                                                    }

                                                    //Signature
                                                    if (controlType.ToLower().CompareTo("signature") == 0 && visible)
                                                    {
                                                        debugLabel.Text += printInfo("Adding signature");
                                                        tcData.Attributes.Add("style", "width:" + cellWidth + "px;direction:" + textDirection + ";");
                                                        tcData.Controls.Add(addSignature(controlId, multiLineDataStyle, "", printControl, required, regEx, Convert.ToInt32(item_num)));
                                                       
                                                    }

                                                    //-------------------------------------------------------------------------------


                                                    //-------------------------------------------------------------------------------
                                                    // Control: Final message
                                                    //-------------------------------------------------------------------------------
                                                    if (controlType.ToLower().CompareTo("finalmessage") == 0 && visible)
                                                    {
                                                        tcData.Controls.Add(addFinalMessage(controlId, MessageLabelStyle, finalMessage));
                                                    }

                                                    /*if (controlType.ToLower().CompareTo("captchamessage") == 0 && visible)
                                                    {
                                                        tcData.Controls.Add(addCaptchaMessage(controlId, labelStyle, finalMessage));
                                                    }
                                                    */
                                                    /* (depricated)
                                                     * if (controlType.ToLower().Equals("captchaimage")) 
                                                      {
                                                          try
                                                          {

                                                              //captchaImg
                                                              System.Web.UI.WebControls.Image captchaImg = new System.Web.UI.WebControls.Image();
                                                              tcData.Controls.Add(captchaImg);
                                                              // if (!Page.IsPostBack)
                                                              {
                                                                  try
                                                                  {

                                                                      captchaImg.Attributes["src"] = "/_layouts/Huji/Handler/Handler.ashx?act=captcha&" + DateTime.Now.Ticks;

                                                                  }
                                                                  catch
                                                                  {

                                                                  }
                                                              }




                                                          }
                                                          catch (Exception ex)
                                                          {
                                                              debugLabel.Text += "<br>captcha :<br>" + ex.ToString();
                                                          }
                                                      }*/
                                                    if (controlType.ToLower().Equals("captchaimage"))
                                                    {
                                                        try
                                                        {
                                                            tcData = new TableCell();
                                                            System.Web.UI.HtmlControls.HtmlGenericControl reCaptchaDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                                                            reCaptchaDiv.Attributes.Add("class", "g-recaptcha");
                                                            reCaptchaDiv.Attributes.Add("data-sitekey", ConfigurationManager.AppSettings["SiteKey"]);

                                                            tcData.Controls.Add(reCaptchaDiv);
                                                            added_captcha = true;

                                                            debugLabel.Text += print_OK_Step("Added recaptcha");
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            debugLabel.Text += "<br>captcha :<br>" + ex.ToString();
                                                        }
                                                    }



                                                    //CaptchaTextBox

                                                    //-------------------------------------------------------------------------------
                                                    // Control: TextLabel
                                                    //-------------------------------------------------------------------------------
                                                    if (controlType.ToLower().CompareTo("textlabel") == 0 && visible)
                                                    {

                                                        temp = "";


                                                        try
                                                        {
                                                            if (adData.Length > 0)
                                                            {
                                                                //add create
                                                                temp = adData;
                                                                if (addColumns)
                                                                {
                                                                    addColumn(controlId, SPFieldType.Text);
                                                                }
                                                                DynamicControl dl = new DynamicControl();
                                                                dl.ID = controlId;
                                                                dl.DataSource = controlId;
                                                                dl.Data = adData;

                                                                dl.Printable = print.ToLower().Equals("no") ? false : true;
                                                                dynamicTextLabelIds.Add(dl);

                                                            }

                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            debugLabel.Text += exceptionMessageBuilder("<b>Add TextLabel </b>" + controlId, ex.Message);
                                                        }


                                                        tcData.Attributes.Add("style", "width:" + cellWidth + "px;direction:" + textDirection + ";");
                                                        tcData.Controls.Add(addTextLabel(controlId, TextLabelStyle, printControl, temp, Convert.ToInt32(item_num)));
                                                    }



                                                    //-------------------------------------------------------------------------------
                                                    // Control: CheckBox
                                                    //-------------------------------------------------------------------------------
                                                    if (controlType.ToLower().CompareTo("checkbox") == 0 && visible)
                                                    {

                                                        HtmlGenericControl _div = new HtmlGenericControl("div");
                                                        _div.Controls.Add(addCheckBox(controlId, checkBoxStyle, false, printControl, required, Convert.ToInt32(item_num)));
                                                        _div.ID = controlId + "_div";
                                                        _div.Attributes.Add("runat", "server");
                                                        _div.Attributes.Add("class", checkBoxStyle);
                                                        tcData.Attributes.Add("style", "width:" + cellWidth + "px;direction:" + textDirection + ";");
                                                        tcData.Controls.Add(_div);
                                                    }

                                                    //-------------------------------------------------------------------------------
                                                    // Control: RadioButtonList
                                                    //-------------------------------------------------------------------------------
                                                    if (controlType.ToLower().CompareTo("radiobuttonlist") == 0 && visible)
                                                    {

                                                        tcData.Attributes.Add("style", "width:" + cellWidth + "px;direction:" + textDirection + ";");
                                                        tcData.Controls.Add(addRadioButtonList(controlId, RadioButtonListStyle, controlList, printControl, temp, direction, required, Convert.ToInt32(item_num)));
                                                    }

                                                    //-------------------------------------------------------------------------------
                                                    // Control: DropDownList
                                                    //-------------------------------------------------------------------------------
                                                    if (controlType.ToLower().CompareTo("dropdownlist") == 0 && visible)
                                                    {
                                                        /* try
                                                         {
                                                             if (userName.CompareTo(oListItem.GetFormattedValue("userName")) == 0)
                                                             {
                                                                 temp = oListItem.GetFormattedValue(controlId).ToString();
                                                             }
                                                         }
                                                         catch { }*/
                                                        tcData.Attributes.Add("style", "width:" + cellWidth + "px;direction:" + textDirection + ";");
                                                        tcData.Controls.Add(addDropDownList(controlId, dropDownListStyle, controlList, printControl, temp, required, Convert.ToInt32(item_num)));
                                                    }

                                                    //-------------------------------------------------------------------------------
                                                    // Control: Table
                                                    //-------------------------------------------------------------------------------
                                                    if (controlType.ToLower().CompareTo("table") == 0 && visible)
                                                    {
                                                        int currentRowsNum = 0;
                                                        int userRowNum = 0;
                                                        string[] RowsArray = { "" };
                                                        try
                                                        {

                                                            if (addColumns)
                                                            {
                                                                addColumn(repeatTableId, rowNum);

                                                                addColumn(repeatTableId + "Rows", SPFieldType.Text);
                                                            }

                                                            try
                                                            {

                                                                foreach (string item in RowsArray)
                                                                {
                                                                    if (item.Trim().Length > 0)
                                                                    {
                                                                        userRowNum++;
                                                                    }

                                                                }
                                                                if (userRowNum > currentRowsNum)
                                                                {
                                                                    currentRowsNum = userRowNum;
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                debugLabel.Text += "<br>convert error: " + ex.Message;
                                                            }

                                                            string[,] userData = new string[currentRowsNum, colNum];
                                                            int tempInt = 0;
                                                            //loads user data
                                                            for (int row = 0; row < currentRowsNum; row++)
                                                            {
                                                                for (int col = 0; col < colNum; col++)
                                                                {
                                                                    tempInt = row + 1;
                                                                    try
                                                                    {
                                                                        if (addColumns)
                                                                        {
                                                                            if (controlTypes[col].ToLower().CompareTo("textbox") == 0)
                                                                            {
                                                                                addColumn(colData[col] + tempInt, SPFieldType.Text);
                                                                            }
                                                                            if (controlTypes[col].ToLower().CompareTo("multilinetextbox") == 0)
                                                                            {
                                                                                addColumn(colData[col] + tempInt, SPFieldType.Note);
                                                                            }
                                                                            if (controlTypes[col].ToLower().Contains("dropdownlist"))
                                                                            {
                                                                                addColumn(colData[col] + tempInt, SPFieldType.Text);
                                                                            }
                                                                            if (controlTypes[col].ToLower().CompareTo("date") == 0)
                                                                            {
                                                                                addColumn(colData[col] + tempInt, SPFieldType.DateTime);
                                                                            }
                                                                            if (controlTypes[col].ToLower().CompareTo("checkBox") == 0)
                                                                            {
                                                                                addColumn(colData[col] + tempInt, SPFieldType.Boolean);
                                                                            }

                                                                        }

                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        debugLabel.Text += "<br>load useer data: " + ex.Message;
                                                                    }

                                                                }
                                                            }

                                                            //get user data from list


                                                            try
                                                            {
                                                                tcData.Attributes.Add("style", "direction:" + textDirection + ";");
                                                                tcData.Controls.Add(addTable(repeatTableId, colNum, colTitle, colData, controlTypes, labelStyle, dataStyle, printControl, addColumns, cellWidth, RowsArray, userData, requiredFields));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                debugLabel.Text += exceptionMessageBuilder("Add table", ex.Message);
                                                            }

                                                            tr.Cells.Add(tcData);
                                                            t.Rows.Add(tr);
                                                            //+= new EventHandler(this.GreetingBtn_Click);
                                                            tr = new TableRow();
                                                            tr.Attributes.Add("direction", textDirection);
                                                            Button addRow = new Button();
                                                            addRow.Text = addRowText;
                                                            addRow.Attributes.Add("tableId", repeatTableId);
                                                            addRow.Attributes.Add("colNum", colNum.ToString());
                                                            addRow.Attributes.Add("maxrowsnum", maxrowsnum.ToString());
                                                            if (printControl)
                                                            {
                                                                addRow.Attributes.Add("print", "yes");
                                                            }
                                                            else
                                                            {
                                                                addRow.Attributes.Add("print", "no");
                                                            }
                                                            addRow.Attributes.Add("rowStyle", labelStyle);
                                                            addRow.Attributes.Add("cssStyle", dataStyle);
                                                            addRow.Attributes.Add("currentRowsNum", currentRowsNum.ToString());
                                                            //rowStyle
                                                            addRow.CssClass = "DynamicAddButton";
                                                            addRow.Click += new EventHandler(this.addTableRow);
                                                            tcData = new TableCell();
                                                            tcData.Attributes.Add("style", "width:" + cellWidth + "px;padding:4px;direction:" + textDirection + ";");
                                                            tcData.Controls.Add(addRow);

                                                            tr.Cells.Add(tcData);

                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            debugLabel.Text += exceptionMessageBuilder("'Dynamic Table' control", ex.Message);
                                                        }
                                                    }

                                                    if (tcData.Controls.Count > 0)
                                                    {
                                                        tcData.Attributes.Add("style", "width:" + cellWidth + "px;");
                                                        tr.Cells.Add(tcData);
                                                        tcData = new TableCell();
                                                        tcData.Width = 10;
                                                        tr.Cells.Add(tcData);
                                                    }


                                                }
                                                catch (Exception ex)
                                                {
                                                    debugLabel.Text += "<br> | " + ex.ToString();
                                                }

                                            }
                                            t.Rows.Add(tr);
                                            formPanel.ContentTemplateContainer.Controls.Add(t);
                                        }
                                    }
                                    formPanel.ContentTemplateContainer.Controls.Add(t);

                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += ex.Message;
                                }

                            }
                            this.Controls.Add(formPanel);
                            foreach (DynamicControl dControl in dynamicFormulaIds)
                            {
                                Control c = FindControl(dControl.ID);
                                ((TextBox)c).Text = EvalFormula(dControl.ID, dControl.Formula).ToString();
                                dControl.Data = ((TextBox)c).Text;
                            }
                        }

                    }



                });
                debugLabel.Text += print_OK_Step("Done loading XML file");
                foreach (clientObject obj in clientControls)
                {

                    Type cType = ((FindControl(obj.controlId)).GetType());

                    if (cType == typeof(TextBox))
                    {
                        TextBox tBox = ((TextBox)FindControl(obj.controlId));
                        tBox.Attributes.Add(obj.clientEvent, obj.eventFunc.Replace("controlId", tBox.ClientID));
                    }
                    if (cType == typeof(RadioButtonList))
                    {
                        debugLabel.Text += printInfo("Adding attributes to RBL items");
                        RadioButtonList rBox = ((RadioButtonList)FindControl(obj.controlId));
                        for (int i = 0; i < rBox.Items.Count; i++)
                        {
                            if (obj.required)
                            {
                                rBox.Items[i].Attributes.Add("onclick", "validateRequiredRBL('" + rBox.ClientID + "','" + obj.validCSS + "','" + obj.invalidCSS + "Red');");
                            }
                        }

                    }
                    if (cType == typeof(DropDownList))
                    {
                        DropDownList DDLBox = ((DropDownList)FindControl(obj.controlId));
                        DDLBox.Attributes.Add(obj.clientEvent, obj.eventFunc.Replace("controlId", DDLBox.ClientID));
                    }
                    if (cType == typeof(CheckBox))
                    {
                        CheckBox cBox = ((CheckBox)FindControl(obj.controlId));
                        cBox.InputAttributes.Add(obj.clientEvent, obj.eventFunc.Replace("controlId", cBox.ClientID));
                    }
                    if (cType == typeof(FileUpload))
                    {
                        FileUpload aBox = ((FileUpload)FindControl(obj.controlId));
                        aBox.Attributes.Add(obj.clientEvent, obj.eventFunc.Replace("controlId", aBox.ClientID));
                    }

                }

               
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Load form from XML", ex.Message);
            }
            return str;
        }

        private void setSuccessMessageStyle(string _message)
        {
            Control Container = FindControl(messageContainerID);
            Control msg = FindControl(messageLabelId); Control img = FindControl(messageImageID);
            ((HtmlGenericControl)img).Attributes["class"] = messageImageSuccessStyle;
            ((Label)msg).Text = _message;
            Container.Visible = true;
            ((HtmlGenericControl)Container).Attributes["class"] = MessageContainerSuccessStyle;
        }

        private void setFailedMessageStyle(string _message)
        {
            Control Container = FindControl(messageContainerID);
            Control msg = FindControl(messageLabelId); Control img = FindControl(messageImageID);
            ((HtmlGenericControl)img).Attributes["class"] = messageImageFailedStyle;
            ((Label)msg).Text = _message;
            Container.Visible = true;
            ((HtmlGenericControl)Container).Attributes["class"] = MessageContainerFailedStyle;
        }

        private void setFailedCaptchaMessageStyle(string _message)
        {
            Control Container = FindControl(captchaMessageContainerID);
            Control msg = FindControl(captchaMessageLabelId); Control img = FindControl(captchaMessageImageID);
            ((HtmlGenericControl)img).Attributes["class"] = messageImageFailedStyle;
            ((Label)msg).Text = _message;
            Container.Visible = true;
            ((HtmlGenericControl)Container).Attributes["class"] = MessageContainerFailedStyle;
        }
        private void HideCaptchaMessageStyle()
        {
            Control Container = FindControl(captchaMessageContainerID);
            ((HtmlGenericControl)Container).Attributes["class"] = "hideCaptcha";
        }
        /// <summary>
        /// Validates form content
        /// </summary>
        /// <returns>'True' if valid, 'False' otherwise</returns>
        protected bool validate()
        {

            bool isValid = true;

            try
            {
                foreach (DynamicControl dControl in dynamicTextBoxIDs)
                {
                    bool validBox = true;
                    TextBox dTextBox = (TextBox)dControl.ControlType;
                    if (dControl.ControlRequired && dTextBox.Text.Length == 0)
                    {
                        isValid = false;
                        validBox = false;
                        dTextBox.CssClass = dataStyle + "Red";
                    }

                    if (dControl.RegEx.Length > 0)
                    {

                        if (!Regex.Match(dTextBox.Text, dControl.RegEx).Success)
                        {
                            isValid = false;
                            validBox = false;
                            dTextBox.CssClass = dataStyle + "Yellow";
                        }
                    }
                    if (validBox)
                    {
                        dTextBox.CssClass = dataStyle;
                    }

                }
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("'Text Box' Content validation", ex.Message);
            }


            try
            {
                foreach (DynamicControl dControl in dynamicDropDownControlIDs)
                {
                    DropDownList dDropDownList = (DropDownList)dControl.ControlType;

                    if (dControl.ControlRequired && (dDropDownList.Text.Length == 0 || dDropDownList.Text.Trim().Equals("-")))
                    {
                        isValid = false;
                        dDropDownList.CssClass = dataStyle + "Red";
                    }
                    else
                    {
                        dDropDownList.CssClass = dataStyle;
                    }

                }

            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("'Drop Down List' Content validation", ex.Message);
            }



            try
            {
                foreach (DynamicControl dControl in dynamicCheckBoxIds)
                {
                    CheckBox dCheckBox = (CheckBox)dControl.ControlType;
                    HtmlGenericControl dCheckBoxDiv = FindControl(dCheckBox.ID + "_div") as HtmlGenericControl;

                    if (dControl.ControlRequired && !(dCheckBox.Checked))
                    {
                        isValid = false;
                        dCheckBoxDiv.Attributes["class"] = checkBoxStyle + "Red";
                    }
                    else
                    {
                        dCheckBoxDiv.Attributes["class"] = checkBoxStyle;
                    }
                }
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("'Check Box' Content validation", ex.Message);
            }

            try
            {
                foreach (DynamicControl dControl in dynamicRadioButtonIds)
                {
                    RadioButtonList dRadioButtonList = (RadioButtonList)dControl.ControlType;
                    if (dControl.ControlRequired && dRadioButtonList.SelectedIndex < 0)
                    {
                        isValid = false;
                        dRadioButtonList.CssClass = RadioButtonListStyle + "Red";
                    }
                    else
                    {
                        dRadioButtonList.CssClass = RadioButtonListStyle;
                    }

                }
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("'Radio Button List' Content validation", ex.Message);
            }

            //datePickerIds
            try
            {
                foreach (DynamicControl dControl in dynamicDatePickerIds)
                {
                    Microsoft.SharePoint.WebControls.DateTimeControl dDatePicker = (Microsoft.SharePoint.WebControls.DateTimeControl)FindControl(dControl.ID);
                    if (dDatePicker.ID.ToString().Equals(dControl.ID) && dControl.ControlRequired && (dDatePicker.IsDateEmpty))
                    {
                        isValid = false;

                        dDatePicker.CssClassTextBox = datePickerStyle + "Red";
                    }
                    else
                    {
                        dDatePicker.CssClassTextBox = datePickerStyle;
                    }

                }
            }
            catch
            {

            }

            try
            {
                foreach (DynamicControl dControl in dynamicAttachmentIds)
                {
                    debugLabel.Text += printInfoTitle("Validating Attachmnets: " + dynamicAttachmentIds.Count);
                    FileUpload file_upload  = (FileUpload)FindControl(dControl.ID);
                    if (file_upload.ID.ToString().Equals(dControl.ID) && dControl.ControlRequired)
                    {
                        debugLabel.Text += printInfo(file_upload.ID.ToString());
                        //FileUpload _upload = (FileUpload)FindControl(dControl.ID);
                        if (dControl.ControlRequired && !file_upload.HasFile && ViewState[dControl.ID + "fileBytes"] == null)
                        {
                            isValid = false;
                            file_upload.CssClass = dataStyle + "Red";
                        }
                        else
                        {
                            file_upload.CssClass = dataStyle;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Attachment validation", ex.Message);
            }

            return isValid;
        }

        /// <summary>
        /// Validate 'Captcha' control
        /// </summary>
        /// <returns></returns>
        protected bool validateCaptcha()
        {

            var result = false;
            var captchaResponse = HttpContext.Current.Request.Form["g-recaptcha-response"];
            var secretKey = ConfigurationManager.AppSettings["SecretKey"];


            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";

            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }
            return result;
        }


        //This method add new column to applicants list

        /// <summary>
        /// Creates SPListColumn 
        /// </summary>
        /// <param name="columnName">SPFiled name</param>
        /// <param name="columnType">SPFiled type</param>
        protected void addColumn(string columnName, SPFieldType columnType)
        {
            bool existColumn = false;
            try
            {
                string url = SPContext.Current.Web.Url.ToString();
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {

                    using (SPSite site = new SPSite(destinationWeb))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList oList = web.Lists[destinationList];
                            web.AllowUnsafeUpdates = true;
                            for (int a = 0; a < oList.Fields.Count; a++)
                            {
                                if (oList.Fields[a].Title.CompareTo(columnName) == 0)
                                    existColumn = true;

                            }
                            if (!existColumn)
                            {
                                oList.Fields.Add(columnName, columnType, false);
                                oList.Update();
                                try
                                {
                                    SPView defaultView = oList.DefaultView;
                                    defaultView.ViewFields.Add(oList.Fields[columnName]);
                                    defaultView.Update();
                                    oList.Update();
                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += "<div style=\"color:red\">Error adding column to list default view: " + ex.Message + "</div>";
                                }
                            }


                        }
                    }
                });
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Add SPListColumn with type: " + columnType, ex.Message);
            }
        }


        /// <summary>
        /// Creates counter column for repeating tables
        /// </summary>
        /// <param name="columnName">SPFiled name</param>
        /// <param name="value">Counter</param>
        protected void addColumn(string columnName, int value)
        {
            bool existColumn = false;
            try
            {
                string url = SPContext.Current.Web.Url.ToString();
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {

                    using (SPSite site = new SPSite(destinationWeb))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList oList = web.Lists[destinationList];
                            web.AllowUnsafeUpdates = true;
                            for (int a = 0; a < oList.Fields.Count; a++)
                            {
                                if (oList.Fields[a].Title.CompareTo(columnName) == 0)
                                    existColumn = true;
                            }
                            if (!existColumn)
                            {
                                oList.Fields.Add(columnName, SPFieldType.Number, false);
                                oList.Update();
                                SPField field = oList.Fields.TryGetFieldByStaticName(columnName);
                                field.DefaultValue = value.ToString();
                                field.Update();
                                oList.Update();

                            }


                        }
                    }
                });
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Add SPListColumn", ex.Message);
            }
        }


        /// <summary>
        /// Creates TextBox Control
        /// </summary>
        /// <param name="controlId">Contorl ID</param>
        /// <param name="cssStyle">CSS calss name</param>
        /// <param name="data">Text value</param>
        /// <param name="printContorl">Print control</param>
        /// <param name="required">yes/no</param>
        /// <param name="_regEx">RegEx for validation</param>
        /// <returns></returns>
        protected Control addTextBox(string controlId, string cssStyle, string data, bool printContorl, bool required, string _regEx, int item_num)
        {
            TextBox textBox = new TextBox();

            if (addColumns)
            {

                addColumn(controlId, SPFieldType.Text);
            }
            textBox.ID = controlId;
            textBox.Wrap = true;
            if (required)
            {
                cObj = new clientObject(controlId, "onblur", "validateRequired('controlId','" + dataStyle + "','" + dataStyle + "Red');");
                cObj.required = required;
                clientControls.Add(cObj);
            }
            if (_regEx.Length > 0)
            {
                cObj = new clientObject(controlId, "onblur", "validateRegExTextBox('controlId','" + dataStyle + "','" + dataStyle + "Yellow','" + _regEx.Replace("\\d", "[0-9]") + "');");
                cObj.required = required;
                clientControls.Add(cObj);
            }
            textBox.CssClass = cssStyle;
            textBox.Text = data;
            textBox.AutoPostBack = false;
            DynamicControl dynamicTextBox = new DynamicControl(textBox, required, controlId, controlId, printContorl, data);
            dynamicTextBox.RegEx = _regEx;
            dynamicTextBox.ControlRequired = required;
            dynamicTextBox.Item = item_num;
            dynamicTextBoxIDs.Add(dynamicTextBox);

            return textBox;
        }


        /// <summary>
        /// For regular controls - not as part of repeating table
        /// </summary>
        /// <param name="controlId">Contorl ID</param>
        /// <param name="cssStyle">CSS calss name</param>
        /// <param name="data">Result value</param>
        /// <param name="_printControl">Print control</param>
        /// <param name="required">yes/no</param>
        /// <param name="formula">Formula expression</param>
        /// <returns></returns>
        protected Control addFormula(string controlId, string cssStyle, string data, bool _printControl, bool required, string formula, int item_num)
        {
            TextBox textBox = new TextBox();
            if (addColumns)
            {
                addColumn(controlId, SPFieldType.Text);
            }
            textBox.ID = controlId;
            textBox.Wrap = true;
            textBox.CssClass = cssStyle;
            textBox.Text = "0";
            textBox.Enabled = false;
            textBox.AutoPostBack = true;
            DynamicControl dynamicFormula = new DynamicControl(textBox, required, controlId, controlId, _printControl, data);
            dynamicFormula.ControlRequired = required;
            dynamicFormula.Formula = formula;
            dynamicFormula.Item = item_num;
            dynamicFormulaIds.Add(dynamicFormula);
            dynamicTextBoxIDs.Add(dynamicFormula);
            return textBox;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlId"></param>
        /// <param name="cssStyle"></param>
        /// <param name="data"></param>
        /// <param name="_printControl"></param>
        /// <param name="required"></param>
        /// <returns></returns>
        protected Control addDatePicker(string controlId, string cssStyle, System.DateTime data, bool _printControl, bool required)
        {
            Microsoft.SharePoint.WebControls.DateTimeControl dateControl = new Microsoft.SharePoint.WebControls.DateTimeControl();
            if (addColumns)
            {

                addColumn(controlId, SPFieldType.DateTime);
            }
            dateControl.ID = controlId;
            dateControl.DateOnly = true;
            dateControl.LocaleId = 2057;
            dateControl.CssClassTextBox = cssStyle;
            dateControl.AutoPostBack = false;

            dateControl.ClearSelection();

            DynamicControl dynamicDatePicker = new DynamicControl(dateControl, required, controlId, controlId, _printControl, data);
            dynamicDatePicker.ControlRequired = required;
            dynamicDatePickerIds.Add(dynamicDatePicker);
            return dateControl;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlId"></param>
        /// <param name="cssStyle"></param>
        /// <param name="data"></param>
        /// <param name="_printControl"></param>
        /// <param name="required"></param>
        /// <param name="_regEx"></param>
        /// <returns></returns>
        protected Control addMultiLineTextBox(string controlId, string cssStyle, string data, bool _printControl, bool required, string _regEx, int item_num)
        {
            TextBox textBox = new TextBox();

            if (addColumns)
            {
                addColumn(controlId, SPFieldType.Note);

            }

            textBox.ID = controlId;
            textBox.Height = MtHeight;
            textBox.BorderStyle = BorderStyle.Solid;
            textBox.BorderWidth = 1;
            textBox.BorderColor = System.Drawing.Color.Gray;
            textBox.TextMode = TextBoxMode.MultiLine;
            textBox.Wrap = true;
            textBox.Text = data;
            try
            {
                textBox.Width = (int)(Convert.ToInt32(cellWidth));
            }
            catch { }
            if (required)
            {
                cObj = new clientObject(controlId, "onblur", "validateRequired('controlId','" + cssStyle + "','" + cssStyle + "Red');");
                cObj.required = required;
                clientControls.Add(cObj);
            }
            textBox.AutoPostBack = false;

            textBox.CssClass = cssStyle;
            textBox.Wrap = false;
            DynamicControl dynamicTextBox = new DynamicControl(textBox, required, controlId, controlId, _printControl, data);
            dynamicTextBox.RegEx = _regEx;
            dynamicTextBox.Item = item_num;
            dynamicTextBoxIDs.Add(dynamicTextBox);
            return textBox;
        }


        protected Control addSignature(string controlId, string cssStyle, string data, bool _printControl, bool required, string _regEx, int item_num)
        {
            HtmlGenericControl container_div = new HtmlGenericControl("div");
            container_div.Attributes.Add("class", "sig_container_div");

            HtmlGenericControl button_div = new HtmlGenericControl("div");
            button_div.Attributes.Add("class", "sig_button_div");

            HtmlGenericControl signature_image_div = new HtmlGenericControl("div");
            signature_image_div.Attributes.Add("class", "sig_image_div");

            Button save_signature = new Button();
            save_signature.ID = "signature_open_btn";
            save_signature.CssClass = "sig_button";
            save_signature.Attributes.Add("onclick", "signatureClick();return false;");
            save_signature.Text = this.WebPart.Signature_Button_Text;
            button_div.Controls.Add(save_signature);
            save_signature.Text = this.WebPart.Signature_Button_Text;

            Image signature_img = new Image();
            //signature_img.ViewStateMode = ViewStateMode.Enabled;
            signature_img.ID = "signature_img";
            try
            {
                signature_img.Attributes["src"] = ViewState["sig"].ToString();
            }
            catch { }
            signature_img.CssClass = "sig_image";
            signature_img.Attributes.Add("runat", "server");
            signature_image_div.Controls.Add(signature_img);

            TextBox sig_store = new TextBox();
            sig_store.ID = "Signature";
            sig_store.Attributes.Add("style", "visibility:hidden");
         

            container_div.Controls.Add(signature_image_div);
            container_div.Controls.Add(button_div);
            container_div.Controls.Add(sig_store);
            //save signature data
            DynamicControl dynamicTextBox = new DynamicControl(sig_store, required, "Signature", "Signature", false, "");
            dynamicTextBox.RegEx = _regEx;
            dynamicTextBox.Item = item_num;
            dynamicTextBoxIDs.Add(dynamicTextBox);

            return container_div;

        }

        /// <summary>
        /// Add 'Check Box' Control
        /// </summary>
        /// <param name="controlId">Control ID</param>
        /// <param name="cssStyle">CSS Class</param>
        /// <param name="data">Value</param>
        /// <param name="_printControl">yes/no</param>
        /// <param name="required">yes/no</param>
        /// <returns>CheckBox Control</returns>
        protected Control addCheckBox(string controlId, string cssStyle, bool data, bool _printControl, bool required, int item_num)
        {

            CheckBox checkBox = new CheckBox();

            checkBox.ID = controlId;
            checkBox.InputAttributes.Add("class", "DynamicCheckBoxSize");
            // checkBox.CssClass = cssStyle;
            if (addColumns)
            {
                addColumn(controlId, SPFieldType.Boolean);
            }
            if (required)
            {
                cObj = new clientObject(controlId, "onclick", "validateRequiredCheckBox('controlId','" + cssStyle + "','" + cssStyle + "Red');");
                cObj.invalidCSS = cssStyle + "Red";
                cObj.validCSS = cssStyle;
                cObj.required = required;
                clientControls.Add(cObj);
            }
            checkBox.Checked = data;
            checkBox.AutoPostBack = false;
            DynamicControl dynamicCheckBox = new DynamicControl(checkBox, required, controlId, controlId, _printControl, data);
            dynamicCheckBox.Item = item_num;
            dynamicCheckBoxIds.Add(dynamicCheckBox);
            return checkBox;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlId"></param>
        /// <param name="cssStyle"></param>
        /// <param name="ListName"></param>
        /// <param name="_printControl"></param>
        /// <param name="data"></param>
        /// <param name="direction"></param>
        /// <param name="required"></param>
        /// <returns></returns>
        protected Control addRadioButtonList(string controlId, string cssStyle, string ListName, bool _printControl, string data, string direction, bool required, int item_num)
        {
            string url = SPContext.Current.Web.Url.ToString();

            RadioButtonList rbList = new RadioButtonList();
            rbList.AutoPostBack = false;
            if (addLists)
            {
                createList(ListName);
            }
            if (addColumns)
            {
                addColumn(controlId, SPFieldType.Text);

            }
            rbList.ID = controlId;
            if (direction.ToLower().CompareTo("horizontal") == 0)
            {
                rbList.RepeatDirection = RepeatDirection.Horizontal;
            }
            else
            {
                rbList.RepeatDirection = RepeatDirection.Vertical;
            }

            rbList.CssClass = cssStyle;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {

                    using (SPSite site = new SPSite(destinationWeb))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList listTitle = web.Lists[ListName];
                            rbList.DataSource = listTitle.Items.GetDataTable();
                            rbList.DataValueField = "Title";
                            rbList.DataTextField = "Title";
                            rbList.DataBind();
                            for (int i = 0; i < rbList.Items.Count; i++)
                            {

                                rbList.Items[i].Attributes.Add("class", RadioButtonListItemStyle);
                                if (data.CompareTo(rbList.Items[i].Text) == 0)
                                {
                                    rbList.SelectedIndex = i;
                                }
                            }

                        }
                    }
                });
                if (required)
                {
                    cObj = new clientObject(controlId, "onclick", "validateRequiredRBL('controlId','" + cssStyle + "','" + cssStyle + "Red');");
                    cObj.required = required;
                    cObj.validCSS = cssStyle;
                    cObj.invalidCSS = cssStyle + "Red";
                    clientControls.Add(cObj);
                }
            }
            catch { }
            DynamicControl dynamicRbList = new DynamicControl(rbList, required, controlId, controlId, _printControl, rbList.SelectedValue);
            dynamicRbList.Item = item_num;
            dynamicRadioButtonIds.Add(dynamicRbList);
            return rbList;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlId"></param>
        /// <param name="cssStyle"></param>
        /// <param name="ListName"></param>
        /// <param name="_printControl"></param>
        /// <param name="required"></param>
        /// <param name="repeating"></param>
        /// <param name="rowNum"></param>
        /// <param name="_SourceList"></param>
        /// <param name="isEnabled"></param>
        /// <param name="_tooltip"></param>
        /// <param name="_filter"></param>
        /// <param name="_filterColumn"></param>
        /// <param name="_displayColumn"></param>
        /// <param name="_category"></param>
        /// <param name="_autoPostBack"></param>
        /// <param name="_ddlGroup"></param>
        /// <param name="_ddlOrder"></param>
        /// <returns></returns>
        protected Control addDependentDropDownList(string controlId, string cssStyle, string ListName, bool _printControl, bool required, bool repeating, int rowNum, string _SourceList, bool isEnabled, string _tooltip, string _filter, string _filterColumn, string _displayColumn, string _category, bool _autoPostBack, string _ddlGroup, int _ddlOrder, int item_num)
        {
            DropDownList dropDownList = new DropDownList();
            UpdatePanel ddl_panel = new UpdatePanel();
            ddl_panel.ID = controlId + "_panel";

            if (addLists)
            {
                try
                {
                    createList(ListName);
                }
                catch (Exception ex)
                {
                    debugLabel.Text += exceptionMessageBuilder("Creating SPList '" + ListName + "'", ex.Message);

                }
            }
            if (addColumns)
            {
                addColumn(controlId, SPFieldType.Text);
            }

            dropDownList.ID = controlId;

            dropDownList.ToolTip = _tooltip;
            if (required)
            {
                cObj = new clientObject(controlId, "onblur", "validateRequiredDDL('controlId','" + cssStyle + "','" + cssStyle + "Red');");
                cObj.required = required;
                clientControls.Add(cObj);
            }


            dropDownList.Attributes.Add("controlId", controlId);
            dropDownList.Attributes.Add("ddlGroup", _ddlGroup);
            dropDownList.Attributes.Add("ddlOrder", _ddlOrder.ToString());
            // dropDownList.Attributes.Add("onchange", "return fetchSending();");
            //parentList
            dropDownList.AutoPostBack = false;

            if (_autoPostBack)
            {
                dropDownList.AutoPostBack = true;
                dropDownList.SelectedIndexChanged += new EventHandler(ddlChanged);

            }
            dropDownList.CssClass = cssStyle;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {

                    using (SPSite site = new SPSite(destinationWeb))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;
                            // This should load only the first item in ddl group. Other items will be updated on 'selected index changed' event
                            if (filter.Length == 0)
                            {
                                SPList listTitle = web.Lists[ListName];
                                SPListItemCollection Items = null;

                                if (_filter.Length > 0)
                                {
                                    Items = listTitle.GetItems(new SPQuery()
                                    {
                                        Query = @"<Where>
                                            <Eq>
                                                <FieldRef Name='" + _filterColumn + "'/>"
                                                        + "<Value Type='Text'>" + _filter + "</Value>"
                                                   + " </Eq>"
                                                 + " </Where>"
                                                 + " <OrderBy>"
                                                  + "    <FieldRef Name='" + _displayColumn + "' Ascending='TRUE' />"
                                                 + " </OrderBy>"
                                    });
                                }
                                else
                                {
                                    Items = listTitle.GetItems(new SPQuery()
                                    {
                                        Query = @"<Where>
                                            
                                              </Where>"
                                                 + " <OrderBy>"
                                                  + "    <FieldRef Name='" + _displayColumn + "' Ascending='TRUE' />"
                                                 + " </OrderBy>"
                                    });
                                }

                                DataView view = new DataView(Items.GetDataTable());
                                string[] columnNames = { _displayColumn };
                                DataTable result = view.ToTable(true, columnNames);

                                dropDownList.DataSource = result;
                                dropDownList.DataValueField = _displayColumn;

                                dropDownList.DataTextField = _displayColumn;
                                dropDownList.DataBind();
                                ListItem item = new ListItem("-");
                                dropDownList.Items.Add(item);

                                dropDownList.Text = "-";

                            }
                            else
                            {
                                dropDownList.Items.Add("-");
                                dropDownList.Text = "-";

                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Add dependent drop down control", ex.Message);

            }
            dropDownList.Enabled = isEnabled;
            DynamicControl dynamicDropDownList = new DynamicControl(dropDownList, required, controlId, controlId, _printControl, "-", rowNum.ToString());
            dynamicDropDownList.FilterControl = _filter;
            dynamicDropDownList.FilterColumn = _filterColumn;

            dynamicDropDownList.FilterList = _SourceList;
            dynamicDropDownList.DisplayColumn = _displayColumn;
            dynamicDropDownList.Data = "-";
            dynamicDropDownList.DdlGroup = _ddlGroup;
            dynamicDropDownList.DdlOrder = _ddlOrder;
            dynamicDropDownList.RowNum = rowNum.ToString();
            dynamicDropDownList.Item = item_num;
            dependentDynamicDropDownControlIDs.Add(dynamicDropDownList);
            dynamicDropDownControlIDs.Add(dynamicDropDownList);
            ddl_panel.ContentTemplateContainer.Controls.Add(dropDownList);
            return ddl_panel;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlChanged(object sender, EventArgs e)
        {
            try
            {

                string currentControlID = ((DropDownList)sender).Attributes["controlId"];
                string group = ((DropDownList)sender).Attributes["ddlGroup"];
                int innerOrder = (int)Convert.ToInt32(((DropDownList)sender).Attributes["ddlOrder"]);

                string filterValue = ((DropDownList)FindControl(currentControlID)).SelectedItem.Text;
                string parentFilter = string.Empty;
                string filterColumnType = string.Empty;
                string valueType = string.Empty;
                string andString = string.Empty;
                SPFieldType vType;
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {

                    using (SPSite site = new SPSite(destinationWeb))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;
                            foreach (DynamicControl dControl in dependentDynamicDropDownControlIDs)
                            {
                                try
                                {
                                    //Load parent control filter
                                    if (dControl.DdlOrder < (innerOrder + 1) && dControl.DdlGroup.Equals(group))
                                    {
                                        SPList listTitle = web.Lists[dControl.FilterList];
                                        filterColumnType = "";
                                        valueType = "";
                                        vType = listTitle.Fields[dControl.DisplayColumn].Type;

                                        if (vType == SPFieldType.Lookup)
                                        {
                                            filterColumnType = "LookupValue='TRUE'";
                                            valueType = "'Lookup'";

                                        }
                                        else
                                        {
                                            filterColumnType = "";
                                            valueType = "'Text'";

                                        }
                                        andString += "<And>";
                                        parentFilter += @"<Eq><FieldRef Name='" + dControl.DisplayColumn + "' " + filterColumnType + " /><Value Type=" + valueType + ">" + ((DropDownList)FindControl(dControl.ID)).SelectedItem.Text + "</Value></Eq></And>";

                                    }
                                    if (dControl.DdlOrder > (innerOrder + 1) && dControl.DdlGroup.Equals(group))
                                    {
                                        DropDownList dropDownList = (DropDownList)FindControl(dControl.ID);
                                        dropDownList.Items.Clear();
                                        ListItem item = new ListItem("-");
                                        dropDownList.Items.Add(item);
                                        dropDownList.Text = "-";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("Load dependent drop down list parent control filter", ex.Message);
                                }

                            }


                            foreach (DynamicControl dControl in dependentDynamicDropDownControlIDs)
                            {

                                // filter and bind dropDownList
                                if (dControl.DdlOrder == (innerOrder + 1) && dControl.DdlGroup.Equals(group))
                                {

                                    try
                                    {

                                        SPList listTitle = web.Lists[dControl.FilterList];
                                        SPListItemCollection newItems = null;


                                        if (dControl.FilterControl.Length > 0)
                                        {
                                            filterColumnType = "";
                                            valueType = "";
                                            vType = listTitle.Fields[dControl.FilterColumn].Type;

                                            if (vType == SPFieldType.Lookup)
                                            {
                                                filterColumnType = "LookupValue='TRUE'";
                                                valueType = "'Lookup'";

                                            }
                                            else
                                            {
                                                filterColumnType = "";
                                                valueType = "'Text'";

                                            }
                                            // Create query                                                                                                                                                                                                                                                   
                                            newItems = listTitle.GetItems(new SPQuery()
                                            {

                                                Query = @"<Where>" + andString + "<Eq>"
                                    + "<FieldRef Name='" + dControl.FilterColumn + "' " + filterColumnType + " />"
                                                                + "<Value Type=" + valueType + ">" + filterValue + "</Value>"
                                                           + " </Eq>" + parentFilter
                                                         + " </Where>"
                                                         + " <OrderBy>"
                                                          + "    <FieldRef Name='" + dControl.DisplayColumn + "' Ascending='TRUE' />"
                                                         + " </OrderBy>"
                                            });


                                            if (newItems.Count > 0)
                                            {
                                                DropDownList dropDownList = (DropDownList)FindControl(dControl.ID);
                                                dropDownList.Items.Clear();

                                                dropDownList.Items.Add("-");

                                                DataView view = new DataView(newItems.GetDataTable());
                                                string[] _columnNames = { dControl.DisplayColumn };
                                                DataTable result = view.ToTable(true, _columnNames);

                                                dropDownList.DataSource = result;

                                                dropDownList.DataValueField = dControl.DisplayColumn;

                                                dropDownList.DataTextField = dControl.DisplayColumn;

                                                dropDownList.DataBind();
                                                ListItem item = new ListItem("-");
                                                dropDownList.Items.Add(item);
                                                dropDownList.Text = "-";

                                            }
                                            else
                                            {

                                                DropDownList dropDownList = (DropDownList)FindControl(dControl.ID);
                                                dropDownList.Items.Clear();
                                                ListItem item = new ListItem("-");
                                                dropDownList.Items.Add(item);
                                                dropDownList.Text = "-";

                                            }

                                        }


                                    }
                                    catch (Exception ex)
                                    {
                                        debugLabel.Text += exceptionMessageBuilder("Filter & bind 'Drop Down List'", ex.Message);
                                    }

                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("'Drop Down List' Changed event", ex.Message);
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlId"></param>
        /// <param name="cssStyle"></param>
        /// <param name="ListName"></param>
        /// <param name="_printControl"></param>
        /// <param name="data"></param>
        /// <param name="required"></param>
        /// <returns></returns>
        protected Control addDropDownList(string controlId, string cssStyle, string ListName, bool _printControl, string data, bool required, int item_num)
        {
            string url = SPContext.Current.Web.Url.ToString();
            DropDownList dropDownList = new DropDownList();
            dropDownList.AutoPostBack = false;
            if (addLists)
            {
                try
                {
                    createList(ListName);
                }
                catch (Exception ex)
                {
                    debugLabel.Text += exceptionMessageBuilder("Add source list for 'Drop Down List' control", ex.Message);
                }
            }
            if (addColumns)
            {
                addColumn(controlId, SPFieldType.Text);
            }
            dropDownList.ID = controlId;
            dropDownList.CssClass = cssStyle;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {

                    using (SPSite site = new SPSite(destinationWeb))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList listTitle = web.Lists[ListName];
                            dropDownList.DataSource = listTitle.Items.GetDataTable();
                            dropDownList.DataValueField = "Title";
                            dropDownList.DataTextField = "Title";
                            dropDownList.DataBind();
                            dropDownList.Text = data;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Bind data to 'Drop Down List' control", ex.Message);
            }
            if (required)
            {
                cObj = new clientObject(controlId, "onblur", "validateRequiredDDL('controlId','" + cssStyle + "','" + cssStyle + "Red');");
                cObj.required = required;
                clientControls.Add(cObj);
            }
            DynamicControl dynamicDropDownList = new DynamicControl(dropDownList, required, controlId, controlId, _printControl, data);
            dynamicDropDownList.Item = item_num;
            dynamicDropDownControlIDs.Add(dynamicDropDownList);
            return dropDownList;
        }

        /// <summary>
        /// Add Label control with data stored in SPListItemField 
        /// </summary>
        /// <param name="controlId">Control ID</param>
        /// <param name="cssStyle">CSS Class</param>
        /// <param name="_printControl">yes/no</param>
        /// <param name="data">Value</param>
        /// <returns></returns>
        protected Control addTextLabel(string controlId, string cssStyle, bool _printControl, string data, int item_num)
        {
            Label TextLabel = new Label();

            TextLabel.ID = controlId;
            TextLabel.CssClass = cssStyle;
            TextLabel.Text = data;
            DynamicControl lbl = new DynamicControl();
            lbl.ID = controlId;
            lbl.Item = item_num;
            dynamicLabelIds.Add(lbl);
            return TextLabel;
        }


        /// <summary>
        /// Add final message
        /// </summary>
        /// <param name="controlId"></param>
        /// <param name="cssStyle"></param>
        /// <param name="data"></param>
        /// <returns></returns>
       /* protected Control addFinalMessage(string controlId, string cssStyle, string data)
        {
            Label labelMessage = new Label();
            labelMessage.ID = controlId;
            labelMessage.CssClass = cssStyle;
            messageLabelId = controlId;
            return labelMessage;
        }*/

        protected HtmlGenericControl addFinalMessage(string controlId, string cssStyle, string data)
        {
            HtmlGenericControl container_div = new HtmlGenericControl("div");
            HtmlGenericControl image_div = new HtmlGenericControl("div");
            HtmlGenericControl text_div = new HtmlGenericControl("div");
            Label labelMessage = new Label();
            container_div.Visible = false;
            container_div.ID = controlId + "_message_container";
            container_div.Attributes.Add("runat", "server");
            container_div.Attributes["class"] = MessageContainerSuccessStyle;
            container_div.Controls.Add(image_div);
            container_div.Controls.Add(text_div);
            //container_div.Visible = false;
            image_div.ID = controlId + "_message_image";
            image_div.Attributes.Add("runat", "server");
            image_div.Attributes.Add("class", MessageImageStyle);
            image_div.Attributes.Add("runat", "server");
            text_div.ID = controlId + "_message_text";
            text_div.Attributes.Add("runat", "server");
            text_div.Attributes.Add("class", messageTextStyle);
            text_div.Controls.Add(labelMessage);


            labelMessage.ID = controlId;
            //labelMessage.CssClass = cssStyle;
            messageLabelId = controlId;
            messageContainerID = controlId + "_message_container";
            messageImageID = controlId + "_message_Image";
            debugLabel.Text += "<br>added final message label";

            return container_div;
        }

        /// <summary>
        /// Add captcha message
        /// </summary>
        /// <param name="controlId"></param>
        /// <param name="cssStyle"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected Control addCaptchaMessage(string controlId, string cssStyle, string data)
        {
            /* Label labelMessage = new Label();
             labelMessage.ID = controlId;
             labelMessage.CssClass = cssStyle;
             messageLabelId = controlId;
             return labelMessage;*/
            HtmlGenericControl container_div = new HtmlGenericControl("div");
            HtmlGenericControl image_div = new HtmlGenericControl("div");
            HtmlGenericControl text_div = new HtmlGenericControl("div");
            Label labelMessage = new Label();
            container_div.Visible = false;
            container_div.ID = controlId + "_message_container";
            container_div.Attributes.Add("runat", "server");
            container_div.Attributes["class"] = MessageContainerSuccessStyle;
            container_div.Controls.Add(image_div);
            container_div.Controls.Add(text_div);
            //container_div.Visible = false;
            image_div.ID = controlId + "_message_image";
            image_div.Attributes.Add("runat", "server");
            image_div.Attributes.Add("class", MessageImageStyle);
            image_div.Attributes.Add("runat", "server");
            text_div.ID = controlId + "_message_text";
            text_div.Attributes.Add("runat", "server");
            text_div.Attributes.Add("class", messageTextStyle);
            text_div.Controls.Add(labelMessage);


            labelMessage.ID = controlId;
            //labelMessage.CssClass = cssStyle;
            captchaMessageLabelId = controlId;
            captchaMessageContainerID = controlId + "_message_container";
            captchaMessageImageID = controlId + "_message_Image";
            debugLabel.Text += "<br>added captcha message label";
            return container_div;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlId">Control ID</param>
        /// <param name="_colNum">Num of columns</param>
        /// <param name="_colTitle">Column titles</param>
        /// <param name="_colIds">Column IDs</param>
        /// <param name="_controlType">Control types</param>
        /// <param name="rowStyle"></param>
        /// <param name="cssStyle"></param>
        /// <param name="_printControl"></param>
        /// <param name="_addColumns"></param>
        /// <param name="cellWidth"></param>
        /// <param name="_rowsNum"></param>
        /// <param name="userData"></param>
        /// <param name="required"></param>
        /// <returns></returns>
        protected Control addTable(string controlId, int _colNum, string[] _colTitle, string[] _colIds, string[] _controlType, string rowStyle, string cssStyle, bool _printControl, bool _addColumns, string cellWidth, string[] _rowsNum, string[,] userData, string[] required)
        {
            string url = SPContext.Current.Web.Url.ToString();
            //string userName = this.Context.User.Identity.Name.ToString();
            Table table = new Table();
            table.CssClass = tableStyle;
            int rowsCounter = 0;
            TextBox textBox = new TextBox();
            DropDownList ddList = new DropDownList();
            Microsoft.SharePoint.WebControls.DateTimeControl dateControl = new Microsoft.SharePoint.WebControls.DateTimeControl();
            CheckBox checkBox = new CheckBox();
            int counterId = 0;
            table.ID = controlId;
            repeatingTableIds.Add(ID);
            TableRow row = new TableRow();
            row.Attributes.Add("direction", textDirection);
            TableCell cell = new TableCell();
            Button RemoveRow = new Button();
            Label titleLabel = new Label();
            int controlIdNum = 0;
            bool _required;
            string cellData = "";
            int counter = 0;

            //cell.Attributes.Add("padding", "5");
            int colTitleCounter = 0;
            try
            {
                row = new TableRow();
                row.Attributes.Add("direction", textDirection);
                row.ID = controlId + "ColTitle";
                foreach (string columnTitle in colTitle)
                {

                    titleLabel.Text = columnTitle;
                    titleLabel.ID = controlId + "ColTitle" + colTitleCounter;
                    titleLabel.CssClass = labelStyle;
                    cell.Attributes.Add("style", "width:" + widthList[counter] + "px;padding:4px;direction:" + textDirection + ";");

                    cell.Controls.Add(titleLabel);
                    row.Cells.Add(cell);
                    colTitleCounter++;
                    cell = new TableCell();

                    titleLabel = new Label();
                    counter++;
                }
                table.Rows.Add(row);
                row = new TableRow();
                row.Attributes.Add("direction", textDirection);
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {

                    using (SPSite site = new SPSite(destinationWeb))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList oList = web.Lists[destinationList];
                            web.AllowUnsafeUpdates = true;
                            SPListItemCollection items = oList.Items;
                            foreach (SPListItem oListItem in items)
                            {
                                if (fullUserName.CompareTo(oListItem.GetFormattedValue("userName")) == 0)
                                {
                                    rowsCounter = 0;

                                    foreach (string j in _rowsNum)
                                    {

                                        if (j.Trim().Length > 0)
                                        {

                                            counterId = Convert.ToInt32(j);
                                            controlIdNum = counterId - 1;
                                            rowsCounter++;
                                            for (int i = 0; i < _colNum; i++)
                                            {
                                                cell = new TableCell();

                                                cell.Attributes.Add("style", "width:" + widthList[i] + "px;padding:4px;direction:" + textDirection + ";");
                                                _required = (required[i].ToLower().CompareTo("yes") == 0) ? true : false;



                                                cellData = HttpUtility.HtmlDecode(oListItem.GetFormattedValue(_colIds[i] + counterId));
                                                if (_controlType[i].ToLower().CompareTo("textbox") == 0)
                                                {
                                                    cell.Controls.Add(addTextBox(_colIds[i] + counterId, dataStyle, cellData, _printControl, _required, "", Convert.ToInt32(item_num)));
                                                }
                                                if (_controlType[i].ToLower().CompareTo("multilinetextbox") == 0)
                                                {
                                                    cell.Controls.Add(addMultiLineTextBox(_colIds[i] + counterId, dataStyle, cellData, _printControl, _required, "", Convert.ToInt32(item_num)));
                                                }
                                                if (_controlType[i].ToLower().Contains("dropdownlist"))
                                                {
                                                    cell.Controls.Add(addDropDownList(_colIds[i] + counterId, dropDownListStyle, _controlType[i].Split(',')[1], _printControl, cellData, _required, Convert.ToInt32(item_num)));
                                                }
                                                if (_controlType[i].ToLower().CompareTo("date") == 0)
                                                {
                                                    cell.Controls.Add(addDatePicker(_colIds[i] + counterId, cssStyle, (DateTime)Convert.ToDateTime(cellData), _printControl, _required));
                                                }
                                                if (_controlType[i].ToLower().CompareTo("checkBox") == 0)
                                                {
                                                    HtmlGenericControl _div = new HtmlGenericControl("div");
                                                    _div.Controls.Add(addCheckBox(_colIds[i] + counterId, checkBoxStyle, (bool)Convert.ToBoolean(cellData), _printControl, _required, Convert.ToInt32(item_num)));
                                                    _div.ID = controlId + "_div";
                                                    _div.Attributes.Add("runat", "server");
                                                    _div.Attributes.Add("class", checkBoxStyle);
                                                    cell.Attributes.Add("style", "width:" + cellWidth + "px;direction:" + textDirection + ";");
                                                    cell.Controls.Add(_div);
                                                }

                                                row.ID = controlId + "Row" + counterId;
                                                row.CssClass = rowStyle;
                                                row.Cells.Add(cell);
                                            }

                                            if (rowNum < rowsCounter)
                                            {
                                                RemoveRow = new Button();
                                                RemoveRow.CssClass = "DynamicAddButton";
                                                RemoveRow.Click += new EventHandler(this.removeTableRow);
                                                RemoveRow.Text = "X";
                                                RemoveRow.ID = controlId + j;
                                                RemoveRow.Attributes.Add("tableId", controlId);
                                                RemoveRow.Attributes.Add("index", j);
                                                RemoveRow.Attributes.Add("buttonId", controlId + j);
                                                cell = new TableCell();

                                                cell.Attributes.Add("style", "width:" + cellWidth + "px;padding:4px;direction:" + textDirection + ";");

                                                cell.Controls.Add(RemoveRow);
                                                row.Cells.Add(cell);
                                            }
                                            table.Rows.Add(row);
                                            row = new TableRow();
                                            row.Attributes.Add("direction", textDirection);
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                });


            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Add Table Control", ex.Message);

            }

            return table;
        }



        /// <summary>
        /// Finds lowest available index
        /// </summary>
        /// <param name="usedIndexes"></param>
        /// <param name="maxRowNum"></param>
        /// <returns>returns -1 if no index available</returns>
        protected int findAvailableIndex(string usedIndexes, int maxRowNum)
        {
            int result = -1;
            for (int a = 1; a <= maxRowNum; a++)
            {
                if (!(usedIndexes.Contains(a.ToString() + ";")))
                {
                    result = a;
                    break;
                }

            }
            return result;
        }

        /// <summary>
        /// Add row to dynamic table control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void addTableRow(object sender, EventArgs e)
        {
            try
            {
                string _tableId = ((Button)sender).Attributes["tableId"];
                int maxrowsnum = Convert.ToInt32(((Button)sender).Attributes["maxrowsnum"]);
                int _colNum = Convert.ToInt32(((Button)sender).Attributes["colNum"]);
                bool _printControl = (((Button)sender).Attributes["print"]).ToLower().Equals("yes") ? true : false;
                string rowStyle = ((Button)sender).Attributes["rowStyle"];
                string cssStyle = ((Button)sender).Attributes["cssStyle"];
                int currentRowsNum = Convert.ToInt32(((Button)sender).Attributes["currentRowsNum"]);
                Button RemoveRow = new Button();
                Control ctl = FindControl(_tableId);
                int counterId;

                TableRow row = new TableRow();
                row.Attributes.Add("direction", textDirection);
                TableCell cell = new TableCell();

                if (maxrowsnum > currentRowsNum)
                {
                    try
                    {
                        string url = SPContext.Current.Web.Url.ToString();
                        //string userName = this.Context.User.Identity.Name.ToString();
                        SPSecurity.RunWithElevatedPrivileges(delegate ()
                        {

                            using (SPSite site = new SPSite(destinationWeb))
                            {
                                using (SPWeb web = site.OpenWeb())
                                {
                                    SPList oList = web.Lists[destinationList];
                                    web.AllowUnsafeUpdates = true;
                                    SPListItemCollection items = oList.Items;
                                    foreach (SPListItem oListItem in items)
                                    {
                                        if (fullUserName.CompareTo(oListItem.GetFormattedValue("userName")) == 0)
                                        {
                                            counterId = findAvailableIndex(oListItem.GetFormattedValue(_tableId + "Rows").ToString(), maxrowsnum);
                                            row.ID = _tableId + "Row" + counterId;
                                            if (counterId > 0)
                                            {
                                                for (int i = 0; i < _colNum; i++)
                                                {
                                                    cell = new TableCell();
                                                    //cell.Attributes.Add("width", cellWidth);
                                                    cell.Attributes.Add("style", "padding:4px;direction:" + textDirection + ";");
                                                    // cell.Attributes.Add("padding", "4");
                                                    if (controlTypes[i].ToLower().CompareTo("textbox") == 0)
                                                    {
                                                        cell.Controls.Add(addTextBox(colData[i] + counterId, dataStyle, "", _printControl, false, "", Convert.ToInt32(item_num)));
                                                    }
                                                    if (controlTypes[i].ToLower().CompareTo("multilinetextbox") == 0)
                                                    {
                                                        cell.Controls.Add(addMultiLineTextBox(colData[i] + counterId, dataStyle, "", _printControl, false, "", Convert.ToInt32(item_num)));
                                                    }
                                                    if (controlTypes[i].ToLower().Contains("dropdownlist"))
                                                    {
                                                        cell.Controls.Add(addDropDownList(colData[i] + counterId, dropDownListStyle, controlTypes[i].Split(',')[1], _printControl, "", false, Convert.ToInt32(item_num)));
                                                    }
                                                    if (controlTypes[i].ToLower().CompareTo("date") == 0)
                                                    {
                                                        cell.Controls.Add(addDatePicker(colData[i] + counterId, cssStyle, DateTime.Today.Date, _printControl, false));
                                                    }
                                                    if (controlTypes[i].ToLower().CompareTo("checkBox") == 0)
                                                    {
                                                        cell.Controls.Add(addCheckBox(colData[i] + counterId, checkBoxStyle, false, _printControl, false, Convert.ToInt32(item_num)));
                                                    }
                                                    row.Cells.Add(cell);
                                                }

                                                RemoveRow.CssClass = "DynamicAddButton";
                                                RemoveRow.Click += new EventHandler(this.removeTableRow);
                                                RemoveRow.Text = "X";
                                                RemoveRow.ID = _tableId + counterId;
                                                RemoveRow.Attributes.Add("tableId", _tableId);
                                                RemoveRow.Attributes.Add("index", counterId.ToString());
                                                RemoveRow.Attributes.Add("buttonId", _tableId + counterId);
                                                cell = new TableCell();

                                                cell.Attributes.Add("style", "padding:4px;");
                                                cell.Controls.Add(RemoveRow);
                                                row.Cells.Add(cell);
                                                row.CssClass = rowStyle;
                                                row.Cells.Add(cell);



                                                ((Table)ctl).Rows.Add(row);
                                                int rows = Convert.ToInt32(oListItem.GetFormattedValue(_tableId));
                                                oListItem[_tableId] = rows + 1;
                                                oListItem[_tableId + "Rows"] += counterId + ";";
                                                oListItem.Update();
                                                oList.Update();
                                            }
                                        }
                                    }


                                }
                            }
                        });
                    }

                    catch { }
                }

            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Add row to 'Table' control", ex.Message);
            }
        }

        /// <summary>
        /// Remove table row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void removeTableRow(object sender, EventArgs e)
        {

            string buttonId = ((Button)sender).Attributes["buttonId"];
            string tableId = ((Button)sender).Attributes["tableId"];
            int index = Convert.ToInt32(((Button)sender).Attributes["index"]);
            string rowId = "row-" + buttonId;
            Control table = FindControl(tableId);
            TableRow tableRow = (TableRow)(FindControl(tableId + "Row" + index));

            try
            {
                string url = SPContext.Current.Web.Url.ToString();
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {

                    using (SPSite site = new SPSite(destinationWeb))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList oList = web.Lists[destinationList];
                            SPListItemCollection items = oList.Items;
                            web.AllowUnsafeUpdates = true;
                            foreach (SPListItem oListItem in items)
                            {
                                if (fullUserName.CompareTo(oListItem.GetFormattedValue("userName")) == 0)
                                {
                                    string tableRows = oListItem.GetFormattedValue(tableId + "Rows");
                                    oListItem[tableId] = Convert.ToInt32(oListItem[tableId]) - 1;
                                    oListItem[tableId + "Rows"] = tableRows.Replace(index + ";", "");
                                    oListItem.Update();
                                    foreach (string controlName in colData)
                                    {
                                        foreach (string item in controlTypes)
                                        {
                                            if (item.ToLower().CompareTo("textbox") == 0)
                                            {
                                                oListItem[controlName + index] = "";
                                            }
                                            if (item.ToLower().CompareTo("multilinetextbox") == 0)
                                            {
                                                oListItem[controlName + index] = "";
                                            }
                                            if (item.ToLower().CompareTo("dropdownlist") == 0)
                                            {
                                                oListItem[controlName + index] = "";
                                            }
                                            if (item.ToLower().CompareTo("radiobuttonlist") == 0)
                                            {
                                                oListItem[controlName + index] = "";
                                            }
                                            if (item.ToLower().CompareTo("checkbox") == 0)
                                            {
                                                oListItem[controlName + index] = false;
                                            }
                                            if (item.ToLower().CompareTo("datepicker") == 0)
                                            {
                                                oListItem[controlName + index] = DateTime.Today.Date;
                                            }
                                        }
                                    }
                                    oListItem.Update();
                                    break;
                                }
                            }

                            oList.Update();
                        }
                    }
                });
                foreach (TableCell cell in tableRow.Cells)
                {
                    cell.Controls.Clear();
                }
                ((Table)table).Rows.Remove(tableRow);

            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Remove row from 'Table' control", ex.Message);
            }

        }

        /*     protected void uploadFile(object sender, EventArgs e)
             {
                 string ctrlId = ((Button)sender).Attributes["ID"];
                 foreach (DynamicControl dControl in dynamicAttachmentIds)
                 {
                     try
                     {
                         if (dControl.ID.Equals(ctrlId))
                         {

                             FileUpload _upload = (FileUpload)FindControl(ctrlId);
                             Label _uploadContent = (Label)FindControl(ctrlId + "Label");
                             //If first time page is submitted and we have file in FileUpload control but not in session 
                             // Store the values to SEssion Object 
                             if (_upload.HasFile)
                             {
                                 Session[dControl.ID] = _upload;
                                 _uploadContent.Text = _upload.FileName;

                                 dControl.FileBytes = _upload.FileBytes;
                                 dControl.UploadFilePath = _upload.FileName;


                             }

                             //messageLabel.Text += "<br>attachment id: " + _upload.FileName;
                             if (_upload.HasFile)
                             {
                                // messageLabel.Text += "<br>added:   " + _upload.FileName;


                             }
                             else
                             {
                                 //messageLabel.Text += "<br>No file ";
                             }
                         }

                     }
                     catch (Exception ex)
                     {
                         messageLabel.Text += "<br>Exception: " + ex.ToString();
                     }
                 }

             }
             */




        /// <summary>
        ///  Save form click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            debugLabel.Text += print_OK_Step("Saving Form");
            try
            {
                ViewState["sig"] = ((TextBox)FindControl("Signature")).Text;
               // debugLabel.Text += print_OK_Step("Set view state value to: " + ViewState["sig"]);
            }
            catch(Exception ex)
            {
                //debugLabel.Text += exceptionMessageBuilder("Button click - Set view state" , ex.Message);

                   ViewState["sig"] = "";
            }
            string url = SPContext.Current.Web.Url.ToString();
           // string userName = this.Context.User.Identity.Name.ToString();
            PDF_Obj pdf_obj = null;
            bool validatedCaptcha;
            try
            {
                if (adminMode)
                {
                    validatedCaptcha = true;
                }
                else
                {
                    if (added_captcha)
                    {
                        validatedCaptcha = validateCaptcha();

                    }
                    else
                    {
                        validatedCaptcha = true;

                    }
                }

                if (validate() && validatedCaptcha)
                {
                    debugLabel.Text += print_OK_Step("Validated form - Now saving");
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        using (SPSite site = new SPSite(destinationWeb))
                        {
                            using (SPWeb web = site.OpenWeb())
                            {
                                web.AllowUnsafeUpdates = true;


                                //Fetching the current list
                                SPList oList = web.Lists[destinationList];
                                SPListItemCollection collListItems = oList.Items;
                              //  bool[] items = new bool[destination_items_num];
                             //   items[0] = true; //this item always exists

                                //set other potential items to false
                                for(int i = 1; i< destination_items_num-1;i++)
                                {
                            //        items[i] = false;
                                }
                                

                                    //add relevant items here
                                    SPListItem oListItem = collListItems.Add();
                                oListItem.Update();
                                oList.Update();
                                oListItem["Title"] = ticketNum + oListItem.ID;


                                oListItem.Update();

                                SPFieldUrlValue urlVal = new SPFieldUrlValue();
                                urlVal.Description = ticketNum + oListItem.ID;
                                if (createSubFolder)
                                {
                                    urlVal.Url = destinationWeb + "/" + destinationFolder + "/" + ticketNum + oListItem.ID;
                                    try
                                    {
                                        oListItem["d_folder"] = destinationFolder + "/" + ticketNum + oListItem.ID;
                                    }
                                    catch { }
                                }
                                else
                                {
                                    urlVal.Url = destinationWeb + "/" + destinationFolder;
                                    try
                                    {
                                        oListItem["d_folder"] = destinationFolder;
                                    }
                                    catch { }
                                }

                                try
                                {
                                    oListItem["username"] = fullUserName;
                                    debugLabel.Text += print_OK_Step("Username: " + fullUserName);
                                }
                                catch(Exception ex) { debugLabel.Text += exceptionMessageBuilder("Save username - optional function", ex.Message); }
                                oListItem["folderLink"] = urlVal;
                                oListItem.Update();

                                oList.Update();

                                debugLabel.Text += print_OK_Step("Added new item");

                                try
                                {
                                    foreach (DynamicControl dControl in dynamicTextBoxIDs)
                                    {
                                        TextBox dTextBox = (TextBox)dControl.ControlType;
                                        oListItem[dControl.DataSource] = dTextBox.Text;

                                    }
                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("Save 'Text Box' controls", ex.Message);
                                }

                                try
                                {
                                    foreach (Parameter param in ParametersList)
                                    {
                                        oListItem[param.BindName] = param.Value;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("Save 'Parameter' controls", ex.Message);
                                }

                                try
                                {
                                    foreach (DynamicControl dControl in dynamicFormulaIds)
                                    {
                                        TextBox dTextBox = (TextBox)dControl.ControlType;
                                        oListItem[dControl.DataSource] = dTextBox.Text;

                                    }
                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("Save 'Formula' controls", ex.Message);
                                }


                                try
                                {
                                    foreach (DynamicControl dControl in dynamicDropDownControlIDs)
                                    {
                                        DropDownList dDropDownList = (DropDownList)dControl.ControlType;
                                        oListItem[dControl.DataSource] = dDropDownList.Text;

                                    }

                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("Save 'Drop Down List' controls", ex.Message);
                                }

                                try
                                {
                                    foreach (DynamicControl dControl in dynamicCheckBoxIds)
                                    {
                                        CheckBox dCheckBox = (CheckBox)dControl.ControlType;
                                        oListItem[dControl.DataSource] = ((CheckBox)dCheckBox).Checked ? true : false;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("Save 'Check Box' controls", ex.Message);
                                }

                                try
                                {
                                    foreach (DynamicControl dControl in dynamicRadioButtonIds)
                                    {
                                        RadioButtonList dRadioButtonList = (RadioButtonList)dControl.ControlType;
                                        oListItem[dControl.DataSource] = ((RadioButtonList)dRadioButtonList).SelectedItem.Text;

                                    }
                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("Save 'Radio Button List' controls", ex.Message);
                                }

                                try
                                {
                                    // Save loaded ad data
                                    foreach (DynamicControl dControl in dynamicTextLabelIds)
                                    {

                                        oListItem[dControl.ID] = dControl.Data;

                                    }
                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("Save 'Text Label' controls", ex.Message);
                                }

                                //datePickerIds
                                try
                                {
                                    foreach (DynamicControl dControl in dynamicDatePickerIds)
                                    {
                                        Microsoft.SharePoint.WebControls.DateTimeControl dDatePicker = (Microsoft.SharePoint.WebControls.DateTimeControl)dControl.ControlType;
                                        oListItem[dControl.DataSource] = ((Microsoft.SharePoint.WebControls.DateTimeControl)dDatePicker).SelectedDate;

                                    }
                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("Save 'Date Picker' controls", ex.Message);
                                }

                                try
                                {
                                    bool existColumn = false;
                                    foreach (DynamicControl dLbl in dynamicLabelIds)
                                    {
                                        if (addColumns)
                                        {
                                            for (int a = 0; a < oList.Fields.Count; a++)
                                            {
                                                if (oList.Fields[a].Title.CompareTo(dLbl.ID) == 0)
                                                {
                                                    existColumn = true;

                                                }
                                            }
                                            if (!existColumn)
                                            {
                                                oList.Fields.Add(dLbl.ID, SPFieldType.Text, false);
                                                oList.Update();
                                            }
                                            existColumn = false;
                                        }
                                        Label lbl = (Label)FindControl(dLbl.ID);
                                        oListItem[dLbl.ID] = lbl.Text;

                                    }
                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("Save 'AD Data' controls", ex.Message);
                                }
                                debugLabel.Text += print_OK_Step("Saving PDF");
                                //=================================================================
                                //modify to save PDF in user folder

                                // SPList destFolder = web.Lists[destinationFolder];
                                debugLabel.Text += printInfo(String.Format("destination url: {0}/{1}", destinationWeb, destinationFolder));

                                SPList destFolder = web.GetList(String.Format("{0}/{1}", destinationWeb, destinationFolder));
                                debugLabel.Text += printInfo("got destination folder");
                                if (destFolder == null)
                                {
                                    debugLabel.Text += printInfo("destination folder is null");
                                }
                                SPFolder folder = destFolder.RootFolder;
                                debugLabel.Text += printInfo("got destination root folder");
                                //SPFolder folder = web.GetList web.getFolderByServerRelativeUrl(String.format("{0}/{1}",destinationWeb,destinationFolder));
                                debugLabel.Text += printInfo("Set destination document library");

                                debugLabel.Text += "is_user_folder: " + is_user_folder;
                                if (createSubFolder)
                                {
                                    debugLabel.Text += printInfo("Creating destination sub folder");
                                    //folder = destFolder.RootFolder.SubFolders.Add(ticketNum + oListItem.ID);
                                    //SPListItem item = folder.Item;
                                  //  item["Title"] = ticketNum + oListItem.ID;
                                    debugLabel.Text += printInfo("got doc lib "+ destFolder.RootFolder.Name);
                                    SPDocumentLibrary documentLib = web.Lists.TryGetList(destFolder.RootFolder.Name) as SPDocumentLibrary;
                                    debugLabel.Text += printInfo("got doc lib ");
                                    string FolderPath =  destinationFolder;
                                    debugLabel.Text += printInfo("folder path " + FolderPath);
                                    SPFolder spfolder = web.GetFolder(FolderPath);
                                    debugLabel.Text += printInfo("got sp folder " );
                                    debugLabel.Text += printInfo("relative url "+ spfolder.ServerRelativeUrl);
                                    SPListItem newFolder = documentLib.Items.Add(spfolder.ServerRelativeUrl, SPFileSystemObjectType.Folder,  ticketNum + oListItem.ID);
                                    debugLabel.Text += printInfo("created sub folder");
                                    SPListItem folder_item = null;
                                    try
                                    {
                                        folder_item = spfolder.Item;
                                        folder_item["Title"] = ticketNum + oListItem.ID;
                                    }
                                    catch { }
                                    debugLabel.Text += printInfo("set title");
                                    newFolder.Update();
                                    debugLabel.Text += printInfo("updated  newFolder");
                                    if (!is_user_folder)
                                    {
                                        try
                                        {
                                            folder_item["description"] = createSubFolderTitle(oListItem) + oListItem.ID;
                                        }
                                        catch { }
                                    }
                                    documentLib.Update();
                                    debugLabel.Text += printInfo("updated  documentLib");
                                    try
                                    {
                                        folder_item.Update();
                                    }
                                    catch { }
                                    debugLabel.Text += printInfo("updated  folder_item");
                                   // destFolder.Update();
                                    debugLabel.Text += printInfo("updated  destFolder");
                                    folder.Update();
                                    debugLabel.Text += printInfo("updated  folder");

                                    debugLabel.Text += "<br>Saving files:";

                                    foreach (DynamicControl dControl in dynamicAttachmentIds)
                                    {
                                        // debugLabel.Text += "<br>Counted " + dynamicAttachmentIds.Count + " attachments controls";
                                        try
                                        {
                                            FileUpload _upload = (FileUpload)FindControl(dControl.ID);

                                            debugLabel.Text += "<br>Has file? " + _upload.HasFile;
                                             debugLabel.Text += "<br>Checking " + dControl.ID + " control";
                                            debugLabel.Text += (ViewState[dControl.ID + "fileBytes"] != null) ? "<br>Session has content" : "<br>Session has no content";

                                            if (_upload.HasFile || ViewState[dControl.ID + "fileBytes"] != null)
                                            {
                                                if (_upload.HasFile)
                                                {
                                                    debugLabel.Text += "<br>Uploading from fileUploader";
                                                    folder.Files.Add(destinationWeb + "/" + FolderPath +"/"+ ticketNum + oListItem.ID+ "/" + _upload.FileName, _upload.FileBytes, true);
                                                }
                                                else if (ViewState[dControl.ID + "fileBytes"] != null)
                                                {
                                                    FileUpload sessionUpload = (FileUpload)Session[dControl.ID + "Upload"];

                                                    folder.Files.Add(destinationWeb + "/" + FolderPath + "/" + ticketNum + oListItem.ID +"/" + ViewState[dControl.ID + "fileName"], (byte[])ViewState[dControl.ID + "fileBytes"], true);

                                                }
                                            }
                                            try
                                            {
                                                folder_item.Update();
                                            }
                                            catch { }
                                            folder.Update();

                                        }
                                        catch (Exception ex)
                                        {
                                            debugLabel.Text += exceptionMessageBuilder("Save 'Attachment' controls", ex.Message);
                                        }
                                    }
                                    folder.Update();

                                }
                                //=================================================================


                                oListItem.Update();
                                oList.Update();


                                /// save unique key
                                /// 
                                try {
                                    if (!this.WebPart.Load_Form_With_No_Parameters_In_URL)
                                    {
                                        debugLabel.Text += print_OK_Step("Saving unique key value");
                                        debugLabel.Text += print_OK_Step("list " + unique_key_response_list);
                                        SPList ukey_list = web.Lists[unique_key_response_list];
                                        SPListItemCollection ukey_list_collection = ukey_list.Items;
                                        SPListItem ukey = ukey_list_collection.Add();
                                        ukey.Update();
                                        ukey_list.Update();
                                        ukey["Title"] = unique_key_value;
                                        ukey["item_id"] = unique_item_id;
                                        ukey.Update();
                                        ukey_list.Update();
                                        debugLabel.Text += print_OK_Step("done");
                                    }
                                }
                                catch(Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("Save unique key", ex.Message);

                                }
                                try
                                {
                                    pdfEmailFieldValue = oListItem.GetFormattedValue(pdfEmailField);
                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("Get 'SendTo' field value", ex.Message);
                                }
                                //labelMessage
                                try
                                {
                                    setSuccessMessageStyle(finalMessage);

                                }
                                catch (Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("Set Message to: '" + messageLabelId + "' control", ex.Message);
                                }

                                try
                                {
                                    if (createPDF)
                                    {
                                        ControlsToPdf printer = new ControlsToPdf(repeatingTableIdsForm,
                                                                                  repeatingTableIds,
                                                                                  dynamicTextBoxIDs,
                                                                                  dynamicDropDownControlIDs,
                                                                                  dynamicLabelIDs,
                                                                                  dynamicCheckBoxIds,
                                                                                  dynamicRadioButtonIds,
                                                                                  dynamicDatePickerIds,
                                                                                  dynamicRepeatingTableIds,
                                                                                  _dataPath,
                                                                                  _configPath,
                                                                                  _headerTag,
                                                                                  _labelTag,

                                                                                  textDataFontSize,
                                                                                  labelFontSize,
                                                                                  headerFontSize,
                                                                                  docHeaderFontSize);
                                        folder.Update();
                                        // debugLabel.Text += "<br />Create pdf: " + createSubFolder;
                                        if (createSubFolder)
                                        {
                                            debugLabel.Text += print_OK_Step("pdf path: " + destinationFolder + "/" + ticketNum + oListItem.ID);
                                            
                                            if (adminMode)
                                            {

                                                pdf_obj = printer.loadXML(filePath, fileName, this.WebPart.fontName.Replace(" ", string.Empty), destinationList, textDirection, fullUserName, destinationWeb, destinationFolder + "/" + ticketNum + oListItem.ID, destinationList, ticketNum + oListItem.ID, is_user_folder, docTypeList, docTypeColumn, docType);
                                                folder.Update();

                                                oListItem.Update();
                                                oList.Update();
                                                ((Button)sender).Visible = false;
                                                debugLabel.Text += print_OK_Step("Done saving item");
                                            }
                                            else
                                            {
                                                pdf_obj = printer.loadXML(filePath, fileName, this.WebPart.fontName.Replace(" ", string.Empty), destinationList, textDirection, fullUserName, destinationWeb, destinationFolder + "/" + ticketNum + oListItem.ID,  destinationList, ticketNum + oListItem.ID, is_user_folder, docTypeList, docTypeColumn, docType);
                                                folder.Update();

                                                oListItem.Update();
                                                oList.Update();
                                                ((Button)sender).Visible = false;
                                                debugLabel.Text += print_OK_Step("Done saving item");
                                            }

                                        }
                                        else
                                        {

                                            pdf_obj = printer.loadXML(filePath, fileName, this.WebPart.fontName.Replace(" ", string.Empty), destinationList, textDirection, fullUserName,  destinationWeb, destinationFolder, destinationList, ticketNum + oListItem.ID, is_user_folder, docTypeList, docTypeColumn, docType);
                                        }
                                        folder.Update();
                                        oListItem.Update();
                                        oList.Update();
                                        ((Button)sender).Visible = false;
                                    }
                                    if (!adminMode)
                                    {
                                        if (this.WebPart.Download_Pdf)
                                        {
                                            /* MemoryStream ms = new MemoryStream(pdf_obj._pdf_bytes);
                                             HttpContext.Current.Response.ContentType = "application/pdf";
                                             HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + pdf_obj.File_Name + ".pdf");
                                             HttpContext.Current.Response.Buffer = true;
                                             ms.WriteTo(HttpContext.Current.Response.OutputStream);*/
                                            try
                                            {

                                                DownloadPDF_Button.Text = pdfDownloadButtonText;
                                                DownloadPDF_Button.Visible = true;
                                                DownloadPDF_Button.Attributes.Add("pdf_bytes", Convert.ToBase64String(pdf_obj._pdf_bytes));
                                                DownloadPDF_Button.Attributes.Add("pdf_name", pdf_obj.File_Name);
                                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "sendClick('" + modalError.ClientID + "','" + finalMessage + "');", true);

                                                if (this.WebPart.Send_Pdf_Attachment)
                                                {
                                                    SendMailWithAttachment(pdfEmailFieldValue, pdf_obj._pdf_bytes, pdf_obj.File_Name + ".pdf");
                                                }


                                            }
                                            catch (Exception ex)
                                            {

                                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "exception", string.Format("alert('{0}');", ex.Message), true);
                                                debugLabel.Text += exceptionMessageBuilder("Download PDF", ex.Message);
                                            }
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CloseWaitDialog", "closeWait();", true);
                                            if (redirectPage.Contains("?"))
                                            {
                                                
                                                Response.Redirect(redirectPage + "&formId=" + ticketNum + oListItem.ID, false);

                                            }
                                            else
                                            {
                                                Response.Redirect(redirectPage + "?formId=" + ticketNum + oListItem.ID, false);

                                            }
                                        }


                                    }
                                    else
                                    {
                                        try
                                        {
                                            debugLabel.Text +="<br> PDF LOG:<br>"+ pdf_obj._pdf_log;
                                            try
                                            {
                                                debugLabel.Text += "PDF byte[] length = " + pdf_obj._pdf_bytes.Length;
                                            }
                                            catch (Exception ex)
                                            {
                                                debugLabel.Text += exceptionMessageBuilder("PDF byte[] size", ex.Message);
                                            }
                                            DownloadPDF_Button.Text = pdfDownloadButtonText;
                                            DownloadPDF_Button.Visible = true;
                                            DownloadPDF_Button.Attributes.Add("pdf_bytes", Convert.ToBase64String(pdf_obj._pdf_bytes));
                                            DownloadPDF_Button.Attributes.Add("pdf_name", pdf_obj.File_Name);
                                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "sendClick('" + modalError.ClientID + "','" + finalMessage + "');", true);

                                            SendMailWithAttachment(pdfEmailFieldValue, pdf_obj._pdf_bytes, pdf_obj.File_Name + ".pdf");

                                            /*   if (this.WebPart.Download_Pdf && doneSaving)
                                               {
                                                   debugLabel.Text += print_OK_Step("Downloading PDF");
                                                   MemoryStream ms = new MemoryStream(pdf_obj._pdf_bytes);
                                                   HttpContext.Current.Response.ContentType = "application/pdf";
                                                   HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + pdf_obj.File_Name + ".pdf");
                                                   HttpContext.Current.Response.Buffer = true;
                                                   ms.WriteTo(HttpContext.Current.Response.OutputStream);
                                               }*/
                                        }
                                        catch (Exception ex)
                                        {
                                            debugLabel.Text += exceptionMessageBuilder("Download PDF", ex.Message);
                                        }
                                    }
                                }

                                catch (Exception ex)
                                {
                                    debugLabel.Text += exceptionMessageBuilder("Create PDF: '" + messageLabelId + "' control", ex.Message);
                                }
                            }
                        }
                    });
                    ((Button)sender).Visible = false;
                }
                else
                {
                    debugLabel.Text += "<br> validation failed";
                    debugLabel.Text += "<br>MessageLabelId: " + messageLabelId;
                    debugLabel.Text += "<br>Missing data text: " + missingDataMessage;
                    setFailedMessageStyle(missingDataMessage);
                }
                Session.Clear();
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Saving item & creating PDF: '" + messageLabelId + "' control", ex.Message);

            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uploadToSession(object sender, EventArgs e)
        {
            string uploadControl = ((Button)sender).Attributes["controlId"];
            string _fileName = ((Button)sender).Attributes["fileName"];
            string _maxFileSize = ((Button)sender).Attributes["fileSize"];

            try
            {
                string tempFileName = ((FileUpload)FindControl(uploadControl)).PostedFile.FileName;
                if (((FileUpload)FindControl(uploadControl)).HasFile)
                {
                    if (Convert.ToInt32(_maxFileSize) >= ((FileUpload)FindControl(uploadControl)).PostedFile.ContentLength)
                    {
                        ViewState[uploadControl + "fileBytes"] = ((FileUpload)FindControl(uploadControl)).FileBytes;
                        Page.ClientScript.RegisterHiddenField(uploadControl + "fileBytes", "yes");
                        if (_fileName.Length > 0)
                        {
                            ViewState[uploadControl + "fileName"] = _fileName + "." + ((FileUpload)FindControl(uploadControl)).PostedFile.FileName.Split('.')[1]; //  ((FileUpload)FindControl(uploadControl)).FileName;
                        }
                        else
                        {
                            string[] fName = ((FileUpload)FindControl(uploadControl)).FileName.Split('/');
                            ViewState[uploadControl + "fileName"] = fName[fName.Length - 1];
                            debugLabel.Text += "Attachment " + uploadControl + " file name: " + fName[fName.Length - 1];
                        }

                        Session[uploadControl + "Upload"] = ((FileUpload)FindControl(uploadControl));
                        ((FileUpload)FindControl(uploadControl)).CssClass = dataStyle;
                        Session[uploadControl + "UploadedFiles"] = ((FileUpload)FindControl(uploadControl)).FileName;
                        Session[uploadControl + "UploadedFilesBytes"] = ((FileUpload)FindControl(uploadControl)).FileBytes;
                        ((Label)FindControl(uploadControl + "Label")).Text = ((FileUpload)FindControl(uploadControl)).FileName;
                        ((Label)FindControl(uploadControl + "Error")).Text = string.Empty;
                    }
                    else
                    {

                        ((Label)FindControl(uploadControl + "Error")).Text = "File size exeeds maximum size.";
                    }
                }
                validate();
            }
            catch
            {
                ((Label)FindControl(uploadControl + "Error")).Text = "Error in file upload.";

            }

        }

        /*
        protected void Upload()

        {
            //save attachment
            if (true)
            {
                try
                {



                    debugLabel.Text += "<br>saving attachment";
                        foreach (DynamicControl dControl in dynamicAttachmentIds)
                        {
                           // messageLabel.Text += "<br>attachment id: " + dControl;
                            FileUpload _upload = (FileUpload)FindControl(dControl.ID);
                            if (_upload.HasFile)
                            {
                               // _upload.SaveAs("c:\"" + _upload.FileName + "-Temp");

                                debugLabel.Text += "<br>" + _upload.FileBytes.Length.ToString();
                                _upload.PostedFile.SaveAs(@"C:\\Temp\\" + _upload.FileName);




                                //oListItem.Update();
                                debugLabel.Text += "<br>Done";
                                // oListItem[dControl.DataSource] = ((Microsoft.SharePoint.WebControls.DateTimeControl)dDatePicker).SelectedDate;
                            }
                            else
                            {
                                debugLabel.Text += "<br>No file";
                            }
                        }


                        
                        
                }
                catch (Exception ex)
                {
                    debugLabel.Text += exceptionMessageBuilder("Attachment Upload", ex.Message);
                    
                }
            }

        }
         */

        /// <summary>
        /// Create Directory Entry
        /// </summary>
        /// <param name="_ldap"></param>
        /// <returns></returns>
        public static DirectoryEntry GetDirectoryEntry(String _ldap)
        {

            DirectoryEntry de = new DirectoryEntry(_ldap);
            try
            {
                using (HostingEnvironment.Impersonate())
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {

                        de.Path = _ldap;
                        de.AuthenticationType = AuthenticationTypes.None;
                    });


                }
            }
            catch { }
            return de;
        }


        /// <summary>
        /// Load AD properties
        /// </summary>
        protected void loadAD()
        {
            debugLabel.Text += print_OK_Step("<br>");

            debugLabel.Text += printInfoTitle("Active Directory Configurations");

            SearchResult res = null;
            //DirectoryEntry resultEntry = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (HostingEnvironment.Impersonate())
                    {
                        //string dm = domain + "\\";
                        //string userName = userName = this.Context.User.Identity.Name.Split('\\')[1];
                        //string userDomain = this.Context.User.Identity.Name.Split('\\')[0];
                        string __ldap = (userDomain.ToLower().Equals(domain.ToLower())) ? this.WebPart.ldap : this.WebPart.ccLdap;

                        debugLabel.Text += printInfo("<b>LDAP</b>: " + __ldap);
                        debugLabel.Text += printInfo("<b>Current user</b>: " + userName);
                        debugLabel.Text += printInfo("<b>Current user domain</b>: " + userDomain);
                        debugLabel.Text += printInfo("---------------------------------<br />");
                        //userName = userName.Replace(dm, string.Empty);
                        DirectoryEntry entry = GetDirectoryEntry(__ldap);
                        DirectorySearcher search = new DirectorySearcher(entry);
                        search.Filter = "(&(objectCategory=Person)(objectClass=user)(SAMAccountName=" + userName + "))";
                        search.PropertiesToLoad.Add("employeeID");              //ID
                        search.PropertiesToLoad.Add("givenName");              //first name
                        search.PropertiesToLoad.Add("sn");                     //surname
                        search.PropertiesToLoad.Add("mail");                   //email
                        search.PropertiesToLoad.Add("telephoneNumber");        //work phone
                        search.PropertiesToLoad.Add("homePhone");              //homePhone
                        search.PropertiesToLoad.Add("mobile");                 //cell phone
                        search.PropertiesToLoad.Add("extensionAttribute5");    //FirstNameHe
                        search.PropertiesToLoad.Add("extensionAttribute6");    //LastNameHe
                        search.PropertiesToLoad.Add("description");            //Description
                        res = search.FindOne();

                        //   debugLabel.Text += "<br />Loading " + userDomain + " user from: " +__ldap;
                        if (userDomain.ToLower().Contains(domain))
                        {

                            loadStandartUserProperties(res);
                        }
                        else
                        {
                            loadCCUserProperties(res);
                        }

                    }
                });
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Loaing user AD object", ex.Message);
            }

        }

        /// <summary>
        /// Load savion domain user properties
        /// </summary>
        /// <param name="__res"></param>
        protected void loadCCUserProperties(SearchResult __res)
        {
            DirectoryEntry __dRes = __res.GetDirectoryEntry();
            if (!(__res == null)) // if found a result
            {
                try
                {
                    firstName = __dRes.Properties["displayName"].Value.ToString().Split(' ')[0];

                }
                catch { }
                try
                {
                    lastName = __dRes.Properties["displayName"].Value.ToString().Split(' ')[1];
                }
                catch { }
                try
                {
                    email = __dRes.Properties["mail"].Value.ToString();  //email
                }
                catch { }
                try
                {
                    workPhone = __dRes.Properties["telephoneNumber"].Value.ToString();  //work phone
                }
                catch { }
                try
                {
                    homePhone = __dRes.Properties["homePhone"].Value.ToString(); //home phone
                }
                catch { }
                try
                {
                    mobilePhone = __dRes.Properties["mobile"].Value.ToString(); //cell phone
                }
                catch { }

                try
                {
                    employeeId = __dRes.Properties["employeeID"].Value.ToString();  //ID
                }
                catch { }
                try
                {
                    firstNameHe = __dRes.Properties["sn"].Value.ToString().Split(' ')[0];  //ID
                }
                catch { }
                try
                {
                    lastNameHe = __dRes.Properties["sn"].Value.ToString().Split(' ')[1];  //ID
                }
                catch { }
                //ad_description
                try
                {
                    ad_description = __dRes.Properties["description"].Value.ToString();  //description
                }
                catch { }
            }
        }


        /// <summary>
        /// Loads standart user properties
        /// </summary>
        /// <param name="__res">Active Directory Object</param>
        protected void loadStandartUserProperties(SearchResult __res)
        {
            DirectoryEntry __dRes = __res.GetDirectoryEntry();
            if (!(__res == null)) // if found a result
            {
                debugLabel.Text += "<br />Found User in AD";
                try
                {
                    firstName = __dRes.Properties["givenName"].Value.ToString();   //first name
                    firstName.ToLower();
                    char[] fname = firstName.ToCharArray();
                    fname[0] = Convert.ToChar(fname[0].ToString().ToUpper());
                    firstName = string.Empty;
                    foreach (char letter in fname)
                    {
                        firstName += letter;
                    }

                }
                catch { }
                try
                {
                    lastName = __dRes.Properties["sn"].Value.ToString();    //surname
                    lastName.ToLower();
                    char[] lname = lastName.ToCharArray();
                    lname[0] = Convert.ToChar(lname[0].ToString().ToUpper());
                    lastName = "";
                    foreach (char letter in lname)
                    {
                        lastName += letter;
                    }
                }
                catch { }
                try
                {
                    email = __dRes.Properties["mail"].Value.ToString();  //email
                }
                catch { }
                try
                {
                    workPhone = __dRes.Properties["telephoneNumber"].Value.ToString();  //work phone
                }
                catch { }
                try
                {
                    homePhone = __dRes.Properties["homePhone"].Value.ToString(); //home phone
                }
                catch { }
                try
                {
                    mobilePhone = __dRes.Properties["mobile"].Value.ToString(); //cell phone
                }
                catch { }

                try
                {
                    employeeId = __dRes.Properties["employeeID"].Value.ToString();  //ID
                }
                catch { }
                try
                {
                    firstNameHe = __dRes.Properties["extensionAttribute5"].Value.ToString();  //ID
                }
                catch { }
                try
                {
                    lastNameHe = __dRes.Properties["extensionAttribute6"].Value.ToString();  //ID
                }
                catch { }
                try
                {
                    ad_description = __dRes.Properties["description"].Value.ToString();  //description
                }
                catch { }
            }
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

            __message += "<div style = \"color:red; text-align: left;\">";
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

            __message += "<div style = \"color:#3d557c; text-align: left;\">";
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

            __message += "<div style = \"color:#3d557c; text-align: left;\">";
            __message += _content;
            __message += "</div>";
            return __message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_content"></param>
        /// <returns></returns>
        protected string print_OK_Step(string _content)
        {
            string __message = string.Empty;

            __message += "<div style = \"color:#4d875f; text-align: left;XML COnfigurations\">";
            __message += _content;
            __message += "</div>";
            return __message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DownloadPDF_Button_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CloseWaitDialog", "closeWait();", true);
                var button = sender as Button;

                byte[] bytes = Convert.FromBase64String(button.Attributes["pdf_bytes"].ToString());
                //byte[] bytes = (byte[])vstate["pdf_bytes"];
                string fileName = button.Attributes["pdf_name"].ToString();
                //string fileName = vstate["pdf_name"].ToString();
                MemoryStream ms = new MemoryStream(bytes);
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
                HttpContext.Current.Response.Buffer = true;
                ms.WriteTo(HttpContext.Current.Response.OutputStream);

            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Download PDF Button Clicked", ex.Message);
            }
        }



        protected void Download_file_Button_Click(object sender, EventArgs e)
        {
            try
            {
                // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CloseWaitDialog", "closeWait();", true);
                debugLabel.Text += print_OK_Step("downloading file");
                Button button = sender as Button;
                string bytes_string = button.Attributes["file_bytes"].ToString();
                byte[] bytes = Convert.FromBase64String(bytes_string);
                string fileName = button.Attributes["file_name"].ToString();
                string file_url = button.Attributes["file_url"].ToString();
                debugLabel.Text += "<br />File URL -> " + file_url;
                debugLabel.Text += "<br />File Name -> " + fileName;
                debugLabel.Text += "<br />Bytes count -> " + bytes.Length;

              
                WebClient myWebClient = new WebClient();
                           
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                myWebClient.DownloadFile("https://gss2.ekmd.huji.ac.il/home/general/GEN15-2018/EKMDInbalGo", "applicationForm.pdf");
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Download file Button Clicked", ex.ToString());
            }
        }


        private StateBag acquireViewStateFromPage(Page page)
        {
            var pageType = typeof(Page);
            var viewStatePropertyDescriptor = pageType.GetProperty("ViewState", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            return (StateBag)viewStatePropertyDescriptor.GetValue(page, null);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            
            try
            {      

                vstate = acquireViewStateFromPage(this.Page);

                //var image = ((Image)FindControl("signature_img"));
                //if (image != null)
                //{
                //    ((Image)FindControl("signature_img")).Attributes["src"] = ViewState["sig"].ToString();
                //}
                //debugLabel.Text += printInfoTitle("Updating signature image: " + ViewState["sig"].ToString());
            }
            catch (Exception ex)
            {
                debugLabel.Text += exceptionMessageBuilder("Pre render", ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendTo"></param>
        /// <param name="attachment_bytes"></param>
        /// <param name="attachment_name"></param>
        protected void SendMailWithAttachment(string sendTo, byte[] attachment_bytes, string attachment_name)
        {
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    mail.From = new System.Net.Mail.MailAddress("do-not-reply@ekmd.huji.ac.il");
                    mail.To.Add(sendTo.Trim());
                    mail.Subject = pdfMailSubject;
                    mail.Body = pdfMailBodyHtml;
                    mail.IsBodyHtml = true;
                    MemoryStream ms = new MemoryStream(attachment_bytes);
                    Attachment attach = new Attachment(ms, attachment_name);
                    mail.Attachments.Add(attach);


                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServer);
                    smtp.Send(mail);
                });

            }
            catch (Exception ex)
            {
                debugLabel.Text += "<br>Send mail with attachment: " + ex.Message;
            }
        }

    }
}
