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
        List<string> folders = new List<string>()
        {
            "Litteratur",
            "Opgaver",
            "Materialer",
            "Diverse",
            "Lektier",
            "Gode Links",
            "Tips",
        };
        List<int> semesters = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 });

        public MainWindow()
        {
            InitializeComponent();
            semesterComboBox.Items.Add("Angiv semester");

            foreach (int value in semesters)
            {
                semesterComboBox.Items.Add(value.ToString() + ". Semester");
            }

            semesterComboBox.SelectedIndex = 0;

        }

        private void CleanUp()
        {
            foreach (System.Windows.Controls.Control childCtrl in MainGrid.Children)
            {
                if (childCtrl is System.Windows.Controls.TextBox)
                {
                    System.Windows.Controls.TextBox Txtbox = (System.Windows.Controls.TextBox)childCtrl;
                    Txtbox.Clear();
                }
            }
            semesterComboBox.SelectedIndex = 0;
        }

        private void CreateDirectories()
        {
            string folderPath = selectedPath + "\\" + semesterComboBox.SelectedItem;

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
        }
        private void CreatePath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selectedPath = fbd.SelectedPath;
                pathLbl.Content = selectedPath;
            }
        }

        private void CreateFolders_Click(object sender, RoutedEventArgs e)
        {
            if (semesterComboBox.SelectedIndex == 0)
            {
                System.Windows.Forms.MessageBox.Show("Du skal angive et semester");
                semesterComboBox.SelectedIndex = 0;
                return;
            }

            if (selectedPath == null)
            {
                System.Windows.Forms.MessageBox.Show("Du skal angive en sti til dine mapper");
                return;
            }

            CreateDirectories();

            CleanUp();
            System.Windows.Forms.MessageBox.Show("Mapper oprettet");
        }
    }
}
