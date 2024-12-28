using HidingGifts.Infrastructure.Services.GiftHiding;
using HidingGifts.Infrastructure.Services.DataOperating;

namespace HidingGifts.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            var service = new GiftHidingService(DataAccessService.Create());

            service.HideGifts();
        }
    }
}