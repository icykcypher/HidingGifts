namespace HidingGifts.Domain.Dtos
{
    /// <summary>
    /// Represents a family member who is associated with gifts and hiding places.
    /// </summary>
    public class FamilyMember
    {
        /// <summary>
        /// Gets or sets the unique identifier for the family member.
        /// </summary>
        public required int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the family member.
        /// </summary>
        public required string Name { get; set; } = string.Empty;
    }
}