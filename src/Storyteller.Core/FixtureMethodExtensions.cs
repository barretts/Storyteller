﻿using System.Reflection;
using FubuCore.Reflection;

namespace Storyteller.Core
{
    internal static class FixtureMethodExtensions
    {
        public static string GetKey(this MethodInfo method)
        {
            var att = method.GetAttribute<AliasAsAttribute>();
            return att == null ? method.Name : att.Alias;
        }
    }
}