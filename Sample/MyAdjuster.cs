﻿using System;
using System.Collections.Generic;
using System.Linq;
using NrsLib.CSharpToTypescriptInterface.TypeAdjuster;
using NrsLib.CSharpToTypescriptInterface.TypeAdjuster.TypeConverter;

namespace Sample {
    class MyAdjuster : ITypeAdjuster {
        private readonly Dictionary<Type, ITypeConverter> cache = new Dictionary<Type, ITypeConverter>();

        private readonly List<ITypeConverter> converters = new List<ITypeConverter>
        {
            new AllAnyConverter(),
        };

        public string ToTypescriptType(Type type) {
            if (!cache.TryGetValue(type, out var converter)) {
                converter = converters.First(x => x.IsSatisfiedBy(type));
                cache[type] = converter;
            }

            return converter.ConvertType(type, this);
        }

        public void AddConverter(ITypeConverter converter) {
            this.converters.Insert(0, converter);
        }
    }
}
