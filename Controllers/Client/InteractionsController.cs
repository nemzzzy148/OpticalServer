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
    string JsonPath = "Data.json";
    // counter vars
    private ServerData serverData;
    // light vars
    public bool light = false;

    public InteractionsController()
    {
        if (System.IO.File.Exists(JsonPath))
        {
            var json = System.IO.File.ReadAllText(JsonPath);
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
        System.IO.File.WriteAllText(JsonPath,json);
    }

    // light functionality

    [HttpGet("lightstate")]
    public IActionResult LightState()
    {
        RuntimeFunctions.Request("light state, current: " + light);
        return Ok(light);
    }
    [HttpGet("lightswitch")]
    public IActionResult LightSwitch()
    {
        light = !light;
        RuntimeFunctions.Request("light switch, new: " + light);
        return Ok(light);
    }
}