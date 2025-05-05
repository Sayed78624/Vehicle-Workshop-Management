namespace VehicleWorkShop.Utilities
{
    public class ResponseStatus
    {
        public int StatusCode { get; set; }
        public string SuccessMessage { get; set; }
        public bool Success
        {
            get
            {
                return StatusCode == 1;
            }
        }
    }
}
