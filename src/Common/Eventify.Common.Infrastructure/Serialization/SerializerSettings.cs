﻿using Newtonsoft.Json;

namespace Eventify.Common.Infrastructure.Serialization;

public static class SerializerSettings
{
    public static readonly JsonSerializerSettings Instance = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };
}
