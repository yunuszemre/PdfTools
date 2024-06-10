using DinkToPdf;
using Newtonsoft.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        int num = 0;
        while (true)
        {
            HtmlToPdfSharp("dockercomposetest", $"id {num}", "test", "test", "Detaysoft", @"C:\Users\P2710\Desktop\logs");
            num++;
        }
    }
    public static string HtmlToPdfSharp(string documentTitle, string documentId, string documentSubject, string documentContent, string companyName, string path)
    {
        try
        {
            var pdfFiles = path;

            if (!Directory.Exists(pdfFiles))
                Directory.CreateDirectory(pdfFiles);

            var pdfText = $"<h1>{documentTitle}</h1><h3>{documentSubject}</h3><p>{documentContent}</p>";

            var converter = new SynchronizedConverter(new PdfTools());

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = DinkToPdf.ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Out = Path.Combine(pdfFiles, documentId + documentTitle + ".pdf")
                },
                Objects = {
                new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = pdfText,
                    WebSettings = { DefaultEncoding = "utf-8" }
                }
            }
            };

            converter.Convert(doc);

            var savePath = Path.Combine(pdfFiles, documentId + documentTitle + ".pdf");
            return savePath;
        }
        catch (Exception ex)
        {
            Console.Out.WriteLine(JsonConvert.SerializeObject(ex));
            Console.WriteLine($"An error occurred while converting HTML to PDF: {ex}");
            return "";
        }
    }
}
