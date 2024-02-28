
namespace Models.Dtos.ResponseDto
{
    public record ProductDetailDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int Stock { get; init; }
        public decimal Price { get; init; }
        public string CategoryName { get; init; }
    }
}
