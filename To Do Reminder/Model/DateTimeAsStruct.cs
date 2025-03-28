namespace To_Do_Reminder.Model
{

    using System;
    /// <summary>
    /// Created by Clive Leddy
    /// Date 2017-12-18
    /// 
    /// This structure takes and converts a DateTime to date and hour as strings.
    /// Date is in the format of "day of week", "month", "day" and "year".
    /// Hour is in the format of "hour" and "minutes".
    /// </summary>
    internal struct DateTimeAsStruct
    {
        #region Fields
        private string date;
        private string hour;
        #endregion

        #region Properties
        /// <summary>
        /// Date as string.
        /// </summary>
        public string Date
        {
            get {
                return date;
            }
        }

        /// <summary>
        /// Hour as string.
        /// </summary>
        public string Hour
        {
            get {
                return hour;
            }
        }
        #endregion

        /// <summary>
        /// Update the structure with date and time values as strings.
        /// </summary>
        /// <param name="dateAndTime">A DateTime</param>
        internal void AddDateAndTime(DateTime dateAndTime)
        {
            date = dateAndTime.ToString("D");
            hour = dateAndTime.ToString("HH:mm");
        }
    }
}
