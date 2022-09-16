using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using OpenIDConnect.AZ204.OperationsAgent.Models;

namespace OpenIDConnect.AZ204.OperationsAgent.Controllers;

[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[Route("[controller]")]
[ApiController]
public class OperationsAgentController : ControllerBase
{
    private static readonly string[] ScopeRequiredByApi = { "OperationsAgent.All" };
    private readonly IDownstreamWebApi _downstreamWebApi;

    public OperationsAgentController(IDownstreamWebApi downstreamWebApi)
    {
        _downstreamWebApi = downstreamWebApi;
    }

    [HttpGet("[action]/{memberId:guid}")]
    [Authorize(Roles = "OperationsAgent")]
    public async Task<IActionResult> GetAgentMemberHistory([FromRoute] Guid memberId)
    {
        HttpContext.VerifyUserHasAnyAcceptedScope(ScopeRequiredByApi);

        var memberInfo = await _downstreamWebApi.CallWebApiForUserAsync<Member>("MembersApi",
            options => { options.RelativePath = $"Members/GetMemberInfo/{memberId}"; });
        return Ok(new AgentMemberHistoryResponse
        {
            MemberInfo = memberInfo,
            AgentMemberHistory = AgentMemberHistory.AgentMemberHistories
        });
    }
}