﻿using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Codibre.CaseInsensitiveEnum
{
    internal class CaseInsensitiveEnumPolicy : JsonNamingPolicy
    {
        private readonly Dictionary<string, string> _map = new Dictionary<string, string>();

        internal CaseInsensitiveEnumPolicy(Type type) {
            foreach(var item in Enum.GetNames(type)) {
                _map.Add(item.ToLower(), item);
            }
        }

        public override string ConvertName(string name) => _map.TryGetValue(name.ToLower(), out var result) ? result : name;
    }
}