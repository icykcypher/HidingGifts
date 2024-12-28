using Newtonsoft.Json;
using HidingGifts.Domain.Dtos;
using HidingGifts.Domain.Static;

namespace HidingGifts.Infrastructure.Services.DataOperating
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
            => JsonConvert.DeserializeObject<IEnumerable<FamilyMember>>(File.ReadAllText(StaticPaths.FamilyMembers)) 
                ?? throw new IOException($"File {StaticPaths.FamilyMembers} was not found!");

        private static IEnumerable<HidingPlace> GetHidingPlaces()
            => JsonConvert.DeserializeObject<IEnumerable<HidingPlace>>(File.ReadAllText(StaticPaths.HidingPlaces))
                ?? throw new IOException($"File {StaticPaths.HidingPlaces} was not found!");

        private static IEnumerable<Gift> GetGifts()
            => JsonConvert.DeserializeObject<IEnumerable<Gift>>(File.ReadAllText(StaticPaths.Gifts))
                ?? throw new IOException($"File {StaticPaths.Gifts} was not found!");

        public void SaveHidingResults(Dictionary<Gift, HidingPlace> dict)
        {
            using (var sw = new StreamWriter(StaticPaths.Results))
            using (var writer = new JsonTextWriter(sw))
            {
                var serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented
                };

                var serializableAssignments = dict.Select(x => new
                {
                    Gift = new
                    {
                        x.Key.Id,
                        x.Key.Name,
                        x.Key.Description,
                        FamilyMember = new
                        {
                            x.Key.FamilyMember.Id,
                            x.Key.FamilyMember.Name
                        }
                    },
                    HidingPlace = new
                    {
                        x.Value.Id,
                        x.Value.Name,
                        x.Value.Description,
                        ProbabilitiesToFind = x.Value.ProbabilitiesToFind
                    }
                }).ToList();

                serializer.Serialize(writer, serializableAssignments);
            }
        }
    }
}