namespace Application.Features.Reports.UsersReport;

using Domain.Repositories;
using Domain.Services;
using MediatR;

public class UsersReportHandler : IRequestHandler<UsersReportQuery, byte[]>
{
    private readonly IUserRepository _repository;
    private readonly IExcelReportService _excel;

    public UsersReportHandler(IUserRepository repository, IExcelReportService excel)
    {
        _repository = repository;
        _excel = excel;
    }

    public async Task<byte[]> Handle(
        UsersReportQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllAsync(cancellationToken);
        return _excel.GenerateUsers(users);
    }
}
