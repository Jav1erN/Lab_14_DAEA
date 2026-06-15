namespace Application.Features.Reports.TicketsReport;

using Domain.Repositories;
using Domain.Services;
using MediatR;

public class TicketsReportHandler : IRequestHandler<TicketsReportQuery, byte[]>
{
    private readonly ITicketRepository _repository;
    private readonly IExcelReportService _excel;

    public TicketsReportHandler(ITicketRepository repository, IExcelReportService excel)
    {
        _repository = repository;
        _excel = excel;
    }

    public async Task<byte[]> Handle(
        TicketsReportQuery request,
        CancellationToken cancellationToken)
    {
        var tickets = await _repository.GetAllAsync(cancellationToken);
        return _excel.GenerateTickets(tickets);
    }
}
