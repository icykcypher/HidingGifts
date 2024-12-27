using HidingGifts.Infrastructure.Services.DataAccessing;

namespace HidingGifts.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            var service = DataAccessService.Create();

            foreach (var member in service.FamilyMembers)
            {
                Console.WriteLine(member.Name);
            }
        }
    }
}