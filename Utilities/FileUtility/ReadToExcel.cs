using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using System.Text;

namespace EduReg.Utilities.FileUtility
{
    public class ReadToExcel
    {
        public static List<dynamic> ReadExcelFile(string filename)
        {
            var response = new List<dynamic>();
            try
            {

                // string filename = string.Format("{0}.xlsx", Departmentname);
                //Lets open the existing excel file and read through its content . Open the excel using openxml sdk
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filename, false))
                {
                    //create the object for workbook part  
                    WorkbookPart workbookPart = doc.WorkbookPart;
                    Sheets thesheetcollection = workbookPart.Workbook.GetFirstChild<Sheets>();
                    StringBuilder excelResult = new StringBuilder();

                    //using for each loop to get the sheet from the sheetcollection  
                    foreach (Sheet thesheet in thesheetcollection)
                    {
                        //excelResult.AppendLine("Excel Sheet Name : " + thesheet.Name);
                        //excelResult.AppendLine("----------------------------------------------- ");
                        //statement to get the worksheet object by using the sheet id  
                        Worksheet theWorksheet = ((WorksheetPart)workbookPart.GetPartById(thesheet.Id)).Worksheet;

                        SheetData thesheetdata = (SheetData)theWorksheet.GetFirstChild<SheetData>();
                        foreach (Row thecurrentrow in thesheetdata)
                        {
                            foreach (Cell thecurrentcell in thecurrentrow)
                            {
                                //statement to take the integer value  
                                string currentcellvalue = string.Empty;
                                if (thecurrentcell.DataType != null)
                                {
                                    if (thecurrentcell.DataType == CellValues.SharedString)
                                    {
                                        int id;
                                        if (Int32.TryParse(thecurrentcell.InnerText, out id))
                                        {
                                            SharedStringItem item = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
                                            if (item.Text != null)
                                            {
                                                //code to take the string value  
                                                excelResult.Append(item.Text.Text + " ");
                                            }
                                            else if (item.InnerText != null)
                                            {
                                                currentcellvalue = item.InnerText;
                                            }
                                            else if (item.InnerXml != null)
                                            {
                                                currentcellvalue = item.InnerXml;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    excelResult.Append(Convert.ToInt16(thecurrentcell.InnerText) + " ");
                                }
                            }
                            excelResult.AppendLine();
                        }
                        excelResult.Append("");
                        Console.WriteLine(excelResult.ToString());
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return response;
        }



        private static string GetCellValue(WorkbookPart wbPart, List<Cell> theCells, string cellColumnReference)
        {
            Cell theCell = null;
            string value = "";
            foreach (Cell cell in theCells)
            {
                if (cell.CellReference.Value.StartsWith(cellColumnReference))
                {
                    theCell = cell;
                    break;
                }
            }
            if (theCell != null)
            {
                value = theCell.InnerText;
                // If the cell represents an integer number, you are done. 
                // For dates, this code returns the serialized value that represents the date. The code handles strings and 
                // Booleans individually. For shared strings, the code looks up the corresponding value in the shared string table. For Booleans, the code converts the value into the words TRUE or FALSE.
                if (theCell.DataType != null)
                {
                    switch (theCell.DataType.Value)
                    {
                        case CellValues.SharedString:
                            // For shared strings, look up the value in the shared strings table.
                            var stringTable = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                            // If the shared string table is missing, something is wrong. Return the index that is in the cell. Otherwise, look up the correct text in the table.
                            if (stringTable != null)
                            {
                                value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
                            }
                            break;
                        case CellValues.Boolean:
                            switch (value)
                            {
                                case "0":
                                    value = "FALSE";
                                    break;
                                default:
                                    value = "TRUE";
                                    break;
                            }
                            break;
                    }
                }
            }
            return value;
        }

        private static string GetCellValue(WorkbookPart wbPart, List<Cell> theCells, int index)
        {
            return GetCellValue(wbPart, theCells, GetExcelColumnName(index));
        }

        private static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;
            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }
            return columnName;
        }

        //Only xlsx files
        public static DataTable GetDataTableFromExcelFile(string filePath, string sheetName = "")
        {
            DataTable dt = new DataTable();
            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
                {
                    WorkbookPart wbPart = document.WorkbookPart;
                    IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    string sheetId = sheetName != "" ? sheets.Where(q => q.Name == sheetName).First().Id.Value : sheets.First().Id.Value;
                    WorksheetPart wsPart = (WorksheetPart)wbPart.GetPartById(sheetId);
                    SheetData sheetdata = wsPart.Worksheet.Elements<SheetData>().FirstOrDefault();
                    int totalHeaderCount = sheetdata.Descendants<Row>().ElementAt(0).Descendants<Cell>().Count();
                    //Get the header                    
                    for (int i = 1; i <= totalHeaderCount; i++)
                    {
                        dt.Columns.Add(GetCellValue(wbPart, sheetdata.Descendants<Row>().ElementAt(0).Elements<Cell>().ToList(), i));
                    }
                    foreach (Row r in sheetdata.Descendants<Row>())
                    {
                        if (r.RowIndex > 1)
                        {
                            DataRow tempRow = dt.NewRow();

                            //Always get from the header count, because the index of the row changes where empty cell is not counted
                            for (int i = 1; i <= totalHeaderCount; i++)
                            {
                                tempRow[i - 1] = GetCellValue(wbPart, r.Elements<Cell>().ToList(), i);
                            }
                            dt.Rows.Add(tempRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
    }
}
