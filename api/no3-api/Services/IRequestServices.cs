using no3_api.Dtos;
using no3_api.Models;

namespace no3_api.Services;

public interface IRequestServices
{
    Task<List<RequestDto>> GetAllRequestsAsync();
    Task<RequestDto?> GetRequestByIdAsync(int requestId);
    Task<RequestDto> CreateRequestAsync(CreateRequestDto requestDto);
    Task<bool> UpdateRequestAsync(int id, UpdateRequestDto requestDto);
    Task<bool> ApproveRequestAsync(int id, int status, string reason);
    Task<bool> RejectRequestAsync(int id, int status, string reason);
    Task<bool> DeleteRequestAsync(int requestId);
}