using ImageMagick;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace ImageToPdfTest {
   class Program {
      static void Main(string[] args) {
         MagickNET.SetLogEvents(LogEvents.All);
         MagickNET.Log += MagickNET_Log;
         string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
         MagickNET.SetGhostscriptDirectory(path);
         string fileName = Path.Combine(path, "480x600.jpg");
         if(!File.Exists(fileName)) {
            throw new FileNotFoundException("File not found", fileName);
         }
         string pdfFileName = Path.Combine(path, "test.pdf");
         if(File.Exists(pdfFileName)) {
            File.Delete(pdfFileName);
         }
         using(var images = new MagickImageCollection()) {
            images.Add(new MagickImage(fileName));
            images.Write(pdfFileName);
            if(!File.Exists(pdfFileName)) {
               throw new FileNotFoundException("Pdf creation failed", pdfFileName);
            }
         }
      }

      private static void MagickNET_Log(object sender, LogEventArgs e) {
         Debug.WriteLine(e.Message);
      }
   }
}
