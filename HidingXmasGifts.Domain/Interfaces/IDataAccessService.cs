using HidingGifts.Domain.Dtos;

namespace HidingGifts.Domain.Interfaces
{
    /// <summary>
    /// Provides access to data related to family members, gifts, and hiding places, 
    /// and supports saving hiding assignments.
    /// </summary>
    public interface IDataAccessService
    {
        /// <summary>
        /// Gets the collection of family members.
        /// </summary>
        IEnumerable<FamilyMember> FamilyMembers { get; }

        /// <summary>
        /// Gets the collection of gifts.
        /// </summary>
        IEnumerable<Gift> Gifts { get; }

        /// <summary>
        /// Gets the collection of hiding places.
        /// </summary>
        IEnumerable<HidingPlace> HidingPlaces { get; }

        /// <summary>
        /// Saves the results of the hiding assignments.
        /// </summary>
        /// <param name="dict">
        /// A dictionary mapping each gift to its assigned hiding place.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="dict"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the <paramref name="dict"/> contains no items.
        /// </exception>
        void SaveHidingResults(Dictionary<Gift, HidingPlace> dict);
    }
}