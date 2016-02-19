
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
using System.Runtime.InteropServices;
using SsdeepNET;

namespace FuzzyBear
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string filePath1 = "";
        private string filePath2 = "";


        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        // A delegate type for hooking up change notifications.
        public delegate void ChangedFileSelection(object sender, EventArgs e);

        public MainWindow()
        {
            //string str1= "Also called fuzzy hashes, Ctph can match inputs that have homologies.";
            //var bytesStr1 = GetBytes(str1);
            //string str2= "Also called fuzzy hashes, CTPH can match inputs that have homologies.";
            //var bytesStr2 = GetBytes(str2);
            

            //var str1_hash = SsdeepNET.Hasher.HashBuffer(bytesStr1, bytesStr1.Length);
            //var str2_hash = SsdeepNET.Hasher.HashBuffer(bytesStr2, bytesStr2.Length);
            //var comparisionResult = Comparer.Compare(str1_hash, str2_hash);

            //System.Diagnostics.Debug.WriteLine("Lookie here: ");
            //System.Diagnostics.Debug.WriteLine(str1_hash);
            //System.Diagnostics.Debug.WriteLine(str2_hash);
            //System.Diagnostics.Debug.WriteLine(comparisionResult);
            //System.Diagnostics.Debug.WriteLine(test.ToString());
            InitializeComponent();
        }

        
        private void Compare_Button_Click(object sender, RoutedEventArgs e)
        {
            // check that both file drops are set
            if (filePath1 != null && filePath2 != null)
            {
                System.Diagnostics.Debug.WriteLine("Lookie here: ");
                var bytesFile1 = File.ReadAllBytes(filePath1);
                var bytesFile2 = File.ReadAllBytes(filePath2);


                var str1_hash = SsdeepNET.Hasher.HashBuffer(bytesFile1, bytesFile1.Length);
                var str2_hash = SsdeepNET.Hasher.HashBuffer(bytesFile2, bytesFile2.Length);
                var comparisionResult = Comparer.Compare(str1_hash, str2_hash);

                OutputBox.Items.Add("--START--");
                OutputBox.Items.Add(String.Format("Comparing {0} and {1}", System.IO.Path.GetFileName(filePath1), System.IO.Path.GetFileName(filePath2)));
                OutputBox.Items.Add("\nComparison Score: " + comparisionResult);
                OutputBox.Items.Add("\nFile 1 fuzzy Hash: " + str1_hash);
                OutputBox.Items.Add("File 2 fuzzy Hash: " + str2_hash);
                
                
                OutputBox.Items.Add("\n--END--\n");
               

            }
            else
            {
                CompareButton.IsEnabled = false;


            }

            //MessageBox.Show("fileOnePath: " + fileOnePath + "\nfileTwoPath: " + fileTwoPath);
        }

        private void File_Selected(object sender, EventArgs e)
        {
            filePath1 = FileOneSelector.getSelectedFilePath();
            filePath2 = FileTwoSelector.getSelectedFilePath();

            //MessageBox.Show("fileOnePath: " + fileOnePath + "\nfileTwoPath: " + fileTwoPath);
            //MessageBox.Show("fileOnePath not null? " + (fileOnePath != null).ToString() + "\nfileTwoPath not null? " + (fileTwoPath != null).ToString());

            // if both files are selected, enable compare button, else disable it.
            if (filePath1 != null && filePath2 != null)
            {
                CompareButton.IsEnabled = true;
            }
            else
            {
                CompareButton.IsEnabled = false;
            }

        }



    }
}
