using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WP.Infrastructures.Core;

//[Serializable]
//public sealed class AppResult
//{
//    public AppResult()
//    { }

//    public AppResult(ProblemDetails problemDetails) => ProblemDetails = problemDetails;

//    public bool IsSuccess => ProblemDetails == null;

//    public ProblemDetails ProblemDetails { get; set; }

//    public static implicit operator AppResult(ProblemDetails problemDetails)
//    {
//        return new()
//        {
//            ProblemDetails = problemDetails
//        };
//    }
//}
public abstract class AbstractAppService
{
    public AppResult DefaultResult() => new();

    public ProblemDetails Problem(HttpStatusCode? statusCode = null, string detail = null, string title = null, string instance = null, string type = null) => new(statusCode, detail, title, instance, type);


}

[Serializable]
public sealed class AppResult
{

    
    public AppResult()
    { }

    public AppResult(object value) => Content = value;

    public AppResult(ProblemDetails problemDetails) => ProblemDetails = problemDetails;

    public bool IsSuccess => ProblemDetails == null && Content != null;

    public object Content { get; set; }

    public ProblemDetails ProblemDetails { get; set; }

    //public static implicit operator AppResult(AppResult result)
    //{
    //    return new()
    //    {
    //        Content = default
    //        ,
    //        ProblemDetails = result.ProblemDetails
    //    };
    //}

    public static implicit operator AppResult(ProblemDetails problemDetails)
    {
        return new()
        {
            Content = default
            ,
            ProblemDetails = problemDetails
        };
    }

    //public static implicit operator AppResult(object value) => new(value);
}



/// <summary>
/// 错误信息类
/// </summary>
[Serializable]
public sealed class ProblemDetails
{
    public ProblemDetails()
    { }

    public ProblemDetails(HttpStatusCode? statusCode, string detail = null, string title = null, string instance = null, string type = null)
    {
        var status = statusCode.HasValue ? (int)statusCode.Value : (int)HttpStatusCode.BadRequest;
        Status = status;
        Title = title ?? "参数错误";
        Detail = detail;
        Instance = instance;
        Type = type ?? string.Concat("https://httpstatuses.com/", status);
    }

    public override string ToString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);

    [JsonPropertyName("detail")]
    public string Detail { get; set; }

    public IDictionary<string, object> Extensions { get; }

    [JsonPropertyName("instance")]
    public string Instance { get; set; }

    [JsonPropertyName("status")]
    public int? Status { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}