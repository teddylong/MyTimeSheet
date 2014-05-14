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
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Net;

namespace MyTimeSheet
{
    /// <summary>
    /// Interaction logic for EveryDayControl.xaml
    /// </summary>
    public partial class EveryDayControl : UserControl, INotifyPropertyChanged
    {
        public EveryDayControl()
        {
            InitializeComponent();

            this.DetailNum = "0";
            weeks.DataContext = this;
            displayname.DataContext = this;
            TotalHoursLabel.DataContext = this;
            
            CustomPanel();
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        List<DetailControl> listDetail = new List<DetailControl>();

        public List<DetailControl> listDetailDetail
        {
            get { return listDetail; }
        }

        List<int> uids = new List<int>();
        public void GetDetailMessage()
        {
            TimeSheetService.TimeSheetServiceClient client = new TimeSheetService.TimeSheetServiceClient();
            try
            {
                string[] result = client.GetDetailData(Convert.ToInt16(this.Weeks), this.DisPlayName);
                int count = result.Length / 6;
                this.DetailNum = count.ToString();

                for (int i = 0; i < count; i++)
                {
                    DetailControl detail = new DetailControl();
                    detail.Number = result[i * 6 + 1];
                    detail.Message = result[i * 6 + 2];
                    detail.Hours = result[i * 6 + 3];
                    detail.UID = result[i * 6 + 5];
                    detail.HoursTextBox.TextChanged += new TextChangedEventHandler(HoursTextBox_TextChanged);
                    Canvas.SetTop(detail,i * 31);
                    MyCanvas.Children.Add(detail);
                    listDetail.Add(detail);
                    uids.Add(Convert.ToInt16(detail.UID));
                    
                }
                
            }
            catch (Exception e)
            { MessageBox.Show(this.DisPlayName + " has no detail message in this week!"); }
            finally
            {
                client.Close();
            }
        }

        void HoursTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            int number = 0;
            for (int i = 0; i < listDetail.Count; i++)
            {
                number = number + Convert.ToInt16(listDetail[i].Hours);
            }
            tt = number;
            PropertyChanged(this, new PropertyChangedEventArgs("tt"));
        }
        //static bool isSecect = false;
        //static Point startPoint = new Point();
        //static FrameworkElement currentElement = null;

        public static readonly DependencyProperty TotalHoursProperty =
        DependencyProperty.Register("TotalHours", typeof(string), typeof(EveryDayControl));

        public static readonly DependencyProperty WeeksProperty =
        DependencyProperty.Register("Weeks", typeof(string), typeof(EveryDayControl));

        public static readonly DependencyProperty DisPlayNameProperty =
        DependencyProperty.Register("DisPlayName", typeof(string), typeof(EveryDayControl));

        public static readonly DependencyProperty PicProperty =
        DependencyProperty.Register("Pic", typeof(string), typeof(EveryDayControl));

        public static readonly DependencyProperty PsProperty =
        DependencyProperty.Register("Ps", typeof(string), typeof(EveryDayControl));

        public static readonly DependencyProperty TimeProperty =
        DependencyProperty.Register("Time", typeof(string), typeof(EveryDayControl));

        public static readonly DependencyProperty DetailNumProperty =
        DependencyProperty.Register("DetailNum", typeof(string), typeof(EveryDayControl));

        public string Pic
        {
            get
            {
                return (string)GetValue(PicProperty);
            }
            set
            {
                SetValue(PicProperty, value);
            }
        }
        
        public string TotalHours
        {
            get
            {
                return (string)GetValue(TotalHoursProperty); 
            }
            set
            {
                SetValue(TotalHoursProperty, value);
            }
        }
        
        public string DetailNum
        {
            get { return (string)GetValue(DetailNumProperty); }
            set
            {
                SetValue(DetailNumProperty, value);
            }
        }
        
        public string Weeks
        {
            get { return (string)GetValue(WeeksProperty); }
            set
            {
                SetValue(WeeksProperty, value);
            }
        }

        public string DisPlayName
        {
            get { return (string)GetValue(DisPlayNameProperty); }
            set
            {
                SetValue(DisPlayNameProperty, value);      
            }
        }

        //private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (isSecect)
        //    {
        //        currentElement.ReleaseMouseCapture();
        //    }
        //    isSecect = false;

        //}

        //private void UserControl_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (isSecect)
        //    {
        //        Point pt = e.GetPosition(null);


        //        Matrix mx = (currentElement.RenderTransform as MatrixTransform).Matrix;
        //        mx.OffsetX += pt.X - startPoint.X;
        //        mx.OffsetY += pt.Y - startPoint.Y;
        //        currentElement.RenderTransform = new MatrixTransform() { Matrix = mx };
        //        startPoint = pt;

        //    }

        //}

        //private void MyBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    currentElement = (sender as FrameworkElement);
        //    currentElement.CaptureMouse();
        //    isSecect = true;
        //    startPoint = e.GetPosition(null);

        //    Information.ZIndex = Information.ZIndex + 100;
        //    Panel.SetZIndex(this, Information.ZIndex);      
        //}

        private void CustomPanel()
        {
            Color color = GetColor();
            MyBorder.Background = new SolidColorBrush(color);

            
        }

        public Color GetColor()
        {
            List<Color> all = new List<Color>();
            all.Add(Color.FromArgb(255, 226, 96, 45));
            all.Add(Color.FromArgb(255, 30, 148, 192));
            all.Add(Color.FromArgb(255, 183, 89, 107));
            all.Add(Color.FromArgb(255, 255, 156, 0));
            all.Add(Color.FromArgb(255, 253, 206, 78));
            all.Add(Color.FromArgb(255, 173, 255, 47));
            all.Add(Color.FromArgb(255, 147, 198, 185));

            Random ra = new Random();
            return all[ra.Next(0, 7)];
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            DetailControl detail = new DetailControl();
            detail.Number = Convert.ToString(listDetail.Count + 1);
            detail.NumberTextBox.IsReadOnly = true;
            detail.HoursTextBox.TextChanged += new TextChangedEventHandler(HoursTextBox_TextChanged);
            listDetail.Add(detail);

            this.Height = listDetail.Count * 31 + 51;

            Canvas.SetTop(detail, (listDetail.Count - 1) * 31);
            MyCanvas.Children.Add(detail);
            
        }

        

        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            int count = listDetail.Count;

            if (count > 0)
            {
                MyCanvas.Children.Remove(listDetail[count - 1]);
                listDetail.Remove(listDetail[count - 1]);
                this.Height = listDetail.Count * 31 + 51;
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = listDetail.Count;
            int result = -1;
            TimeSheetService.TimeSheetServiceClient client = new TimeSheetService.TimeSheetServiceClient();
            try
            {
                for (int i = 0; i < count; i++)
                {
                    int pid = Convert.ToInt16(listDetail[i].Number);
                    string text = listDetail[i].Message;
                    double hour = Convert.ToDouble(listDetail[i].Hours);
                    result = client.SubmitData(Convert.ToInt16(this.Weeks), this.DisPlayName, null, "aaa", DateTime.Now.ToString(), pid, text, hour);
                    MessageBox.Show(result.ToString());
                }
                if (result >= 0)
                {
                    MessageBox.Show("Sumbit Successfully!");
                }
                else
                {
                    MessageBox.Show("Sumbit Failed!");
                }
                
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
            finally
            {
                client.Close();
            }
        }

        public void UpdateMySelfTimeSheet(object sender, RoutedEventArgs e)
        {
            TimeSheetService.TimeSheetServiceClient client = new TimeSheetService.TimeSheetServiceClient();
            string name = this.DisPlayName;
            int week = Convert.ToInt16(this.Weeks);
            int result = -1;

            for (int i = 0; i < uids.Count; i++)
            {
                for (int j = 0; j < listDetail.Count; j++)
                {
                    if (uids[i] != Convert.ToInt16(listDetail[j].UID))
                    {
                        client.DeleteDetailDate(week, name, uids[i]);
                    }
                    
                }
            }

            for (int i = 0; i < listDetail.Count; i++)
            {
                result = client.UpdateDetailData(week, name, Convert.ToInt16(listDetail[i].Number), listDetail[i].Message, Convert.ToDouble(listDetail[i].Hours), Convert.ToInt16(listDetail[i].UID));
            }
            if (result >= 0)
            {
                MessageBox.Show("Sumbit Successfully!");
            }
            else
            {
                MessageBox.Show("Sumbit Failed!");
            }
        }

        public int tt
        {
            get;
            set;
        }

        public string DisplayAlias(string username)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                string xpath = "//username/name[@id='" + username + "']";
                string result = Information.UserNameFile.SelectSingleNode(xpath).InnerText;
                return result;
                
            }
            catch (Exception ex)
            {
                return "Anonymous";
            }
        }

        public string AliasToUserName(string alias)
        {
            try
            {
                foreach (XmlNode node in Information.UserNameFile.SelectNodes("//name"))
                {
                    if (alias == node.InnerText)
                    {
                        string UserName = node.Attributes[@"id"].Value;
                        return UserName;
                    }
                }
                return "Anonymous";
            }
            catch (Exception ex)
            {
                return "Anonymous";
            }
        }

        public string GetXMLConfig(string file)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string result = wc.DownloadString(file);
            return result;
        }

    }
}
