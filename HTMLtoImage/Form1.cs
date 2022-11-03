using Microsoft.Reporting.WinForms;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace HTMLtoImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //web.Navigate(@"J:\C# Project\CM\CM\CM\bin\Debug\model\defaultA4.htm");
            web.Navigate("www.youtube.com");
            //this.reportViewer1.LocalReport.ReportPath = @"c:\users\pc\source\repos\htmltoimage\HTMLtoImage\Report1.rdlc";
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            byte[] img = CaptureWebPageBytesP(web.Document.Body.InnerHtml, null, null);
            String base64String = Convert.ToBase64String(img);
            Form2 f = new Form2();
            f.base64String = base64String;
            f.showredlc();
        }

        private byte[] CaptureWebPageBytesP(string body, int? width, int? height)
        {
            MessageBox.Show("start capture");
            byte[] data;
            // create a hidden web browser, which will navigate to the page
            using (WebBrowser web = new WebBrowser())
            {
                web.ScrollBarsEnabled = false; // we don't want scrollbars on our image
                web.ScriptErrorsSuppressed = true; // don't let any errors shine through
                                                    web.Navigate("about:blank");
                                                   //web.Navigate("www.google.Com");
                                                   // wait until the page is fully loaded
                while (web.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
                    System.Windows.Forms.Application.DoEvents();
                web.Document.Body.InnerHtml = body;

                // set the size of our web browser to be the same size as the page
                if (width == null)
                    width =  web.Document.Body.ScrollRectangle.Width;
                if (height == null)
                    height = web.Document.Body.ScrollRectangle.Height;
                web.Width = width.Value;
                web.Height = height.Value;
                // a bitmap that we will draw to

                MessageBox.Show("x" + web.Location.X);
                using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width.Value, height.Value))
                {
                    // draw the web browser to the bitmap
                    web.DrawToBitmap(bmp, new Rectangle(web.Location.X, web.Location.Y, web.Width, web.Height));
                    // draw the web browser to the bitmap
                    using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                    {
                        EncoderParameter qualityParam = null;
                        EncoderParameters encoderParams = null;
                        try
                        {
                            ImageCodecInfo imageCodec = null;
                            //imageCodec = getEncoderInfo("image/jpeg");
                            imageCodec = GetEncoderInfo("image/jpeg");
                            // Encoder parameter for image quality
                            qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

                            encoderParams = new EncoderParameters(1);
                            encoderParams.Param[0] = qualityParam;
                            bmp.Save(stream, imageCodec, encoderParams);
                            bmp.Save("J:\\A.jpg");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("errur");
                            throw new Exception();

                        }
                        finally
                        {
                            
                            if (encoderParams != null)
                                encoderParams.Dispose();
                            if (qualityParam != null)
                                qualityParam.Dispose();
                        }
                        bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        stream.Position = 0;
                        data = new byte[stream.Length];
                        stream.Read(data, 0, (int)stream.Length);
                    }
                }
            }
            MessageBox.Show("save");
            return data;
        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}
