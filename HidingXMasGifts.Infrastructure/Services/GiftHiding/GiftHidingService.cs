using HidingGifts.Domain.Dtos;
using HidingGifts.Infrastructure.Services.DataOperating;

namespace HidingGifts.Infrastructure.Services.GiftHiding
{
    public class GiftHidingService(DataAccessService dataAccessService)
    {
        private readonly DataAccessService _dataAccessService = dataAccessService;

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
                        : 1.0;

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
                else throw new InvalidOperationException("Not enough unique hiding places for all gifts.");
            }

            _dataAccessService.SaveHidingResults(assignments);
        }
    }
}