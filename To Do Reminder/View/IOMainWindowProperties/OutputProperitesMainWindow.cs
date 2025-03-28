namespace To_Do_Reminder.View.IOMainWindowProperties
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Threading;
    using To_Do_Reminder.Model;

    /// <summary>
    /// Created by Clive Leddy
    /// Date: 2017-12-14
    /// 
    /// This class has a "IS-A" relationship with the class InputPropertiesMainWindow. It extends 
    /// the InputPropertiesMainWindow and its properties are bound to WPF controls. The WPF in turn
    /// uses the properties in this class for IO user interface interaction. The methods in the class
    /// are used to manipulate the content of the properties.
    /// 
    /// </summary>
    internal class OutputProperitesMainWindow : InputPropertiesMainWindow, INotifyPropertyChanged
    {

        #region Fields
        private string toDoIOText;
        private DateTime? dateTimeIO;
        private string realTime;
        private List<string> priorities;
        private int prioritySelected;
        private ObservableCollection<Task> toDoList;
        #endregion

        #region Properties
        /// <summary>
        /// To do input output as text.
        /// </summary>
        public string ToDoIOText
        {
            get {
                return toDoIOText;
            }
            set {
                toDoIOText = value;
                OnPropertyChanged("ToDoIOText");
            }
        }

        /// <summary>
        /// A list of to do tasks.
        /// </summary>
        public ObservableCollection<Task> ToDoList
        {
            get {
                return toDoList;
            }
            set {
                toDoList = value;
                OnPropertyChanged("ToDoList");
            }
        }

        /// <summary>
        /// A list of priorities.
        /// </summary>
        public List<string> Priorities
        {
            get {
                return priorities;
            }
            set {
                priorities = value;
                OnPropertyChanged("Priorities");
            }
        }

        /// <summary>
        /// A priority has been selected.
        /// </summary>
        public int PrioritySelected
        {
            get {
                return prioritySelected;
            }
            set {
                prioritySelected = value;
                OnPropertyChanged("PrioritySelected");
            }
        }

        /// <summary>
        /// A date and time input output.
        /// </summary>
        public DateTime? DateTimeIO
        {
            get {
                return dateTimeIO;
            }
            set {
                dateTimeIO = value;
                OnPropertyChanged("DateTimeIO");
            }
        }

        /// <summary>
        /// The current time.
        /// </summary>
        public string RealTime
        {
            get {
                return realTime;
            }
            set {
                realTime = value;
                OnPropertyChanged("RealTime");
            }
        }
        #endregion

        public OutputProperitesMainWindow()
        {
            Initialize();//initialize the class
        }

        /// <summary>
        /// Initialize the class
        /// </summary>
        private void Initialize()
        {
            Priorities = new List<string>();//initialize the list of priorities
            AddPriorityToList();//populate the priorities list
                                //priority has yet to be selected
            ResetPrioritySelected();
            //set the time to the current time
            ResetDateTime();
        }

        /// <summary>
        /// Reset priority selected.
        /// </summary>
        protected void ResetPrioritySelected()
        {
            PrioritySelected = -1;
        }

        /// <summary>
        /// Reset date and time to null.
        /// </summary>
        protected void ResetDateTime()
        {
            DateTimeIO = null;
        }

        /// <summary>
        /// Reset the ToDoList.
        /// </summary>
        protected void ResetToDoList()
        {
            ToDoList = new ObservableCollection<Task>();
        }

        /// <summary>
        /// Clear the UI.
        /// </summary>
        protected void ClearUI()
        {
            ResetPrioritySelected();
            ResetDateTime();
            ResetToDoList();
            ToDoIOText = string.Empty;
        }

        /// <summary>
        /// Add the priorities to a property list.
        /// </summary>
        /// <param name="priority">An enum of PriorityEnum</param>
        protected void AddPriorityToList()
        {
            string priorityAsTextError = "Something went wrong";
            string priorityAsText;

            //search the members of PriorityEnum for the comboBox member
            foreach (PriorityEnum iPriorityEnum in Enum.GetValues(typeof(PriorityEnum)))
            {
                priorityAsText = iPriorityEnum.ToString();

                if (!string.IsNullOrWhiteSpace(priorityAsText))
                {
                    priorityAsText = priorityAsText.Replace("_", " ");//replace underscores
                    Priorities.Add(priorityAsText);
                }
                else
                {
                    Priorities.Add(priorityAsTextError);
                }

            }
        }

        /// <summary>
        /// Get the current time during object run time.
        /// </summary>
        protected void GetTime()
        {
            DispatcherTimer runTime = new DispatcherTimer();//create a DispatcherTimer control

            runTime.Interval = TimeSpan.FromSeconds(1);//set the time interval to 1 second
            runTime.Tick += timer_Tick;//subscribe to the Tick event by adding a method to the Tick event
            runTime.Start();//start the DispatcherTimer
                            //could use 
                            //runTime.IsEnabled = true;//set the DispatcherTimer to enabled

        }

        /// <summary>
        ///The DispatcherTimer calls this method when the Tick event has been meet.
        ///Update the class property to present the current time.
        /// </summary>
        /// <param name="sender">An object, not used.</param>
        /// <param name="e">An EventArgs, not used.</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            RealTime = DateTime.Now.ToLongTimeString();//get current time as string
        }

        #region INotifyPropertyChanged members
        /// <summary>
        /// This is boiler plate code that was added when one want to notify a change on a class
        /// property. This is where the code and the UI communicate through the event handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise a notification that a property has been changed.
        /// </summary>
        /// <param name="propertyName">A string of the property name</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
