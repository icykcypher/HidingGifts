namespace HidingGifts.Domain.Dtos
{
    /// <summary>
    /// Represents a hiding place where a gift can be hidden.
    /// </summary>
    public class HidingPlace
    {
        /// <summary>
        /// Gets or sets the unique identifier for the hiding place.
        /// </summary>
        public required int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the hiding place.
        /// </summary>
        public required string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the hiding place.
        /// </summary>
        public required string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the gift hidden in this hiding place, if any.
        /// </summary>
        public Gift? Gift { get; set; }

        /// <summary>
        /// Gets or sets the probabilities that specific family members will find the gift in this hiding place.
        /// The key represents the family member's ID, and the value represents the probability (a double value between 0 and 1).
        /// </summary>
        public required Dictionary<int, double> ProbabilitiesToFind { get; set; } = [];
    }
}