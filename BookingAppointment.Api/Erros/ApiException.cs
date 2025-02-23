namespace BookingAppointment.Api.Erros
{
    public class ApiException : ApiResponse
    {
        public string? Details { get; set; }
        public ApiException(int StatusCode, string? Message=null ,string? details = null): base(StatusCode , Message)
        {
            Details = details;
        }
    }
}
