using Microsoft.AspNetCore.Mvc;
using DotNetAPI.Organizations.Services;
using DotNetAPI.Organizations.DTOs;

namespace DotNetAPI.Organizations
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationService _organizationService;

        public OrganizationController(OrganizationService organizationService)
        {
            _organizationService = organizationService ?? throw new ArgumentNullException(nameof(organizationService));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationDto dto)
        {
            var organization = await _organizationService.CreateOrganization(dto.OrganizationName, dto.ApiKey);
            return Ok(organization);
        }

        [HttpGet("{organizationId}")]
        public async Task<IActionResult> GetOrganization([FromHeader] string apiKey, [FromRoute] Guid organizationId)
        {
            var organization = await _organizationService.GetOrganization(apiKey, organizationId);
            return Ok(organization);
        }

        [HttpPut("{organizationId}/banned-list")]
        public async Task<IActionResult> UpdateBannedList([FromHeader] string apiKey, [FromRoute] Guid organizationId, [FromBody] UpdateBannedListDto dto)
        {
            var organization = await _organizationService.UpdateBannedList(apiKey, organizationId, dto.BannedList);
            return Ok(organization);
        }

        [HttpDelete("{organizationId}")]
        public async Task<IActionResult> DeleteOrganization([FromHeader] string apiKey, [FromRoute] Guid organizationId)
        {
            await _organizationService.DeleteOrganization(apiKey, organizationId);
            return NoContent();
        }
    }
}
