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

        public static PdfPTable Table(int column, float[] ratio)
        {
            PdfPTable table = new PdfPTable(column);
            table.TotalWidth = 515f;
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
            PdfPCell cell = new PdfPCell(new Phrase(new Chunk(content, FontConfig.BoldItalicFont)));
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

        public static PdfPCell TableCellBoldBody(string content, int format, int colspan)
        {
            PdfPCell cell = new PdfPCell(new Phrase(new Chunk(content, FontConfig.BoldFont)));
            cell.Colspan = colspan;
            cell.HorizontalAlignment = format;
            return cell;
        }
    }
}
