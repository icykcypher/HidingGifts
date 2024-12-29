using HidingGifts.Domain.Dtos;
using HidingGifts.Domain.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace HidingGifts.Infrastructure.Services.GiftHiding
{
    /// <summary>
    /// Service for assigning gifts to hiding places based on probabilities.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="GiftHidingService"/> class.
    /// </remarks>
    /// <param name="dataAccessService">The data access service used to retrieve and store data.</param>
    public class GiftHidingService(IDataAccessService dataAccessService) : IGiftHidingService
    {
        /// <summary>
        /// Data access service providing family members, gifts, and hiding places.
        /// </summary>
        [NotNull]
        private readonly IDataAccessService _dataAccessService = dataAccessService ?? throw new ArgumentNullException(nameof(dataAccessService));

        /// <summary>
        /// Assigns each gift to the best available hiding place based on probabilities.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if there are not enough unique hiding places for all gifts.
        /// </exception>
        public void HideGifts()
        {
            var assignments = new Dictionary<Gift, HidingPlace>();
            var usedHidingPlaces = new HashSet<int>();

            foreach (var gift in _dataAccessService.Gifts)
            {
                HidingPlace? bestHidingPlace = null;
                var minProbability = double.MaxValue;

                foreach (var hidingPlace in _dataAccessService.HidingPlaces)
                {
                    if (usedHidingPlaces.Contains(hidingPlace.Id)) continue;

                    var probabilityToFind = hidingPlace.ProbabilitiesToFind.ContainsKey(gift.FamilyMember.Id)
                        ? hidingPlace.ProbabilitiesToFind[gift.FamilyMember.Id]
                        : 1.0; // Default probability if no specific value is defined

                    if (probabilityToFind < minProbability)
                    {
                        minProbability = probabilityToFind;
                        bestHidingPlace = hidingPlace;
                    }
                }

                if (bestHidingPlace != null)
                {
                    assignments[gift] = bestHidingPlace;
                    usedHidingPlaces.Add(bestHidingPlace.Id);
                }
                else
                {
                    throw new InvalidOperationException("Not enough unique hiding places for all gifts.");
                }
            }

            _dataAccessService.SaveHidingResults(assignments);
        }
    }

}