using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace AODGameLibrary.CollisionChecking
{
    /// <summary>
    /// MeshPart的扩展，由大地无敌-范若余在2009年12月19日添加
    /// </summary>
    public static class MeshPartExtension
    {
        /*
        	// Get the mesh part's associated vertices.
					Vector3[] vertices = new Vector3[meshPart.NumVertices];
					mesh.VertexBuffer.GetData<Vector3>(
						meshPart.StreamOffset + (meshPart.BaseVertex * meshPart.VertexStride),
						vertices,
						0,
						meshPart.NumVertices,
						meshPart.VertexStride);

					// Transform the vertices to object space.
					Vector3.Transform(vertices, ref bone, vertices);
         */
        ///// <summary>
        ///// 得到顶点位置
        ///// </summary>
        ///// <param name="mesh"></param>
        ///// <param name="Positions"></param>
        //public static List<Vector3> GetPositions(this ModelMesh mesh)
        //{
        //    List<Vector3> positions = new List<Vector3>();
        //    foreach (ModelMeshPart meshPart in mesh.MeshParts)
        //    {

        //        // Get the mesh part's associated vertices.
        //        Vector3[] vertices = new Vector3[meshPart.NumVertices];
        //        mesh.VertexBuffer.GetData<Vector3>(
        //            meshPart.StreamOffset + (meshPart.BaseVertex * meshPart.VertexStride),
        //            vertices,
        //            0,
        //            meshPart.NumVertices,
        //            meshPart.VertexStride);

        //        //将顶点添加到集合
        //        positions.AddRange(vertices);
        //    }
        //    return positions;
            
        //}
        ///// <summary>
        ///// 得到Mesh的碰撞盒
        ///// </summary>
        ///// <param name="mesh"></param>
        ///// <returns></returns>
        //public static BoundingBox GetBoundingBox(this ModelMesh mesh)
        //{
        //    List<Vector3> a = mesh.GetPositions();
        //    if (a.Count > 0)
        //    {
        //        return BoundingBox.CreateFromPoints(a);
        //    }
        //    else return new BoundingBox();
        //}
    }
}
