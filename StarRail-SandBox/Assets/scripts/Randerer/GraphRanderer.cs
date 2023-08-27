using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapElement;

namespace Rander
{
    public class GraphRanderer
    {
        public MapElement.Galaxy graph;
        public Material voronoiMaterial;


        public GraphRanderer(MapElement.Galaxy galaxy, Material material)
        {
            this.graph = galaxy;
            this.voronoiMaterial = material;
        }

        public void RenderGraph()
        {
            Matrix4x4 worldToCamera = Camera.main.worldToCameraMatrix;
            voronoiMaterial.SetMatrix("_WorldToCameraMatrix", worldToCamera);

            Matrix4x4 projectionMatrix = Camera.main.projectionMatrix;
            voronoiMaterial.SetMatrix("_ProjectionMatrix", projectionMatrix);

            Texture2D positionTexture = new Texture2D(800, 1, TextureFormat.RGBAFloat, false);
            Texture2D colorTexture = new Texture2D(800, 1, TextureFormat.RGBAFloat, false);

            int index = 0;
            foreach (var vertex in graph.stars)
            {
                float normalizedX = (vertex.pos.x + 100f) / 2200.0f;
                float normalizedY = (vertex.pos.y + 100f) / 2200.0f;

                positionTexture.SetPixel(index, 0, new Color(normalizedX, normalizedY, 0, 0));
                colorTexture.SetPixel(index, 0, vertex.color);

                Debug.Log(vertex.id + ": " + normalizedX + "," + normalizedY);

                index++;
            }

            positionTexture.Apply();
            colorTexture.Apply();

            voronoiMaterial.SetTexture("_PositionTex", positionTexture);
            voronoiMaterial.SetTexture("_ColorTex", colorTexture);
        }
    }
}

