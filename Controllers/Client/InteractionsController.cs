using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using OpticalServer.Models;
using System.Text.Json;
using System.IO;
using OpticalServer.Functions;

[ApiController]
[Route("api/[controller]")]
public class InteractionsController : ControllerBase
{

    private ServerData serverData;
    public InteractionsController()
    {
        if (System.IO.File.Exists(Configurations.JsonDataPath))
        {
            var json = System.IO.File.ReadAllText(Configurations.JsonDataPath);
            try
            {
                serverData = JsonSerializer.Deserialize<ServerData>(json);
            }
            catch
            {
                RuntimeFunctions.Request("Json empty!", true);
            }
        }
        else
        {
            serverData = new ServerData{ Counter = 0 };
            Save();
        }
    }

    // counter functionality

    [HttpGet("upcounter")]
    public IActionResult UpCounter()
    {
        if (serverData == null)
        {
            RuntimeFunctions.Request("counter data isn't loaded!", true);
            return Ok(0);
        }
        serverData.Counter++;
        Save();
        RuntimeFunctions.Request("counter went up, new: " + serverData.Counter);
        return Ok(serverData.Counter);
    }
    [HttpGet("getcounter")]
    public IActionResult GetCounter()
    {
        if (serverData == null)
        {
            RuntimeFunctions.Request("counter data isn't loaded!", true);
            return Ok(0);
        }
        RuntimeFunctions.Request("get counter request, current: " + serverData.Counter);
        return Ok(serverData.Counter);
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(serverData, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        System.IO.File.WriteAllText(Configurations.JsonDataPath,json);
    }

    // light functionality

    [HttpGet("lightstate")]
    public IActionResult LightState()
    {
        RuntimeFunctions.Request("light state, current: " + RuntimeFunctions.LightSwitchState);
        return Ok(RuntimeFunctions.LightSwitchState);
    }
    [HttpGet("lightswitch")]
    public IActionResult LightSwitch()
    {
        RuntimeFunctions.LightSwitchState = !RuntimeFunctions.LightSwitchState;
        RuntimeFunctions.Request("light switch, new: " + RuntimeFunctions.LightSwitchState);
        return Ok(RuntimeFunctions.LightSwitchState);
    }
}