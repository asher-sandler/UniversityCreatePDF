
<%@ Assembly Name="Survey_Form, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2965031e2ab099fa" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VisualWebPart1UserControl.ascx.cs" Inherits="Survey_Form.VisualWebPart1.VisualWebPart1UserControl" %>





<SharePoint:CssRegistration ID="CssRegistration1" Name="/Style Library/Survey_Form/Survey_Form.css" After="corev4.css" runat="server"></SharePoint:CssRegistration>

<script src='https://www.google.com/recaptcha/api.js'></script>
<script src="https://www.google.com/recaptcha/api.js?onload=renderRecaptcha&render=explicit" async defer></script>

<script src="/Style Library/Survey_Form/signature/js/signature_pad.umd.js"></script>
 <script src="/Style Library/Survey_Form/signature/js/app.js"></script>


<%-- <script src="/Style Library/Survey_Form/signature/js/signature_pad.umd.js"></script>
  <script src="/Style Library/Survey_Form/signature/js/app.js"></script>  --%>
   
<meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">


<script type="text/javascript">
    

    var modal = document.getElementById('EditItemModal');
    var spanX = document.getElementById("closeElement");
    // When the user clicks on <span> (x), close the modal

    function closeModal() {
        var modal = document.getElementById('EditItemModal');
        modal.style.display = "none";
        return false;
    }




    // When the user clicks anywhere outside of the modal, close it
   window.onclick = function (event) {
        //alert('window clicked');
        var modal = document.getElementById('EditItemModal');
        if (event.target == modal) {

           modal.style.display = "none";
        }
    }

    function sendClick(errorLabelID, message) {
        var modal = document.getElementById('EditItemModal');
        document.getElementById(errorLabelID).innerHTML = message;
        modal.style.display = "block";
    }

    function signatureClick() {
        var modal = document.getElementById('modal-signature');
        
        // modal.style.display = "block";
        modal.style.visibility = "visible";
    }

    function sendDone() {
        var modal = document.getElementById('EditItemModal');
        modal.style.display = "none";
    }

var waitDialog;

 function closeWait() {
     var modal = document.getElementById('EditItemModal');
    
           modal.style.display = "none";
    
 }
 
 



 function validateRequiredRBL(controlID, validCssClass, invalidCssClass) {
     try {
         var content = document.getElementById(controlID);
         content.className = validCssClass;
     }
     catch (err) {
           
     }


 }

 function validateRequired(controlID, validCssClass, invalidCssClass) {
     try {
         var content = document.getElementById(controlID).value;
         var obj = document.getElementById(controlID);
         if (content.length > 0) {
             obj.className = validCssClass;
             obj.value = obj.value;
         }
         else {
             obj.className = invalidCssClass;
             obj.value = obj.value;

         }
     }
     catch (err) {
         alert(err); 
     }


 }

 function validateRequiredDDL(controlID, validCssClass, invalidCssClass) {
     try {
         var content = document.getElementById(controlID).value;
         var obj = document.getElementById(controlID);
         if (content.length > 0 && content!='-') {
             obj.className = validCssClass;
         }
         else {
             obj.className = invalidCssClass;
         }
     }
     catch (err) {
            
     }


 }
 function validateRequiredCheckBox(controlID, validCssClass, invalidCssClass) {
     try {
           
         var obj = document.getElementById(controlID);
         var div_obj = document.getElementById(controlID+'_div');
         if (obj.checked) {
             //obj.className = validCssClass;
             div_obj.className = validCssClass;
         }
         else {
             // obj.className = invalidCssClass;
             div_obj.className = invalidCssClass;
         }
     }
     catch (err) {
         alert(err);
     }


 }

 function validateRequiredAttachment(controlID,attachmentState, validCssClass, invalidCssClass) {
     try {

         var obj = document.getElementById(controlID);
         var attachmentStateObj = document.getElementById(attachmentState);
         if (obj.value == '' && attachmentStateObj =='yes') {
             obj.className = validCssClass;
         }
         else {
            
             obj.className = invalidCssClass;
         }
     }
     catch (err) {
         alert(err);
     }


 }

    
   
 function validateRequiredMultiLine(controlID, validCssClass, invalidCssClass) {
     try {
         var content = document.getElementById(controlID).value;
         var obj = document.getElementById(controlID);
            
         if (content.length > 0) {
             obj.style = validCssClass;
         }
         else {
             obj.style = invalidCssClass;
         }
     }
     catch (err) {

     }


 }

 function validateRegExTextBox(controlID, validCssClass, invalidCssClass,_regex) {
     try {
         //  var obj = document.getElementById(controlID);
           
         //  var valid = obj.value.test(_regex);
         // if (valid) {
         //    obj.setAttribute('class', validCssClass);                 
         // }
         // else {
         //    obj.setAttribute('class',invalidCssClass);
         //}

         var content = document.getElementById(controlID).value;
         var obj = document.getElementById(controlID);

         var RegExObj = new RegExp(_regex);
         var numOfOccur = content.match(RegExObj);

           

         if (!(numOfOccur == null)) {
              
             obj.className = validCssClass;
                
         }
         else {
             obj.className = invalidCssClass;
         }
     }
     catch (err) {
         alert(err);
     }


 }
 
 function Download_files(controlID, fileBytes, file_name) {
     try{
         var byteArray = base64ToByteArray(fileBytes);
         var blob = new Blob([byteArray], { type: "application/pdf" });

         var file = new File([blob], file_name);
         window.open(window.URL.createObjectURL(file));
     
         alert(window.URL.createObjectURL(blob));
        // alert('ok');
     }
     catch (err) {
         alert('Error: ' + err);
     }
 }

 function base64ToByteArray(base64String) {
     try {
         var sliceSize = 1024;
         var byteCharacters = atob(base64String);
         var bytesLength = byteCharacters.length;
         var slicesCount = Math.ceil(bytesLength / sliceSize);
         var byteArrays = new Array(slicesCount);

         for (var sliceIndex = 0; sliceIndex < slicesCount; ++sliceIndex) {
             var begin = sliceIndex * sliceSize;
             var end = Math.min(begin + sliceSize, bytesLength);

             var bytes = new Array(end - begin);
             for (var offset = begin, i = 0; offset < end; ++i, ++offset) {
                 bytes[i] = byteCharacters[offset].charCodeAt(0);
             }
             byteArrays[sliceIndex] = new Uint8Array(bytes);
         }
         return byteArrays;
     } catch (e) {
         alert("Couldn't convert to byte array: " + e);
         return undefined;
     }
 }

 </script>

<style>
     .modal {
    visibility:hidden;/* Hidden by default */
    position: fixed; /* Stay in place */
    z-index: 1; /* Sit on top */
    left: 0;
    top: 0;
    width: 100%; /* Full width */
    height: 100%; /* Full height */
    overflow: auto; /* Enable scroll if needed */
    background-color: rgb(0,0,0); /* Fallback color */ 
    background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
    border:none !important;    
    /*background-color: rgb(256,256,256);*/
    /*background-color: rgba(256,256,256,0.4);*/
}

/* Modal Content/Box */
.modal-content { 
   /* border-radius: 25px;*/
    /*background-color: #fefefe;*/
    border:none !important;
    margin: 20% auto; /* 15% from the top and centered */
    padding: 20px;
   /* border: 1px solid #888;*/
    width: 50%; /* Could be more or less, depending on screen size */
    font-size:large;
    vertical-align:central
}

.modal-signature { 
   /* border-radius: 25px;*/
    /*background-color: #fefefe;*/
    border:none !important;
    margin: 20% auto; /* 15% from the top and centered */
    padding: 20px;
   /* border: 1px solid #888;*/
    width: 50%; /* Could be more or less, depending on screen size */
    font-size:large;
    vertical-align:central
}

/* The Close Button */
.close {
    color: #aaa;
    float: right;
    font-size: 20px;
    font-weight: bold;
}

.close:hover,
.close:focus {
    color: black;
    text-decoration: none;
    cursor: pointer;
}

    .modalHeader {
        background-color:#FFA533;
        height:51px;
border:none !important;
    }
       .modalMessage {
           
           border:none !important;
       }
       .messageLocation {
             font-size:20pt;
           font-family:    "Segoe UI Light","Segoe UI","Segoe",Tahoma,Helvetica,Arial,sans-serif !important;
           position: relative;
            top: 50%;
            transform: translateY(-50%); 
           text-align:center;
           color:#fff !important;
       }

       .modalProgress {
           background-image:url("/Style Library/DynamicForm/loading.gif");
          
         border:none !important;
         height:252px; 
         width:441px;
          display: block;
          text-align:center;   
                 
       }
        .modalProgressDiv {         
          width:100%;
          padding:0 20%;               
          border:none !important;
       }
   
  

</style>


<link rel="stylesheet" href="/Style Library/Survey_Form/signature/css/signature-pad.css">

 






   <div id="EditItemModal" class="modal">

  <!-- Modal content -->
  <div class="modal-content">
    <!--span class="close" ><asp:Label ID="closeElement" Text="x" runat="server"></asp:Label></span-->
      
     
      <div class="messageLocation" ><asp:Label ID="modalError" runat="server" CssClass="modalMessage"></asp:Label> </div>
      <br />
      <div>
          <asp:Button ID="modalClose" runat="server" Text="Close" CssClass="SendButton"  Visible="False"  /></div>
      <asp:Button ID="DownloadPDF_Button" runat="server" Text="Download PDF & Close" CssClass="DownloadButton"  Visible="False" OnClick="DownloadPDF_Button_Click"  /></div>
  </div>
<asp:HiddenField  runat="server" id="sigField" ViewStateMode="Enabled" />

<div id="modal-signature" class="modal">
    <div class="modal-content">
        <div class="sig_body">
     <div id="signature-pad" class="signature-pad">
    <div class="signature-pad--body">
      <canvas></canvas>
    </div>
    <div class="signature-pad--footer">
      <div class="description">Sign above</div>

      <div class="signature-pad--actions">
        <div>
          <button type="button" class="button clear" data-action="clear">Clear</button>
        

        </div>
        <div>
             <%--<button type="button"  class="button save" onclick="signature_cancel();" runat="server" id="cancel">Cancel</button>--%>
          <button type="button"  class="button save" runat="server" id="export_png">Ok</button>
         
        </div>
      </div>
    </div>
  </div>
 <script src="/Style Library/Survey_Form/signature/js/signature_pad.umd.js"></script>
 <script src="/Style Library/Survey_Form/signature/js/app.js"></script>
</div>
  </div>
     </div>

<asp:UpdatePanel ID="MessagePanel1" runat="server">
    <ContentTemplate>
        <div>
             <asp:Label ID="Start_End_Label" runat="server" Visible="False"></asp:Label>
         </div>
         <asp:Label ID="messageLabel" runat="server"></asp:Label>
         <br />
         <asp:Label ID="debugLabel" runat="server" Visible="false"></asp:Label>
        <div class="start_end_message_label_div"><asp:Label ID="Start_End_Message_Label" runat="server" CssClass="start_end_message_label"></asp:Label></div>
    
    
    
    
    </ContentTemplate>
</asp:UpdatePanel>

