using SFML.System;
using System;

namespace TerrariaCloneV2.Utils
{
	class MathUtils
	{
		public static float GetDistance(Vector2f v1, Vector2f v2) {
			float x = v2.X - v1.X;
			float y = v2.Y - v1.Y;

			return (float)Math.Sqrt(x * x + y * y);
		}

		public static float GetDistance(int x1, int y1, int x2, int y2) {
			float x = x2 - x1;
			float y = y2 - y1;

			return (float)Math.Sqrt(x * x + y * y);
		}

		// определяет длину вектора
		public static float GetDistance(Vector2f vec) {
			return (float)Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
		}
	}
}
