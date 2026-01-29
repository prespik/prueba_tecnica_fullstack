using System.Text.Json;
using TaskManager.Api.DTOs;

namespace TaskManager.Api.Services
{
    public static class JsonHelper
    {
        public static string? Serialize(TaskAdditionalDataDto? data)
            => data == null ? null : JsonSerializer.Serialize(data);

        public static TaskAdditionalDataDto? Deserialize(string? json)
            => string.IsNullOrEmpty(json)
                ? null
                : JsonSerializer.Deserialize<TaskAdditionalDataDto>(json);
    }

}