using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BaoHien.Common
{
    public class FontConfig
    {
        public static Font HeaderFont = headerFont();
        private static Font headerFont()
        {
            BaseFont bfTimes = BaseFont.CreateFont(@"C:\Windows\Fonts\CAMBRIAB.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font f = new Font(bfTimes, 15, Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            return f;
        }

        public static Font BoldFont = boldFont();
        private static Font boldFont()
        {
            BaseFont bfTimes = BaseFont.CreateFont(@"C:\Windows\Fonts\COUR.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font times = new Font(bfTimes, 12, Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            return times;
        }

        public static Font BoldItalicFont = boldItalicFont();
        private static Font boldItalicFont()
        {
            BaseFont bfTimes = BaseFont.CreateFont(@"C:\Windows\Fonts\COUR.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font times = new Font(bfTimes, 12, Font.BOLDITALIC, iTextSharp.text.BaseColor.BLACK);
            return times;
        }

        public static Font ItalicFont = italicFont();
        private static Font italicFont()
        {
            BaseFont bfTimes = BaseFont.CreateFont(@"C:\Windows\Fonts\COUR.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font times = new Font(bfTimes, 12, Font.ITALIC, iTextSharp.text.BaseColor.BLACK);
            return times;
        }

        public static Font SmallItalicFont = smallItalicFont();
        private static Font smallItalicFont()
        {
            BaseFont bfTimes = BaseFont.CreateFont(@"C:\Windows\Fonts\COUR.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font times = new Font(bfTimes, 10, Font.ITALIC, iTextSharp.text.BaseColor.BLACK);
            return times;
        }

        public static Font NormalFont = normalFont();
        private static Font normalFont()
        {
            BaseFont bfTimes = BaseFont.CreateFont(@"C:\Windows\Fonts\COUR.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font times = new Font(bfTimes, 12, Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            return times;
        }
    }
}
