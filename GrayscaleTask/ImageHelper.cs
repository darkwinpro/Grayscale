using System;
using System.Drawing;
using System.IO;

/// <summary>
/// Этот класс создан для демонстрации работы с изображениями
/// для студентов GameDev Academy, которые еще не знают ООП.
/// </summary>
public static class ImageHelper
{
    /// <summary>
    /// Преобразует три массива R, G, B в цвета пикселей и сохраняет
    /// получившееся изображение по указанному пути.
    /// </summary>
    public static void WriteImage(string imageName, byte[,] redColor, byte[,] greenColor, byte[,] blueColor)
    {
        var width = redColor.GetLength(0);
        var height = redColor.GetLength(1);
        
        var image = new Bitmap(width, height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var color = Color.FromArgb(redColor[x, y], greenColor[x, y], blueColor[x, y]);
                image.SetPixel(x, y, color);
            }
        }

        image.Save(imageName);
    }

    public static void ReadImage(string imageName, out byte[,] redColor, out byte[,] greenColor, out byte[,] blueColor)
    {
        // Проверяем, что указаный файл существует.
        CheckThatFileExists(imageName);

        // Загружаем указаное изображение.
        using (var image = Image.FromFile(imageName))
        {
            CheckFileFormat(image);

            // Преобразуем цвет пискелей в три двумерных массива R, G, B
            ReadColors(image as Bitmap, out redColor, out greenColor, out blueColor);
        }
    }

    /// <summary>
    /// Преобразуем цвет пискелей в три двумерных массива R, G, B.
    /// Класс Color пока осознанно не используем, чтобы не вводить лишние абстракции
    /// пока полностью не разобрались с ООП.
    /// </summary>
    private static void ReadColors(Bitmap image, out byte[,] redColor, out byte[,] greenColor, out byte[,] blueColor)
    {
        redColor = new byte[image.Width, image.Height];
        greenColor = new byte[image.Width, image.Height];
        blueColor = new byte[image.Width, image.Height];

        for (int x = 0; x < image.Width; x++)
        {
            for (int y = 0; y < image.Height; y++)
            {
                var color = image.GetPixel(x, y);
                redColor[x, y] = color.R;
                greenColor[x, y] = color.G;
                blueColor[x, y] = color.B;
            }
        }
    }

    private static void CheckFileFormat(Image image)
    {
        if (!IsBitmap(image))
        {
            Console.WriteLine("Пожалуйста проверьте, что файл, который вы указали - является BMP.");
            throw new NotSupportedException($"Не поддерживаемый формат файла {image.GetType().Name}");
        }
    }

    private static bool IsBitmap(Image image)
    {
        return image is Bitmap;
    }

    private static void CheckThatFileExists(string imageName)
    {
        if (!File.Exists(imageName))
        {
            Console.WriteLine($"Файл по заданному пути - '{imageName}' не найден.");
            Console.WriteLine($"Попробуйте ввести полный путь к файлу и проверьте, что файл там " +
                              $"точно есть.");

            throw new FileNotFoundException(imageName);
        }
    }

    private static string FixFileName(string fileName)
    {
        var isJustFileName = !fileName.Contains(Path.DirectorySeparatorChar.ToString());
        return isJustFileName ? Path.DirectorySeparatorChar + fileName : fileName;
    }
}