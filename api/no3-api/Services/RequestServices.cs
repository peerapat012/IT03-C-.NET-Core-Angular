using Microsoft.EntityFrameworkCore;
using no3_api.Data;
using no3_api.Dtos;
using no3_api.Models;

namespace no3_api.Services;

public class RequestServices(AppDbContext context) : IRequestServices
{
    public async Task<List<RequestDto>> GetAllRequestsAsync()
    {
        var requestList = await context.Requests.Select(request => new RequestDto
        {
            Id = request.Id,
            Title = request.Title,
            ResponseReason = request.ResponseReason,
            Status = request.Status
        }).ToListAsync();
        return requestList;
    }

    public async Task<RequestDto?> GetRequestByIdAsync(int requestId)
    {
        var exitsRequest = await context.Requests.Select(request => new RequestDto
        {
            Id = request.Id,
            Title = request.Title,
            ResponseReason = request.ResponseReason,
            Status = request.Status
        }).FirstOrDefaultAsync(request => request.Id == requestId);
        return exitsRequest;
    }

    public async Task<RequestDto> CreateRequestAsync(CreateRequestDto requestDto)
    {
        var newRequest = new RequestListModel
        {
            Title = requestDto.Title,
            ResponseReason = requestDto.ResponseReason,
            Status = requestDto.Status
        };
        context.Requests.Add(newRequest);
        await context.SaveChangesAsync();
        return new RequestDto
        {
            Id = newRequest.Id,
            Title = newRequest.Title,
            ResponseReason = newRequest.ResponseReason,
            Status = newRequest.Status
        };
    }

    public async Task<bool> UpdateRequestAsync(int id, UpdateRequestDto requestDto)
    {
        var existingRequest = await context.Requests.FirstOrDefaultAsync(request => request.Id == id);
        if (existingRequest == null) return false;

        existingRequest.Title = requestDto.Title;
        existingRequest.ResponseReason = requestDto.ResponseReason;
        existingRequest.Status = requestDto.Status;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ApproveRequestPerIdAsync(int id, int status, string reason)
    {
        var existingRequest = await context.Requests.FirstOrDefaultAsync(request => request.Id == id);
        if (existingRequest == null) return false;

        existingRequest.Status = (RequestStatus)status;
        existingRequest.ResponseReason = reason;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RejectRequestPerIdAsync(int id, int status, string reason)
    {
        var existingRequest = await context.Requests.FirstOrDefaultAsync(request => request.Id == id);
        if (existingRequest == null) return false;

        existingRequest.Status = (RequestStatus)status;
        existingRequest.ResponseReason = reason;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteRequestAsync(int requestId)
    {
        var existingRequest = await context.Requests.FirstOrDefaultAsync(request => request.Id == requestId);
        if (existingRequest == null) return false;
        context.Requests.RemoveRange(existingRequest);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ApproveRequestMutipleIdAsync(int status, MultiApproveOrRejectDto approveDto)
    {
        var existingRequests = await context.Requests.Where(request => approveDto.Ids.Contains(request.Id)).ToListAsync();
        if (!existingRequests.Any()) return false;
        foreach (var existingRequest in existingRequests)
        {
            existingRequest.ResponseReason = approveDto.ResponseReason;
            existingRequest.Status = (RequestStatus)status;
        }
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RejectRequestMutipleIdAsync(int status, MultiApproveOrRejectDto rejectDto)
    {
        var existingRequests = await context.Requests.Where(request => rejectDto.Ids.Contains(request.Id)).ToListAsync();
        if (!existingRequests.Any()) return false;
        foreach (var existingRequest in existingRequests)
        {
            existingRequest.ResponseReason = rejectDto.ResponseReason;
            existingRequest.Status = (RequestStatus)status;
        }
        await context.SaveChangesAsync();
        return true;
    }


}