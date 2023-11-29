using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;

namespace Ribbon_ToolbarDemo
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the New File command. Clears the MainTextBox to start a new document.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            MainTextBox.Clear();
        }

        /// <summary>
        /// Handles the Open File command. Opens a file dialog, reads the selected file, and displays its content in MainTextBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                MainTextBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// Handles the Save File command. Opens a save file dialog and writes the content of MainTextBox to the selected file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, MainTextBox.Text);
            }
        }

        // Documentation for other functions like PrintFile_Click, CloseFile_Click, etc. can be added here.

        /// <summary>
        /// Handles the Undo command. Undoes the last action in MainTextBox, if possible.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (MainTextBox.CanUndo)
            {
                MainTextBox.Undo();
            }
        }

        /// <summary>
        /// Handles the Redo command. Redoes the last undone action in MainTextBox, if possible.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            if (MainTextBox.CanRedo)
            {
                MainTextBox.Redo();
            }
        }

        /// <summary>
        /// Handles the Cut command. Cuts the selected text from MainTextBox and places it in the clipboard.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            MainTextBox.Cut();
        }

        /// <summary>
        /// Handles the Copy command. Copies the selected text from MainTextBox to the clipboard.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            MainTextBox.Copy();
        }

        /// <summary>
        /// Handles the Paste command. Pastes the text from the clipboard into MainTextBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            MainTextBox.Paste();
        }

        /// <summary>
        /// Handles the Search command. Placeholder for search functionality.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            // Placeholder for search functionality
        }

        /// <summary>
        /// Handles the Exit Application command. Closes the application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void ExitApplication_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Placeholder for Ribbon Selection Changed event. Currently has no implementation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data that provides information about the event.</param>
        private void Ribbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Placeholder for ribbon selection changed event
        }

    }
}