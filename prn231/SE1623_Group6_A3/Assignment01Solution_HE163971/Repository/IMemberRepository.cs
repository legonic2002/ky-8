using Assignment01Solution_HE163971.Models;

namespace Lab01_ASP.NETCoreWebAPI.Repository
{
    public interface IMemberRepository
    {
        void SaveMember(Member p);
        Member GetMemberById(int id);
        void DeleteMember(Member p);
        void UpdateMember(Member p);
      
        List<Member> GetMembers();
    }
}
