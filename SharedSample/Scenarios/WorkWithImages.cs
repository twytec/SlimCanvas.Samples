using System;
using System.Collections.Generic;
using System.Text;
using SlimCanvas;

namespace SharedSample.Scenarios
{
    public class WorkWithImages : ISamples
    {
        Canvas canvas;
        SlimCanvas.View.Controls.Image img;
        SlimCanvas.View.Controls.Image clipImg;
        SlimCanvas.View.Controls.Image editImg;

        double width = 1000;

        public async void LoadScenario(Canvas canvas)
        {
            this.canvas = canvas;
            canvas.Clear();
            canvas.Background = new SlimCanvas.View.SolidColorBrush(new SlimCanvas.Color(47, 47, 47, 255));
            canvas.AutoResize = true;

            //Read image file from assets
            using (var logoFile = await canvas.Assets.GetFileFromAssetsAsync("TWyTecLogo600x300.png"))
            {
                //Create bitmap
                var bitmap = await canvas.Graphics.BitmapAsync(logoFile);

                //Create image from bitmap
                img = new SlimCanvas.View.Controls.Image()
                {
                    Source = bitmap,
                    HorizontalAlignment = SlimCanvas.View.Controls.EnumTypes.HorizontalAlignment.Left,
                    VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Top,
                    Margin = new Margin(20, 20, 0, 0)
                };
                canvas.Children.Add(img);

                //Clip image
                clipImg = new SlimCanvas.View.Controls.Image()
                {
                    Source = bitmap,
                    HorizontalAlignment = SlimCanvas.View.Controls.EnumTypes.HorizontalAlignment.Right,
                    VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Top,
                    Clip = new Rect(172, 102, 355, 137),
                    Margin = new Margin(0, 20, 20, 0)
                };
                canvas.Children.Add(clipImg);

                //Get pixel from bitmap and create new bitmap
                var pixel = bitmap.GetPixels();
                var bitmap2 = await canvas.Graphics.BitmapAsync(pixel, bitmap.Width);

                //Crop
                bitmap2.CropBitmap(new Rect(172, 102, 355, 137));

                //Scale
                var newWidth = bitmap2.Width * 0.5d; //Half size
                var newHeight = bitmap2.Height * 0.5d;
                bitmap2.ScaleBitmap(newWidth, newHeight, BitmapInterpolationMode.Linear);

                editImg = new SlimCanvas.View.Controls.Image()
                {
                    Source = bitmap2,
                    HorizontalAlignment = SlimCanvas.View.Controls.EnumTypes.HorizontalAlignment.Center,
                    VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Center
                };
                canvas.Children.Add(editImg);
            }

            ResizeElement(canvas.Width);
            canvas.SizeChanged += Canvas_SizeChanged;
        }

        private void Canvas_SizeChanged(object sender, SlimCanvas.View.Controls.EventTypes.SizeChangedEventArgs e)
        {
            ResizeElement(e.NewWidth);
        }

        void ResizeElement(double w)
        {
            var scal = w / width;
            if (scal <= 1)
            {
                img.Scale = new Vector2(scal, scal);
                clipImg.Scale = new Vector2(scal, scal);
                editImg.Scale = new Vector2(scal, scal);
            }
        }
    }
}
