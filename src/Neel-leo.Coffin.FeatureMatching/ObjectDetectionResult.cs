using System.Collections.Generic;

namespace Neel_leo.Coffin.FeatureMaching
{
    public class ObjectDetectionResult
    {
        public byte[] ImageData { get; set; }
        public IList<ObjectDetectionPoint> Points { get; set; }
    }
    
    public record ObjectDetectionPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}