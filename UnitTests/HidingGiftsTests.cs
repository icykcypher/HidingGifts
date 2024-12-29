using Moq;
using HidingGifts.Domain.Dtos;
using HidingGifts.Domain.Interfaces;
using HidingGifts.Infrastructure.Services.GiftHiding;

namespace UnitTests
{
    public class HidingGiftsTests
    {
        private readonly Mock<IDataAccessService> _dataAccessMock;
        private readonly GiftHidingService _service;

        public HidingGiftsTests()
        {
            _dataAccessMock = new Mock<IDataAccessService>();
            _service = new GiftHidingService(_dataAccessMock.Object);
        }

        private static IEnumerable<HidingPlace> GetMockHidingPlaces() =>
            new List<HidingPlace>
            {
            new HidingPlace
            {
                Id = 1, Name = "Closet", Description = "Master bedroom closet",
                ProbabilitiesToFind = new Dictionary<int, double> { { 1, 0.2 }, { 2, 0.5 } }
            },
            new HidingPlace
            {
                Id = 2, Name = "Attic", Description = "Old attic",
                ProbabilitiesToFind = new Dictionary<int, double> { { 1, 0.8 }, { 2, 0.1 } }
            }
            };

        private static IEnumerable<Gift> GetMockGifts() =>
            new List<Gift>
            {
            new() { Id = 1, Name = "Toy", FamilyMember = new FamilyMember { Id = 1, Name = "Alice" } },
            new() { Id = 2, Name = "Book", FamilyMember = new FamilyMember { Id = 2, Name = "Bob" } }
            };

        [Fact]
        public void HideGifts_ShouldAssignEachGiftToUniqueHidingPlace()
        {
            _dataAccessMock.Setup(m => m.Gifts).Returns(GetMockGifts());
            _dataAccessMock.Setup(m => m.HidingPlaces).Returns(GetMockHidingPlaces());

            _service.HideGifts();

            _dataAccessMock.Verify(m => m.SaveHidingResults(It.Is<Dictionary<Gift, HidingPlace>>(dict =>
                dict.Count == 2 &&
                dict.Any(pair => pair.Key.Id == 1 && pair.Value.Id == 1) &&
                dict.Any(pair => pair.Key.Id == 2 && pair.Value.Id == 2)
            )), Times.Once);
        }

        [Fact]
        public void HideGifts_ShouldThrowException_WhenNotEnoughHidingPlaces()
        {
            // Arrange
            _dataAccessMock.Setup(m => m.Gifts).Returns(GetMockGifts());
            _dataAccessMock.Setup(m => m.HidingPlaces).Returns(new List<HidingPlace>
        {
            new HidingPlace
            {
                Id = 1, Name = "Closet", Description = "Master bedroom closet",
                ProbabilitiesToFind = new Dictionary<int, double> { { 1, 0.2 } }
            }
        });

            Assert.Throws<InvalidOperationException>(() => _service.HideGifts());
        }

        [Fact]
        public void HideGifts_ShouldDefaultProbabilityToOne_WhenFamilyMemberNotInProbabilities()
        {
            _dataAccessMock.Setup(m => m.Gifts).Returns(GetMockGifts());
            _dataAccessMock.Setup(m => m.HidingPlaces).Returns(new List<HidingPlace>
        {
            new HidingPlace
            {
                Id = 1, Name = "Closet", Description = "Master bedroom closet",
                ProbabilitiesToFind = new Dictionary<int, double> { { 2, 0.5 } } // Missing Alice
            },
            new HidingPlace
            {
                Id = 2, Name = "Attic", Description = "Old attic",
                ProbabilitiesToFind = new Dictionary<int, double> { { 1, 0.8 }, { 2, 0.1 } }
            }
        });

            _service.HideGifts();

            _dataAccessMock.Verify(m => m.SaveHidingResults(It.Is<Dictionary<Gift, HidingPlace>>(dict =>
                dict[GetMockGifts().ElementAt(0)].Id == 2 &&
                dict[GetMockGifts().ElementAt(1)].Id == 1
            )));
        }
    }
}