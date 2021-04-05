using MollyEngine.Core;
using System;

namespace MollyEngine.Core
{
    public interface ICollider
    {
        bool isColliderDisabled { get; set; } 
        bool isColliding(out GameObject collide);

        bool isColliding();

        bool isCollidingWith(GameObject gameObject);
    }
}
