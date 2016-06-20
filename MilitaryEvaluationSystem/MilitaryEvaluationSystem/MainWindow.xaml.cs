using MilitaryEvaluationSystem.Models;
using MilitaryEvaluationSystem.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private static List<ShirtData> allData;

        public Timer t;

        private static HelperMethods h;

        public static Stopwatch stopwatch;

        public string traineeName;

        public MainWindow() {
            InitializeComponent();
            Init();
            t = new Timer();
            t.Interval = 500;
            t.Elapsed += new ElapsedEventHandler(UpdateGraphs);
            
        }

        public void Init() {
            endSessionButton.IsEnabled = false;
            data = new List<ShirtData>();
            allData = new List<ShirtData>();
            h = new HelperMethods(data);
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
            PDFGenerator generator = new PDFGenerator(traineeName, allData);
            MessageBoxResult m = MessageBox.Show("Your session has been succesfully exported to a pdf.",
                                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Webservice.CloseConnection();
        }

        private void exportSessionButton_Click(object sender, RoutedEventArgs e) {
            
            //p.createPDF(data, heartChart);
            //MessageBoxResult m = MessageBox.Show("Your session has been succesfully exported to a pdf.", 
            //                                        "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void AddNewData(DataModel d) {
            ShirtData s = new ShirtData();
            float stressLevel = h.CalculateStressLevel(d.beatsPerMinute, d.temperature);
            s.FromDataModelToShirtData(d, stopwatch.ElapsedMilliseconds, stressLevel);
            if (data.Count > 50) {
                data.RemoveAt(0);
            }
            data.Add(s);
            allData.Add(s);
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
                float stressLevel = h.CalculateStressLevel(data[data.Count - 1].beatsPerMinute, data[data.Count - 1].temperature);
                if (stressLevel > 6.0 && stressLevel <= 7.5) {
                    stressLevelText.Foreground = Brushes.Orange;
                }
                else if (stressLevel > 7.5) {
                    stressLevelText.Foreground = Brushes.Red;
                }
                else {
                    stressLevelText.Foreground = Brushes.Black;
                }
                stressLevelText.Content = stressLevel.ToString();
            }));
        }
    }
}
