using no3_api.Dtos;
using no3_api.Models;

namespace no3_api.Services;

public interface IRequestServices
{
    Task<List<RequestDto>> GetAllRequestsAsync();
    Task<RequestDto?> GetRequestByIdAsync(int requestId);
    Task<RequestDto> CreateRequestAsync(CreateRequestDto requestDto);
    Task<bool> UpdateRequestAsync(int id, UpdateRequestDto requestDto);
    Task<bool> ApproveRequestPerIdAsync(int id, int status, string reason);
    Task<bool> ApproveRequestMutipleIdAsync(int status, MultiApproveOrRejectDto approveDto);
    Task<bool> RejectRequestPerIdAsync(int id, int status, string reason);
    Task<bool> RejectRequestMutipleIdAsync(int status, MultiApproveOrRejectDto rejectDto);
    Task<bool> DeleteRequestAsync(int requestId);
}