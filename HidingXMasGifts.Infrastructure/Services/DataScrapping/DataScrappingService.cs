using Newtonsoft.Json;
using HidingGifts.Domain.Dtos;
using HidingGifts.Domain.Static;

namespace HidingGifts.Infrastructure.Services.DataScrapping
{
    public class DataScrappingService
    {
        public IEnumerable<FamilyMember> FamilyMembers { get; private set; } = [];
        public IEnumerable<HidingPlace> HidingPlaces { get; private set; } = [];
        public IEnumerable<Gift> Gifts { get; private set; } = [];

        public IEnumerable<FamilyMember> GetFamilyMembers()
            => JsonConvert.DeserializeObject<IEnumerable<FamilyMember>>(File.ReadAllText(StaticClaims.FamilyMembers)) 
                ?? throw new IOException($"File {StaticClaims.FamilyMembers} was not found!");


    }
}