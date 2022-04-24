using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer.Discovery;
using ZebraPrinter.WPF.Models;
using ZebraPrinter.WPF.Services;

namespace ZebraPrinter.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Property Definations
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private string _BarcodeText;

        public string BarcodeText
        {
            get { return _BarcodeText; }
            set
            {
                _BarcodeText = value;
                NotifyPropertyChanged();
            }
        }
        private string _txt_one;

        public string txt_one
        {
            get { return _txt_one; }
            set
            {
                _txt_one = value;
                NotifyPropertyChanged();
            }
        }
        private string _txt_two;

        public string txt_two
        {
            get { return _txt_two; }
            set
            {
                _txt_two = value;
                NotifyPropertyChanged();
            }
        }
        private string _txt_three;

        public string txt_three
        {
            get { return _txt_three; }
            set
            {
                _txt_three = value;
                NotifyPropertyChanged();
            }
        }

        private string _txt_four;

        public string txt_four
        {
            get { return _txt_four; }
            set
            {
                _txt_four = value;
                NotifyPropertyChanged();
            }
        }
        private string _txt_five;

        public string txt_five
        {
            get { return _txt_five; }
            set
            {
                _txt_five = value;
                NotifyPropertyChanged();
            }
        }
        private string _txt_six;

        public string txt_six
        {
            get { return _txt_six; }
            set
            {
                _txt_six = value;
                NotifyPropertyChanged();
            }
        }
        private int _Copy;

        public int Copy
        {
            get { return _Copy; }
            set
            {
                _Copy = value;
                NotifyPropertyChanged();
            }
        }



        #endregion

        #region  Page error 

        Dictionary<string, string> validationErrors = new Dictionary<string, string>();
        public string Error
        {
            get
            {
                return null;
            }
        }
        public string this[string name]
        {
            get
            {
                string result = null;
                if (name == "BarcodeText")
                {
                    if (BarcodeText == null || BarcodeText.Trim() == "")
                    {
                        result = "This Field Cannot Be Empty...";
                        if (!validationErrors.ContainsKey(name))
                            validationErrors.Add(name, result);
                    }
                    else
                    {
                        validationErrors.Remove(name);
                    }

                }
                else if (name == "txt_one")
                {
                    if (txt_one == null || txt_one.Trim() == "")
                    {
                        result = "This Field Cannot Be Empty";
                        if (!validationErrors.ContainsKey(name))
                            validationErrors.Add(name, result);
                    }
                    else
                    {
                        validationErrors.Remove(name);
                    }
                }
                else if (name == "txt_two")
                {
                    if (txt_two == null || txt_two.Trim() == "")
                    {
                        result = "This Field Cannot Be Empty";
                        if (!validationErrors.ContainsKey(name))
                            validationErrors.Add(name, result);
                    }
                    else
                    {
                        validationErrors.Remove(name);
                    }
                }
                else if (name == "txt_three")
                {
                    if (txt_three == null || txt_three.Trim() == "")
                    {
                        result = "This Field Cannot Be Empty";
                        if (!validationErrors.ContainsKey(name))
                            validationErrors.Add(name, result);
                    }
                    else
                    {
                        validationErrors.Remove(name);
                    }
                }
                else if (name == "txt_four")
                {
                    if (txt_four == null || txt_four.Trim() == "")
                    {
                        result = "This Field Cannot Be Empty";
                        if (!validationErrors.ContainsKey(name))
                            validationErrors.Add(name, result);
                    }
                    else
                    {
                        validationErrors.Remove(name);
                    }
                }
                else if (name == "txt_five")
                {
                    if (txt_five == null || txt_five.Trim() == "")
                    {
                        result = "This Field Cannot Be Empty";
                        if (!validationErrors.ContainsKey(name))
                            validationErrors.Add(name, result);
                    }
                    else
                    {
                        validationErrors.Remove(name);
                    }
                }
                else if (name == "txt_six")
                {
                    if (txt_six == null || txt_six.Trim() == "")
                    {
                        result = "This Field Cannot Be Empty";
                        if (!validationErrors.ContainsKey(name))
                            validationErrors.Add(name, result);
                    }
                    else
                    {
                        validationErrors.Remove(name);
                    }
                }


                return result;
            }
        }

        #endregion
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            List<MasterArticle> articles =  CommonService.GetAllMasterArticles()?.ToList();
        }
        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            for (int copycounter = 0; copycounter < Copy; copycounter++)
            {
                basicPrint(BarcodeText, txt_one, txt_two, txt_three, txt_four, txt_five, txt_six);
            }
        }
        public List<DiscoveredPrinter> GetUSBPrinters()
        {
            List<DiscoveredPrinter> printerList = new List<DiscoveredPrinter>();
            try
            {
                foreach (DiscoveredUsbPrinter usbPrinter in UsbDiscoverer.GetZebraUsbPrinters())
                {
                    printerList.Add(usbPrinter);
                    Console.WriteLine(usbPrinter);
                }
            }
            catch (ConnectionException e)
            {
                Console.WriteLine($"Error discovering local printers: {e.Message}");
            }


            Console.WriteLine("Done discovering local printers.");
            return printerList;
        }
        public void basicPrint(string zplbarcode, string lbl_one, string lbl_two, string lbl_three, string lbl_four, string lbl_five, string lbl_six)
        {
            List<DiscoveredPrinter> printerList = GetUSBPrinters();

            string zpl_string = @"^XA
                    ^FO155,20^A0,25^FDTWELVE^FS
                    ^FO10,50^A0,15^FDFSNA-PANB-TB22-05F-126^FS 
                    ^FO300,50^A0,15^FD6/7.^FS
                    ^FO10,70^A0,15^FDBOYS PANJABI^FS 
                    ^FO300,70^A0,15^FDBLUE^FS
                    ^FO85,90^BY2^BCN,50,,,,A^FD10010159849^FS
                    ^FO50,175^A0,20^FDBDT: 250.0000^FS
                    ^FO300,175^A0,20^FD(+Vat)^FS

                    ^FO565,20^A0,25^FDTWELVE^FS
                    ^FO420,50^A0,15^FDFSNA-PANB-TB22-05F-126^FS 
                    ^FO710,50^A0,15^FD6/7.^FS
                    ^FO420,70^A0,15^FDBOYS PANJABI^FS 
                    ^FO710,70^A0,15^FDBLUE^FS
                    ^FO495,90^BY2^BCN,50,,,,A^FD10010159849^FS
                    ^FO460,175^A0,20^FDBDT: 250.0000^FS
                    ^FO710,175^A0,20^FD(+Vat)^FS
                    ^XZ";
            if (printerList.Count > 0)
            {
                // in this case, we arbitrarily are printing to the first found printer  
                DiscoveredPrinter discoveredPrinter = printerList[0];
                Connection connection = discoveredPrinter.GetConnection();
                connection.Open();
                connection.Write(Encoding.UTF8.GetBytes(zpl_string));
            }
            else Console.WriteLine("No Printers found to print to.");


        }


    }
}
