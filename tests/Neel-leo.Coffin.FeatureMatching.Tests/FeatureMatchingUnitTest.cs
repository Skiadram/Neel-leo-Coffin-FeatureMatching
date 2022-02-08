using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Neel_leo.Coffin.FeatureMaching;
using Xunit;

namespace Neel_leo.Coffin.FeatureMaching.Tests;

public class FeatureMatchingUnitTest
{
    [Fact]
    public async Task ObjectShouldBeDetectedCorrectly()
    {
        var executingPath = GetExecutingPath();
        var imageScenesData = new List<byte[]>();
        foreach (var imagePath in
            Directory.EnumerateFiles(Path.Combine(executingPath, "Scenes")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            imageScenesData.Add(imageBytes);
        }
        var objectImageData = await
            File.ReadAllBytesAsync(Path.Combine(executingPath, "Neel-leo.Coffin.jpg"));
        var detectObjectInScenesResults = new
            ObjectDetection().DetectObjectInScenes(objectImageData, imageScenesData);
        
        Assert.Equal("[{\"X\":1717,\"Y\":879},{\"X\":539,\"Y\":1468},{\"X\":1004,\"Y\":2360},{\"X\":2167,\"Y\":1764}]",
            JsonSerializer.Serialize(detectObjectInScenesResults[0].Points));

        Assert.Equal("[{\"X\":2204,\"Y\":1483},{\"X\":1270,\"Y\":523},{\"X\":603,\"Y\":1747},{\"X\":1822,\"Y\":2142}]",
            JsonSerializer.Serialize(detectObjectInScenesResults[1].Points));
    }
    private static string GetExecutingPath()
    {
        var executingAssemblyPath =
            Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    } 

}