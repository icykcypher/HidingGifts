namespace HidingGifts.Domain.Static
{
    /// <summary>
    /// Provides static file paths for application data and results.
    /// </summary>
    public static class StaticPaths
    {
        /// <summary>
        /// Gets the file path to the JSON file containing family member data.
        /// </summary>
        public static string FamilyMembers => @"..\..\..\..\src\FamilyMembers.json";

        /// <summary>
        /// Gets the file path to the JSON file containing gift data.
        /// </summary>
        public static string Gifts => @"..\..\..\..\src\Gifts.json";

        /// <summary>
        /// Gets the file path to the JSON file containing hiding place data.
        /// </summary>
        public static string HidingPlaces => @"..\..\..\..\src\HidingPlaces.json";

        /// <summary>
        /// Gets the file path to the JSON file where hiding results will be saved.
        /// </summary>
        public static string Results => @"..\..\..\..\src\HidingResults.json";
    }

}