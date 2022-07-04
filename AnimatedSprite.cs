using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaCloneV2
{
	// информация о кадре
	class AnimationFrame
	{
		public int col, row;
		public float time; // время пока кадр активен

		public AnimationFrame(int col, int row, float time) {
			this.col = col;
			this.row = row;
			this.time = time;
		}
	}

	// анимация
	class Animation
	{
		private AnimationFrame[] frames;

		private float timer;

		private AnimationFrame currentFrame;
		private int currentFrameIndex;

		public Animation(params AnimationFrame[] frames) {
			
			this.frames = frames;
			Reset();
		}

		// начать анимация сначала
		public void Reset() {
			
			timer = 0f;
			currentFrameIndex = 0;
			currentFrame = frames[0];
		}

		// переключение на следующий кадр
		public void NextFrame() {
			
			timer = 0f;
			currentFrameIndex += 1;
		
			if (currentFrameIndex == frames.Length) {
				currentFrameIndex = 0;
			}

			currentFrame = frames[currentFrameIndex];
		}

		// получить текущий кадр
		public AnimationFrame GetCurrentFrame(float dt) {

			timer += dt;

			if (timer > currentFrame.time) {
				NextFrame();
			}

			return currentFrame;
		}
	}

	class AnimatedSprite : Transformable, Drawable
	{
		public float timeStep = 0.05f;

		private RectangleShape rectShape;
		private SpriteSheet spriteSheet;
		private SortedDictionary<string, Animation> animations =
			new SortedDictionary<string, Animation>();

		private Animation currentAnimation;
		private string currentAnimationName;

		public Color Color {
			set { rectShape.FillColor = value; }
			get { return rectShape.FillColor; }
		}

		public AnimatedSprite(Texture texture, SpriteSheet spriteSheet) {
			
			this.spriteSheet = spriteSheet;
			
			rectShape = new RectangleShape(new Vector2f(spriteSheet.SubWidth, spriteSheet.SubHeight));
			rectShape.Origin = new Vector2f(spriteSheet.SubWidth / 2, spriteSheet.SubHeight / 2);
			rectShape.Texture = texture;
		}

		public void AddAnimation(string name, Animation animation) {
			
			animations[name] = animation;
			currentAnimation = animation;
			currentAnimationName = name;
		}

		public void Play(string name) {
			
			if (currentAnimationName == name) {
				return;
			}

			currentAnimation = animations[name];
			currentAnimationName = name;
			currentAnimation.Reset();
		}

		public IntRect GetTextureRect() {
			
			var currentFrame = currentAnimation.GetCurrentFrame(timeStep);
			return spriteSheet.GetTextureRect(currentFrame.col, currentFrame.row);
		}

		public void Draw(RenderTarget target, RenderStates states) {

			rectShape.TextureRect = GetTextureRect();

			states.Transform *= Transform;
			target.Draw(rectShape, states);
		}
	}
}
