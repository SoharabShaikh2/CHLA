using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface IOrganizationRepository<T>
    {
        Task<OrganizationDto> GetOrganization(int organizationId);
        Task<List<OrganizationDto>> GetOrganizationList();
        Task<List<OrganizationUserDto>> GetOrganizationUsers(int organizationId);
    }
}
