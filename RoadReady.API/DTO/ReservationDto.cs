public class ReservationDto
{
    public int UserId { get; set; }
    public int ReservationId { get; set; }
    public int VehicleId { get; set; }
    public string? VehicleModel { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime DropOffDate { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsCancelled { get; set; }
}
