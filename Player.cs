using SFML.System;
using SFML.Graphics;
using SFML.Window;
using TerrariaCloneV2.Entities;
using System.Diagnostics;

namespace TerrariaCloneV2
{
	class Player : Entity
	{
		//private RectangleShape rectDirection;

		private float horizontalSpeed = 4f;
		private float horizontalSpeedAcceleration = 0.2f;

		public Color HairColor = new Color(255, 0, 0);
		public Color BodyColor = new Color(255, 229, 186);
		public Color ShirtColor = new Color(255, 255, 0);
		public Color LegsColor = new Color(0, 76, 135);

		private AnimatedSprite animatedSpriteHair;
		private AnimatedSprite animatedSpriteHead;
		private AnimatedSprite animatedSpriteShirt;
		private AnimatedSprite animatedSpriteUndershirt;
		private AnimatedSprite animatedSpriteHands;
		private AnimatedSprite animatedSpriteLegs;
		private AnimatedSprite animatedSpriteShoes;

		public Player(World world) : base(world) {

			rect = new RectangleShape(new Vector2f(Tile.TILE_SIZE * 1.5f, Tile.TILE_SIZE * 2.8f));
			rect.Origin = new Vector2f(rect.Size.X / 2, 0);

			/*rectDirection = new RectangleShape(new Vector2f(50, 3));
			rectDirection.FillColor = Color.Red;
			rectDirection.Position = new Vector2f(0, rect.Size.Y / 2 - 1);*/

			animatedSpriteHair = new AnimatedSprite(Content.texPlayerHair, 
				new SpriteSheet(
					1, 14, 0, 
					(int)Content.texPlayerHair.Size.X,
					(int)Content.texPlayerHair.Size.Y)
			);

			animatedSpriteHair.Position = new Vector2f(0, 19f); // приподнимем волосы вверх
			animatedSpriteHair.Color = HairColor;
			animatedSpriteHair.AddAnimation("idle", new Animation(
				new AnimationFrame(0, 0, 0.1f)
			));
			animatedSpriteHair.AddAnimation("run", new Animation(
				new AnimationFrame(0, 0, 0.1f),
				new AnimationFrame(0, 1, 0.1f),
				new AnimationFrame(0, 2, 0.1f),
				new AnimationFrame(0, 3, 0.1f),
				new AnimationFrame(0, 4, 0.1f),
				new AnimationFrame(0, 5, 0.1f),
				new AnimationFrame(0, 6, 0.1f),
				new AnimationFrame(0, 7, 0.1f),
				new AnimationFrame(0, 8, 0.1f),
				new AnimationFrame(0, 9, 0.1f),
				new AnimationFrame(0, 10, 0.1f),
				new AnimationFrame(0, 11, 0.1f),
				new AnimationFrame(0, 12, 0.1f),
				new AnimationFrame(0, 13, 0.1f)
			));


			animatedSpriteHead = new AnimatedSprite(Content.texPlayerHead,
				new SpriteSheet(
					1, 20, 0,
					(int)Content.texPlayerHead.Size.X,
					(int)Content.texPlayerHead.Size.Y)
			);
			animatedSpriteHead.Position = new Vector2f(0, 19f); // приподнимем волосы вверх
			animatedSpriteHead.Color = BodyColor;
			animatedSpriteHead.AddAnimation("idle", new Animation(
				new AnimationFrame(0, 0, 0.1f)
			));
			animatedSpriteHead.AddAnimation("run", new Animation(
				new AnimationFrame(0, 6, 0.1f),
				new AnimationFrame(0, 7, 0.1f),
				new AnimationFrame(0, 8, 0.1f),
				new AnimationFrame(0, 9, 0.1f),
				new AnimationFrame(0, 10, 0.1f),
				new AnimationFrame(0, 11, 0.1f),
				new AnimationFrame(0, 12, 0.1f),
				new AnimationFrame(0, 13, 0.1f),
				new AnimationFrame(0, 14, 0.1f),
				new AnimationFrame(0, 15, 0.1f),
				new AnimationFrame(0, 16, 0.1f),
				new AnimationFrame(0, 17, 0.1f),
				new AnimationFrame(0, 18, 0.1f),
				new AnimationFrame(0, 19, 0.1f)
			));

			animatedSpriteShirt = new AnimatedSprite(Content.texPlayerShirt,
				new SpriteSheet(
					1, 20, 0,
					(int)Content.texPlayerShirt.Size.X,
					(int)Content.texPlayerShirt.Size.Y)
			);
			animatedSpriteShirt.Position = new Vector2f(0, 19f); // приподнимем волосы вверх
			animatedSpriteShirt.Color = ShirtColor;
			animatedSpriteShirt.AddAnimation("idle", new Animation(
				new AnimationFrame(0, 0, 0.1f)
			));
			animatedSpriteShirt.AddAnimation("run", new Animation(
				new AnimationFrame(0, 6, 0.1f),
				new AnimationFrame(0, 7, 0.1f),
				new AnimationFrame(0, 8, 0.1f),
				new AnimationFrame(0, 9, 0.1f),
				new AnimationFrame(0, 10, 0.1f),
				new AnimationFrame(0, 11, 0.1f),
				new AnimationFrame(0, 12, 0.1f),
				new AnimationFrame(0, 13, 0.1f),
				new AnimationFrame(0, 14, 0.1f),
				new AnimationFrame(0, 15, 0.1f),
				new AnimationFrame(0, 16, 0.1f),
				new AnimationFrame(0, 17, 0.1f),
				new AnimationFrame(0, 18, 0.1f),
				new AnimationFrame(0, 19, 0.1f)
			));

			animatedSpriteUndershirt = new AnimatedSprite(Content.texPlayerUndershirt,
				new SpriteSheet(
					1, 20, 0,
					(int)Content.texPlayerUndershirt.Size.X,
					(int)Content.texPlayerUndershirt.Size.Y)
			);
			animatedSpriteUndershirt.Position = new Vector2f(0, 19f); // приподнимем волосы вверх
			animatedSpriteUndershirt.Color = ShirtColor;
			animatedSpriteUndershirt.AddAnimation("idle", new Animation(
				new AnimationFrame(0, 0, 0.1f)
			));
			animatedSpriteUndershirt.AddAnimation("run", new Animation(
				new AnimationFrame(0, 6, 0.1f),
				new AnimationFrame(0, 7, 0.1f),
				new AnimationFrame(0, 8, 0.1f),
				new AnimationFrame(0, 9, 0.1f),
				new AnimationFrame(0, 10, 0.1f),
				new AnimationFrame(0, 11, 0.1f),
				new AnimationFrame(0, 12, 0.1f),
				new AnimationFrame(0, 13, 0.1f),
				new AnimationFrame(0, 14, 0.1f),
				new AnimationFrame(0, 15, 0.1f),
				new AnimationFrame(0, 16, 0.1f),
				new AnimationFrame(0, 17, 0.1f),
				new AnimationFrame(0, 18, 0.1f),
				new AnimationFrame(0, 19, 0.1f)
			));

			animatedSpriteHands = new AnimatedSprite(Content.texPlayerHands,
				new SpriteSheet(
					1, 20, 0,
					(int)Content.texPlayerHands.Size.X,
					(int)Content.texPlayerHands.Size.Y)
			);
			animatedSpriteHands.Position = new Vector2f(0, 19f); // приподнимем волосы вверх
			animatedSpriteHands.Color = BodyColor;
			animatedSpriteHands.AddAnimation("idle", new Animation(
				new AnimationFrame(0, 0, 0.1f)
			));
			animatedSpriteHands.AddAnimation("run", new Animation(
				new AnimationFrame(0, 6, 0.1f),
				new AnimationFrame(0, 7, 0.1f),
				new AnimationFrame(0, 8, 0.1f),
				new AnimationFrame(0, 9, 0.1f),
				new AnimationFrame(0, 10, 0.1f),
				new AnimationFrame(0, 11, 0.1f),
				new AnimationFrame(0, 12, 0.1f),
				new AnimationFrame(0, 13, 0.1f),
				new AnimationFrame(0, 14, 0.1f),
				new AnimationFrame(0, 15, 0.1f),
				new AnimationFrame(0, 16, 0.1f),
				new AnimationFrame(0, 17, 0.1f),
				new AnimationFrame(0, 18, 0.1f),
				new AnimationFrame(0, 19, 0.1f)
			));

			animatedSpriteLegs = new AnimatedSprite(Content.texPlayerLegs,
				new SpriteSheet(
					1, 20, 0,
					(int)Content.texPlayerLegs.Size.X,
					(int)Content.texPlayerLegs.Size.Y)
			);
			animatedSpriteLegs.Position = new Vector2f(0, 19f); // приподнимем волосы вверх
			animatedSpriteLegs.Color = LegsColor;
			animatedSpriteLegs.AddAnimation("idle", new Animation(
				new AnimationFrame(0, 0, 0.1f)
			));
			animatedSpriteLegs.AddAnimation("run", new Animation(
				new AnimationFrame(0, 6, 0.1f),
				new AnimationFrame(0, 7, 0.1f),
				new AnimationFrame(0, 8, 0.1f),
				new AnimationFrame(0, 9, 0.1f),
				new AnimationFrame(0, 10, 0.1f),
				new AnimationFrame(0, 11, 0.1f),
				new AnimationFrame(0, 12, 0.1f),
				new AnimationFrame(0, 13, 0.1f),
				new AnimationFrame(0, 14, 0.1f),
				new AnimationFrame(0, 15, 0.1f),
				new AnimationFrame(0, 16, 0.1f),
				new AnimationFrame(0, 17, 0.1f),
				new AnimationFrame(0, 18, 0.1f),
				new AnimationFrame(0, 19, 0.1f)
			));

			animatedSpriteShoes = new AnimatedSprite(Content.texPlayerShoes,
				new SpriteSheet(
					1, 20, 0,
					(int)Content.texPlayerShoes.Size.X,
					(int)Content.texPlayerShoes.Size.Y)
			);
			animatedSpriteShoes.Position = new Vector2f(0, 19f); // приподнимем волосы вверх
			animatedSpriteShoes.Color = HairColor;
			animatedSpriteShoes.AddAnimation("idle", new Animation(
				new AnimationFrame(0, 0, 0.1f)
			));
			animatedSpriteShoes.AddAnimation("run", new Animation(
				new AnimationFrame(0, 6, 0.1f),
				new AnimationFrame(0, 7, 0.1f),
				new AnimationFrame(0, 8, 0.1f),
				new AnimationFrame(0, 9, 0.1f),
				new AnimationFrame(0, 10, 0.1f),
				new AnimationFrame(0, 11, 0.1f),
				new AnimationFrame(0, 12, 0.1f),
				new AnimationFrame(0, 13, 0.1f),
				new AnimationFrame(0, 14, 0.1f),
				new AnimationFrame(0, 15, 0.1f),
				new AnimationFrame(0, 16, 0.1f),
				new AnimationFrame(0, 17, 0.1f),
				new AnimationFrame(0, 18, 0.1f),
				new AnimationFrame(0, 19, 0.1f)
			));
		}

		public override void OnKill() {
			Spawn(this.startPosition);
		}

		public override void OnWallCollided() {

			//Debug.Print("Collided");

			//throw new NotImplementedException();
		}

		public override void UpdateEntity() {
			UpdateMovement();
		}

		public override void DrawEntity(RenderTarget target, RenderStates states) {

			target.Draw(animatedSpriteHead, states);
			target.Draw(animatedSpriteHair, states);
			target.Draw(animatedSpriteShirt, states);
			target.Draw(animatedSpriteUndershirt, states);
			target.Draw(animatedSpriteHands, states);
			target.Draw(animatedSpriteLegs, states);
			target.Draw(animatedSpriteShoes, states);
		}

		private void UpdateMovement() {

			bool isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
			bool isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);

			bool isMove = isMoveLeft || isMoveRight;

			if (isMove) {

				if (isMoveLeft) {

					// если игрок резко сменил направление движения, убираем инерцию
					if (movement.X > 0) {
						movement.X = 0;
					}

					movement.X -= horizontalSpeedAcceleration;
					Direction = -1;

				} else if (isMoveRight) {

					if (movement.X < 0) {
						movement.X = 0;
					}

					movement.X += horizontalSpeedAcceleration;
					Direction = 1;
				}

				if (movement.X > horizontalSpeed) {
					movement.X = horizontalSpeed;
				} else if (movement.X < -horizontalSpeed) {
					movement.X = -horizontalSpeed;
				}

			} else {

				movement.X = 0;
			}
		}
	}
}
