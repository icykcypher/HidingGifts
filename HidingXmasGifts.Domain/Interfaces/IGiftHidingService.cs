namespace HidingGifts.Domain.Interfaces
{
    /// <summary>
    /// Defines a service for assigning gifts to hiding places based on specific criteria.
    /// </summary>
    public interface IGiftHidingService
    {
        /// <summary>
        /// Assigns each gift to an appropriate hiding place and performs the necessary data saving operations using IDataAccessService.
        /// </summary>
        /// <remarks>
        /// The implementation should ensure that:
        /// <list type="bullet">
        /// <item>Each gift is assigned to a unique hiding place.</item>
        /// <item>Hiding places are selected based on defined probabilities or criteria.</item>
        /// <item>Results are saved to the data store after the assignments are made.</item>
        /// </list>
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// Thrown if there are not enough hiding places for all gifts.
        /// </exception>
        void HideGifts();
    }
}