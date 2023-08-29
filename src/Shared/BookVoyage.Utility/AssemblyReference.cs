using System.Reflection;

namespace BookVoyage.Utility;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}