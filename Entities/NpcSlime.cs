using SFML.Graphics;
using SFML.System;
using System;

namespace TerrariaCloneV2.Entities
{
	class NpcSlime : Entity
	{

		private SpriteSheet spriteSheet;

		private float waitTimer = 0;
		private float waitTime = 1f;

		public NpcSlime(World world) : base(world) {

			spriteSheet = new SpriteSheet(
				1, 2, 0, 
				(int)Content.texNpcSlime.Size.X,
				(int)Content.texNpcSlime.Size.Y
			);

			rect = new RectangleShape(new Vector2f(spriteSheet.SubWidth / 1.5f, spriteSheet.SubHeight / 1.5f));
			rect.Origin = new Vector2f(rect.Size.X / 2, 0);
			rect.FillColor = new Color(0, 255, 0, 150);

			rect.Texture = Content.texNpcSlime;
			rect.TextureRect = spriteSheet.GetTextureRect(0, 0);
		}

		public override void OnKill() {
			Spawn(this.startPosition);
		}

		public override void OnWallCollided() {
			Direction *= -1;

			// третий закон Ньютона + немножко замедляем скорость
			velocity = new Vector2f(-velocity.X * 0.8f, velocity.Y);
		}

		public override void UpdateEntity() {
			if (isFly == false) {

				if (waitTimer >= waitTime) {
					velocity = new Vector2f(Direction * Program.Rand.Next(1, 10), -Program.Rand.Next(6, 9));

					waitTimer = 0;
				
				} else {
					waitTimer += 0.05f;
					velocity.X = 0;
				}

				rect.TextureRect = spriteSheet.GetTextureRect(0, 0);
			
			} else {

				rect.TextureRect = spriteSheet.GetTextureRect(0, 1);
			}
		}

		public override void DrawEntity(RenderTarget target, RenderStates states) {
			//throw new NotImplementedException();
		}
	}
}
