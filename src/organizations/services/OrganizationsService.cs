using System;
using System.Threading.Tasks;
using DotNetAPI.Organizations.Entities;
using DotNetAPI.Organizations.Repositories;
using MongoDB.Driver;

namespace DotNetAPI.Organizations.Services
{
    public class OrganizationService
    {
        private readonly OrganizationRepository _organizationRepository;

        public OrganizationService(OrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
        }

        public async Task<Organization> CreateOrganization(string organizationName, string apiKey)
        {
            var organization = new Organization(organizationName, apiKey);
            await _organizationRepository.InsertOrganization(organization);
            return organization;
        }

        public async Task<Organization> GetOrganization(string apiKey, Guid organizationId)
        {
            var organization = await _organizationRepository.GetOrganizationById(organizationId);
            if (organization == null || organization.ApiKey != apiKey)
            {
                throw new UnauthorizedAccessException("Invalid API key or organization ID.");
            }
            return organization;
        }

        public async Task<Organization> UpdateBannedList(string apiKey, Guid organizationId, List<Guid> bannedList)
        {
            var organization = await GetOrganization(apiKey, organizationId);
            organization.BannedList = bannedList;
            organization.UpdatedAt = DateTime.UtcNow;
            await _organizationRepository.UpdateOrganization(organization);
            return organization;
        }

        public async Task DeleteOrganization(string apiKey, Guid organizationId)
        {
            var organization = await GetOrganization(apiKey, organizationId);
            organization.DeletedAt = DateTime.UtcNow;
            await _organizationRepository.UpdateOrganization(organization);
        }
    }
}
