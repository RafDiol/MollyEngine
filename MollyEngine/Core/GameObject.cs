using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MollyEngine.MollyEngine.Core
{
    public class GameObject : Object
    {
        public Vector2 Position = null;
        public Scale Scale = null;
        public string Tag = "";
        public uint renderingPriority = 0;
        public Type type = null;

        public void DestroySelf()
        {
            MollyEngine.UnregisterGameObject(this);
        }
    }
}
