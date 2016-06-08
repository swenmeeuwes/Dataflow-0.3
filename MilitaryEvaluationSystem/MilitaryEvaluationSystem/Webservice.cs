using MilitaryEvaluationSystem.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MilitaryEvaluationSystem {
    public static class Webservice {

        private static SerialPort serialPort;

        public static string[] GetAvailablePorts() {
            return SerialPort.GetPortNames();
        }

        public static void Connect(string portName) {
            serialPort = new SerialPort();
            serialPort.PortName = portName;
            serialPort.BaudRate = 115200;

            if (!serialPort.IsOpen)
                serialPort.Open();

            serialPort.DataReceived += SerialPort_DataReceived;
            Console.WriteLine("Connected!");
        }

        public static void CloseConnection() {
            serialPort.Close();
            serialPort.DataReceived -= SerialPort_DataReceived;
            
        }

        private static void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            SerialPort serialPort = (SerialPort)sender;
            string receivedString = serialPort.ReadLine();
            if (!IsValidJson(receivedString))
                return;
            DataModel data = JsonConvert.DeserializeObject<DataModel>(receivedString);
            ShirtData s = new ShirtData();
            s.FromDataModelToShirtData(data);
            MainWindow.AddNewData(s);
        }

        private static bool IsValidJson(string strInput) {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex) {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else {
                return false;
            }
        }
    }

}
