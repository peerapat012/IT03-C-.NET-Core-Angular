namespace no3_api.Models;

public class RequestListModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ResponseReason { get; set; } = string.Empty;
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
}

public enum RequestStatus
{
    Approved,
    Rejected,
    Pending
}