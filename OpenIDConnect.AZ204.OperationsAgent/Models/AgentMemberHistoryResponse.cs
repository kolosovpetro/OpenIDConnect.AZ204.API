using System.Collections.Generic;

namespace OpenIDConnect.AZ204.OperationsAgent.Models;

public class AgentMemberHistoryResponse
{
    public Member MemberInfo { get; set; }
    public List<AgentMemberHistory> AgentMemberHistory { get; set; }
}