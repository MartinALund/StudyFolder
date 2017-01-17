using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudyFolder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string selectedPath;
        static List<string> folders = new List<string>()
        {
            "Litteratur",
            "Opgaver",
            "Materialer",
            "Diverse",
            "Lektier",
            "Gode Links",
            "Tips",
        };

        public MainWindow()
        {
            InitializeComponent();
            comboBox.Items.Add("1");
            comboBox.Items.Add("2");
            comboBox.Items.Add("3");
            comboBox.Items.Add("4");
            comboBox.Items.Add("5");
            comboBox.Items.Add("6");
            comboBox.Items.Add("7");
            comboBox.Items.Add("8");
            comboBox.Items.Add("9");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            string folderPath = selectedPath + "\\" + comboBox.SelectedItem + ". Semester";
            System.IO.Directory.CreateDirectory(folderPath);

            foreach (System.Windows.Controls.Control childCtrl in MainGrid.Children)
            {
                if (childCtrl is System.Windows.Controls.TextBox)
                {
                    System.Windows.Controls.TextBox Txtbox = (System.Windows.Controls.TextBox)childCtrl;
                    if (Txtbox.Text != string.Empty)
                    {
                        string subject = Txtbox.Text;
                        string subjectPath = folderPath + "\\" + subject;
                        foreach (var item in folders)
                        {
                            string path = System.IO.Path.Combine(subjectPath, item);
                            System.IO.Directory.CreateDirectory(path);
                        }
                    }
                }
            }
            System.Windows.Forms.MessageBox.Show("Folders & subfolders created successfully");
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selectedPath = fbd.SelectedPath;
                pathLbl.Content = selectedPath;
            }
        }
    }
}
