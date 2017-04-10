using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VDProjDiff;

namespace VDProjdiff
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string BrowseForFile()
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".vdproj";
            dlg.Filter = "Visual studio installer projects files (*.vdproj)|*.vdproj";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                return dlg.FileName;
            }
            return String.Empty;
        }

        private void OldFileClicked(object sender, RoutedEventArgs e)
        {
           oldfilelocation.Text = BrowseForFile();
        }

        private void NewFileClicked(object sender, RoutedEventArgs e)
        {
            newfilelocation.Text = BrowseForFile();
        }

        private void ProcessfilesClick(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";


            Cursor oldCursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            
            try
            {
                
                if (String.IsNullOrEmpty(oldfilelocation.Text) || String.IsNullOrEmpty(newfilelocation.Text))
                {
                    MessageBox.Show("Please fill in both files");
                    return;
                }

                if (!System.IO.File.Exists(oldfilelocation.Text))
                {
                    MessageBox.Show("Old file doesn't exist.");
                    return;
                }

                if (!System.IO.File.Exists(newfilelocation.Text))
                {
                    MessageBox.Show("New file doesn't exist.");
                    return;
                }

                // now determine differences.
                VDProjComparer comp = new VDProjComparer(oldfilelocation.Text, newfilelocation.Text);
                textBox.Text = comp.ToString();
            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Mouse.OverrideCursor = oldCursor;
            }

            
        }
    }
}
