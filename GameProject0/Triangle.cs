using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
namespace GameProject0
{
    /// <summary>
    /// A class for rendering a single triangle
    /// store endpoints as vertices.  Verts define 
    /// coordinate of triangle endpoint in 3d (vector3d)
    /// triangles defined by order of verts presented to 
    /// graphics card as a graphics primitive type
    /// </summary>
    public class Triangle
    {
        /// <summary>
        /// The vertices of the triangle
        /// </summary>
        VertexPositionColor[] vertices;
        /// <summary>
        /// The effect to use rendering the triangle
        /// </summary>
        BasicEffect effect;
        /// <summary>
        /// The game this triangle belongs to 
        /// </summary>
        Game game;
        /// <summary>
        /// Initializes the vertices of the triangle
        /// </summary>
        void InitializeVertices()
        {
            vertices = new VertexPositionColor[3];
            // vertex 0
            vertices[0].Position = new Vector3(0, 1, 0);
            vertices[0].Color = Color.ForestGreen;
            // vertex 1
            vertices[1].Position = new Vector3(1, 1, 0);
            vertices[1].Color = Color.Green;
            // vertex 2 
            vertices[2].Position = new Vector3(1, 0, 0);
            vertices[2].Color = Color.LawnGreen;
        }
        /// <summary>
        /// Constructs a triangle instance
        /// </summary>
        /// <param name="game">The game that is creating the triangle</param>
        public Triangle(Game game)
        {
            this.game = game;
            InitializeVertices();
            InitializeEffect();
        }

        /// <summary>
        /// Initializes the BasicEffect to render our triangle
        /// </summary>
        void InitializeEffect()
        {
            effect = new BasicEffect(game.GraphicsDevice);
            effect.World = Matrix.Identity;
            effect.View = Matrix.CreateLookAt(
                new Vector3(0, 0, 4), // The camera position
                new Vector3(0, 0, 0), // The camera target,
                Vector3.Up            // The camera up vector
            );
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,                         // The field-of-view 
                game.GraphicsDevice.Viewport.AspectRatio,   // The aspect ratio
                0.1f, // The near plane distance 
                100.0f // The far plane distance
            );
            effect.VertexColorEnabled = true;
        }
        /// <summary>
        /// Draws the triangle
        /// </summary>
        public void Draw()
        {
            // Cache old rasterizer state
            RasterizerState oldState = game.GraphicsDevice.RasterizerState;

            // Disable backface culling 
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            game.GraphicsDevice.RasterizerState = rasterizerState;

            // Apply our effect
            effect.CurrentTechnique.Passes[0].Apply();

            // Draw the triangle
            game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleList,
                vertices,       // The vertex data 
                0,              // The first vertex to use
                1               // The number of triangles to draw
            );

            // Restore the prior rasterizer state 
            game.GraphicsDevice.RasterizerState = oldState;
        }

        /// <summary>
        /// Rotates the triangle around the y-axis
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime)
        {
            float angle = (float)gameTime.TotalGameTime.TotalSeconds;
            effect.World = Matrix.CreateRotationY(angle);
        }
    }

}
