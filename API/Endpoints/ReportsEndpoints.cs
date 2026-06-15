namespace API.Endpoints;

using Application.Features.Reports.TicketsReport;
using Application.Features.Reports.UsersReport;
using MediatR;

public static class ReportsEndpoints
{
    private const string ExcelContentType =
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    public static IEndpointRouteBuilder MapReports(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/reports")
            .WithTags("Reports");

        group.MapGet("/users", async (ISender sender, CancellationToken cancellationToken) =>
            {
                var report = await sender.Send(new UsersReportQuery(), cancellationToken);
                return Results.File(report, ExcelContentType, "Usuarios.xlsx");
            })
            .WithName("DownloadUsersReport")
            .WithSummary("Genera el reporte Excel de usuarios")
            .Produces(StatusCodes.Status200OK, contentType: ExcelContentType);

        group.MapGet("/tickets", async (ISender sender, CancellationToken cancellationToken) =>
            {
                var report = await sender.Send(new TicketsReportQuery(), cancellationToken);
                return Results.File(report, ExcelContentType, "Tickets.xlsx");
            })
            .WithName("DownloadTicketsReport")
            .WithSummary("Genera el reporte Excel de tickets")
            .Produces(StatusCodes.Status200OK, contentType: ExcelContentType);

        return app;
    }
}
