namespace AppServiceBus.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{        
    public ClientController()
    {                
    }

    [HttpPost]
    public async Task<IActionResult> Post(
            [FromBody]Client client,
            [FromServices] ISendMessageQueue sendMessage
        )
    {
        bool ok = await sendMessage.Send(client, "client");
        return Ok(client);
    }
}