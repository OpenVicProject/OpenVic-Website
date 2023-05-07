using Newtonsoft.Json;

namespace GameDevPortal.WebAPI.Models;

public record ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
