namespace eMuhasebeServer.Application.Features.Reports.SalesReports;

public sealed record SalesReportResponse
{
    public List<DateOnly> Dates = new();
    public List<decimal> Amounts = new();
}