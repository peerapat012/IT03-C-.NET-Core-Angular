using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using no3_api.Dtos;
using no3_api.Models;
using no3_api.Services;

namespace no3_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestController(IRequestServices services) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<RequestDto>>> GetRequestAsync()
    {
        return Ok(await services.GetAllRequestsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RequestDto>> GetRequestByIdAsync(int id)
    {
        var exitRequest = await services.GetRequestByIdAsync(id);
        if (exitRequest == null) return NotFound();
        return Ok(await services.GetRequestByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<RequestDto>> CreateRequestAsync(CreateRequestDto requestDto)
    {
        var createRequest = await services.CreateRequestAsync(requestDto);
        return CreatedAtAction(nameof(GetRequestByIdAsync), new { id = createRequest.Id }, createRequest);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRequestAsync(int id, UpdateRequestDto requestDto)
    {
        var updatedRequest = await services.UpdateRequestAsync(id, requestDto);
        return updatedRequest ? NoContent() : NotFound("Request with the given id does not exist");
    }

    [HttpPatch("{id}/approve")]
    public async Task<ActionResult> ApproveRequestAsync(int id, ApproveOrRejectDto requestDto)
    {
        var existingRequest = await services.GetRequestByIdAsync(id);
        if (existingRequest?.Status == RequestStatus.Approved)
            return NotFound("This request with the given id is already approved");

        var updatedRequest = await services.ApproveRequestPerIdAsync(id, 0, requestDto.ResponseReason);
        return updatedRequest
            ? Ok(await services.GetRequestByIdAsync(id))
            : NotFound("Request with the given id does not exist");
    }

    [HttpPatch("{id}/reject")]
    public async Task<ActionResult> RejectRequestAsync(int id, ApproveOrRejectDto requestDto)
    {
        var existingRequest = await services.GetRequestByIdAsync(id);
        if (existingRequest?.Status == RequestStatus.Rejected)
            return NotFound("This request with the given id is already rejected");

        var updatedRequest = await services.ApproveRequestPerIdAsync(id, 1, requestDto.ResponseReason);
        return updatedRequest
            ? Ok(await services.GetRequestByIdAsync(id))
            : NotFound("Request with the given id does not exist");
    }

    [HttpPatch("approve")]
    public async Task<ActionResult> MultipleApproveAsync([FromBody] MultiApproveOrRejectDto requestDto)
    {
        var existingRequests = await services.GetAllRequestsAsync();
        var filter = existingRequests.Where(request => requestDto.Ids.Contains(request.Id)).ToList();
        if (filter.Any(q => q.Status != RequestStatus.Pending))
            return NotFound("Some of this request with the given id is already approved");

        var approveRequest = await services.ApproveRequestMutipleIdAsync(0, requestDto);
        return approveRequest
            ? Ok(await services.GetAllRequestsAsync())
            : NotFound("Request with the given id does not exist");
    }

    [HttpPatch("reject")]
    public async Task<ActionResult> MultipleRejectAsync([FromBody] MultiApproveOrRejectDto rejectDto)
    {
        var existingRequests = await services.GetAllRequestsAsync();
        var filter = existingRequests.Where(request => rejectDto.Ids.Contains(request.Id)).ToList();
        if (filter.Any(q => q.Status != RequestStatus.Pending))
            return NotFound("Some of this request with the given id is already rejected");

        var rejectRequest = await services.RejectRequestMutipleIdAsync(1, rejectDto);
        return rejectRequest
            ? Ok(await services.GetAllRequestsAsync())
            : NotFound("Request with the given id does not exist");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteById(int id)
    {
        return await services.DeleteRequestAsync(id)
            ? NoContent()
            : NotFound("Request with the given id does not exist");
    }
}