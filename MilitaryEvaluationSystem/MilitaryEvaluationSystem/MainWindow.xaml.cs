using MilitaryEvaluationSystem.Models;
using MilitaryEvaluationSystem.Windows;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;

namespace MilitaryEvaluationSystem {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private static List<ShirtData> data;

        public Timer t;


        public MainWindow() {
            InitializeComponent();
            Init();
            t = new Timer();
            t.Interval = 500;
            t.Elapsed += new ElapsedEventHandler(UpdateGraphs);
        }

        public void Init() {
            endSessionButton.IsEnabled = false;
            exportSessionButton.IsEnabled = false;
            data = new List<ShirtData>();
        }

        private void UpdateGraphs(object sender, ElapsedEventArgs e) {
            SetGraphs(data);
            SetTextBoxes(data);
        }

        private void startSessionButton_Click(object sender, RoutedEventArgs e) {
            Popup p = new Popup(this);
            p.Owner = this;
            p.Show();

        }

        private void endSessionButton_Click(object sender, RoutedEventArgs e) {
            startSessionButton.IsEnabled = true;
            endSessionButton.IsEnabled = false;
            exportSessionButton.IsEnabled = true;
            Webservice.CloseConnection();
        }

        private void exportSessionButton_Click(object sender, RoutedEventArgs e) {
            PDFGenerator p = new PDFGenerator("test1");
            p.createPDF(data, heartChart);
            MessageBoxResult m = MessageBox.Show("Your session has been succesfully exported to a pdf.", 
                                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void AddNewData(ShirtData s) {
            if (data.Count > 50) {
                data.RemoveRange(0, 10);
            }
            data.Add(s);
        }


        private void SetGraphs(List<ShirtData> data) {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate () {
                List<KeyValuePair<string, int>> heartbeatData = new List<KeyValuePair<string, int>>();
                List<KeyValuePair<string, float>> bodyTempData = new List<KeyValuePair<string, float>>();

                for (int i = 0; i < data.Count; i++) {
                    heartbeatData.Add(new KeyValuePair<string, int>(i.ToString(), data[i].beatsPerMinute));
                    bodyTempData.Add(new KeyValuePair<string, float>(i.ToString(), data[i].temperature));
                }

            ((LineSeries)heartChart.Series[0]).ItemsSource = heartbeatData;
                ((LineSeries)tempChart.Series[0]).ItemsSource = bodyTempData;

                
            }));
            
        }

        private void SetTextBoxes(List<ShirtData> data) {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate () {

                heartBeatText.Content = data[data.Count - 1].beatsPerMinute.ToString();
                bodyTempText.Content = data[data.Count - 1].temperature.ToString();
                float stressLevel = HelperMethods.CalculateStressLevel(data[data.Count - 1].beatsPerMinute, data[data.Count - 1].temperature);
                if (stressLevel > 6.0 && stressLevel <= 7.5) {
                    stressLevelText.Foreground = Brushes.Orange;
                }
                else if (stressLevel > 7.5) {
                    stressLevelText.Foreground = Brushes.Red;
                }
                else {
                    stressLevelText.Foreground = System.Windows.Media.Brushes.Black;
                }
                stressLevelText.Content = stressLevel.ToString();
            }));
        }
    }
}
