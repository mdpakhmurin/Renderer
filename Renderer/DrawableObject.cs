﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.IO;
using Scene.Utilities;

namespace Scene.Defaults
{
    public class DrawableObject
    {
        virtual public void Draw(Camera camera) { }
    }

    public partial class VoxelGrid : DrawableObject
    {
        private ShaderProgram shaderProgram;
        private Drawer drawer;
        private Data data;

        public VoxelGrid()
        {
            drawer = new Drawer(this);
            data = new Data(this);
            data.GenerateGrid(new Vector3(3,3,3));
        }

        override public void Draw(Camera camera)
        {
            base.Draw(camera);

            drawer.Draw(camera);
        }

    }

    public partial class VoxelGrid : DrawableObject
    {
        private class Drawer
        {
            private VoxelGrid voxelGrid;
            private int vbo;
            private int vao;

            public Drawer(VoxelGrid voxelGrid)
            {
                this.voxelGrid = voxelGrid;
                init();
            }

            /// <summary>
            /// Инициализирует VBO, занося в него данные
            /// описывающие экран отрисовки.
            /// </summary>
            private void initVBO()
            {
                // Координаты углов "экрана" отрисовки
                //
                // Экран - 3х мерная плоскость (состояющая из двух треугольников)
                // на которой будет происходить отрисовка
                // 
                // Каждая строчка - координаты x,y соответствующего угла
                // (координата z устанавливается в 0 в шейдере)
                float[] canvasVerticesPos =
                {
                    -0.9f, -0.9f,
                     0.9f, -0.9f,
                     0.9f,  0.9f,
                     0.9f,  0.9f,
                    -0.9f,  0.9f,
                    -0.9f, -0.9f
                };


                vbo = GL.GenBuffer();

                // Установка активного VBO для всей программы
                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

                // Загрузка данных из canvasVerticesPos в VBO
                GL.BufferData(
                    BufferTarget.ArrayBuffer,
                    canvasVerticesPos.Length * sizeof(float),
                    canvasVerticesPos,
                    BufferUsageHint.StaticDraw);
            }

            /// <summary>
            /// Инициализирует VAO, используя данные из VBO
            /// </summary>
            private void initVAO()
            {
                // Установка активного VBO
                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

                vao = GL.GenVertexArray();

                // Установка активного VAO
                GL.BindVertexArray(vao);

                // Указывает активный массив атрибутов вершин
                GL.EnableVertexAttribArray(0);
                // Связывает активный VBO с активным индексом VAO
                GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);

                GL.BindVertexArray(0);
            }

            /// <summary>
            /// Загружает шейдеры и создает шейдерную программу
            /// </summary>
            private void initShaderProgram()
            {
                var vertexShader = new VertexShader(File.ReadAllText(@"Resources\Shaders\VertexShader.glsl"));
                var fragmentShader = new FragmentShader(File.ReadAllText(@"Resources\Shaders\FragmentShader.glsl"));

                voxelGrid.shaderProgram = new ShaderProgram(vertexShader, fragmentShader);
            }

            /// <summary>
            /// Начальная инициализация отрисовщика
            /// </summary>
            private void init()
            {
                initVBO();
                initVAO();
                initShaderProgram();
            }

            public void Draw(Camera camera)
            {
                voxelGrid.shaderProgram.Use();

                // Установка позиции камеры,
                GL.Uniform3(
                    voxelGrid.shaderProgram.GetUniform("cameraPosition"),
                    camera.Transform.Position.GetAbsolutePosition());

                // Использование VAO для отрисовки треугольников.
                // (2 треугольника образующих прямоугольник экрана отрисовки).
                GL.BindVertexArray(vao);
                GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
                GL.BindVertexArray(0);
            }
        }
    }
    
    public partial class VoxelGrid : DrawableObject
    {
        private class Data
        {
            private VoxelGrid voxelGrid;

            public Data(VoxelGrid voxelGrid)
            {
                this.voxelGrid = voxelGrid;
            }

            public void GenerateGrid(Vector3 size)
            {
                voxelGrid.shaderProgram.Use();

                int ssbo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ShaderStorageBuffer, ssbo);

                int[] voxels = new int[3 * 3 * 3 * 3];
                for (int i = 0; i < voxels.Length; i++)
                {
                    voxels[i] = 1;
                }
                GL.BufferData(BufferTarget.ShaderStorageBuffer, sizeof(int) * voxels.Length, voxels, BufferUsageHint.DynamicDraw);
                GL.BindBuffersBase(BufferRangeTarget.ShaderStorageBuffer, 3, 1, ref ssbo);
                GL.BindBuffer(BufferTarget.ShaderStorageBuffer, 0);
                Console.WriteLine(GL.GetError());
            }
        }
    }
}