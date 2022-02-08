// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Neel_leo.Coffin.FeatureMaching;

static class Program
{
    static void Main(string[] args)
    {
        var ex = execute(args[0], args[1]);
        ex.Wait();


    }

    private static async Task execute(string obj, string scene)
    {
        try
        {
            var executingPath = GetExecutingPath();
            var imageScenesData = new List<byte[]>();
            foreach (var imagePath in
                Directory.EnumerateFiles(scene))
            {
                var imageBytes = await File.ReadAllBytesAsync(imagePath);
                imageScenesData.Add(imageBytes);
            }

            var objectImageData = await
                File.ReadAllBytesAsync(obj);
            var detectObjectInScenesResults = new
                ObjectDetection().DetectObjectInScenes(objectImageData, imageScenesData);
            foreach (var objectDetectionResult in detectObjectInScenesResults)
            {
                Console.WriteLine($"Points:{JsonSerializer.Serialize(objectDetectionResult.Points)}");
            }
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("File not found !");
        }
        

    }
    private static string GetExecutingPath()
    {
        var executingAssemblyPath =
            Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
}


    
