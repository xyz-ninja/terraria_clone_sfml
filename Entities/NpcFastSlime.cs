using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace TerrariaCloneV2.Entities
{
	class NpcFastSlime : NpcSlime
	{
		public NpcFastSlime(World world) : base (world) {
			rect.FillColor = new Color(255, 20, 20, 200);
		}

		public override Vector2f GetJumpVelocity() {
			return new Vector2f(Direction * Program.Rand.Next(15, 100), -Program.Rand.Next(8, 15));
		}
	}
}
