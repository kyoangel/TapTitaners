using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TapTitaner.Services;

public class SaveFilterAttribute : ActionFilterAttribute
{
    private readonly GameDataRecorder _gameDataRecorder;

    public SaveFilterAttribute(GameDataRecorder gameDataRecorder)
    {
        _gameDataRecorder = gameDataRecorder;
    }

    public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result;
        if (result is OkObjectResult okObjectResult)
        {
            var serialize = JsonSerializer.Serialize(okObjectResult.Value);
            // var hero = ((JObject)JsonConvert.DeserializeObject(serialize))?.SelectToken("_hero");
            // var mp = hero.SelectToken("ManaPoints");
            // var lv = hero.SelectToken("Lv");

            _gameDataRecorder.Save(serialize);
        }
        return base.OnResultExecutionAsync(context, next);
    }
}