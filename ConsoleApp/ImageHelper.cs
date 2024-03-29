﻿using System;
using System.Drawing;
using System.IO;

namespace Epam.Exercises.CleanCode.AdvancedAscii.ConsoleApp
{
    public class ImageHelper
    {
        private const int LASTBYTE = 0xFF;
        private const int BYTE = 8;
        private const int TWOBYTES = 16;
        private readonly Bitmap image;

        public ImageHelper(string fileName)
        {
            this.image = this.LoadImageFromFile(fileName);
        }

        public int GetWidth()
        {
            return this.image.Width;
        }

        public int GetHeight()
        {
            return this.image.Height;
        }

        public int GetGreen(Point point)
        {
            int rgbValue = this.GetRgbValue(point);
            return (rgbValue >> BYTE) & LASTBYTE;
        }

        public int GetBlue(Point point)
        {
            int rgbValue = this.GetRgbValue(point);
            return rgbValue & LASTBYTE;
        }

        public int GetRed(Point point)
        {
            int rgbValue = this.GetRgbValue(point);
            return (rgbValue >> TWOBYTES) & LASTBYTE;
        }

        public int GetRGB(Point point)
        {
            return GetRed(point) + GetBlue(point) + GetGreen(point);
        }

        private int GetRgbValue(Point point)
        {
            if (point.X < 0 || point.X >= this.image.Width || point.Y < 0 || point.Y >= this.image.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(point));
            }

            return this.image.GetPixel(point.X, point.Y).ToArgb();
        }

        private Bitmap LoadImageFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException($"File not found: {fileName}", nameof(fileName));
            }

            using (var img = Image.FromFile(fileName))
            {
                return new Bitmap(img);
            }
        }
    }
}