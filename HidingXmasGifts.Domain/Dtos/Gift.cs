namespace HidingGifts.Domain.Dtos
{
    /// <summary>
    /// Represents a gift that can be assigned to a family member and hidden in a specific place.
    /// </summary>
    public class Gift
    {
        /// <summary>
        /// Gets or sets the unique identifier for the gift.
        /// </summary>
        public required int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the gift.
        /// </summary>
        public required string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the gift.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the family member associated with the gift.
        /// </summary>
        public required FamilyMember FamilyMember { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current gift.
        /// </summary>
        /// <param name="obj">The object to compare with the current gift.</param>
        /// <returns>
        /// <c>true</c> if the specified object is a <see cref="Gift"/> and has the same <see cref="Id"/>; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) => obj is Gift other && Id == other.Id;

        /// <summary>
        /// Serves as the default hash function for the <see cref="Gift"/> class.
        /// </summary>
        /// <returns>A hash code for the current gift, based on its <see cref="Id"/>.</returns>
        public override int GetHashCode() => Id.GetHashCode();
    }

}