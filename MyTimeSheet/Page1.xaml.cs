using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Resources;
using System.Globalization;
using System.Collections;
using System.Xml;
using System.Net;
using System.Windows.Threading;
using System.Configuration;
using Microsoft.Win32;

namespace MyTimeSheet
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        List<EveryDayControl> list = new List<EveryDayControl>();
        List<string> allUser = new List<string>();
        EveryDayControl last = new EveryDayControl();
        DateTime now = DateTime.Now;
        double screenH = 0;
        double screenW = 0;
        bool HasSelf = false;

        public static Loading loadingPage
        {
            set;
            get;
        }

        public static Loading2 loadingPage2
        {
            set;
            get;
        }
        
        public Page1()
        {
            InitializeComponent();
            
            CaluScreen();
            GetConfigFile();
            ShowItem();
        }
        public void CaluScreen()
        {
            screenH = SystemParameters.WorkArea.Height;
            screenW = SystemParameters.WorkArea.Width;
            this.Width = screenW - 10;
            this.Height = screenH - 100;
            MyCanvas.Height = screenH - 200;
                
        }

        public void GetConfigFile()
        {
            string usernameAddress = ConfigurationSettings.AppSettings.Get("username").ToString();
            Information.UserNameAddress = usernameAddress;
            string fileget = GetXMLConfig(Information.UserNameAddress);
            if (fileget != "" && fileget != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(fileget);
                Information.UserNameFile = doc;
            }
            else
            {
                MessageBox.Show("Can't get the UserName.xml file, \n Are you in the fareast domain?");
                App.Current.Shutdown();
                
            }
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            EveryDayControl everyday = new EveryDayControl();
            list.Add(everyday);
            GetLogo(everyday);
            everyday.DisPlayName = everyday.AliasToUserName(System.Environment.UserName);

            everyday.Weeks = WeekOfYear(calendar1.SelectedDate.Value).ToString();
            MyCanvas.Items.Add(everyday);
            button1.IsEnabled = false;

        }

        private void calendar1_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime dt = calendar1.SelectedDate.Value;
            int weekofyear_now = WeekOfYear(now);
            int weekofyear = WeekOfYear(dt);
            HasSelf = false;
            button1.IsEnabled = true;
            SelectedWeekLabel.Content = weekofyear.ToString();
            if (weekofyear_now != weekofyear)
            {
                SelectedWeekLabel.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                SelectedWeekLabel.Foreground = new SolidColorBrush(Colors.Black);
            }

            Thread newWindowThread = new Thread(new ThreadStart(LoadingPage2));
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();

            GetData(weekofyear);
            GetOverView(weekofyear);
            loadingPage2.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
        (Action)delegate() { loadingPage2.Close(); });
        }

        public void GetOverView(int week)
        {
            OverViewPanel.Children.Clear();
            allUser.RemoveRange(0, allUser.Count);
            
            foreach (XmlNode node in Information.UserNameFile.SelectNodes("//name"))
            {
                allUser.Add(node.Attributes[@"id"].Value);
            }
            if (allUser.Count > 0)
            {
                for (int i = 0; i < allUser.Count; i++)
                {
                    Label lb = new Label();
                    lb.Foreground = new SolidColorBrush(Colors.Red);
                    lb.Content = allUser[i];
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (lb.Content.ToString() == list[j].DisPlayName)
                        {
                            lb.Foreground = new SolidColorBrush(Colors.Black);
                            lb.FontStyle = FontStyles.Oblique;
                        }

                    }
                    OverViewPanel.Children.Add(lb);
                }
            }
        }

        private int WeekOfYear(DateTime dt)
        {
            DateTime print = new DateTime(dt.Year, 1, 1);
            return (dt.DayOfYear + print.DayOfWeek - dt.DayOfWeek - 8) / 7 + 2;
        }

        private void ShowItem()
        {
            int weekofyear_now = WeekOfYear(now);
            CurrentWeekLabel.Content = weekofyear_now.ToString();
            SelectedWeekLabel.Content = weekofyear_now.ToString();

            Thread newWindowThread = new Thread(new ThreadStart(LoadingPage));
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();

            GetData(weekofyear_now);
            GetOverView(weekofyear_now);

            loadingPage.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
        (Action)delegate() { loadingPage.Close(); });
        }

        public void LoadingPage()
        {
            loadingPage = new Loading();
            loadingPage.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            loadingPage.Show();
            System.Windows.Threading.Dispatcher.Run();
        }
        public void LoadingPage2()
        {
            loadingPage2 = new Loading2();
            loadingPage2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            loadingPage2.Show();
            System.Windows.Threading.Dispatcher.Run();
        }

        public void GetData(int weekofyear)
        {
           
            TimeSheetService.TimeSheetServiceClient client = new TimeSheetService.TimeSheetServiceClient();
            try
            {
                string[] result = client.GetData(weekofyear);
                if (result == null)
                {
                    MessageBox.Show("You select the " + weekofyear + " week of this year.\n" + "There is NO Data in this week!");
                    for (int i = 0; i < list.Count; i++)
                    {
                        MyCanvas.Items.Remove(list[i]);
                    }

                    list.RemoveRange(0, list.Count);
                    return;
                }
                int totalCount = result.Length;
                int count = totalCount / 6;

                for (int i = 0;i<list.Count;i++)
                {
                    MyCanvas.Items.Remove(list[i]);
                }
                
                list.RemoveRange(0, list.Count);
                for (int i = 0; i < count; i++)
                {
                    EveryDayControl day = new EveryDayControl();
                    day.Weeks = result[i * 6 + 1];
                    day.DisPlayName = result[i * 6 + 2];
                    string alias = day.DisplayAlias(day.DisPlayName);
                    day.Pic = result[i * 6 + 3];
                    GetLogo(day);
                    day.GetDetailMessage();
                    if (alias == System.Environment.UserName)
                    {
                        day.Submit.Visibility = System.Windows.Visibility.Visible;
                        day.button1.Visibility = System.Windows.Visibility.Visible;
                        day.button2.Visibility = System.Windows.Visibility.Visible;
                        HasSelf = true;
                        day.Submit.Click -= new RoutedEventHandler(day.Button_Click);
                        day.Submit.Click += new RoutedEventHandler(day.UpdateMySelfTimeSheet);
                    }
                    else
                    {
                        day.Submit.Visibility = System.Windows.Visibility.Hidden;
                        day.button1.Visibility = System.Windows.Visibility.Hidden;
                        day.button2.Visibility = System.Windows.Visibility.Hidden;
                        
                    }

                    day.Height = Convert.ToInt16(day.DetailNum) * 31 +51;
                    
                    MyCanvas.Items.Add(day);
                    list.Add(day);
                }
                if (HasSelf)
                {
                    button1.IsEnabled = false;
                }
                else
                {
                    button1.IsEnabled = true;
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        public void GetLogo(EveryDayControl everyday)
        {
            Assembly assembly = Assembly.GetAssembly(this.GetType());
            string resourceName = assembly.GetName().Name + ".g";
            ResourceManager rm = new ResourceManager(resourceName, assembly);

            List<string> list = new List<string>();


            using (ResourceSet set = rm.GetResourceSet(CultureInfo.CurrentCulture, true, true))
            {
                foreach (DictionaryEntry res in set)
                {
                    if (res.Key.ToString().Contains("emotions"))
                    {
                        list.Add(res.Key.ToString());
                    }
                }
            }

            Random ra = new Random();
            string filePath = list[ra.Next(0, list.Count)].ToString();
            filePath = "pack://application:,,,/" + filePath;
            everyday.UploadPIC.ImageSource = new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));
        }

        public string GetXMLConfig(string file)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                string result = wc.DownloadString(file);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void Exportbtn_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count > 0)
            {
                int week = 0;
                string title = "";
                if (calendar1.SelectedDate == null)
                {
                    week = WeekOfYear(now);
                    title = "MSNBC_TimeSheet_" + now.Year.ToString() + week.ToString() + ".html";
                }
                else
                {
                    week = WeekOfYear(calendar1.SelectedDate.Value);
                    title = "MSNBC_TimeSheet_" + calendar1.SelectedDate.Value.Year.ToString() + week.ToString() + ".html";
                }
                string file = @"<html><body><center><h2>" + title + "<table border=1 style=\"font-family: Tahoma;font-size:17px;\">";
                foreach (EveryDayControl day in list)
                {
                    file = file + "<tr><td style=\"background-color:Orange\">" + day.DisPlayName + "</td><td style=\"background-color:Fuchsia\">Task Number</td><td style=\"background-color:Lime\">Detail Message</td><td style=\"background-color:Red\">Hours</td><td style=\"background-color:Pink\">Total Hours: " + day.tt + "</tr>";
                    for (int i = 0; i < day.listDetailDetail.Count; i++)
                    {
                        if (day.listDetailDetail[i].Message == "" || day.listDetailDetail[i].Message == null)
                        {
                            day.listDetailDetail[i].Message = @"&nbsp";
                        }
                        if (day.listDetailDetail[i].Hours == "" || day.listDetailDetail[i].Hours == null)
                        {
                            day.listDetailDetail[i].Hours = @"&nbsp";
                        }
                        file = file + "<tr>" + "<td>" + "&nbsp" + "</td>" + "<td>" + day.listDetailDetail[i].Number + "</td>" + "<td>" + day.listDetailDetail[i].Message + "</td>" + "<td>" + day.listDetailDetail[i].Hours + "</td>" + "</tr>";
                    }
                }
                file = file + "</table></center></body></html>";

                SaveFileDialog sf = new SaveFileDialog();
                sf.FileName = title;
                Nullable<bool> result = sf.ShowDialog();
                if (result == true)
                {
                    string filename = sf.FileName;
                    SaveFile(sf.FileName, file);
                }
            }
            else
            {
                MessageBox.Show("There is no Data in this week!");
            }
        }
        public void SaveFile(string name, string text)
        {
            FileStream fs = File.Open(name, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.Write(text);
            }
            finally
            {
                sw.Close();
                sw.Dispose();
            }
        }

        
 
    }
}
