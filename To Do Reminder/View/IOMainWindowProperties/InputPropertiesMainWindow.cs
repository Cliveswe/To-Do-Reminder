namespace To_Do_Reminder.View.IOMainWindowProperties
{

    using System;

    /// <summary>
    /// Created by Clive Leddy
    /// Date: 2017-12-14
    /// 
    /// This is a class of properties and one method. The method converts a enum member
    /// to a string. The string is used in a property and the property is bound to a 
    /// control in WPF.
    /// </summary>
    internal class InputPropertiesMainWindow
    {
        #region Fields

        #endregion

        #region Properties

        #region Captions
        /// <summary>
        /// To Do Reminder caption used in main window title.
        /// </summary>
        public string MainWindowTitle
        {
            get {
                return GetInputEnumToString(CaptionsViewEnum.To_Do_Reminder);
            }
        }

        /// <summary>
        /// Date and time caption used in textblock.
        /// </summary>
        public string DateAndTimeText
        {
            get {
                return GetInputEnumToString(CaptionsViewEnum.Date_and_time);

            }
        }

        /// <summary>
        /// Priority caption used in textblock and listview
        /// </summary>
        public string Priority
        {
            get {
                return GetInputEnumToString(CaptionsViewEnum.Priority);
            }
        }

        /// <summary>
        /// Description caption used in textblock and listview
        /// </summary>
        public string Description
        {
            get {
                return GetInputEnumToString(CaptionsViewEnum.Description);
            }
        }

        #region Buttons
        /// <summary>
        /// Add button caption used in button
        /// </summary>
        public string AddButton
        {
            get {
                return GetInputEnumToString(CaptionsViewEnum.Add);
            }
        }

        /// <summary>
        /// Change button caption used in button
        /// </summary>
        public string ChangeButton
        {
            get {
                return GetInputEnumToString(CaptionsViewEnum.Change);
            }
        }

        /// <summary>
        /// Delete button caption used in button
        /// </summary>
        public string DeleteButton
        {
            get {
                return GetInputEnumToString(CaptionsViewEnum.Delete);
            }
        }
        #endregion

        /// <summary>
        /// To Do caption used in groupbox
        /// </summary>
        public string ToDo
        {
            get {
                return GetInputEnumToString(CaptionsViewEnum.To_Do);
            }
        }

        /// <summary>
        /// Date caption used in listview
        /// </summary>
        public string Date
        {
            get {
                return GetInputEnumToString(CaptionsViewEnum.Date);
            }
        }

        /// <summary>
        /// Hour caption used in listview
        /// </summary>
        public string Hour
        {
            get {
                return GetInputEnumToString(CaptionsViewEnum.Hour);
            }
        }
        #endregion


        #endregion

        public InputPropertiesMainWindow()
        {

        }


        /// <summary>
        /// Convert a enum to a string.
        /// </summary>
        /// <param name="caption">An CaptionViewEnum enum.</param>
        /// <returns>A string the is converted from the enum CaptionViewEnum.</returns>
        protected string GetInputEnumToString(CaptionsViewEnum caption)
        {
            string captionAsText = "Something went wrong!";

            //search the members of CaptionViewEnum for the method parameter
            foreach (CaptionsViewEnum iCaptionViewEnum in Enum.GetValues(typeof(CaptionsViewEnum)))
            {
                if (iCaptionViewEnum == caption)
                {
                    captionAsText = iCaptionViewEnum.ToString();//convert an enum to string
                    captionAsText = captionAsText.Replace("_", " ");//replace underscores
                }
            }
            return captionAsText;
        }
    }
}
