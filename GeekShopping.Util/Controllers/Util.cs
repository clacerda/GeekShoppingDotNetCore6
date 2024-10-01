using GeekShopping.Util.Models.ShippingCost;
using GeekShopping.Util.Models.ShippingRequest;
using GeekShopping.Util.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GeekShopping.Util.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class Util : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IReadApiExternal _readApiExternal;

        public Util(IConfiguration configuration, IReadApiExternal readApiExternal)
        {
            _configuration = configuration;
            _readApiExternal = readApiExternal;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateShipment([FromBody] ShipmentRequest request)
        {
            var token = _configuration["ApiSettings:Token"];

            var url = _configuration["ApiSettings:BasePath"];

            var response = _readApiExternal.ConfigureHttpClient(url, token, request).Result;

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var shipmentList = JsonSerializer.Deserialize<List<ShippingCost>>(jsonResponse);

                return Ok(shipmentList);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, errorContent);
            }
        }
    }
}
