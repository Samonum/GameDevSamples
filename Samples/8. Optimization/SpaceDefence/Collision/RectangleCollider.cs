using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceDefence.Collision
{
    public class RectangleCollider : Collider, IEquatable<RectangleCollider>
    {
        public Rectangle shape;

        public RectangleCollider(Rectangle shape)
        {
            this.shape = shape;
        }

        public override bool Contains(Vector2 loc)
        {
            return shape.Contains(loc);
        }

        public bool Equals(RectangleCollider other)
        {
            return shape == other.shape;
        }

        public override Rectangle GetBoundingBox()
        {
            return shape;
        }

        public override bool Intersects(CircleCollider other)
        {
            return other.Intersects(this);
        }

        public override bool Intersects(RectangleCollider other)
        {
            return shape.Intersects(other.shape);
        }

        public override bool Intersects(LinePieceCollider other)
        {
            return other.Intersects(this);
        }
    }
}
