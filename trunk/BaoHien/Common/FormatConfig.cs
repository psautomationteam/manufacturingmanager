using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BaoHien.Common
{
    public class FormatConfig
    {
        public static Paragraph ParaHeader(string header)
        {
            Paragraph pHeader = new Paragraph(header, FontConfig.HeaderFont);
            pHeader.Alignment = Element.ALIGN_CENTER;
            return pHeader;
        }

        public static Paragraph ParaRightBeforeHeader(string content)
        {
            Paragraph msp = new Paragraph(content, FontConfig.SmallItalicFont);
            msp.IndentationLeft = 350;
            msp.Alignment = Element.ALIGN_LEFT;
            return msp;
        }

        public static Paragraph ParaRightBeforeHeaderRight(string content)
        {
            Paragraph msp = new Paragraph(content, FontConfig.BoldFont);
            return msp;
        }

        public static Paragraph ParaRightBelowHeader(string content)
        {
            Paragraph msp = new Paragraph(content, FontConfig.ItalicFont);
            msp.Alignment = Element.ALIGN_CENTER;
            return msp;
        }

        public static Paragraph ParaCommonInfo(string field, string value)
        {
            Paragraph pr = new Paragraph();
            Phrase phr = new Phrase();
            Chunk chk1 = new Chunk(field, FontConfig.BoldFont);
            Chunk chk2 = new Chunk(value, FontConfig.NormalFont);
            phr.Add(chk1);
            phr.Add(chk2);
            pr.Add(phr);
            return pr;
        }

        public static Paragraph ParaCommonInfoAllBold(string field, string value)
        {
            Paragraph pr = new Paragraph();
            Phrase phr = new Phrase();
            Chunk chk1 = new Chunk(field, FontConfig.BoldFont);
            Chunk chk2 = new Chunk(value, FontConfig.BoldFont);
            phr.Add(chk1);
            phr.Add(chk2);
            pr.Add(phr);
            return pr;
        }

        public static PdfPTable Table(int column, float[] ratio)
        {
            PdfPTable table = new PdfPTable(column);
            table.TotalWidth = 550f;
            table.LockedWidth = true;
            table.SetWidths(ratio);
            table.HorizontalAlignment = 1;
            table.SpacingBefore = 15f;
            table.SpacingAfter = 15f;
            return table;
        }

        public static PdfPCell TableCellHeader(string content)
        {
            return TableCellHeaderCommon(content, PdfPCell.BOX);
        }

        public static PdfPCell TableCellHeaderCommon(string content, int bolder)
        {
            PdfPCell cell = new PdfPCell(new Phrase(new Chunk(content, FontConfig.BoldFont)));
            cell.Border = bolder;
            cell.HorizontalAlignment = 1;
            return cell;
        }

        public static PdfPCell TableCellBody(string content, int format)
        {
            PdfPCell cell = new PdfPCell(new Phrase(new Chunk(content, FontConfig.NormalFont)));
            cell.HorizontalAlignment = format;
            return cell;
        }

        public static PdfPCell TableCellBodyCustom(string content, int format, Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(new Chunk(content, font)));
            cell.HorizontalAlignment = format;
            cell.Border = PdfPCell.NO_BORDER;
            return cell;
        }

        public static PdfPCell TableCellBoldBody(string content, int format, int colspan)
        {
            PdfPCell cell = new PdfPCell(new Phrase(new Chunk(content, FontConfig.BoldFont)));
            cell.Colspan = colspan;
            cell.HorizontalAlignment = format;
            return cell;
        }
    }

    class PdfWriterEvents : IPdfPageEvent
    {
        string watermarkText = string.Empty;
        Image watermarkImage;

        public PdfWriterEvents(string watermark)
        {
            watermarkText = watermark;
        }

        public PdfWriterEvents(Image watermark)
        {
            watermarkImage = watermark;
        }

        public void OnOpenDocument(PdfWriter writer, Document document) { }
        public void OnCloseDocument(PdfWriter writer, Document document) { }
        public void OnStartPage(PdfWriter writer, Document document)
        {
            float fontSize = 80;
            float xPosition = 300;
            float yPosition = 400;
            float angle = 45;
            try
            {
                PdfContentByte under = writer.DirectContentUnder;
                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\CAMBRIAB.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                under.BeginText();
                under.SetColorFill(BaseColor.LIGHT_GRAY);
                under.SetFontAndSize(baseFont, fontSize);
                under.ShowTextAligned(PdfContentByte.ALIGN_CENTER, watermarkText, xPosition, yPosition, angle);
                under.EndText();
                if (watermarkImage != null)
                    under.AddImage(watermarkImage);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
        public void OnEndPage(PdfWriter writer, Document document) { }
        public void OnParagraph(PdfWriter writer, Document document, float paragraphPosition) { }
        public void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition) { }
        public void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title) { }
        public void OnChapterEnd(PdfWriter writer, Document document, float paragraphPosition) { }
        public void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title) { }
        public void OnSectionEnd(PdfWriter writer, Document document, float paragraphPosition) { }
        public void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, String text) { }

    }

    public class WaterMarkEvent : PdfPageEventHelper
    {
        private Image waterMark;
        public WaterMarkEvent(Image img)
        {
            waterMark = img;
        }
        public void OnStartPage(PdfWriter writer, Document doc)
        {
            PdfContentByte content = writer.DirectContentUnder;
            content.AddImage(waterMark);
        }
    }
}
