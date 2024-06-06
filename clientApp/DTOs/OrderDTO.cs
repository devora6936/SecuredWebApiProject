namespace DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public double? OrderSum { get; set; }

        public int? UserId { get; set; }

        public ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();

    }
}
