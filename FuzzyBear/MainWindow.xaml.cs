
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
        //private string filePath1 = "";
        //private string filePath2 = "";

        private string seedFilePath = "";
        private List<string> comparisonFilePaths = new List<string>();


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
            InitializeComponent();
        }


        private void Compare_Button_Click(object sender, RoutedEventArgs e)
        {
            // check that both file drops are set
            if (!String.IsNullOrEmpty(seedFilePath) && comparisonFilePaths.Count > 0)
            {
                //System.Diagnostics.Debug.WriteLine("\n\n\nseedFilePath:");
                //System.Diagnostics.Debug.WriteLine(seedFilePath);
                OutputBox.Items.Add("--START FUZZY BEARS--");
                //OutputBox.Items.Add("INFO -> Comparison Score; 0 - not similar; 100 - identical");
                OutputBox.Items.Add(String.Format("Seed file: {0}", System.IO.Path.GetFileName(seedFilePath)));
                var seedBytes = File.ReadAllBytes(seedFilePath);
                var seedHash = SsdeepNET.Hasher.HashBuffer(seedBytes, seedBytes.Length);
                OutputBox.Items.Add(String.Format("Seed file hash: {0}", seedHash));

                foreach (string filepath in comparisonFilePaths)
                {
                    //check if file exists
                    OutputBox.Items.Add(String.Format("\tComparing {0} to seedfile...", System.IO.Path.GetFileName(filepath)));
                    var compBytes = File.ReadAllBytes(filepath);
                    var compHash = SsdeepNET.Hasher.HashBuffer(compBytes, compBytes.Length);
                    var comparisionResult = Comparer.Compare(seedHash, compHash);
                    OutputBox.Items.Add(String.Format("\t\tCompared file hash: {0}", compHash));
                    OutputBox.Items.Add(String.Format("\t\tComparison Score: {0}", comparisionResult));
                    OutputBox.Items.Add("");
                }
                OutputBox.Items.Add("");
                OutputBox.Items.Add("--END FUZZY BEARS--");
                OutputBox.Items.Add("");
            }
            else
            {
                CompareButton.IsEnabled = false;
            }

            //MessageBox.Show("fileOnePath: " + fileOnePath + "\nfileTwoPath: " + fileTwoPath);
        }

        private void File_Selected(object sender, EventArgs e)
        {
            if (FileOneSelector.getSelectedFilePath().Count > 0)
            {
                seedFilePath = FileOneSelector.getSelectedFilePath()[0];
            }
            
            comparisonFilePaths = FileTwoSelector.getSelectedFilePath();

            ////System.Diagnostics.Debug.WriteLine("Lookie here: ");
            //System.Diagnostics.Debug.WriteLine(String.Format("SEED FILE: {0}\n ", seedFilePath));
            //var i = 0;
            //foreach (string s in comparisonFilePaths)
            //{
            //    System.Diagnostics.Debug.WriteLine(String.Format("comp file {0}: {1}", i.ToString(), s));
            //    i++;
            //}
            //System.Diagnostics.Debug.WriteLine("END LOOKIE HERE");


            // if both files are selected, enable compare button, else disable it.
            if (!String.IsNullOrEmpty(seedFilePath) && comparisonFilePaths.Count > 0)
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
