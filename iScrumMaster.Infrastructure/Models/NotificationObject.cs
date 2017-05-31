using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

namespace iScrumMaster.Infrastructure
{
    public class NotificationObject : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        #region Implement INotifyPropertyChanged Interface
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Implement INotifyDataErrorInfo

        /// <summary>
        /// Key --> propertyname, Value --> list of validation errors.
        /// </summary>
        private Dictionary<string, List<string>> errorList = new Dictionary<string, List<string>>();

        /// <summary>
        /// Checks if there are validation errors
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return errorList.Values.SelectMany(item => item.TakeWhile(item1 => string.IsNullOrEmpty(item1))).Count() > 0;
            }
        }

        /// <summary>
        /// Gets list of all the Errors
        /// </summary>
        public List<string> ErrorList
        {
            get
            {
                return errorList.Values.SelectMany(item => item.TakeWhile(item1 => string.IsNullOrEmpty(item1))).ToList();
                                                                            
            }
        }

        /// <summary>
        /// Gets the list of errors for a propertyname
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        /// <returns>Validation errors related to the parameter propertyName</returns>
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            CheckErrorCollectionForProperty(propertyName);

            //return validation errors related to this property
            return errorList[propertyName];
        }

        /// <summary>
        /// The first time the property's error list are asked for, it creates an empty error list
        /// </summary>
        /// <param name="propertyName"></param>
        private void CheckErrorCollectionForProperty(string propertyName)
        {
            if (!errorList.ContainsKey(propertyName))
                errorList[propertyName] = new List<string>();
        }


        /// <summary>
        /// Add a validation error to a property name
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="error">The validation error message</param>
        protected void AddError(string propertyName, string error)
        {
            //To check if errors for the property exists. If not creates a new empty list of errors.
            CheckErrorCollectionForProperty(propertyName);

            errorList[propertyName].Add(error);
            RaiseErrorsChanged(propertyName);
        }


        /// <summary>
        /// Remove a validation error from a property name
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="error">The validation error message to be removed</param>
        protected void RemoveError(string propertyName, string error)
        {
            if (errorList[propertyName].Contains(error))
            {
                errorList[propertyName].Remove(error);
                RaiseErrorsChanged(propertyName);
            }
        }



        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        /// <summary>
        /// Notifies the UI of validation error state change
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        private void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
        #endregion
    }
}
