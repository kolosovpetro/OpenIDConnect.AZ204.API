using System;
using System.Collections.Generic;

namespace OpenIDConnect.AZ204.OperationsAgent.Models;

public class AgentMemberHistory
{
    public string AgentName { get; set; }
    public string ReasonForCall { get; set; }
    public DateTime InteractionDate { get; set; }

    public static List<AgentMemberHistory> AgentMemberHistories => GetAgentMemberHistories();

    private static List<AgentMemberHistory> GetAgentMemberHistories()
    {
        var agentMemberHistoryList = new List<AgentMemberHistory>
        {
            new AgentMemberHistory
            {
                AgentName = "Andria",
                ReasonForCall = "Issue with the billing",
                InteractionDate = DateTime.Now
            },
            new AgentMemberHistory
            {
                AgentName = "Robert",
                ReasonForCall = "Issue with the billing",
                InteractionDate = DateTime.Now
            }
        };

        return agentMemberHistoryList;
    }
}