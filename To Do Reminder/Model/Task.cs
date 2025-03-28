namespace To_Do_Reminder.Model
{
    using System;
    using View;

    /// <summary>
    /// Created by Clive Leddy
    /// Date: 2017-12-16
    /// 
    /// This class is a "HAS-A" relationship with both class MainWindowView, TaskManager and ToDoReminderViewModel.
    /// It holds data for a specific task. 
    /// This class uses constructor overloading and constructor chaining.
    /// </summary>
    internal class Task
    {
        private const string message = "You look tired have a break!";
        private const PriorityEnum priority = PriorityEnum.Important;

        #region Fields
        private string taskDescription;//a task description
        private DateTime taskDateTime;//a task date and time
        private int taskPriority;//a task priority
        private string taskPriorityAsString;//a task priority as a string
        private DateTimeAsStruct dateAndTimeStu;//a task date and time converted to string
        private string date;//date as string
        private string hour;//hour as sting
        #endregion

        #region Properties
        /// <summary>
        /// A to do task description, cannot be null or empty.
        /// </summary>
        public string TaskDescription
        {
            get {
                return taskDescription;
            }
            private set {
                taskDescription = value;
            }
        }

        /// <summary>
        /// A to do task deadline as date and time. All tasks require some
        /// form of deadline date and time and cannot be null.
        /// </summary>
        internal DateTime TaskDateTime
        {
            get {
                return taskDateTime;
            }
            private set {
                taskDateTime = value;
            }
        }

        /// <summary>
        /// A to do task priority. Each To Do task requires a priority!
        /// </summary>
        internal int TaskPriority
        {
            get {
                return taskPriority;
            }
            private set {
                taskPriority = value;
            }
        }

        /// <summary>
        /// A to do task priority as a string.
        /// </summary>
        public string TaskPriorityAsString
        {
            get {

                return taskPriorityAsString;
            }
            private set {
                taskPriorityAsString = value;
            }
        }

        /// <summary>
        /// A task date converted to string.
        /// </summary>
        public string Date
        {
            get {
                return dateAndTimeStu.Date;
            }
            private set {
                date = value;
            }
            
        }

        /// <summary>
        /// A task hour converted to string.
        /// </summary>
        public string Hour
        {
            get {
                return dateAndTimeStu.Hour;
            }
            private set {
                hour = value;
            }
        }
        #endregion

        /// <summary>
        /// Class default constructor that sets the task to the current date and time,
        /// priority to less important and a description as "Have a coffee!".
        /// </summary>
        internal Task() : this(DateTime.Now, (int)priority, message)
        {
        }

        /// <summary>
        /// Create a new task with a given date and time, priority and task description.
        /// </summary>
        /// <param name="dateTime">A DateTime.</param>
        /// <param name="priority">A enum PriorityEnum.</param>
        /// <param name="description">A string.</param>
        internal Task(DateTime dateTime, int priority, string description)
        {
            //update class properties
            taskDateTime = dateTime;
            taskPriority = priority;
            taskDescription = description;

            UpdateStringProperties();//update string properties
        }

        /// <summary>
        /// Create a new task with a given date and time, priority and task description.
        /// </summary>
        /// <param name="dateTime">A DateTime? a possible null parameter</param>
        /// <param name="priority">A enum PriorityEnum.</param>
        /// <param name="description">A string.</param>
        public Task(DateTime? dateTime, int priority, string description)
        {
            //update class properties
            if (ValidateDateTime(dateTime))
            {
                taskDateTime = (DateTime)dateTime;
            }
            else
            {
                taskDateTime = DateTime.Now;
            }

            taskPriority = priority;
            taskDescription = description;

            UpdateStringProperties();//update string properties
        }

        /// <summary>
        /// A copy constructor that enables one to copy one object
        /// from another object.
        /// </summary>
        /// <param name="otherTask">Task object that contains data that will 
        /// be copied using "this"</param>
        internal Task(Task otherTask)
        {
            this.taskDescription = otherTask.taskDescription;
            this.taskDateTime = otherTask.taskDateTime;
            this.taskPriority = otherTask.taskPriority;

            this.taskPriorityAsString = otherTask.taskPriorityAsString;
            this.dateAndTimeStu = otherTask.dateAndTimeStu;
        }

        /// <summary>
        /// Validate the class properties, they must contain data. 
        /// </summary>
        /// <returns>A bool</returns>
        internal bool Validate()
        {
            bool notValid = false;

            //validate taskDescription, allow empty spaces
            if (string.IsNullOrEmpty(taskDescription))
            {
                return notValid;
            }

            //validate the taskPrirority
            if (!ValidatePriority())
            {
                return notValid;
            }

            //validate taskDateTime
            if (!ValidateDateTime())
            {
                return notValid;
            }

            return !notValid;
        }

        #region Private methods

        /// <summary>
        /// Convert the properties DateTime and PriorityEnmu to string properties if they are valid.
        /// </summary>
        private void UpdateStringProperties()
        {
            if (Validate())
            {
                SetPriorityToString();//convert taskPriority to a string

                dateAndTimeStu = new DateTimeAsStruct();
                dateAndTimeStu.AddDateAndTime(taskDateTime);//update the strut DateAndTimeStu

                Date = dateAndTimeStu.Date;//update class property Date
                Hour = dateAndTimeStu.Hour;//update class property Hour
            }
        }

        /// <summary>
        /// Initialize the class with default data.
        /// </summary>
        private void Initialize()
        {
            taskDateTime = DateTime.Now;
            taskPriority = (int)priority;
            taskDescription = message;

            UpdateStringProperties();//update string properties
        }

        /// <summary>
        /// Validate the class property for Date and Time. If the property 
        /// is null return false.
        /// </summary>
        /// <returns>A bool</returns>
        private bool ValidateDateTime()
        {
            if (taskDateTime == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validate the class property for Date and Time. If the property 
        /// is null return false.
        /// </summary>
        /// <param name="dateTime">A DateTime? a possible null parameter.</param>
        /// <returns>A bool</returns>
        private bool ValidateDateTime(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// If the taskPriority property is correct update the property 
        /// taskPriorityAsString property or else do nothing. 
        /// </summary>
        private void SetPriorityToString()
        {
            if (ValidatePriority())
            {
                TaskPriorityAsString = PriorityToString(taskPriority);
            }
        }

        /// <summary>
        /// Validate the class taskPriority to a list of PriorityEnum. If the class
        /// taskPriority is in the PriorityEnum return true else false.
        /// </summary>
        /// <returns>A bool.</returns>
        private bool ValidatePriority()
        {
            //step through the PriorityEnum
            foreach (PriorityEnum iPriorityEnum in Enum.GetValues(typeof(PriorityEnum)))
            {
                if ((int)iPriorityEnum == taskPriority)//found the search item
                {
                    return true;
                }
            }
            return false;//did not find the search item
        }

        /// <summary>
        /// Convert the enum PriorityEnum member to a formated string.
        /// </summary>
        /// <param name="priority">A int.</param>
        /// <returns>A string.</returns>
        private string PriorityToString(int priority)
        {
            string strPriority = string.Empty;

            //strPriority = priority.ToString();
            PriorityEnum temp = (PriorityEnum)priority;
            strPriority = temp.ToString();
            strPriority = strPriority.Replace("_", " ");

            return strPriority;
        }
        #endregion

        /// <summary>
        /// Override the ToString() method to display this task date and time,
        /// priority and description.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Date and time: {0}, Priority: {1}, Description: {2}",
                taskDateTime, taskPriorityAsString, taskDescription);
        }

    }
}
