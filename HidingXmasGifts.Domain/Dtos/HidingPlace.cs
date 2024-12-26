﻿namespace HidingXMasGifts.Domain.Dtos
{
    public class HidingPlace
    {
        public required int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Description { get; set; } = string.Empty;
        public Gift? Gift { get; set; }
        public required Dictionary<FamilyMember, double> ProbabillitiesToFind { get; set; } = [];
    }
}