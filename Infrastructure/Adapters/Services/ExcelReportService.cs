namespace Infrastructure.Services;

using ClosedXML.Excel;
using Domain.Entities;
using Domain.Services;

public class ExcelReportService : IExcelReportService
{
    public byte[] GenerateUsers(List<user> users)
    {
        using var workbook = new XLWorkbook();

        var ws = workbook.Worksheets.Add("Usuarios");

        ws.Cell(1,1).Value = "Usuario";
        ws.Cell(1,2).Value = "Email";
        ws.Cell(1,3).Value = "Fecha";

        int row = 2;

        foreach(var user in users)
        {
            ws.Cell(row,1).Value = user.username;
            ws.Cell(row,2).Value = user.email;
            ws.Cell(row,3).Value = user.created_at;

            row++;
        }

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return stream.ToArray();
    }

    public byte[] GenerateTickets(List<ticket> tickets)
    {
        using var workbook = new XLWorkbook();

        var ws = workbook.Worksheets.Add("Tickets");

        ws.Cell(1,1).Value = "Titulo";
        ws.Cell(1,2).Value = "Estado";
        ws.Cell(1,3).Value = "Usuario";
        ws.Cell(1,4).Value = "Creado";

        int row = 2;

        foreach(var ticket in tickets)
        {
            ws.Cell(row,1).Value = ticket.title;
            ws.Cell(row,2).Value = ticket.status;
            ws.Cell(row,3).Value = ticket.user.username;
            ws.Cell(row,4).Value = ticket.created_at;

            row++;
        }

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return stream.ToArray();
    }
}
