using System;

namespace GrayscaleTask
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);
            ImageHelper.ReadImage("eiffel_tower.jpg",
                out var redColor,
                out var greenColor,
                out var blueColor);

            GrayscaleImage(redColor, greenColor, blueColor);
            ImageHelper.WriteImage("eiffel_tower_grayscale.jpg", redColor, greenColor, blueColor);
        }

        public static void GrayscaleImage(byte[,] red, byte[,] green, byte[,] blue)
        {
            // TODO: Напишите реализацию данного метода
            // Ожидается, что метод будет преобразовывать каждый пиксель исходного изображения
            // в какой-то оттенок серого
            // Формула преобразования пикселя в серый - (R + G + B) / 3
            
            for (var x = 0; x < red.GetLength(0); x++)
            {
                for (var y = 0; y < red.GetLength(1); y++)
                {
                    
                    red[x,y] = (byte)((red[x,y] + green[x, y] + blue[x, y])/ 3);
                    green[x, y] = red[x, y];
                    blue[x, y] = red[x, y];
                }
            }
        }
    }
}