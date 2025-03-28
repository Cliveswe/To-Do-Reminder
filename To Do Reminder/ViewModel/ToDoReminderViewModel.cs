namespace To_Do_Reminder.ViewModel
{
    using View;
    using Model;
    using System;
    using Contact_RegistryG.ViewModel.Commands;
    using System.Windows;

    /// <summary>
    /// Created by Clive Leddy
    /// Date: 2017-12-14
    /// 
    /// This class has a "IS-A" relationship with the class OutputPropedrtiesMainWindow. In fact is the 
    /// beginning of a chained IS-A relationship with other classes. In addition, this class has a "Has-A"
    /// relationship with the class TaskManager. It also contains all the methods that are bound to the 
    /// WPF Button and ListView controls. All command properties, validation logic and methods are defined 
    /// in methods of this class.
    /// 
    /// </summary>
    internal class ToDoReminderViewModel : MainWindowView
    {
        private const int noItemSelected = -1;

        #region Fields
        private TaskManager taskManagerList;
        private int toDoReminderSelectedItem;
        private Task selectedTask;
        #endregion

        #region Properties
        /// <summary>
        /// A To Do Reminder has been selected.
        /// </summary>
        public int ToDoReminderSelectedItem
        {
            get {
                return toDoReminderSelectedItem;
            }
            set {
                toDoReminderSelectedItem = value;
                if (value > noItemSelected)
                {
                    SelectedTask = taskManagerList.GetTask(value);
                    PopulateUIControlsFromTaskManager(SelectedTask);
                }
            }
        }

        /// <summary>
        /// A copy of a task that has been selected.
        /// </summary>
        public Task SelectedTask
        {
            get {
                return selectedTask;
            }
            private set {
                selectedTask = value;
            }
        }

        /// <summary>
        /// A task manager object.
        /// </summary>
        internal TaskManager TaskManagerList
        {
            get {
                return taskManagerList;
            }
        }
        #endregion

        /// <summary>
        /// Class constructor
        /// </summary>
        public ToDoReminderViewModel()
        {
            taskManagerList = new TaskManager();//create the task manager
            AddRelayCommands();
            ToDoReminderSelectedItem = noItemSelected;
        }

        #region Command Properties

        /// <summary>
        /// Add a To Do to the task manager.
        /// </summary>
        public RelayCommands AddToDoCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Remove a To Do to the task manager.
        /// </summary>
        public RelayCommands DeleteToDoCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Edit a To Do to the task manager.
        /// </summary>
        public RelayCommands ChangeToDoCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Ctrl+N a new To Do.
        /// </summary>
        public RelayCommands CtrlNCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Alt+F4 a shutdown to do reminder.
        /// </summary>
        public RelayCommands AltF4Command
        {
            get;
            set;
        }

        /// <summary>
        /// Ctrl+A a display to do reminder app info.
        /// </summary>
        public RelayCommands CtrlACommand
        {
            get;
            set;
        }


        #endregion

        #region Command validation logic

        /// <summary>
        /// Determine if the UI can add a To Do task to the task manager.
        /// </summary>
        /// <param name="parameter">An object as a generic parameter</param>
        /// <returns>A bool</returns>
        internal bool AddToDoCanUpdate(object parameter)
        {
            int objParameter = ConvertObjToInt(parameter);

            if (ToDoReminderSelectedItem > noItemSelected)
            {
                return false;
            }

            if (!ISValid)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determine if the user interface can change or delete a To Do task from the task manager.
        /// The parameter is an int bound to the ListView control SelectedItem. This method returns 
        /// false if a to do task is not selected or it the user interface input controls contain
        /// no input other wise it returns true. If an bound item is selected the user interface is 
        /// populated wi
        /// </summary>
        /// <param name="patameter">An object.</param>
        /// <returns>A bool.</returns>
        internal bool ChangeOrRemoveToDoCanUpdate(object parameter)
        {
            int objParameter = ConvertObjToInt(parameter);

            if ((ToDoReminderSelectedItem != objParameter) || (ToDoReminderSelectedItem <= noItemSelected))
            {

                return false;
            }

            if (!ISValid)
            {

                return false;
            }

            return true;
        }

        /// <summary>
        /// The user interface can always clear the input controls.
        /// </summary>
        /// <param name="parameter">An object as a generic parameter</param>
        /// <returns>A bool</returns>
        internal bool DefaultTaskCanUpdate(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Convert a object to an int.
        /// </summary>
        /// <param name="parameter">An object</param>
        /// <returns>A int</returns>
        private int ConvertObjToInt(object parameter)
        {
            return Convert.ToInt32(parameter);
        }

        #endregion

        #region Command methods

        /// <summary>
        /// This is a command method that is connected to the Add button. Add a To Do Task to the TaskManager.
        /// Also, trigger the UI update if the Task could be added to the TaskManager.
        /// If the task could not be added to the TaskManager then show a pop-up message UI.
        /// </summary>
        /// <param name="parameter">An object, not used!</param>
        internal void AddToDoToTaskManager(object parameter)
        {
            //create a new task
            Task addTask = new Task(DateTimeIO, PrioritySelected, ToDoIOText);

            //add the task to the task manager
            if (TaskManagerList.AddTask(addTask))
            {
                ResetUI();
            }
            else
            {
                string message = string.Format("While adding a task to the task manager an error occurred!" +
                    "\nDateTimeIo: {0}\nPrioritySelected: {1}\nToDoIOText: {2}",
                    DateTimeIO.ToString(), PrioritySelected, ToDoIOText);
                string caption = "Trying to add task to task manager!";
                AppErrorMessage(message, caption);
            }
        }

        /// <summary>
        /// Remove a task from the task manager.
        /// </summary>
        /// <param name="parameter"></param>
        internal void RemoveToDoFromTaskManager(object parameter)
        {
            int objParameter = ConvertObjToInt(parameter);

            //remove a task from the task manager
            if (TaskManagerList.DeleteTask(objParameter))
            {
                ResetUI();
            }
            else
            {
                string message = string.Format("While removing a task from the task manager an error occurred!" +
                    "\nIndex: {0}\n", objParameter);
                string caption = "Removing a task from task manager!";
                AppErrorMessage(message, caption);
            }
        }

        /// <summary>
        /// Modify a selected task from the task manager.
        /// </summary>
        /// <param name="parameter">An object.</param>
        internal void ChangeToDoFromTaskManager(object parameter)
        {
            Task changeTask = null;
            if (ConvertObjToInt(parameter) == ToDoReminderSelectedItem)
            {
                changeTask = new Task(DateTimeIO, PrioritySelected, ToDoIOText);
                taskManagerList.AddTask(changeTask, ConvertObjToInt(parameter));
                ResetUI();
            }
            else
            {
                string message = string.Format("While modifying a task in the task manager an error occurred!" +
                    "\nIndex {0}\nDateTimeIo: {1}\nPrioritySelected: {2}\nToDoIOText: {3}.",
                    ConvertObjToInt(parameter), DateTimeIO.ToString(), PrioritySelected, ToDoIOText);
                string caption = "Modifying a task in the task manager!";
                AppErrorMessage(message, caption);
            }
        }


        /// <summary>
        /// Ctrl+N command that clears the user interface input controls and re-freshes
        /// a list of To Do reminders.
        /// </summary>
        /// <param name="parameter">An object.</param>
        internal void CtrlNTaskManager(object parameter)
        {
            ResetUI();
        }

        /// <summary>
        /// Alt+F4 command that terminates the application.
        /// </summary>
        /// <param name="parameter">An object.</param>
        internal void AltF4TaskManager(object parameter)
        {
            Window windowApp = new Window();
            int f4Ans;
            windowApp = (Window)parameter;

            f4Ans = AppShutdown();
            if (f4Ans == 1)
            {

                windowApp.Close();
            }

        }

        /// <summary>
        /// Ctrl+A command that displays the information on how to use the application.
        /// The parameter is not used.
        /// </summary>
        /// <param name="parameter">An object.</param>
        internal void CtrlATaskManager(object parameter)
        {

            AppDisplayInfo();
        }

        /// <summary>
        /// Extrapolate data from a selected task from a list of tasks.
        /// </summary>
        /// <param name="reteriveTask">A Task</param>
        private void PopulateUIControlsFromTaskManager(Task retrievedTask)
        {
            if (retrievedTask.Validate())
            {
                DateTimeIO = retrievedTask.TaskDateTime;
                PrioritySelected = retrievedTask.TaskPriority;
                ToDoIOText = retrievedTask.TaskDescription;
            }
            else
            {
                string message = string.Format("While repopulating user interface an error occurred!" +
                   "\nDateTimeIo: {0}\nPrioritySelected: {1}\nToDoIOText: {2}.",
                   DateTimeIO.ToString(), PrioritySelected, ToDoIOText);
                string caption = "Refreshing the user interface!";
                AppErrorMessage(message, caption);
            }
        }

        /// <summary>
        /// Retrieve all tasks from the task manager and populate the user
        /// interface property.
        /// </summary>
        private void PoulateListOfTasksFromManager()
        {
            for (int index = 0; index < TaskManagerList.Count; index++)
            {
                ToDoList.Add(TaskManagerList.GetTask(index));
            }
        }

        /// <summary>
        /// Clear and repopulate the user interface.
        /// </summary>
        private void ResetUI()
        {
            ClearUI();
            PoulateListOfTasksFromManager();
            ToDoReminderSelectedItem = noItemSelected;
            SelectedTask = null;
        }
        #endregion

        #region Create relay commands

        /// <summary>
        /// Add all the commands used by this application.
        /// </summary>
        private void AddRelayCommands()
        {
            //list of commands
            AddToDoCommand = new RelayCommands(AddToDoToTaskManager, AddToDoCanUpdate);
            DeleteToDoCommand = new RelayCommands(RemoveToDoFromTaskManager, ChangeOrRemoveToDoCanUpdate);
            ChangeToDoCommand = new RelayCommands(ChangeToDoFromTaskManager, ChangeOrRemoveToDoCanUpdate);

            //input gesture commands
            CtrlNCommand = new RelayCommands(CtrlNTaskManager, DefaultTaskCanUpdate);
            AltF4Command = new RelayCommands(AltF4TaskManager, DefaultTaskCanUpdate);
            CtrlACommand = new RelayCommands(CtrlATaskManager, DefaultTaskCanUpdate);
        }

        #endregion
    }
}
