using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Web.Hosting;
using Microsoft.SharePoint;



namespace Survey_Form
{
    class ADLoader
    {
        DirectoryEntry _de;
        UserProperties _up;
        string _domain;
        public ADLoader(string ldap, string domain)
        {
            _de = GetDirectoryEntry(ldap);
            _up = new UserProperties();
            _domain = domain;
        }

        public ADLoader(string ldap, string user, string pass, string domain)
        {
            _de = GetDirectoryEntry(ldap, user, pass);
            _up = new UserProperties();
            _domain = domain;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ldap">LDAP string</param>
        /// <returns>DirectoryEntry</returns>
        private static DirectoryEntry GetDirectoryEntry(String ldap)
        {

            DirectoryEntry de = new DirectoryEntry(ldap);
            try
            {
                using (HostingEnvironment.Impersonate())
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {

                        de.Path = ldap;
                        de.AuthenticationType = AuthenticationTypes.None;
                    });


                }
            }
            catch { }
            return de;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ldap">LDAP string</param>
        /// <param name="_adUser">String Username</param>
        /// <param name="_adPassword">String password</param>
        /// <returns>DirectoryEntry</returns>
        private static DirectoryEntry GetDirectoryEntry(String ldap, string _adUser, string _adPassword)
        {
            DirectoryEntry de = new DirectoryEntry(ldap, _adUser, _adPassword);
            using (HostingEnvironment.Impersonate())
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    de.Path = ldap;
                    de.AuthenticationType = AuthenticationTypes.None;
                });
            }
            return de;
        }


        /// <summary>
        /// Returns user object
        /// </summary>
        /// <param name="_userName"></param>
        /// <returns>SearchResult user object</returns>
        private SearchResult getUserObject(string _userName)
        {
            SearchResult result = null;
            DirectorySearcher search = new DirectorySearcher(_de);
            search.Filter = "(&(objectCategory=Person)(objectClass=user)(SAMAccountName=" + _userName + "))";
            search.PropertiesToLoad.Add("employeeID");              //ID
            search.PropertiesToLoad.Add("givenName");              //first name
            search.PropertiesToLoad.Add("sn");                     //surname
            search.PropertiesToLoad.Add("mail");                   //email
            search.PropertiesToLoad.Add("telephoneNumber");        //work phone
            search.PropertiesToLoad.Add("homePhone");              //homePhone
            search.PropertiesToLoad.Add("mobile");                 //cell phone
            search.PropertiesToLoad.Add("extensionAttribute5");    //FirstNameHe
            search.PropertiesToLoad.Add("extensionAttribute6");    //LastNameHe
            return result;
        }


        /// <summary>
        /// Loads AD properties in Savion domain
        /// </summary>
        /// <param name="userObject"></param>
        protected void updateUserProperties(SearchResult userObject)
        {

            if (!(userObject == null)) // if found a result
            {
                {// Get first name - makes sure that the first letter is upper case, and all the other lower cars
                    _up.FirstName = userObject.Properties["givenName"][0].ToString();   //first name
                    _up.FirstName.ToLower();
                    char[] fname = _up.FirstName.ToCharArray();
                    fname[0] = Convert.ToChar(fname[0].ToString().ToUpper());
                    _up.FirstName = string.Empty;
                    foreach (char letter in fname)
                    {
                        _up.FirstName += letter;
                    }
                }

                {// Get last name - makes sure that the first letter is upper case, and all the other lower cars
                    _up.LastName = userObject.Properties["sn"][0].ToString();    //surname
                    _up.LastName.ToLower();
                    char[] lname = _up.LastName.ToCharArray();
                    lname[0] = Convert.ToChar(lname[0].ToString().ToUpper());
                    _up.LastName = string.Empty;
                    foreach (char letter in lname)
                    {
                        _up.LastName += letter;
                    }
                }
                _up.FirstNameHe = userObject.Properties["extensionAttribute5"][0].ToString(); // first name in Hebrew
                _up.LastNameHe = userObject.Properties["extensionAttribute6"][0].ToString(); // lasr name in Hebrew
                _up.Email = userObject.Properties["mail"][0].ToString();  //email
                _up.EmployeeID = userObject.Properties["employeeID"][0].ToString(); // TZ
                _up.Phone = userObject.Properties["telephoneNumber"][0].ToString(); //Phone Number   


            }

        }

        /// <summary>
        /// Loads AD properties in standart domain
        /// </summary>
        /// <param name="userObject"></param>
       /* protected void updateUserProperties(SearchResult userObject)
        {
            string tempEnName = string.Empty;
            string tempHeName = string.Empty;

            if (!(userObject == null)) // if found a result
            {
                // in CC domain ""givenName" field conatain English values for both first and last names in the format: "FirstName LastName"
                // and the field "sn" contains Hebrew values for both first and last names in the format: "FirstNameHe LastNameHes"
                tempEnName = userObject.Properties["givenName"][0].ToString(); 
                {// Get first name - makes sure that the first letter is upper case, and all the other lower cars
                    _up.FirstName = userObject.Properties["givenName"][0].ToString();   //first name
                    _up.FirstName.ToLower();
                    char[] fname = _up.FirstName.ToCharArray();
                    fname[0] = Convert.ToChar(fname[0].ToString().ToUpper());
                    _up.FirstName = string.Empty;
                    foreach (char letter in fname)
                    {
                        _up.FirstName += letter;
                    }
                }

                {// Get last name - makes sure that the first letter is upper case, and all the other lower cars
                    _up.LastName = userObject.Properties["sn"][0].ToString();    //surname
                    _up.LastName.ToLower();
                    char[] lname = _up.LastName.ToCharArray();
                    lname[0] = Convert.ToChar(lname[0].ToString().ToUpper());
                    _up.LastName = string.Empty;
                    foreach (char letter in lname)
                    {
                        _up.LastName += letter;
                    }
                }
                _up.FirstNameHe = userObject.Properties["extensionAttribute5"][0].ToString(); // first name in Hebrew
                _up.LastNameHe = userObject.Properties["extensionAttribute6"][0].ToString(); // lasr name in Hebrew
                _up.Email = userObject.Properties["mail"][0].ToString();  //email
                _up.EmployeeID = userObject.Properties["employeeID"][0].ToString(); // TZ
                _up.Phone = userObject.Properties["telephoneNumber"][0].ToString(); //Phone Number   


            }

        }*/
    }
}
