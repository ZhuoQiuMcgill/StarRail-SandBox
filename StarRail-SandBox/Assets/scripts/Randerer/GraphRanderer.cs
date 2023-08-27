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
            Camera mainCamera = Camera.main;
            float nearClip = mainCamera.nearClipPlane;
            float farClip = mainCamera.farClipPlane;

            Vector3 topLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, mainCamera.pixelHeight, nearClip));
            Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, mainCamera.pixelHeight, nearClip));
            Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, nearClip));
            Vector3 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, 0, nearClip));

            // Normalize the coordinates
            Vector3 normalizedTopLeft = NormalizeCameraCorner(topLeft);
            Vector3 normalizedTopRight = NormalizeCameraCorner(topRight);
            Vector3 normalizedBottomLeft = NormalizeCameraCorner(bottomLeft);
            Vector3 normalizedBottomRight = NormalizeCameraCorner(bottomRight);

            // Pass these normalized coordinates to the shader
            voronoiMaterial.SetVector("_TopLeft", normalizedTopLeft);
            voronoiMaterial.SetVector("_TopRight", normalizedTopRight);
            voronoiMaterial.SetVector("_BottomLeft", normalizedBottomLeft);
            voronoiMaterial.SetVector("_BottomRight", normalizedBottomRight);

            Debug.Log(normalizedTopLeft.x);

            Texture2D positionTexture = new Texture2D(800, 1, TextureFormat.RGBAFloat, false);
            Texture2D colorTexture = new Texture2D(800, 1, TextureFormat.RGBAFloat, false);

            int index = 0;
            foreach (var vertex in graph.stars)
            {
                float normalizedX = (vertex.pos.x + 100f) / 2200.0f;
                float normalizedY = (vertex.pos.y + 100f) / 2200.0f;

                positionTexture.SetPixel(index, 0, new Color(normalizedX, normalizedY, 0, 0));
                colorTexture.SetPixel(index, 0, vertex.color);

                // Debug.Log(vertex.id + ": " + normalizedX + "," + normalizedY);

                index++;
            }

            positionTexture.Apply();
            colorTexture.Apply();

            voronoiMaterial.SetTexture("_PositionTex", positionTexture);
            voronoiMaterial.SetTexture("_ColorTex", colorTexture);
        }

        private Vector3 NormalizeCameraCorner(Vector3 corner)
        {
            float normalizedX = (corner.x + 100f) / 2200.0f;
            float normalizedY = (corner.y + 100f) / 2200.0f;
            return new Vector3(Mathf.Clamp01(normalizedX), Mathf.Clamp01(normalizedY), 0);
        }
    }
}

