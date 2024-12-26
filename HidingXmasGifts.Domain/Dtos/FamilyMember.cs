namespace HidingGifts.Domain.Dtos
{
    public class FamilyMember
    {
        public required int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
    }
}