using System;
using System.Linq;
using System.Text;
using DAL;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using DAL.Helper;

namespace BaoHien.Common
{
    public class Global
    {
        static SystemUser currentUser = null;
        static string iPAddrress;

        public static string convertToCurrency(string valueInput)

        {
            decimal total = 0;
            decimal.TryParse(valueInput, out total);
            CultureInfo vietnam = new CultureInfo(1066);
            CultureInfo usa = new CultureInfo("en-US");

            NumberFormatInfo nfi = usa.NumberFormat;
            nfi = (NumberFormatInfo)nfi.Clone();
            NumberFormatInfo vnfi = vietnam.NumberFormat;
            nfi.CurrencySymbol = vnfi.CurrencySymbol;
            nfi.CurrencyNegativePattern = vnfi.CurrencyNegativePattern;
            nfi.CurrencyPositivePattern = vnfi.CurrencyPositivePattern;
            nfi.CurrencyDecimalDigits = 0;
            nfi.CurrencyGroupSeparator = ".";
            string tmp = total.ToString("c", nfi);
            return tmp.Remove(tmp.ToString().Length - 1);
        }

        public static SystemUser CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }

        public static void checkDirSaveFile()
        {
            if (!Directory.Exists(BHConstant.SAVE_IN_DIRECTORY))
            {
                Directory.CreateDirectory(BHConstant.SAVE_IN_DIRECTORY);
            }
        }

        public static string formatCurrencyText(string content, string _preFix, char _thousandsSeparator, char _decimalsSeparator)
        {
            int _decimalPlaces = 0;
            int counter = 1;
            int counter2 = 0;
            char[] charArray = content.ToCharArray();
            StringBuilder str = new StringBuilder();

            for (int i = charArray.Length - 1; i >= 0; i--)
            {
                str.Insert(0, charArray.GetValue(i));
                if (_decimalPlaces == 0 && counter == 3)
                {
                    counter2 = counter;
                }

                if (counter == _decimalPlaces && i > 0)
                {
                    if (_decimalsSeparator != Char.MinValue)
                        str.Insert(0, _decimalsSeparator);
                    counter2 = counter + 3;
                }
                else if (counter == counter2 && i > 0)
                {
                    if (_thousandsSeparator != Char.MinValue)
                        str.Insert(0, _thousandsSeparator);
                    counter2 = counter + 3;
                }
                counter = ++counter;
            }
            return (_preFix != "" && str.ToString() != "") ? _preFix + " " + str.ToString() : (str.ToString() != "") ? str.ToString() : "";
        }

        public static string formatVNDCurrencyText(string content)
        {
            return formatCurrencyText(content, "(VND) ", '.', ',');
        }

        public static string formatCurrencyTextWithoutMask(string content)
        {
            return formatCurrencyText(content, "", '.', ',');
        }

        public static string convertCurrencyToText(string number)
        {
            string[] dv = { "", "Mươi", "Trăm", "Nghìn", "Triệu", "Tỉ" };
            string[] cs = { "Không", "Một", "Hai", "Ba", "Bốn", "Năm", "Sáu", "Bảy", "Tám", "Chín" };
            string doc;
            int i, j, k, n, len, found, ddv, rd;

            len = number.Length;
            number += "ss";
            doc = "";
            found = 0;
            ddv = 0;
            rd = 0;

            i = 0;
            while (i < len)
            {
                //So chu so o hang dang duyet
                n = (len - i + 2) % 3 + 1;

                //Kiem tra so 0
                found = 0;
                for (j = 0; j < n; j++)
                {
                    if (number[i + j] != '0')
                    {
                        found = 1;
                        break;
                    }
                }

                //Duyet n chu so
                if (found == 1)
                {
                    rd = 1;
                    for (j = 0; j < n; j++)
                    {
                        ddv = 1;
                        switch (number[i + j])
                        {
                            case '0':
                                if (n - j == 3) doc += cs[0] + " ";
                                if (n - j == 2)
                                {
                                    if (number[i + j + 1] != '0') doc += "lẻ ";
                                    ddv = 0;
                                }
                                break;
                            case '1':
                                if (n - j == 3) doc += cs[1] + " ";
                                if (n - j == 2)
                                {
                                    doc += "Mười ";
                                    ddv = 0;
                                }
                                if (n - j == 1)
                                {
                                    if (i + j == 0) k = 0;
                                    else k = i + j - 1;

                                    if (number[k] != '1' && number[k] != '0')
                                        doc += "Mốt ";
                                    else
                                        doc += cs[1] + " ";
                                }
                                break;
                            case '5':
                                if (i + j == len - 1)
                                    doc += "Lăm ";
                                else
                                    doc += cs[5] + " ";
                                break;
                            default:
                                doc += cs[(int)number[i + j] - 48] + " ";
                                break;
                        }

                        //Doc don vi nho
                        if (ddv == 1)
                        {
                            doc += dv[n - j - 1] + " ";
                        }
                    }
                }


                //Doc don vi lon
                if (len - i - n > 0)
                {
                    if ((len - i - n) % 9 == 0)
                    {
                        if (rd == 1)
                            for (k = 0; k < (len - i - n) / 9; k++)
                                doc += "Tỉ ";
                        rd = 0;
                    }
                    else
                        if (found != 0) doc += dv[((len - i - n + 1) % 9) / 3 + 2] + " ";
                }

                i += n;
            }

            if (len == 1)
                if (number[0] == '0' || number[0] == '5') return cs[(int)number[0] - 48];

            return doc + " Đồng";
        }

        public static bool isAdmin()
        {
            return (CurrentUser != null) && (CurrentUser.Type == BHConstant.USER_TYPE_ID1) ? true : false;
        }

        public static string GetTempSeedID(string prefix)
        {
            string result = prefix + BaoHienRepository.GetBaoHienDBDataContext().GetSystemDate().ToString("ddMMyy") + "----";
            return result;
        }

        public static DataGridViewTextBoxColumn CreateCell(string DataPropertyName, string HeaderText, int Width)
        {
            DataGridViewTextBoxColumn cell = new DataGridViewTextBoxColumn();
            cell.DataPropertyName = DataPropertyName;
            cell.HeaderText = HeaderText;
            cell.Width = Width;
            cell.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            return cell;
        }

        public static DataGridViewTextBoxColumn CreateCellWithAlignment(string DataPropertyName, string HeaderText, int Width, DataGridViewContentAlignment alignment)
        {
            DataGridViewTextBoxColumn cell = new DataGridViewTextBoxColumn();
            cell.DataPropertyName = DataPropertyName;
            cell.HeaderText = HeaderText;
            cell.Width = Width;
            cell.DefaultCellStyle.Alignment = alignment;
            return cell;
        }

        public static DataGridViewImageColumn CreateCellDeleteAction()
        {
            DataGridViewImageColumn cell = new DataGridViewImageColumn();
            cell.Image = Properties.Resources.erase;
            cell.HeaderText = "Xóa";
            cell.Width = 40;
            cell.ImageLayout = DataGridViewImageCellLayout.Normal;
            cell.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            return cell;
        }

        public static int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {

            // If the part does not contain a SharedStringTable, create one.
            if (shareStringPart.SharedStringTable == null)
            {
                shareStringPart.SharedStringTable = new SharedStringTable();
            }
            int i = 0;

            // Iterate through all the items in the SharedStringTable. If the text already exists, return its index.
            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    return i;
                }
                i++;
            }

            // The text does not exist in the part. Create the SharedStringItem and return its index.
            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            shareStringPart.SharedStringTable.Save();
            return i;
        }

        public static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one. 
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {

                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                //foreach (Cell cell in row.Elements<Cell>())
                //{
                // if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                // {
                // refCell = cell;
                // break;
                // }
                //}
                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);
                worksheet.Save();
                return newCell;
            }
        }

        public static bool ExportDataGridViewToXLS(string filepath, DataGridView dgw)
        {
            try
            {
                FileInfo file = new FileInfo(filepath);
                if (file.Exists)
                    file.Delete();
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                // Add a WorkbookPart to the document.
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                // Add Sheets to the Workbook.
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                // Append a new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Default" };
                sheets.Append(sheet);

                string cl = "";
                uint row = 2;
                Cell cell;
                int index;
                SharedStringTablePart shareStringPart;
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    for (int j = 0; j < dgw.Columns.Count; j++)
                    {
                        cl = Convert.ToString(Convert.ToChar(65 + j));
                        if (spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
                            shareStringPart = spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
                        else
                            shareStringPart = spreadsheetDocument.WorkbookPart.AddNewPart<SharedStringTablePart>();
                        if (row == 2)
                        {
                            index = Global.InsertSharedStringItem(dgw.Columns[j].HeaderText, shareStringPart);
                            cell = Global.InsertCellInWorksheet(cl, row - 1, worksheetPart);
                            cell.CellValue = new CellValue(index.ToString());
                            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                        }
                        // Insert the text into the SharedStringTablePart.
                        string value = dgw[j, i].Value != null ? ((j == 7 || j == 8) ? dgw[j, i].Value.ToString().Replace(".", "") : dgw[j, i].Value.ToString()) : "";
                        index = Global.InsertSharedStringItem(value, shareStringPart);
                        cell = Global.InsertCellInWorksheet(cl, row, worksheetPart);
                        cell.CellValue = new CellValue(index.ToString());
                        cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                    }
                    row++;
                }
                spreadsheetDocument.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void DisableDropDownWhenSuggesting(ComboBox cb)
        {
            cb.DroppedDown = false;
        }
    }
}
