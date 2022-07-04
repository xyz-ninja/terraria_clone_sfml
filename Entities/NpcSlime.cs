using SFML.Graphics;
using SFML.System;
using System;

namespace TerrariaCloneV2.Entities
{
	class NpcSlime : Entity
	{
		public NpcSlime(World world) : base(world) {
			rect = new RectangleShape();
		}

		public override void DrawEntity(RenderTarget target, RenderStates states) {
			throw new NotImplementedException();
		}

		public override void OnKill() {
			throw new NotImplementedException();
		}

		public override void OnWallCollided() {
			throw new NotImplementedException();
		}

		public override void UpdateEntity() {
			throw new NotImplementedException();
		}
	}
}
