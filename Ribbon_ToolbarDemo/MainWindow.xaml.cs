using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;

namespace Ribbon_ToolbarDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            // Logik zum Erstellen eines neuen Dokuments
            MainTextBox.Clear();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                MainTextBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, MainTextBox.Text);
            }
        }

        // Weitere Funktionen wie PrintFile_Click, CloseFile_Click, etc.

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (MainTextBox.CanUndo)
            {
                MainTextBox.Undo();
            }
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            if (MainTextBox.CanRedo)
            {
                MainTextBox.Redo();
            }
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            MainTextBox.Cut();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            MainTextBox.Copy();
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            MainTextBox.Paste();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void  ExitApplication_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ribbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

    }
}
    