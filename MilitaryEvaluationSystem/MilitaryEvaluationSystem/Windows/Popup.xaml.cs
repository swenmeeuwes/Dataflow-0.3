using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MilitaryEvaluationSystem.Windows {
    /// <summary>
    /// Interaction logic for Popup.xaml
    /// </summary>
    public partial class Popup : Window {
        private MainWindow m;

        public Popup(MainWindow m) {
            InitializeComponent();
            this.m = m;
            GetPorts();
        }

        private void GetPorts() {
            string[] ports = Webservice.GetAvailablePorts();
            foreach(string s in ports)
                comboBox.Items.Add(s);
        }

        private void popupOK_Click(object sender, RoutedEventArgs e) {
            Webservice.Connect(comboBox.SelectedItem.ToString());
            m.t.Enabled = true;
            this.Close();
            m.startSessionButton.IsEnabled = false;
            m.endSessionButton.IsEnabled = true;
        }

        private void popupCancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
