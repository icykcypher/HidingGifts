using Newtonsoft.Json;
using HidingGifts.Domain.Dtos;
using HidingGifts.Domain.Static;

namespace HidingGifts.Infrastructure.Services.DataAccessing
{
    public class DataAccessService
    {
        public IEnumerable<FamilyMember> FamilyMembers { get; private set; } = [];
        public IEnumerable<HidingPlace> HidingPlaces { get; private set; } = [];
        public IEnumerable<Gift> Gifts { get; private set; } = [];

        private DataAccessService(IEnumerable<FamilyMember> familyMembers, IEnumerable<HidingPlace> hidingPlaces, IEnumerable<Gift> gifts)
        {
            this.FamilyMembers = familyMembers;
            this.HidingPlaces = hidingPlaces;
            this.Gifts = gifts;
        }

        public static DataAccessService Create()
        {
            var famMembers = GetFamilyMembers();
            var hidingPlaces = GetHidingPlaces();
            var gifts = GetGifts();
            return new DataAccessService(famMembers, hidingPlaces, gifts);
        }

        private static IEnumerable<FamilyMember> GetFamilyMembers()
            => JsonConvert.DeserializeObject<IEnumerable<FamilyMember>>(File.ReadAllText(StaticClaims.FamilyMembers)) 
                ?? throw new IOException($"File {StaticClaims.FamilyMembers} was not found!");

        private static IEnumerable<HidingPlace> GetHidingPlaces()
            => JsonConvert.DeserializeObject<IEnumerable<HidingPlace>>(File.ReadAllText(StaticClaims.HidingPlaces))
                ?? throw new IOException($"File {StaticClaims.HidingPlaces} was not found!");

        private static IEnumerable<Gift> GetGifts()
            => JsonConvert.DeserializeObject<IEnumerable<Gift>>(File.ReadAllText(StaticClaims.Gifts))
                ?? throw new IOException($"File {StaticClaims.Gifts} was not found!");
    }
}