namespace Application.Features.Reports.UsersReport;

using MediatR;

public record UsersReportQuery : IRequest<byte[]>;
