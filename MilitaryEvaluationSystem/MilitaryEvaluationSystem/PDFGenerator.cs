using iTextSharp.text;
using iTextSharp.text.pdf;
using MilitaryEvaluationSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MilitaryEvaluationSystem {
    public class PDFGenerator {
        private Document doc;
        private FileStream output;
        private List<ShirtData> data;

        private string fileName;
        private string trainee;

        public PDFGenerator(string trainee, List<ShirtData> data) {
            this.fileName = "Activity_Log";
            this.trainee = trainee;
            this.data = data;
            doc = new Document();

            GeneratePDF();
        }
        
        private void GeneratePDF() {

            StringBuilder builder = new StringBuilder();
            builder.Append("Activity Logs/");
            builder.Append(fileName);
            builder.Append("_");
            builder.Append(trainee);
            builder.Append("_");
            builder.Append(DateTime.Now.ToString("dd-MM-yyyy"));
            builder.Append(".pdf");

            output = new FileStream(builder.ToString(), FileMode.Create, FileAccess.Write);

            PdfWriter pdf = PdfWriter.GetInstance(doc, output);
            doc.Open();

            builder.Clear();

            builder.Append("Passed milliseconds");
            builder.Append("                ");
            builder.Append("Heart beat");
            builder.Append("                ");
            builder.Append("Body Temp.");
            builder.Append("                ");
            builder.Append("Stress level");

            doc.Add(new Paragraph(builder.ToString()));

            for (int i = 0; i < data.Count; i++) {
                builder.Clear();
                builder.Append(data[i].milliseconds);
                builder.Append("                                        ");
                builder.Append(data[i].beatsPerMinute);
                builder.Append("                                    ");
                builder.Append(data[i].temperature);
                builder.Append("                                    ");
                builder.Append(data[i].stressLevel);

                doc.Add(new Paragraph(builder.ToString()));
            }

            doc.Close();

        }
    }
}
