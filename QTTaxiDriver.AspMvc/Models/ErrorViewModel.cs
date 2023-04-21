namespace QTTaxiDriver.AspMvc.Models
{
    public partial class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
