using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using App.Auxilary;
using Aspose.Cells;
using DatEx.Creatio.DataModel.Auxilary;
using OfficeOpenXml;
using ITIS = DatEx.Creatio.DataModel.ITIS;

namespace App
{
    public class ExcellWriter
    {
        private Workbook wb { get; set; } = new Workbook();

        private String FilePath { get; set; }


        public ExcellWriter(String filePath)
        {
            FilePath = filePath;
            wb.Worksheets.RemoveAt(0);            
        }


        public void SaveMappingsInfo()
        {
            List<Type> dataTypes1 = new List<Type>
            {
                typeof(ITIS.Account),
                typeof(ITIS.Contact),
                typeof(ITIS.ContactCareer),
                typeof(ITIS.Department),
                typeof(ITIS.Employee),
                typeof(ITIS.EmployeeCareer),
                typeof(ITIS.EmployeeJob),
                typeof(ITIS.ITISCounterpartyLegalStatus),
                typeof(ITIS.ITISEmploymentType),
                typeof(ITIS.Job),
                typeof(ITIS.OrgStructureUnit),
            };

            List<Type> dataTypes2 = new List<Type>
            {
                typeof(ITIS.ITISPurchasingArticle),
                typeof(ITIS.ITISCompaniesNomenclature),
                typeof(ITIS.ITISNomenclatureGroups),
                typeof(ITIS.Unit),
            };

            SaveSyncSettingsObjectInfo();
            SaveMappings(dataTypes1);
            SaveMappings(dataTypes2);

            wb.Save(FilePath, SaveFormat.Xlsx);

            var workBookFileInfo = new FileInfo(FilePath);
            using (var excelPackage = new ExcelPackage(workBookFileInfo))
            {
                var worksheet = excelPackage.Workbook.Worksheets.SingleOrDefault(x => x.Name == "Evaluation Warning");
                excelPackage.Workbook.Worksheets.Delete(worksheet);
                excelPackage.Workbook.Worksheets.First().Select();
                excelPackage.Save();
            }
        }


        private void SaveSyncSettingsObjectInfo()
        {
            Type type = typeof(SyncSettings);
            String typeName = type.Name;
            Worksheet sheet = wb.Worksheets.Add(typeName);
            sheet.IsGridlinesVisible = false;

            sheet.Cells.Merge(0, 0, 1, 4);
            sheet.Set(0, 0, EStyle.SheetHeaader, type.Name);
            
            DocAttribute typeDocAttr = type.GetCustomAttribute<DocAttribute>();
            if(typeDocAttr != null)
            {
                sheet.Cells.Merge(1, 0, 1, 4);
                sheet.Set(1, 0, EStyle.SheetHeaader, typeDocAttr.FriendlyName);
                sheet.Cells.Merge(2, 0, 1, 4);
                sheet.Set(2, 0, EStyle.SheetHeaader, typeDocAttr.Remarks);
            }

            Int32 row = 4;
            Int32 col = 0;

            sheet.Set(row, col++, EStyle.TableHeader, "Type");
            sheet.Set(row, col++, EStyle.TableHeader, "Property");
            sheet.Set(row, col++, EStyle.TableHeader, "Дружественное название свойства");
            sheet.Set(row, col++, EStyle.TableHeader, "Пояснения");

            foreach (var prop in type.GetProperties())
            {
                col = 0;
                row++;

                sheet.Set(row, col++, EStyle.MappableProp, prop.PropertyType.Name);
                sheet.Set(row, col++, EStyle.MappableProp, prop.Name);
                DocAttribute docAttr = prop.GetCustomAttribute<DocAttribute>();
                if (docAttr == null) continue;
                sheet.Set(row, col++, EStyle.MappableProp, docAttr.FriendlyName);
                sheet.Set(row, col++, EStyle.MappableProp, docAttr.Remarks);
            }
            sheet.AutoFitColumns(0, 10);
            sheet.AutoFitRows(0, row);
        }

        private void SaveMappings(List<Type> dataTypes)
        {
            foreach (Type type in dataTypes)
            {
                String typeName = type.Name;

                CreatioTypeAttribute creatioType = type.GetCustomAttribute<CreatioTypeAttribute>(true);
                String typeTitle = creatioType.Title;

                Worksheet sheet = wb.Worksheets.Add(typeName);
                sheet.IsGridlinesVisible = false;

                sheet.Cells.Merge(0, 0, 1, 2);
                sheet.Set(0, 0, EStyle.SheetHeaader, typeTitle);
                sheet.Cells.Merge(0, 2, 1, 5);
                sheet.Set(0, 2, EStyle.SheetHeaader, typeName);

                Int32 row = 1;
                Int32 col = 0;
                sheet.Set(row, col++, EStyle.TableHeader, "Тип");
                sheet.Set(row, col++, EStyle.TableHeader, "Заголовок");
                sheet.Set(row, col++, EStyle.TableHeader, "Type");
                sheet.Set(row, col++, EStyle.TableHeader, "Name");
                sheet.Set(row, col++, EStyle.TableHeader, "Новое");
                sheet.Set(row, col++, EStyle.TableHeader, "Маппинг в OData");
                sheet.Set(row, col++, EStyle.TableHeader, "Примечания");

                foreach (var prop in type.GetProperties().OrderBy(p => p.Name))
                {
                    CreatioPropAttribute creatioPropAttr = prop.GetCustomAttribute<CreatioPropAttribute>(true);
                    if (creatioPropAttr == null) continue;
                    List<MapAttribute> mapAttrs = prop.GetCustomAttributes<MapAttribute>(true).ToList();
                    MapRemarksAttribute mapRemmAttr = prop.GetCustomAttribute<MapRemarksAttribute>(true);
                    CreatioTypeAttribute creatioTypeAttr = prop.GetCustomAttribute<CreatioTypeAttribute>(true);
                    CreatioPropNotExistInDataModelOfITISAttribute newPropAttr = prop.GetCustomAttribute<CreatioPropNotExistInDataModelOfITISAttribute>(true);
                    ++row;

                    EStyle currentRowStyle = mapAttrs.Count > 0 ? EStyle.MappableProp : EStyle.Default;
                    col = 0;
                    sheet.Set(row, col++, currentRowStyle, GetCreatioTypeName(prop));
                    sheet.Set(row, col++, currentRowStyle, creatioPropAttr.CreatioTitle);
                    sheet.Set(row, col++, currentRowStyle, prop.PropertyType.Name);
                    sheet.Set(row, col++, currentRowStyle, prop.Name);
                    sheet.Set(row, col++, currentRowStyle, newPropAttr != null ? "✔": null);
                    sheet.Set(row, col++, currentRowStyle, mapAttrs.Count > 0 ? String.Join("\n", mapAttrs.Select(x => $"{(x.Implemented ? "✔" : "❌")} {x}")) : null);                    
                    sheet.Set(row, col++, currentRowStyle, mapRemmAttr);
                }

                sheet.AutoFitColumns(0, 10);
                sheet.AutoFitRows(0, row);
            }
        }

        private readonly static Dictionary<Type, String> typesMap = new Dictionary<Type, string>
        {
            { typeof(String), "Строка" },
            { typeof(Double), "Дробное число" },
            { typeof(Double?), "Дробное число" },
            { typeof(Int32), "Целое число" },
            { typeof(Int32?), "Целое число" },
            { typeof(DateTime), "Дата / время" },
            { typeof(DateTime?), "Дата / время" },
            { typeof(Boolean), "Булево" },
            { typeof(Boolean?), "Булево" },
            { typeof(Guid?), "Guid" },
            { typeof(Guid), "Guid" },
        };

        static String GetCreatioTypeName(PropertyInfo p)
        {
            var propType = p.PropertyType.GetCustomAttribute<CreatioTypeAttribute>(false)?.Title;
            if (!string.IsNullOrEmpty(propType)) return $"Справочник<{propType}>";
            
            var attribute = (CreatioPropAttribute)p.GetCustomAttributes(typeof(CreatioPropAttribute), false).FirstOrDefault();
            if(attribute != null && !String.IsNullOrWhiteSpace(attribute.CreatioType)) return attribute.CreatioType;

            String typeName;
            if (!typesMap.TryGetValue(p.PropertyType, out typeName))
                throw new Exception($"Русскоязычное соответствие для примитивного типа {p.PropertyType} не найдено.");
            return typeName;
        }
    }

    public enum EStyle
    {
        SheetHeaader,
        TableHeader,
        Default,
        MappableProp
    }

    public static class Ext_Excell
    {
        private static readonly Dictionary<EStyle, Style> Styles = new Dictionary<EStyle, Style>();

        static Ext_Excell()
        {
            Workbook wb = new Workbook();

            Style sheetHeadersStyle = wb.CreateStyle();
            sheetHeadersStyle.VerticalAlignment = TextAlignmentType.Top;
            sheetHeadersStyle.HorizontalAlignment = TextAlignmentType.Left;
            sheetHeadersStyle.Font.Color = Color.Red;
            sheetHeadersStyle.Font.IsBold = true;
            sheetHeadersStyle.Font.Name = "Calibri";
            sheetHeadersStyle.Font.Size = 14;
            Styles.Add(EStyle.SheetHeaader, sheetHeadersStyle);

            Style tableHeadersStyle = wb.CreateStyle();
            tableHeadersStyle.VerticalAlignment = TextAlignmentType.Top;
            tableHeadersStyle.HorizontalAlignment = TextAlignmentType.Left;
            tableHeadersStyle.IsTextWrapped = true;
            tableHeadersStyle.Pattern = BackgroundType.Solid;
            tableHeadersStyle.ForegroundColor = Color.LightSteelBlue;
            tableHeadersStyle.Font.Color = Color.SteelBlue;
            tableHeadersStyle.Font.IsBold = true;
            tableHeadersStyle.Font.Name = "Calibri";
            tableHeadersStyle.Font.Size = 8;
            tableHeadersStyle.SetBorder(BorderType.LeftBorder, CellBorderType.Thin, Color.LightSteelBlue);
            tableHeadersStyle.SetBorder(BorderType.TopBorder, CellBorderType.Thin, Color.LightSteelBlue);
            tableHeadersStyle.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.LightSteelBlue);
            tableHeadersStyle.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.LightSteelBlue);
            Styles.Add(EStyle.TableHeader, tableHeadersStyle);

            Style defaultStyle = wb.CreateStyle();
            defaultStyle.VerticalAlignment = TextAlignmentType.Top;
            defaultStyle.HorizontalAlignment = TextAlignmentType.Left;
            defaultStyle.IsTextWrapped = true;
            defaultStyle.Font.Color = Color.LightGray;
            defaultStyle.Font.Name = "Calibri";
            defaultStyle.Font.Size = 8;
            defaultStyle.SetBorder(BorderType.LeftBorder, CellBorderType.Thin, Color.LightSteelBlue);
            defaultStyle.SetBorder(BorderType.TopBorder, CellBorderType.Thin, Color.LightSteelBlue);
            defaultStyle.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.LightSteelBlue);
            defaultStyle.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.LightSteelBlue);
            Styles.Add(EStyle.Default, defaultStyle);

            Style mapablePropStyle = wb.CreateStyle();
            mapablePropStyle.VerticalAlignment = TextAlignmentType.Top;
            mapablePropStyle.HorizontalAlignment = TextAlignmentType.Left;
            mapablePropStyle.IsTextWrapped = true;
            mapablePropStyle.Font.Name = "Calibri";
            mapablePropStyle.Font.Size = 8;
            mapablePropStyle.SetBorder(BorderType.LeftBorder, CellBorderType.Thin, Color.LightSteelBlue);
            mapablePropStyle.SetBorder(BorderType.TopBorder, CellBorderType.Thin, Color.LightSteelBlue);
            mapablePropStyle.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.LightSteelBlue);
            mapablePropStyle.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.LightSteelBlue);
            Styles.Add(EStyle.MappableProp, mapablePropStyle);
        }


        
        public static Cell Set(this Worksheet worksheet, Int32 row, Int32 col, EStyle style, Boolean value)
        {
            Cell cell = worksheet.Cells[row, col];
            cell.PutValue(value);
            cell.SetStyle(Styles[style]);
            return cell;
        }

        public static Cell Set(this Worksheet worksheet, Int32 row, Int32 col, EStyle style, DateTime value)
        {
            Cell cell = worksheet.Cells[row, col];
            cell.PutValue(value);
            cell.SetStyle(Styles[style]);
            return cell;
        }

        public static Cell Set(this Worksheet worksheet, Int32 row, Int32 col, EStyle style, Double value)
        {
            Cell cell = worksheet.Cells[row, col];
            cell.PutValue(value);
            cell.SetStyle(Styles[style]);
            return cell;
        }

        public static Cell Set(this Worksheet worksheet, Int32 row, Int32 col, EStyle style, Int32 value)
        {
            Cell cell = worksheet.Cells[row, col];
            cell.PutValue(value);
            cell.SetStyle(Styles[style]);
            return cell;
        }

        public static Cell Set(this Worksheet worksheet, Int32 row, Int32 col, EStyle style, Object value = null)
        {
            Cell cell = worksheet.Cells[row, col];
            cell.PutValue(value);
            cell.SetStyle(Styles[style]);
            return cell;
        }

        public static Cell Set(this Worksheet worksheet, Int32 row, Int32 col, EStyle style, String value)
        {
            Cell cell = worksheet.Cells[row, col];
            cell.PutValue(value);
            cell.SetStyle(Styles[style]);
            return cell;
        }
    }
}
