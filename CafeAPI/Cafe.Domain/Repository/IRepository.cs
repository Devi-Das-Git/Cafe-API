

using Microsoft.VisualBasic.FileIO;

namespace Cafe.Domain.Repository
{
    public interface ICafeRepository 
    {
        int Add(Cafe.Domain.Models.Cafe cafe);

        int AddEmployee(Cafe.Domain.Models.Employee Employee);
        int RemoveCafe(Guid Id);

        int RemoveEmployee(Guid Id);

        int UpdateEmployee(Cafe.Domain.Models.Employee Employee);

        int UpdateCafe(Cafe.Domain.Models.Cafe cafe);
    }

}
