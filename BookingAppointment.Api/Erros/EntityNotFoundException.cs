namespace BookingAppointment.Api.Erros
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, object id)
            : base($"{entityName} with ID {id} was not found")
        {
        }
    }
}
