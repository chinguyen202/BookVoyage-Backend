using System.Reflection;

namespace BookVoyage.Application;

public static class AssemblyReference
{
    public static readonly Assembly ApplicationAssembly = typeof(AssemblyReference).Assembly;
}