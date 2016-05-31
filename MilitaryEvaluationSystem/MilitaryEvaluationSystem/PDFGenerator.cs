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
    class PDFGenerator {
        Document doc;
        FileStream output;

        public PDFGenerator(string fileName) {
            doc = new Document();
            output = new FileStream("/Users/Jesse/Documents/Visual Studio 2015/Projects/MilitaryEvaluationSystem/" + fileName + ".pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public void createPDF(List<ShirtData> data, Chart lineChart) {
            ConvertGraph(lineChart);
            PdfWriter pdf = PdfWriter.GetInstance(doc, output);
            doc.Open();
            Image i = Image.GetInstance("/Users/Jesse/Documents/Visual Studio 2015/Projects/MilitaryEvaluationSystem/graph.png");
            doc.Add(new Paragraph("Your heart beat data:"));
            i.ScaleToFit(500, 1000);
            doc.Add(i);
            
            doc.Close();          
        }

        public void ConvertGraph(Chart lineChart) {
            RenderTargetBitmap image = new RenderTargetBitmap(
                (int)lineChart.ActualWidth,
                600,
                96d,
                96d,
                PixelFormats.Default);
            image.Render(lineChart);

            using (FileStream stream = new FileStream("/Users/Jesse/Documents/Visual Studio 2015/Projects/MilitaryEvaluationSystem/graph.png", FileMode.Create)) {
                PngBitmapEncoder png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(image));
                png.Save(stream);
            }
            

        }
    }
}
