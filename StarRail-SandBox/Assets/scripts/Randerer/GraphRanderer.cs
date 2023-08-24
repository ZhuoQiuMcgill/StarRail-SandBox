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


            List<Vector4> positions = new List<Vector4>();
            List<Vector4> colors = new List<Vector4>();

            foreach (var vertex in graph.stars)
            {
                positions.Add(new Vector4(vertex.pos.x, vertex.pos.y, 0, 0));
                colors.Add(vertex.color);
            }

            voronoiMaterial.SetVectorArray("_Positions", positions.ToArray());
            voronoiMaterial.SetVectorArray("_Colors", colors.ToArray());
        }

    }
}

