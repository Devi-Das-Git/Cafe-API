namespace CafeAPI.Application.Queries
{
    public interface ICafeQueries
    {
        Task<IEnumerable<Cafe>> GetCafeByLocationAsync(string location);
        Task<IEnumerable<Employee>> GetEmployeesByCafe(string cafe);
    }
}
