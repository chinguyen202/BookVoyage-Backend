using System.Reflection;

namespace BookVoyage.Persistence;

public static class AssemblyReference
{
    public static readonly Assembly InfrastructureAssembly = typeof(AssemblyReference).Assembly;
}