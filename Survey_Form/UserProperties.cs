using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey_Form
{
    class UserProperties
    {

        //private string _domain;
        private string _title;
        private string _username;
        private string _firstName;
        private string _lastName;
        private string _firstNameHe;
        private string _lastNameHe;
        private string _employeeID;
        private string _email;
        private string _country;
        private string _userGroup;
        private string _gender;
        private string _phone;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public UserProperties()
        {

            _username = string.Empty;
            _title = string.Empty;
            _firstName = string.Empty;
            _lastName = string.Empty;
            _firstNameHe = string.Empty;
            _lastNameHe = string.Empty;
            _employeeID = string.Empty;
            _email = string.Empty;
            _country = string.Empty;
            _userGroup = string.Empty;
            _gender = string.Empty;
            _phone = string.Empty;
        }


        /// <summary>
        /// Get/Set title
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        /// <summary>
        /// Get/Set username
        /// </summary>
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        /// <summary>
        /// Get/Set first name
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        /// <summary>
        /// Get/Set last name
        /// </summary>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        /// <summary>
        /// Get/Set first name in Hebrew
        /// </summary>
        public string FirstNameHe
        {
            get
            {
                return _firstNameHe;
            }
            set
            {
                _firstNameHe = value;
            }
        }

        /// <summary>
        /// Get/Set last name in Hebrew
        /// </summary>
        public string LastNameHe
        {
            get
            {
                return _lastNameHe;
            }
            set
            {
                _lastNameHe = value;
            }
        }

        /// <summary>
        /// Get/Set employee ID (personal ID num)
        /// </summary>
        public string EmployeeID
        {
            get
            {
                return _employeeID;
            }
            set
            {
                _employeeID = value;
            }
        }

        /// <summary>
        /// Get/Set email address
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        /// <summary>
        /// Get/Set country
        /// </summary>
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
            }
        }

        /// <summary>
        /// Get/Set user group name in AD
        /// </summary>
        public string UserGroup
        {
            get
            {
                return _userGroup;
            }
            set
            {
                _userGroup = value;
            }
        }

        /// <summary>
        /// Get/Set gender
        /// </summary>
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }

        /// <summary>
        /// Get/Set phone
        /// </summary>
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
            }
        }



    }
}
