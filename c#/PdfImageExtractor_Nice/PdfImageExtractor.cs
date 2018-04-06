using System;
using System.Drawing;
using System.IO;
using Direct.Shared;

namespace PdfImageExtractor.Library
{
    [DirectSealed]
    [DirectDom("PdfImageExtractor Library")]
    [ParameterType(false)]
    public static class PdfImageExtractor
    {

        [DirectDom("Extracts the image from the Pdf path provided")]
        [DirectDomMethod("Extract Image from Pdf {file location}")]
        [MethodDescriptionAttribute("Extracts the image from the Pdf path provided and store it in the same directory")]
        public static string ExtractImage(string pdfFilePath)
        {
            var images = PdfUtils.PdfImageExtractor.ExtractImages(pdfFilePath);
            Image[] dataMatrixImages = new Image[2];
            String dataMatrixImageName = "";
            int i = 0;
            var directory = Path.GetDirectoryName(pdfFilePath);
            foreach (var name in images.Keys)
            {
                dataMatrixImages[i] = images[name];
                i++;
                dataMatrixImageName = name;
                if (images.Count != 2)
                {
                    images[name].Save(Path.Combine(directory, name));
                }
            }
            int width = dataMatrixImages[1].Width;
            int height = dataMatrixImages[1].Height;
            Bitmap bitmap = new Bitmap(dataMatrixImages[0]);
            height += bitmap.Height;
            Bitmap finalImage = new Bitmap(width, height);
            //get a graphics object from the image so we can draw on it
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalImage))
            {
                //set background color
                g.Clear(System.Drawing.Color.Black);
                //go through each image and draw it on the final image
                int offset = 0;
                foreach (System.Drawing.Bitmap image in dataMatrixImages)
                {
                    g.DrawImage(image,
                      new System.Drawing.Rectangle(0, offset, image.Width, image.Height));
                    offset += image.Height;
                }
            }
            finalImage.Save(Path.Combine(directory, dataMatrixImageName));
            return Path.Combine(directory, dataMatrixImageName);
        }
    }
}

