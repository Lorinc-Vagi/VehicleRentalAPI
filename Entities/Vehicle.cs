namespace VehicleRentalAPI.Entities
{
    public class Vehicle
    {
        public int Id { set;get; }
        public string Model { set;get; }
        public string LicencePlate { set; get; }
        public decimal DailyRate {  set; get; }
        public bool Avalible { set; get; }
    }
}
