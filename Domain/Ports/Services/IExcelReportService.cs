namespace Domain.Services;

using Domain.Entities;

public interface IExcelReportService
{
    byte[] GenerateUsers(List<user> users);

    byte[] GenerateTickets(List<ticket> tickets);
}
