using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using OpenIDConnect.AZ204.Members.Models;

namespace OpenIDConnect.AZ204.Members.Controllers;

[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[Authorize]
[ApiController]
[Route("[controller]")]
public class MembersController : ControllerBase
{
    private static readonly string[] ScopeRequiredByApi = { "MembersApi.All" };

    [HttpGet("[action]/{memberId:guid}")]
    [Authorize(Roles = "Members.Readonly")]
    public async Task<IActionResult> GetMemberInfo([FromRoute] Guid memberId)
    {
        HttpContext.VerifyUserHasAnyAcceptedScope(ScopeRequiredByApi);

        var member = Member.MembersList.FirstOrDefault(x => x.MemberId == memberId);

        if (member == null)
        {
            return await Task.FromResult(NotFound());
        }

        return await Task.FromResult(Ok(member));
    }
}