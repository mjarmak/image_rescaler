using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Collections.Generic;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// Image rescaling
    /// </summary>
    public class RescaleImageTest : ITest
    {
        public void Run()
        {
            // TODO
            // Grab an image from a public URL and write a function that rescales the image to a desired format
            // The use of 3rd party plugins is permitted
            // For example: 100x80 (thumbnail) and 1200x1600 (preview)

            //////////////////////////////////////////////////////////////
            // I have used the Drawing library. Rescaled Images are saved in "dotNet\dotNET developer 2019\GeneralKnowledge.Test\bin\Debug"
            // The tool rescales the image such that the width matches the desired one, then checks if the desired height is smaller
            // than the rescaled height, if not then it cannot be croped, so it rescales it again such that the heights match, then crops so that
            // the resulting image's resolution matches the desired resolution.
            //A few examples results are stored in "dotNet\dotNET developer 2019\GeneralKnowledge.Test\bin\Debug\ImageRescaleResults"
            List<string> imageUrl = new List<string>();
            imageUrl.Add("https://i.imgur.com/4qZ7ua8.jpg"); //big landscap
            imageUrl.Add("https://i.imgur.com/TtJIOrK.png"); //small landscape
            imageUrl.Add("https://i.imgur.com/29dGHzf.png"); //big portrait
            imageUrl.Add("https://i.imgur.com/EGAH5K4.jpg"); //small portrait

            int res_width = 10;
            int res_height = 500;

            for(int i = 0; i < imageUrl.Count; i++)
                RescaleImage(imageUrl[i], "dope_"+i, res_width, res_height, true);
            
        }

        public void RescaleImage(string imageUrl, string name, int p_width, int p_height, bool Crop)
        {
            WebClient webClient = new WebClient();
            Stream stream = webClient.OpenRead(imageUrl);
            Image I = Image.FromStream(stream);
            I.Save(name + "_original.bmp");
            int I_width = I.Width;
            int I_height = I.Height;
            int o_width;
            int o_height;
            
            Bitmap O;
            Bitmap Out = new Bitmap(p_width, p_height);
            
            float aspect_ratio;
            aspect_ratio = (float)p_height / (float)I_height;
            o_height = p_height;
            o_width = (int)(Math.Floor(I_width * aspect_ratio));

            if (o_width < p_width) {
                aspect_ratio = (float)p_width / (float)o_width;
                o_width = p_width;
                o_height = (int)(Math.Floor(o_height * aspect_ratio));
            }
            O = new Bitmap(I, o_width, o_height);
            if (Crop)
            {
                int start_x = (o_width - p_width) / 2;
                int start_y = (o_height - p_height) / 2;
                if (start_x + p_width > o_width)
                    start_x = 0;
                if (start_y + p_height > o_height)
                    start_y = 0;
                Out = O.Clone(new Rectangle(start_x, start_y, p_width, p_height), Out.PixelFormat);
            }
            else
                Out = O;
                
            
            Console.WriteLine(name + " saved");
            Out.Save(name+"_" + Out.Width + "x" + Out.Height + ".bmp");
        }
    }
}
