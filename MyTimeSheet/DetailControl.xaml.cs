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

namespace MyTimeSheet
{
    /// <summary>
    /// Interaction logic for DetailControl.xaml
    /// </summary>
    public partial class DetailControl : UserControl, INotifyPropertyChanged
    {
        public DetailControl()
        {
            InitializeComponent();
            BindingThings();
            this.HoursTextBox.TextChanged += new TextChangedEventHandler(HoursTextBox_TextChanged);
        }

        void HoursTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Hours = this.HoursTextBox.Text;
            PropertyChanged(this, new PropertyChangedEventArgs("Hours"));
        }

        public void BindingThings()
        {
            NumberTextBox.DataContext = this;
            MessageTextBox.DataContext = this;
            HoursTextBox.DataContext = this;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty NumberProperty =
        DependencyProperty.Register("Number", typeof(string), typeof(EveryDayControl));
        public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register("Message", typeof(string), typeof(EveryDayControl));
        
        public static readonly DependencyProperty UIDProperty =
        DependencyProperty.Register("UID", typeof(string), typeof(EveryDayControl));

        public string UID
        {
            get { return (string)GetValue(UIDProperty); }
            set
            {
                SetValue(UIDProperty, value);
            }
        }
        public string Number
        {
            get { return (string)GetValue(NumberProperty); }
            set
            {
                SetValue(NumberProperty, value);
            }
        }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set
            {
                SetValue(MessageProperty, value);
            }
        }

        public string Hours
        {
            get;
            set;

        }

        private void NumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)))
            {
                e.Handled = true;
            }

        }

        private void HoursTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)))
            {
                e.Handled = true;
            }
        }

        
    }
}
