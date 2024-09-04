﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq;
using System.Reflection;
public static class AssemblyInfo
{
    /// <summary> Gets the git hash value from the assembly
    /// or null if it cannot be found. </summary>
    /// 

    public static string GetGitHash()
    {
        var asm = typeof(AssemblyInfo).Assembly;
        var attrs = asm.GetCustomAttributes<AssemblyMetadataAttribute>();
        return attrs.FirstOrDefault(a => a.Key == "GitHash")?.Value;
    }
}