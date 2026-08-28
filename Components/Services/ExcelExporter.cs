using System.Globalization;
using System.Reflection;
using System.Text;

namespace booking_room_admin.Components.Services;

public enum ExportCellType
{
    Text,
    Number,
    Date
}

public class ExportColumn
{
    public string Header { get; set; } = "";
    public string PropertyName { get; set; } = "";
    public ExportCellType CellType { get; set; } = ExportCellType.Text;
}

public static class ExcelExporter
{
    public static byte[] ToCsv(IEnumerable<ExportColumn> columns, IEnumerable<object> rows)
    {
        var sb = new StringBuilder();
        sb.AppendLine(string.Join(",", columns.Select(c => EscapeCsv(c.Header))));
        foreach (var row in rows)
        {
            var values = columns.Select(c => EscapeCsv(GetValue(row, c)?.ToString() ?? ""));
            sb.AppendLine(string.Join(",", values));
        }
        var bytes = Encoding.UTF8.GetBytes(sb.ToString());
        return Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();
    }

    public static byte[] ToXlsx(IEnumerable<ExportColumn> columns, IEnumerable<object> rows)
    {
        var cols = columns.ToList();
        var rowList = rows.ToList();

        var sheet = new StringBuilder();
        sheet.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        sheet.Append("<worksheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\"><sheetData>");

        sheet.Append(Row(1, cols.Select((c, i) => Cell(Address(i, 1), 1, c.Header, false))));

        int r = 2;
        foreach (var row in rowList)
        {
            var cells = cols.Select((c, i) =>
            {
                var val = GetValue(row, c);
                if (c.CellType == ExportCellType.Number && val != null)
                    return Cell(Address(i, r), 0, FormatNumber(val), true);
                if (c.CellType == ExportCellType.Date && val is DateTime dt)
                    return Cell(Address(i, r), 2, dt.ToOADate().ToString(CultureInfo.InvariantCulture), true);
                return Cell(Address(i, r), 0, val?.ToString() ?? "", false);
            });
            sheet.Append(Row(r, cells));
            r++;
        }
        sheet.Append("</sheetData></worksheet>");

        var contentTypes = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
            "<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\">" +
            "<Default Extension=\"rels\" ContentType=\"application/vnd.openxmlformats-package.relationships+xml\"/>" +
            "<Default Extension=\"xml\" ContentType=\"application/xml\"/>" +
            "<Override PartName=\"/xl/workbook.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml\"/>" +
            "<Override PartName=\"/xl/worksheets/sheet1.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml\"/>" +
            "<Override PartName=\"/xl/styles.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml\"/>" +
            "</Types>";

        var rootRels = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
            "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
            "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument\" Target=\"xl/workbook.xml\"/>" +
            "</Relationships>";

        var workbook = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
            "<workbook xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" " +
            "xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\">" +
            "<sheets><sheet name=\"Data\" sheetId=\"1\" r:id=\"rId1\"/></sheets></workbook>";

        var workbookRels = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
            "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
            "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet\" Target=\"worksheets/sheet1.xml\"/>" +
            "<Relationship Id=\"rId2\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles\" Target=\"styles.xml\"/>" +
            "</Relationships>";

        var styles = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
            "<styleSheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\">" +
            "<fonts count=\"2\"><font><sz val=\"11\"/><name val=\"Calibri\"/></font><font><b/><sz val=\"11\"/><name val=\"Calibri\"/></font></fonts>" +
            "<fills count=\"2\"><fill><patternFill patternType=\"none\"/></fill><fill><patternFill patternType=\"gray125\"/></fill></fills>" +
            "<borders count=\"1\"><border/></borders>" +
            "<cellStyleXfs count=\"1\"><xf numFmtId=\"0\" fontId=\"0\" fillId=\"0\" borderId=\"0\"/></cellStyleXfs>" +
            "<cellXfs count=\"3\">" +
            "<xf numFmtId=\"0\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\"/>" +
            "<xf numFmtId=\"0\" fontId=\"1\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyFont=\"1\"/>" +
            "<xf numFmtId=\"14\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyNumberFormat=\"1\"/>" +
            "</cellXfs>" +
            "<cellStyles count=\"1\"><cellStyle name=\"Normal\" xfId=\"0\" builtinId=\"0\"/></cellStyles>" +
            "</styleSheet>";

        using var stream = new MemoryStream();
        using (var zip = new System.IO.Compression.ZipArchive(stream, System.IO.Compression.ZipArchiveMode.Create, true))
        {
            AddEntry(zip, "[Content_Types].xml", contentTypes);
            AddEntry(zip, "_rels/.rels", rootRels);
            AddEntry(zip, "xl/workbook.xml", workbook);
            AddEntry(zip, "xl/_rels/workbook.xml.rels", workbookRels);
            AddEntry(zip, "xl/styles.xml", styles);
            AddEntry(zip, "xl/worksheets/sheet1.xml", sheet.ToString());
        }
        return stream.ToArray();
    }

    private static void AddEntry(System.IO.Compression.ZipArchive zip, string name, string content)
    {
        var entry = zip.CreateEntry(name);
        using var w = new StreamWriter(entry.Open(), new UTF8Encoding(false));
        w.Write(content);
    }

    private static string Row(int index, IEnumerable<string> cells) =>
        $"<row r=\"{index}\">{string.Concat(cells)}</row>";

    private static string Cell(string address, int style, string value, bool isNumeric)
    {
        if (isNumeric)
            return $"<c r=\"{address}\" s=\"{style}\"><v>{EscapeXml(value)}</v></c>";
        return $"<c r=\"{address}\" s=\"{style}\" t=\"inlineStr\"><is><t xml:space=\"preserve\">{EscapeXml(value)}</t></is></c>";
    }

    private static string Address(int col, int row) => ColLetter(col) + row;

    private static string ColLetter(int index)
    {
        string s = "";
        int n = index;
        while (n >= 0)
        {
            s = (char)('A' + (n % 26)) + s;
            n = n / 26 - 1;
        }
        return s;
    }

    private static string FormatNumber(object val) =>
        val switch
        {
            IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
            _ => val.ToString() ?? ""
        };

    private static object? GetValue(object row, ExportColumn col)
    {
        var prop = row.GetType().GetProperty(col.PropertyName, BindingFlags.Public | BindingFlags.Instance);
        if (prop == null) return null;
        var val = prop.GetValue(row);
        if (col.CellType == ExportCellType.Date && val is string s && DateTime.TryParse(s, out var dt))
            return dt;
        return val;
    }

    private static string EscapeXml(string s) =>
        s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")
         .Replace("\"", "&quot;").Replace("'", "&apos;");

    private static string EscapeCsv(string s)
    {
        if (s.Contains(",") || s.Contains("\"") || s.Contains("\n"))
            return "\"" + s.Replace("\"", "\"\"") + "\"";
        return s;
    }
}
