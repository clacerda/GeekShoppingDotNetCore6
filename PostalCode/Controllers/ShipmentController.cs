using Microsoft.AspNetCore.Mvc; 
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ShipmentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateShipment([FromBody] ShipmentRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var url = "https://sandbox.melhorenvio.com.br/api/v2/me/shipment/calculate";

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiI5NTYiLCJqdGkiOiIwYjg0YjZkYWYzMzNjMDU2MjFjZjlhNDAzNjBlNzcxMWI3N2NjNzZmNTVhZjNjYTEwNmUyZWViMWMwMmY5ZTU2YTZhMzkwMWYyYjI0NWI2NCIsImlhdCI6MTcyNzM1MDg5OS42NjM0MTYsIm5iZiI6MTcyNzM1MDg5OS42NjM0MTksImV4cCI6MTc1ODg4Njg5OS42NTYwNCwic3ViIjoiOWQxODlkMGItYzRlMy00MjMxLWE4N2EtMWFjMGYyYmQxMmM3Iiwic2NvcGVzIjpbInNoaXBwaW5nLWNhbGN1bGF0ZSJdfQ.bEnBUh24r_MhXLnACqBYgcJRdaJzHYmSrPpJWYxkjbFDu22n78U5otr0AbleGH4_D3s7G20CkSNgyR89Iopb8DWRbUkLu-ZWj9AcgwONlPviTfvbnI2znLufLIIsla5oTJW6BQMBf3fu4MMMEbxf4eFKGIgo8JPpzR0s5bJESof6JgbxtaAQGkE7nh0nBjC1J88y3HY7cbZNQEAXO3As-iHm2ysWiiaAL9pmmUS5oYRfOX9ASgphHKKBLKtc92H4ohbc_dcm3qBbTgtgo3lbIaSDi9-D9O27A87WXkljOG_iuBX5MfoCWhaeH5tfPdSYIYy3uNp-mJeuujDgERywuqxCj-HpFu9T60bcxk3o_QYTuJR4qCw7H2B6xpfif-bq0jmzR46mYca8WzRoqBePA7IbGqyn3ZFG7pWaaHjXSrjuUpGitqYpwcR6sKtxQl8bdDb0FFfzu0z4b2KKQHM9dMHGUj2ueoacFyc_1F-4Jfq5yfR1C-_w7rNNYdt71jJdIcrsqarxuiintx501yMcW2NniCLithmVqh-dJvY-np8ox7rUUIeCHMnnIMJFiKLtQbJIRrK8EfhKu8MSiTJOWjTo2kX7TRPZqZcPv9T3oPmLR5LK-WY7tIsZNd404P1M7KCjogcqACtmqFeYqUv5xnyfuSKp2l87SWSs9S1Qlp0");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("AplicacaoClaudio/1.0");

            var jsonRequest = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return Ok(jsonResponse); // Retorne a resposta de sucesso
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync(); // Ler a mensagem de erro da resposta
                return StatusCode((int)response.StatusCode, errorContent); // Retornar a mensagem de erro
            }
        }
    }

}
