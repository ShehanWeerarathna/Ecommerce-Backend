using System.Text.Json;
using Ecommerce_Backend.DTOs.RequestHelpers;

namespace Ecommerce_Backend.Extentions;

public static class HttpExtentions
{
    public static void AddPaginationHeader(this HttpResponse response, MetaData metaData)
    {
        var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

        response.Headers.Add("Pagination", JsonSerializer.Serialize(metaData, options));
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }

}