using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;

namespace komfort
{
    public static class CaptchaGenerator
    {
        private static readonly Random rnd = new Random();

        public static (Bitmap Image, string Code) Generate(int width = 140, int height = 48, int length = 4)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var code = new string(Enumerable.Range(0, length).Select(_ => chars[rnd.Next(chars.Length)]).ToArray());
            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                g.Clear(Color.White);

                for (int i = 0; i < 6; i++)
                {
                    using var pen = new Pen(Color.FromArgb(rnd.Next(120, 220), rnd.Next(120, 220), rnd.Next(120, 220)), 1f);
                    var p1 = new Point(rnd.Next(width), rnd.Next(height));
                    var p2 = new Point(rnd.Next(width), rnd.Next(height));
                    g.DrawLine(pen, p1, p2);
                }

                var fonts = new[] { "Candara", "Candara", "Candara" };
                for (int i = 0; i < code.Length; i++)
                {
                    using var font = new Font(fonts[rnd.Next(fonts.Length)], 20 + rnd.Next(-2, 4), FontStyle.Bold);
                    using var brush = new SolidBrush(Color.FromArgb(rnd.Next(30, 140), rnd.Next(30, 140), rnd.Next(30, 140)));
                    var x = 8 + i * (width - 16) / length + rnd.Next(-2, 3);
                    var y = rnd.Next(2, 10);
                    var angle = rnd.Next(-30, 30);

                    g.TranslateTransform(x, y);
                    g.RotateTransform(angle);
                    g.DrawString(code[i].ToString(), font, brush, 0, 0);
                    g.RotateTransform(-angle);
                    g.TranslateTransform(-x, -y);
                }

                for (int i = 0; i < 80; i++)
                {
                    bmp.SetPixel(rnd.Next(width), rnd.Next(height), Color.FromArgb(rnd.Next(150, 255), rnd.Next(150, 255), rnd.Next(150, 255)));
                }
            }
            return (bmp, code);
        }
    }
}
