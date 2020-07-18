using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using Syncfusion.Pdf.Parsing;

namespace Central_Line_Tracing
{
    public partial class Form1 : Form
    {
        private Image image;
        private Skeletonization skeletonization;
        private Bitmap bitmap;
        public Form1()
        {
            InitializeComponent();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
           
            skeletonization = new Skeletonization(bitmap);



            bool[][] t = skeletonization.FromImageToBool();


            t = skeletonization.ZhangSuenThinning(t);



            pictureBox2.Image = skeletonization.FromBoolToImage(t);
        
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(openFileDialog1.FileName);
                //Exporting specify page index as image
                int count = loadedDocument.Pages.Count;

                for (int i = 0; i < count; i++)
                {
                    bitmap = loadedDocument.ExportAsImage(i);
                    //Save the image as JPG format
                    bitmap.Save("Image.jpg", ImageFormat.Jpeg);

                }



                //Close the document
                loadedDocument.Close(true);

            }

            pictureBox1.Image = bitmap;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
              
                System.Windows.Forms.Application.Exit();
            }
        }

        

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                    string fileName = saveFileDialog1.FileName;

                pictureBox2.Image.Save(fileName, ImageFormat.Png);
                    

            }
        }
    }
}
