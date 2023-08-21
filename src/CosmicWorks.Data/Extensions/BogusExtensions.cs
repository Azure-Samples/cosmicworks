using Bogus;

namespace CosmicWorks.Data.Extensions;

internal static class BogusExtensions
{
    public static T ArrayElement<T>(this Randomizer randomizer, params T[] items) => randomizer.ArrayElement<T>(items);
}