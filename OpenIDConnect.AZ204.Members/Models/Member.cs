using System;
using System.Collections.Generic;

namespace OpenIDConnect.AZ204.Members.Models;

public class Member
{
    public Guid MemberId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Address { get; }

    public Member(Guid memberId, string firstName, string lastName, string address)
    {
        MemberId = memberId;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
    }

    public static IEnumerable<Member> MembersList => GenerateMembers();

    private static IEnumerable<Member> GenerateMembers()
    {
        var firstId = Guid.Parse("e77cf2cb-3f3a-4f0b-ac5a-90a3263d075a");
        var secondId = Guid.Parse("fd3c67c5-c6ff-4a5d-a166-98ece1b7752b");

        var membersList = new List<Member>
        {
            new Member(firstId, "Steve", "Robert", "1 Infinite Loop, Cupertino, California"),
            new Member(secondId, "Adam", "Taylor", "5 Cherry Springs,Redmond, Washington, United States")
        };

        return membersList;
    }
}