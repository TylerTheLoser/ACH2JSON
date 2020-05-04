using System;
using System.Windows.Forms;
using System.IO;

namespace ACH2JSON
{
    public partial class mainWindow : Form
    {
        public mainWindow()
        {
            InitializeComponent();
        }

        public string thatpath { get; set; }
        public void SaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JSON Files | *.json";
            sfd.DefaultExt = "json";
            string sfdname = sfd.FileName;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Path.GetFileName(sfd.FileName);
                thatpath = Path.GetFullPath(sfd.FileName);
                //Console.WriteLine("from main: " + thatpath);
            }
            else
            {
                Console.WriteLine("bad path");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "C:\\";
                ofd.Filter = "dat files (*.dat)|*.dat|All files (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;
                    SaveFile();
                    var fileStream = ofd.OpenFile();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        Conversion conv = new Conversion();
                        //Console.WriteLine("from file save " + thatpath);
                        conv.getFilePath(thatpath);
                        string line;
                        int counter = 0;
                        int PECount = 0;
                        int ADCount = 0;
                        while((line = reader.ReadLine()) != null)
                        {
                            if(line.Substring(0, 1).Contains("1"))
                            {
                                Console.WriteLine("File Header Record");
                                //Console.WriteLine(line); //debug
                                conv.FHRConv(line); //parse the file header record
                                counter++;
                            } else if(line.Substring(0, 1).Contains("5"))
                            {
                                Console.WriteLine("Batch Header Record");
                                //Console.WriteLine(line); //debug
                                int gimme = conv.BHRConv(line);
                                if(gimme == 1) //CTX
                                {
                                    //somehow pass this to the payment entry reading...
                                    Console.WriteLine("CTX File!");
                                } else if(gimme == 2) //CCD
                                {
                                    Console.WriteLine("CCD File!");
                                } else if(gimme == 3) //PPD
                                {
                                    Console.WriteLine("PPD file!");
                                } else if(gimme == 4) //TEL
                                {
                                    Console.WriteLine("TEL file!");
                                } else if(gimme == 5) //WEB
                                {
                                    Console.WriteLine("WEB file!");
                                } else if(gimme == 9)
                                {
                                    Console.WriteLine("default value returned");
                                }
                                counter++;
                            } else if(line.Substring(0, 1).Contains("6"))
                            {
                                //what i believe should happen is a new PE construct should be initialized each time
                                //PEConstruct pec = new PEConstruct(line);
                                conv.PEConv(line);
                                Console.WriteLine("Payment Entry");
                                //Console.WriteLine(line); //debug
                                PECount++;
                                conv.PaymentCount = PECount;
                                counter++;
                            } else if (line.Substring(0, 1).Contains("7"))
                            {
                                Console.WriteLine("Addenda Record");
                                //Console.WriteLine(line); //debug
                                conv.ARConv(line);
                                ADCount++;
                                conv.AddendaCount = ADCount;
                                counter++;
                            } else if (line.Substring(0, 1).Contains("8"))
                            {
                                conv.BCRConv(line);
                                Console.WriteLine("Batch Control Record");
                                //Console.WriteLine(line); //debug
                                counter++;
                            } else if (line.Substring(0, 1).Contains("9"))
                            {
                                conv.FCRConv(line);
                                Console.WriteLine("File Control Record");
                                //Console.WriteLine(line); //debug
                                counter++;
                            } else //test this
                            {
                                MessageBox.Show("Unknown Record Found, check debug");
                                Console.WriteLine("Unknown Record at line {0}", counter);
                                Console.WriteLine(line);
                                counter++;
                            }
                        }
                        MessageBox.Show("Conversion Complete.");
                        reader.Close();
                        
                        Console.WriteLine("There were {0} lines.", counter);
                    }
                }
            }
        }
    }
}
