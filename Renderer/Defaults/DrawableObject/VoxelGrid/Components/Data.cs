using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace Scene.Defaults
{
    public partial class VoxelGrid : DrawableObject
    {
        public class _Data
        {
            // Примечание: В шейдерах 430 версии существует выравнивание блоков
            // данных: например для получения vec3 в шейдере, через ssbo,
            // в буфер необходимо передать vector4.
            private VoxelGrid voxelGrid;
            private int ssbo;

            public _Data(VoxelGrid voxelGrid)
            {
                this.voxelGrid = voxelGrid;
            }

            public void GenerateGrid(Vector3i size)
            {
                voxelGrid.shaderProgram.Use();

                // Сохранение размера воксельной сетки
                GL.Uniform3(voxelGrid.shaderProgram.GetUniform("voxelGridSize"), size.X, size.Y, size.Z);
                
                // Инициализация ssbo
                GL.DeleteBuffer(ssbo);
                ssbo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ShaderStorageBuffer, ssbo);
                GL.BufferData(BufferTarget.ShaderStorageBuffer, 16 * size.X * size.Y * size.Z, IntPtr.Zero, BufferUsageHint.DynamicDraw);
                GL.BindBuffersBase(BufferRangeTarget.ShaderStorageBuffer, 3, 1, ref ssbo);
                GL.BindBuffer(BufferTarget.ShaderStorageBuffer, 0);
            }

            public void SetVoxel(Vector3i pos, Vector3 color)
            {
                // Получение размера сетки
                var size = new int[3];
                GL.GetUniform(voxelGrid.shaderProgram.Id, voxelGrid.shaderProgram.GetUniform("voxelGridSize"), size);

                //// Проверка на выход за границы
                //if (pos.X < 0 || pos.X >= size[0] || pos.Y < 0 || pos.Y >= size[1] || pos.Z < 0 || pos.Z >= size[2])
                //    throw new IndexOutOfRangeException("Индекс выходит за границы сетки");

                // Установка данных
                var id = pos.X + pos.Y * size[0] + pos.Z * size[1] * size[2];
                GL.NamedBufferSubData(ssbo, new IntPtr(16 * id), 12, ref color);
            }

            public Vector3 GetVoxel(Vector3i pos)
            {
                // Получение размера сетки
                var size = new int[3];
                GL.GetUniform(voxelGrid.shaderProgram.Id, voxelGrid.shaderProgram.GetUniform("voxelGridSize"), size);

                // Проверка на выход за границы
                if (pos.X < 0 || pos.X >= size[0] || pos.Y < 0 || pos.Y >= size[1] || pos.Z < 0 || pos.Z >= size[2])
                    throw new IndexOutOfRangeException("Индекс выходит за границы сетки");

                // Получение данных
                Vector3 color = new Vector3(0, 0, 0);
                var id = pos.X + pos.Y * size[0] + pos.Z * size[1] * size[2];
                GL.GetNamedBufferSubData(ssbo, new IntPtr(16 * id), 12, ref color);

                return color;
            }

            public Vector3i Size
            {
                get
                {
                    // Получение размера сетки
                    var size = new int[3];
                    GL.GetUniform(voxelGrid.shaderProgram.Id, voxelGrid.shaderProgram.GetUniform("voxelGridSize"), size);

                    return new Vector3i(size[0], size[1], size[2]);
                }
            }
        }
    }
}
