using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Border_MenüleisteDemo
{
    /// <summary>
    /// Main window logic for a task management application.
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> tasks = new ObservableCollection<string>();

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            TasksListBox.ItemsSource = tasks;
        }

        /// <summary>
        /// Adds a new task to the task list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TaskInput.Text))
            {
                tasks.Add(TaskInput.Text);
                TaskInput.Clear();
            }
        }

        /// <summary>
        /// Edits the selected task in the task list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListBox.SelectedItem != null && !string.IsNullOrWhiteSpace(TaskInput.Text))
            {
                tasks[TasksListBox.SelectedIndex] = TaskInput.Text;
                TaskInput.Clear();
            }
        }

        /// <summary>
        /// Deletes the selected task from the task list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListBox.SelectedItem != null)
            {
                tasks.RemoveAt(TasksListBox.SelectedIndex);
            }
        }

        /// <summary>
        /// Clears all tasks from the task list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void NewTasks_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            tasks.Clear();
        }

        /// <summary>
        /// Loads tasks from a text file into the task list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void LoadTasks_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Textdatei (*.txt)|*.txt"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                var lines = File.ReadAllLines(openFileDialog.FileName);
                tasks.Clear();
                foreach (var line in lines)
                {
                    tasks.Add(line);
                }
            }
        }

        /// <summary>
        /// Saves the current tasks to a text file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void SaveTasks_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Textdatei (*.txt)|*..txt"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllLines(saveFileDialog.FileName, tasks);
            }
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void Close_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}