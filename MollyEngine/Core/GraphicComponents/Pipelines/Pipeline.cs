using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace MollyEngine.Core.GraphicComponents.Pipelines
{
    public enum AvailablePipelines
    {
        LegacyPipeline = 0, MollyNewPipeline = 1
    }

    public class Pipeline
    {

        public MollyEngine gameEngine { get; set; }
        public Graphics graphics { get; set; }
    }
}
