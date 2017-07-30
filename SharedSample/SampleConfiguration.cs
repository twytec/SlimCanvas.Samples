using System;
using System.Collections.Generic;
using System.Text;

namespace SharedSample
{
    public class SampleConfiguration
    {
        public List<SamplesModel> GetAllSamples()
        {
            List<SamplesModel> samples = new List<SamplesModel>
            {
                new SamplesModel() { Description = "Hello World", ClassType = typeof(Scenarios.HalloWorld) },
                new SamplesModel() { Description = "Draw primitive", ClassType = typeof(Scenarios.DrawPrimitive) },
                new SamplesModel() { Description = "Gradient", ClassType = typeof(Scenarios.Gradient) },
                new SamplesModel() { Description = "Work with image", ClassType = typeof(Scenarios.WorkWithImages) },
                new SamplesModel() { Description = "Animate", ClassType = typeof(Scenarios.Animate) },
                new SamplesModel() { Description = "Pointer input handling", ClassType = typeof(Scenarios.PointerInputHandling) },
                new SamplesModel() { Description = "Camera", ClassType = typeof(Scenarios.Camera) },
            };

            return samples;
        }
    }
}
