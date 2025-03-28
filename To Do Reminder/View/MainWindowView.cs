namespace To_Do_Reminder.View
{
    using System.Windows;
    using System.ComponentModel;
    using View.IOMainWindowProperties;

    /// <summary>
    /// Created by Clive Leddy
    /// Date: 2017-12-14
    /// 
    /// This class has a "IS-A" relationship with the class OutputPropedrtiesMainWindow. It extends
    /// the class OutputProperitesMainWindow with additional methods that are used in the ViewModel
    /// of the design pattern Model View ViewModel (MVVM). 
    /// This class contains methods that are use to interact with the user such as a MessageBox. The 
    /// class also contains logic that determines the rules of input via the user interface.
    /// 
    /// </summary>
    internal class MainWindowView : OutputProperitesMainWindow, IDataErrorInfo
    {
        public MainWindowView()
        {
            GetTime();

        }

        /// <summary>
        /// A message box that ask if the user wants to terminate the running app. If the use
        /// presses OK return 1, Cancel return 0, otherwise return -1.
        /// </summary>
        /// <returns>An int.</returns>
        internal int AppShutdown()
        {
            string messageBoxText = "Are you sure that you want to exit the program?";
            string messageBoxCaption = "Confirmation!";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxImage image = MessageBoxImage.Warning;
             
            MessageBoxResult messageSelected = MessageBox.Show(messageBoxText, messageBoxCaption, button, image);

            switch (messageSelected)
            {
                case MessageBoxResult.OK:
                    return 1;
                case MessageBoxResult.Cancel:
                    return 0;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Display an message box with an error text and error caption. The parameter messageBoxText contains
        /// the error message and messageBoxCaprion contains the error caption.
        /// </summary>
        /// <param name="messageBoxText">A string.</param>
        /// <param name="messageBoxCaption">A string.</param>
        internal void AppErrorMessage(string messageBoxText, string messageBoxCaption)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage image = MessageBoxImage.Error;

            MessageBoxResult messageBox = MessageBox.Show(messageBoxText, messageBoxCaption.ToUpper(), button, image);

        }

        /// <summary>
        /// Display the applications instructions.
        /// </summary>
        internal void AppDisplayInfo()
        {
            var window = new AboutWindow();
            window.Show();
        }

        #region IDataErrorInfo members
        /// <summary>
        /// This Error property is not used
        /// </summary>
        string IDataErrorInfo.Error
        {
            get {
                return null;
            }
        }

        /// <summary>
        /// This is a indexer that accepts a property name from the WPF and then executes
        /// an appropriate validation.
        /// </summary>
        /// <param name="propertyName">A string name of a property</param>
        /// <returns>A non empty string containing a error message or null</returns>
        string IDataErrorInfo.this[string propertyName]
        {
            get {
                return GetValidationError(propertyName);//delegate the logic to another method
            }
        }
        #endregion

        #region Validation logic
        /// <summary>
        /// Keep track of the input and out properties need to be validated.
        /// </summary>
        static readonly string[] validateProperties = {
            "DateTimeIO", "PrioritySelected", "ToDoIOText"
        };

        /// <summary>
        /// A is validation property 
        /// </summary>
        public bool ISValid
        {
            get {
                return ValidateProperties();
            }
        }

        /// <summary>
        /// Validate all the UI properties and check if any of them contain a null value.
        /// </summary>
        /// <returns>True if null false otherwise</returns>
        protected bool ValidateProperties()
        {
            foreach (string property in validateProperties)
            {
                //check the validation
                if (GetValidationError(property) != null)
                {
                    return false;//the validation failed.
                }
            }
            return true;
        }

        /// <summary>
        /// Logic to find a appropriate error message.
        /// </summary>
        /// <param name="propertyName">A string name of a property</param>
        /// <returns>A string containing a message or null</returns>
        private string GetValidationError(string property)
        {
            string errorMessage = null;

            switch (property)
            {
                case "DateTimeIO":
                    errorMessage = ValidateDateTimeIO();
                    break;
                case "PrioritySelected":
                    errorMessage = ValidatePriorities();
                    break;
                case "ToDoIOText":
                    errorMessage = ValidateDescription();
                    break;
                default:
                    break;
            }

            return errorMessage;
        }

        /// <summary>
        /// Validate if a date and time have been selected.
        /// </summary>
        /// <returns>A string if no date and time has not been selected otherwise null.</returns>
        private string ValidateDateTimeIO()
        {
            string errorStr = null;

            if (!DateTimeIO.HasValue)
            {
                errorStr = "All \"To Do's\" must have a date!";
            }
            return errorStr;
        }

        /// <summary>
        /// Validate if a priority has been selected.
        /// </summary>
        /// <returns>A string if no priority has not been selected otherwise null.</returns>
        private string ValidatePriorities()
        {
            string errorStr = null;

            if (!ValidatePriorities(PrioritySelected))
            {
                errorStr = "Please select a priority for your \"To Do\"!";
            }
            return errorStr;
        }

        /// <summary>
        /// Validate if a given priority exists in the list of priorities.
        /// </summary>
        /// <param name="priorityItem">A int.</param>
        /// <returns>True if in the list of priorities otherwise false.</returns>
        private bool ValidatePriorities(int priorityItem)
        {
            return PriorityEnum.IsDefined(typeof(PriorityEnum), priorityItem);
        }


        /// <summary>
        /// A to do description must contain some text.
        /// </summary>
        /// <returns>A string with a error message or null.</returns>
        private string ValidateDescription()
        {
            string errorStr = null;

            if (string.IsNullOrWhiteSpace(ToDoIOText))
            {
                errorStr = "Enter a \"To Do\" description?";
            }
            return errorStr;
        }

        #endregion

    }
}
