using Assignment01Solution_HE163971.Models;

namespace Assignment01Solution_HE163971.DAO
{
    public class MemberDAO
    {
        public static List<Member> GetMembers() {
            var list = new List<Member>();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.Members.ToList();

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return list;
        }

        public static Member FindMemberById(int proId)
        {
            var list = new Member();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.Members.SingleOrDefault(x => x.MemberId ==proId);

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return list;
        }
        public static void SaveMember(Member p)
        {
            
            try
            {
                using (var context = new MyDbContext())
                {
                   context.Members.Add(p);
                    context.SaveChanges();

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
           
        }
        public static void UpdateMember(Member p)
        {

            try
            {
                using (var context = new MyDbContext())
                {
                    context.Members.Update(p);

                    context.SaveChanges();

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
        public static void DeleteMember(Member p)
        {
             
            try
            {
                using (var context = new MyDbContext())
                {
                   var list = context.Members.SingleOrDefault(x => x.MemberId == p.MemberId);
                    context.Members.Remove(list);
                    context.SaveChanges();
                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }


    }
}
