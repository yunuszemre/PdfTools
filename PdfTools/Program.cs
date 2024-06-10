using HtmlRendererCore.PdfSharp;
using PdfSharpCore;

internal class Program
{
    private static void Main(string[] args)
    {
        int num = 0;
        while (true)
        {
            HtmlToPdf();
            num++;

            if (num == 1)
                break;
        }
        while (true)
        {

        };
    }
    private static void HtmlToPdf()
    {

        try
        {
            var htmlContent = "<html><body><h1>Hello, World!</h1></body></html>";
            var filePath = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa.pdf";
            //if (!Directory.Exists(filePath))
            //    Directory.CreateDirectory(filePath);
            var pdfDocument = PdfGenerator.GeneratePdf(htmlContent, PageSize.A4);

            pdfDocument.Save(filePath);
        }
        catch (Exception ex)
        {
            var a = new Exception(ex.Message);
            throw a;
        }


    }
    
}
