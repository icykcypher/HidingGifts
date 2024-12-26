namespace HidingXMasGifts.Domain.Dtos
{
    public class Gift
    {
        public required int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}