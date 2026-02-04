using no3_api.Models;

namespace no3_api.Dtos;

public class CreateRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string ResponseReason { get; set; } = string.Empty;
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
}