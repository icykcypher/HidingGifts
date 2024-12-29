using Newtonsoft.Json;
using HidingGifts.Domain.Dtos;
using HidingGifts.Domain.Static;
using HidingGifts.Domain.Interfaces;

namespace HidingGifts.Infrastructure.Services.DataOperating
{
    /// <summary>
    /// Provides data access functionality for family members, hiding places, and gifts.
    /// </summary>
    public class DataAccessService : IDataAccessService
    {
        /// <summary>
        /// Gets the collection of family members.
        /// </summary>
        public IEnumerable<FamilyMember> FamilyMembers { get; private set; } = [];

        /// <summary>
        /// Gets the collection of hiding places.
        /// </summary>
        public IEnumerable<HidingPlace> HidingPlaces { get; private set; } = [];

        /// <summary>
        /// Gets the collection of gifts.
        /// </summary>
        public IEnumerable<Gift> Gifts { get; private set; } = [];

        /// <summary>
        /// Private constructor to initialize the service with the given data.
        /// </summary>
        /// <param name="familyMembers">Collection of family members.</param>
        /// <param name="hidingPlaces">Collection of hiding places.</param>
        /// <param name="gifts">Collection of gifts.</param>
        private DataAccessService(IEnumerable<FamilyMember> familyMembers, IEnumerable<HidingPlace> hidingPlaces, IEnumerable<Gift> gifts)
        {
            FamilyMembers = familyMembers;
            HidingPlaces = hidingPlaces;
            Gifts = gifts;
        }

        /// <summary>
        /// Creates an instance of <see cref="DataAccessService"/> by loading data from JSON files.
        /// </summary>
        /// <returns>An initialized instance of <see cref="DataAccessService"/>.</returns>
        /// <remarks>Exits the application with an error code if data loading fails.</remarks>
        public static DataAccessService Create()
        {
            try
            {
                var famMembers = GetFamilyMembers();
                var hidingPlaces = GetHidingPlaces();
                var gifts = GetGifts();

                return new DataAccessService(famMembers, hidingPlaces, gifts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Environment.Exit(1);
                return null; // This line is never reached but is required for method signature compliance.
            }
        }

        /// <summary>
        /// Loads family member data from the corresponding JSON file.
        /// </summary>
        /// <returns>Collection of <see cref="FamilyMember"/>.</returns>
        /// <exception cref="IOException">Thrown if the JSON file cannot be found or read.</exception>
        private static IEnumerable<FamilyMember> GetFamilyMembers()
            => JsonConvert.DeserializeObject<IEnumerable<FamilyMember>>(File.ReadAllText(StaticPaths.FamilyMembers))
                ?? throw new IOException($"File {StaticPaths.FamilyMembers} was not found!");

        /// <summary>
        /// Loads hiding place data from the corresponding JSON file.
        /// </summary>
        /// <returns>Collection of <see cref="HidingPlace"/>.</returns>
        /// <exception cref="IOException">Thrown if the JSON file cannot be found or read.</exception>
        private static IEnumerable<HidingPlace> GetHidingPlaces()
            => JsonConvert.DeserializeObject<IEnumerable<HidingPlace>>(File.ReadAllText(StaticPaths.HidingPlaces))
                ?? throw new IOException($"File {StaticPaths.HidingPlaces} was not found!");

        /// <summary>
        /// Loads gift data from the corresponding JSON file.
        /// </summary>
        /// <returns>Collection of <see cref="Gift"/>.</returns>
        /// <exception cref="IOException">Thrown if the JSON file cannot be found or read.</exception>
        private static IEnumerable<Gift> GetGifts()
            => JsonConvert.DeserializeObject<IEnumerable<Gift>>(File.ReadAllText(StaticPaths.Gifts))
                ?? throw new IOException($"File {StaticPaths.Gifts} was not found!");

        /// <summary>
        /// Saves hiding results to a JSON file.
        /// </summary>
        /// <param name="dict">A dictionary mapping gifts to hiding places.</param>
        /// <exception cref="ArgumentNullException">Thrown if the dictionary is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the dictionary is empty.</exception>
        /// <remarks>Exits the application with an error code if saving fails.</remarks>
        public void SaveHidingResults(Dictionary<Gift, HidingPlace> dict)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(dict);

                if (dict.Count <= 0)
                    throw new ArgumentException("Hiding assignments must contain at least one item.", nameof(dict));

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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Environment.Exit(1);
            }
        }
    }
}