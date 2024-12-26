using HidingGifts.Infrastructure.Services.DataScrapping;

namespace HidingGifts.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            var service = new DataScrappingService();
            var list = service.GetFamilyMembers();

            foreach (var member in list)
            {
                Console.WriteLine(member.Name);
            }
        }
    }
}