namespace Application.Features.Reports.TicketsReport;

using MediatR;

public record TicketsReportQuery : IRequest<byte[]>;
