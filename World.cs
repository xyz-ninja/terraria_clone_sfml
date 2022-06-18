using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaCloneV2
{
	class World : Transformable, Drawable
	{
		public const int WORLD_SIZE = 5;

		Chunk[][] chunks;

		public World() {
			chunks = new Chunk[WORLD_SIZE][];

			for (int i = 0; i < WORLD_SIZE; i++) {
				chunks[i] = new Chunk[WORLD_SIZE];
			}

			for (int x = 0; x < Chunk.CHUNK_SIZE; x++) {
				for (int y = 0; y < Chunk.CHUNK_SIZE; y++) {
					
					SetTile(TILE_TYPE.GROUND, x, y);
				}
			}

			for (int x = Chunk.CHUNK_SIZE; x < Chunk.CHUNK_SIZE * 2; x++) {
				for (int y = 0; y < Chunk.CHUNK_SIZE; y++) {

					SetTile(TILE_TYPE.GRASS, x, y);
				}
			}
		}

		public void SetTile(TILE_TYPE type, int x, int y) {
			
			var chunk = GetChunk(x, y);
			var tilePos = GetTilePositionInChunk(x, y);

			chunk.SetTile(type, tilePos.X, tilePos.Y);
		}

		public Chunk GetChunk(int x, int y) {
			
			int X = x / Chunk.CHUNK_SIZE;
			int Y = y / Chunk.CHUNK_SIZE;

			if (chunks[X][Y] == null) {
				chunks[X][Y] = new Chunk(new Vector2i(X, Y));
			}

			return chunks[X][Y];
		}
		public Vector2i GetTilePositionInChunk(int x, int y) {

			int X = x / Chunk.CHUNK_SIZE;
			int Y = y / Chunk.CHUNK_SIZE;

			return new Vector2i(x - X * Chunk.CHUNK_SIZE, y - Y * Chunk.CHUNK_SIZE);
		}

		public void Draw(RenderTarget target, RenderStates states) {
			
			for (int x = 0; x < WORLD_SIZE; x++) {
				for (int y = 0; y < WORLD_SIZE; y++) {

					var currentChunk = chunks[x][y];
					
					if (currentChunk == null) {
						continue;
					}

					target.Draw(currentChunk);
				}
			}
		}
	}
}
