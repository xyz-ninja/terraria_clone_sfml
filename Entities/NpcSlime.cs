using SFML.Graphics;
using SFML.System;
using System;

namespace TerrariaCloneV2.Entities
{
	class NpcSlime : Entity
	{
		public NpcSlime(World world) : base(world) {
			rect = new RectangleShape(new Vector2f(Tile.TILE_SIZE * 1.5f, Tile.TILE_SIZE * 1f));
			rect.Origin = new Vector2f(rect.Size.X / 2, 0);
			rect.FillColor = Color.Green;
		}

		public override void OnKill() {
			Spawn(this.startPosition);
		}

		public override void OnWallCollided() {
			Direction *= -1;
		}

		public override void UpdateEntity() {
			if (isFly == false) {
				velocity = new Vector2f(Direction * 10, -7);
			}
		}

		public override void DrawEntity(RenderTarget target, RenderStates states) {
			//throw new NotImplementedException();
		}
	}
}
