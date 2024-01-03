using Assignment01Solution_HE163971.DAO;
using Assignment01Solution_HE163971.Models;
using Lab01_ASP.NETCoreWebAPI.Repository;

namespace Assignment01Solution_HE163971.Repository
{
    public class MemberRepository : IMemberRepository
    {
      
        
        public List<Member> GetMembers() => MemberDAO.GetMembers();

        public Member GetMemberById(int id) => MemberDAO.FindMemberById(id);

        public void SaveMember(Member p) => MemberDAO.SaveMember(p);

        public void DeleteMember(Member p) => MemberDAO.DeleteMember(p);

        public void UpdateMember(Member p) => MemberDAO.UpdateMember(p);

        
    }
}
