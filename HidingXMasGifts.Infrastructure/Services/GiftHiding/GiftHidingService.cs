using HidingGifts.Infrastructure.Services.DataAccessing;

namespace HidingGifts.Infrastructure.Services.GiftHiding
{
    public class GiftHidingService(DataAccessService dataAccessService)
    {
        private DataAccessService _dataAccessService = dataAccessService;


    }
}