using Microsoft.Win32;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
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

namespace Progressbar_ToolTipDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class PdfFileInfo
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the click event of the PDF Import button. Opens a dialog to select PDF files,
        /// adds them to the list if not already present, and displays a message if a duplicate is detected
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An RoutedEventArgs that contains the event data</param>
        private void BtnPdfImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    var pdfFile = new PdfFileInfo
                    {
                        FileName = System.IO.Path.GetFileName(filePath),
                        FilePath = filePath
                    };

                    if (!pdfListBox.Items.Cast<PdfFileInfo>().Any(p => p.FilePath == pdfFile.FilePath))
                    {
                        pdfListBox.Items.Add(pdfFile);
                    }
                    else
                    {
                        MessageBox.Show($"{pdfFile.FileName} wurde bereits importiert.", "Duplikat erkannt", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the click event of the Move Up button. Moves the selected PDF item up in the list
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An RoutedEventArgs that contains the event data</param>
        private void MovePdfUp_Click(object sender, RoutedEventArgs e)
        {
            MoveSelectedItem(-1);
        }

        /// <summary>
        /// Handles the click event of the Move Down button. Moves the selected PDF item down in the list
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An RoutedEventArgs that contains the event data</param>
        private void MovePdfDown_Click(object sender, RoutedEventArgs e)
        {
            MoveSelectedItem(1);
        }

        /// <summary>
        /// Moves the selected item in the PDF list either up or down based on the direction provided
        /// </summary>
        /// <param name="direction">The direction to move the selected item. -1 for up, 1 for down</param>
        private void MoveSelectedItem(int direction)
        {
            int selectedIndex = pdfListBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                int newIndex = selectedIndex + direction;
                if (newIndex < 0 || newIndex >= pdfListBox.Items.Count)
                    return;

                var itemToMove = pdfListBox.Items[selectedIndex];
                pdfListBox.Items.RemoveAt(selectedIndex);
                pdfListBox.Items.Insert(newIndex, itemToMove);
                pdfListBox.SelectedIndex = newIndex;
            }
        }

        /// <summary>
        /// Merges imported pdf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MergePdf_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                DefaultExt = "pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    PdfDocument outputDocument = new PdfDocument();
                    int fileCount = pdfListBox.Items.Count;
                    MergeProgressBar.Visibility = Visibility.Visible;

                    for (int i = 0; i < fileCount; i++)
                    {
                        PdfFileInfo pdfFile = (PdfFileInfo)pdfListBox.Items[i];
                        PdfDocument inputDocument = PdfReader.Open(pdfFile.FilePath, PdfDocumentOpenMode.Import);

                        foreach (PdfPage page in inputDocument.Pages)
                        {
                            outputDocument.AddPage(page);
                        }

                        //update progress
                        double progress = (double)(i + 1) / fileCount * 100;
                        MergeProgressBar.Value = progress;
                    }

                    outputDocument.Save(saveFileDialog.FileName);
                    MessageBox.Show("PDF-Dateien wurden erfolgreich zusammengeführt.", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);

                    // ProgressBar reset and hide
                    MergeProgressBar.Value = 0;
                    MergeProgressBar.Visibility = Visibility.Hidden;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    // ProgressBar reset and hide if error
                    MergeProgressBar.Value = 0;
                    MergeProgressBar.Visibility = Visibility.Hidden;
                }
            }
        }

    }
}