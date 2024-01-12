using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanManagementSystem.controllerandmodel
{
    internal class function_display_collateralimage
    {
        //Displaly image function
        public void DisplayImage(byte[] imageBytes, Form1 form)
        {
            // Convert byte array to MemoryStream
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                Image originalImage = Image.FromStream(ms);

                // Adjust zoom factor as needed
                double zoomFactor = 2.0;

                // Calculate the new size of the image
                int newWidth = (int)(originalImage.Width * zoomFactor);
                int newHeight = (int)(originalImage.Height * zoomFactor);

                // Create a new Bitmap with the new size
                Bitmap zoomedBitmap = new Bitmap(newWidth, newHeight);

                using (Graphics g = Graphics.FromImage(zoomedBitmap))
                {
                    // Set interpolation mode for better image quality
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    // Draw the zoomed image
                    g.DrawImage(originalImage, new Rectangle(0, 0, newWidth, newHeight));
                }

                // Set the zoomed image to the PictureBox
                form.picturebox_dashboard_loanlist_collateralupload.Image = zoomedBitmap;

                // Center the image in the PictureBox
                form.picturebox_dashboard_loanlist_collateralupload.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }
    }
}
