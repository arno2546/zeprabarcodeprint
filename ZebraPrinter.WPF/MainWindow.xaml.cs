using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        List<MasterArticle> SELECTED_ARTICLES = new List<MasterArticle>();
        //List<Vw_MasterArticle> ALL_ARTICLES = new List<Vw_MasterArticle>();
        bool IS_VAT_INCLUSIVE = false;
        MasterArticleDataContext MasterArticleDataContext =
            new MasterArticleDataContext(Properties.Settings.Default.TwelveInventoryConnectionString);
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            articlesGrid.ItemsSource = SELECTED_ARTICLES;
            //SELECTED_ARTICLES =  CommonService.GetAllMasterArticles()?.ToList();
            //ALL_ARTICLES = MasterArticleDataContext.ExecuteQuery<Vw_MasterArticle>("SELECT * FROM Vw_MasterArticle").ToList();
            //CustomerCmboBx.ItemsSource = ALL_ARTICLES;
        }
        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            basicPrint();
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
        public void basicPrint()
        {
            //List<DiscoveredPrinter> printerList = GetUSBPrinters();

            //List<MasterArticle> tempArticleList = new List<MasterArticle>();

            //foreach(MasterArticle article in SELECTED_ARTICLES)
            //{
            //    for(int i = 1; i <= article.Qty; i++)
            //    {
            //        tempArticleList.Add(article);
            //    }
            //}

            for (int i = 0; i < SELECTED_ARTICLES.Count; i++)
            {
                for (int j = 1; j <= SELECTED_ARTICLES[i].Qty; j++)
                {
                    if (SELECTED_ARTICLES[i].Qty > 1)
                    {
                        //Console.WriteLine(SELECTED_ARTICLES[i].ProductName + " x2");
                        printZpl(GetFormattedZPLString(SELECTED_ARTICLES[i]));
                        SELECTED_ARTICLES[i].Qty -= 2;
                    }
                    else if (SELECTED_ARTICLES[i].Qty == 1)
                    {                     
                        //Console.WriteLine(SELECTED_ARTICLES[i].ProductName + " x1");
                        SELECTED_ARTICLES[i].Qty -= 1;
                        if (SELECTED_ARTICLES.ElementAtOrDefault(i + 1) != null)
                        {
                            printZpl(GetFormattedZPLString(SELECTED_ARTICLES[i], SELECTED_ARTICLES[i+1]));
                            SELECTED_ARTICLES[i + 1].Qty -= 1;
                            //Console.WriteLine(SELECTED_ARTICLES[i+1].ProductName + " x1");
                        }
                        else
                        {
                            printZpl(GetFormattedZPLString(SELECTED_ARTICLES[i], false));
                        }
                    }
                }
            }
            clearSelection();
        }

        public string GetFormattedZPLString(MasterArticle article, bool dual = true)
        {
            string left = $@"^XA
                ^FO155,20^A0,25^FDTWELVE^FS
                ^FO10,50^A0,15^FD{article.ProductName}^FS 
                ^FO300,50^A0,15^FD{article.SizeName}^FS
                ^FO10,70^A0,15^FD{article.GroupName}^FS 
                ^FO300,70^A0,15^FD{article.ColorName}^FS
                ^FO85,90^BY2^BCN,50,,,,A^FD{article.Barcode}^FS
                ^FO50,175^A0,20^FDBDT: {getArticleRPU(article)}^FS
                ^FO300,175^A0,20^FD{(IS_VAT_INCLUSIVE ? "(+Vat)" : "")}^FS";

            string right = $@"^FO565,20^A0,25^FDTWELVE^FS
                ^FO420,50^A0,15^FD{article.ProductName}^FS 
                ^FO710,50^A0,15^FD{article.SizeName}^FS
                ^FO420,70^A0,15^FD{article.GroupName}^FS 
                ^FO710,70^A0,15^FD{article.ColorName}^FS
                ^FO495,90^BY2^BCN,50,,,,A^FD{article.Barcode}^FS
                ^FO460,175^A0,20^FDBDT: {getArticleRPU(article)}^FS
                ^FO710,175^A0,20^FD{(IS_VAT_INCLUSIVE ? "(+Vat)" : "")}^FS
                ^XZ";
            return dual == true ? left + right : left + "^XZ";
        }
        
        public string GetFormattedZPLString(MasterArticle leftArticle, MasterArticle rightArticle)
        {
            return $@"^XA
                ^FO155,20^A0,25^FDTWELVE^FS
                ^FO10,50^A0,15^FD{leftArticle.ProductName}^FS 
                ^FO300,50^A0,15^FD{leftArticle.SizeName}^FS
                ^FO10,70^A0,15^FD{leftArticle.GroupName}^FS 
                ^FO300,70^A0,15^FD{leftArticle.ColorName}^FS
                ^FO85,90^BY2^BCN,50,,,,A^FD{leftArticle.Barcode}^FS
                ^FO50,175^A0,20^FDBDT:{getArticleRPU(leftArticle)}^FS
                ^FO300,175^A0,20^FD{(IS_VAT_INCLUSIVE ? "(+Vat)" : "")}^FS

                ^FO565,20^A0,25^FDTWELVE^FS
                ^FO420,50^A0,15^FD{rightArticle.ProductName}^FS 
                ^FO710,50^A0,15^FD{rightArticle.SizeName}^FS
                ^FO420,70^A0,15^FD{rightArticle.GroupName}^FS 
                ^FO710,70^A0,15^FD{rightArticle.ColorName}^FS
                ^FO495,90^BY2^BCN,50,,,,A^FD{rightArticle.Barcode}^FS
                ^FO460,175^A0,20^FDBDT:{getArticleRPU(rightArticle)}^FS
                ^FO710,175^A0,20^FD{(IS_VAT_INCLUSIVE ? "(+Vat)" : "")}^FS
                ^XZ";
        }

        public void printZpl(string zplString)
        {
            List<DiscoveredPrinter> printerList = GetUSBPrinters();
            if (printerList.Count > 0)
            {
                // in this case, we arbitrarily are printing to the first found printer  
                DiscoveredPrinter discoveredPrinter = printerList[0];
                Connection connection = discoveredPrinter.GetConnection();
                connection.Open();
                connection.Write(Encoding.UTF8.GetBytes(zplString));
            }
            else Console.WriteLine("No Printers found to print to.");
        }

        public decimal getArticleRPU(MasterArticle article)
        {
            if (IS_VAT_INCLUSIVE)
            {
                return article.RPU + (article.RPU * article.VatPercent / 100);
            }
            return article.RPU;
        }

        private void articleSearchBx_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && articleSearchBx.CurrentText.Length > 0)
            {
                //ProductGrid.ItemsSource = ALL_ARTICLES
                //    .Where(article => (article.Barcode != null && article.Barcode.Contains(articleSearchBx.CurrentText)) 
                //    || (article.ProductName != null && article.ProductName.Contains(articleSearchBx.CurrentText)))?.ToList() ?? new List<Vw_MasterArticle>();

                ProductGrid.ItemsSource = MasterArticleDataContext.ExecuteQuery<Vw_MasterArticle>($"SELECT TOP 100 * FROM Vw_MasterArticle WHERE ProductName like '%{articleSearchBx.CurrentText}%' or Barcode like '%{articleSearchBx.CurrentText}%'").ToList();
                prodGridPopUp.IsOpen = true;
            }
        }

        private void ProductGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Vw_MasterArticle selectedArticle = ProductGrid.SelectedItem as Vw_MasterArticle;
            List<Vw_MasterArticle> articlesToAdd = MasterArticleDataContext
                .ExecuteQuery<Vw_MasterArticle>($"SELECT * FROM Vw_MasterArticle WHERE ProductName = '{selectedArticle.ProductName}'")
                .ToList();
            foreach (Vw_MasterArticle article in articlesToAdd)
            {
                addToSelectedArticles(CommonService.ConvertToMasterArticle(article));
            }
        }

        private void addToSelectedArticles(MasterArticle articleToAdd)
        {
            MasterArticle existingArticle = SELECTED_ARTICLES.Where(article => article.Barcode == articleToAdd.Barcode).FirstOrDefault();
            if (existingArticle != null)
            {
                existingArticle.Qty += 1;
            }
            else
            {
                SELECTED_ARTICLES.Add(articleToAdd);
            }
            articlesGrid.Items.Refresh();
        }      

        private void vatInclusiveCheck_Click(object sender, RoutedEventArgs e)
        {
            IS_VAT_INCLUSIVE = vatInclusiveCheck?.IsChecked ?? false;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            clearSelection();
        }

        private void clearSelection()
        {
            SELECTED_ARTICLES.Clear();
            articlesGrid.Items.Refresh();
        }
    }
}
