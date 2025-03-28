namespace To_Do_Reminder.Model
{
    using System;
    using System.Collections.Generic;
    using View;

    /// <summary>
    /// Created by Clive Leddy
    /// Date: 2017-12-16
    /// 
    /// The purpose of this class is to manage Tasks in a list of tasks. This is done by either
    /// adding, removing or modifying a task.
    /// This class is a "HAS-A" relationship with both class ToDoReminderViewModel and Task.
    /// The method AddTask is overloaded.
    /// ********************************* Please note! *********************************
    /// * In the class constructor there is a call to the TestData(). This method      *
    /// * generates some test tasks to the list of tasks activate it to include test   *
    /// * data.                                                                        *
    /// ********************************************************************************
    /// </summary>
    internal class TaskManager
    {
        private const int startPos = 0;

        #region Fields
        private List<Task> listOfTasks;
        #endregion

        #region Properties
        /// <summary>
        /// A list of tasks to manage.
        /// </summary>
        private List<Task> ListOfTasks
        {
            get {
                return listOfTasks;
            }
            set {
                listOfTasks = value;
            }
        }

        /// <summary>
        /// Total number of tasks in the task manager.
        /// </summary>
        internal int Count
        {
            get {
                return listOfTasks.Count;
            }
        }
        #endregion

        /// <summary>
        /// Class constructor to manage a list of tasks.
        /// </summary>
        internal TaskManager()
        {
            ListOfTasks = new List<Task>();//create a new list of tasks to manage

            //TestData();//method containing test date
        }

        /// <summary>
        ///  Add a task in the form of date and time, a priority and 
        ///  a task description to the task manager. Returns
        /// true if successful otherwise false.
        /// </summary>
        /// <param name="dateTime">A DateTime.</param>
        /// <param name="priority">An int.</param>
        /// <param name="description">A string</param>
        /// <returns>A bool.</returns>
        internal bool AddTask(DateTime dateTime, int priority, string description)
        {
            bool addTask = false;

            //create a new task
            Task newTask = new Task(dateTime, priority, description);

            //add the task to the list of tasks
            addTask = AddTask(newTask); //UpdateTaskManager(newTask);

            return addTask;
        }

        /// <summary>
        /// Add a task to the task manager.Returns
        /// true if successful otherwise false.
        /// </summary>
        /// <param name="newTask">A Task.</param>
        /// <returns>A bool.</returns>
        internal bool AddTask(Task newTask)
        {
            bool addTask = false;

            //add the task to the list of tasks
            addTask = UpdateTaskManager(newTask, Count);

            return addTask;
        }

        /// <summary>
        /// Add a task to the task manager at a given index. Returns
        /// true if successful otherwise false.
        /// </summary>
        /// <param name="taskToChange">An Task.</param>
        /// <param name="index">An int.</param>
        /// <returns>A bool.</returns>
        internal bool AddTask(Task taskToChange, int index)
        {
            bool changeTask = false;

            changeTask = UpdateTaskManager(taskToChange, index);
            
            return changeTask;
        }

        /// <summary>
        /// Remove a task at index from the task manager. The parameter index
        /// is the specific index in the list. If the element at index could
        /// be removed return true otherwise false.
        /// </summary>
        /// <param name="index">An int.</param>
        /// <returns>A bool.</returns>
        internal bool DeleteTask(int index)
        {
            bool deleteTask = false;

            if (listOfTasks[index].Validate())
            {
                listOfTasks.RemoveAt(index);
                deleteTask = true;
            }

            return deleteTask;
        }

        /// <summary>
        /// Get a task at a given index.
        /// </summary>
        /// <param name="index">An int.</param>
        /// <returns>A Task or null.</returns>
        internal Task GetTask(int index)
        {
            Task getTask = null;
            Task newTask = null;

            if ((index >= startPos) && (index < Count))
            {
                getTask = listOfTasks[index];//get task at index
                newTask = new Task(getTask);//make a copy of the task
                getTask = newTask;//overwrite the old task with a copy
            }
            else
            {
                getTask = null;
            }

            return getTask;
        }

        #region Private methods (including test data)
        /// <summary>
        /// Update the task manager by adding a new task of a changed task to the list of tasks.
        /// If successful return true otherwise false.
        /// </summary>
        /// <param name="objTask">A Task.</param>
        /// <param name="index">An int.</param>
        /// <returns>True if the objTask is valid otherwise false.</returns>
        private bool UpdateTaskManager(Task objTask, int index)
        {
            bool addTask = false;

            if (objTask.Validate())
            {
                if (index == Count)//last position in the list of tasks
                {
                    ListOfTasks.Add(objTask);//add a task in the last position of the list
                }
                else
                {
                    listOfTasks.RemoveAt(index);//remove the current element at index
                    listOfTasks.Insert(index, objTask);//insert the new task at index
                }
                addTask = true;
            }

            return addTask;
        }
        
        /// <summary>
        /// Test data to showcase the app!
        /// </summary>
        private void TestData()
        {
            AddTask(new Task(DateTime.Now, (int)PriorityEnum.Less_important, "Hi Clive, date is now!"));
            AddTask(new Task(DateTime.Now.AddDays(2), (int)PriorityEnum.More_importent, "Hi Clive time for a fika in 2 days! :)"));
            AddTask(new Task(DateTime.Now.AddYears(3), (int)PriorityEnum.Important, "Hi Clive, what year is it (" + DateTime.Now.ToString("D") + ")?"));
            AddTask(new Task());
            AddTask(DateTime.Now, (int)PriorityEnum.Important, "Testing method AddTask(DateTime dateTime, int priority, string message)");
        }
        #endregion
    }
}
