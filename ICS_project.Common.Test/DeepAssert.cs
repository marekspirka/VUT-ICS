using KellermanSoftware.CompareNetObjects;

namespace ICS_project.Common.Test;

public static class DeepAssert
{
    public static void Equal<T>(T? expected, T? actual, params string[] propertiesToIgnore)
    {
        CompareLogic compareLogic = new()
        {
            Config =
            {
                MembersToIgnore = new List<string>(),
                IgnoreCollectionOrder = true,
                IgnoreObjectTypes = true,
                CompareStaticProperties = false,
                CompareStaticFields = false
            }
        };

        foreach (var str in propertiesToIgnore)
            compareLogic.Config.MembersToIgnore.Add(str);

        var comparisonResult = compareLogic.Compare((object)expected!, (object)actual!);
        if (!comparisonResult.AreEqual)
            throw new EqualObject((object)expected!, (object)actual!, comparisonResult.DifferencesString);
    }
}