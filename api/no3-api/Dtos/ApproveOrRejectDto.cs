using no3_api.Models;

namespace no3_api.Dtos;

public class ApproveOrRejectDto
{
    public string ResponseReason { get; set; } = string.Empty;
}

public class MultiApproveOrRejectDto
{
    public List<int> Ids { get; set; } = new List<int>();
    public string ResponseReason { get; set; } = string.Empty;
}