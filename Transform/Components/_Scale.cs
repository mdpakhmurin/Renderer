﻿using OpenTK.Mathematics;
using System;

namespace Scene.SceneObject
{
    public partial class SceneObject
    {
        public partial class _Transform
        {
            public class _Scale
            {
                // Все трансформации объекта.
                _Transform transform;

                // Размер объекта, относительно родителя.
                Vector3 scale;

                public _Scale(_Transform transform)
                {
                    this.transform = transform;
                    scale = new Vector3(1, 1, 1);
                }

                /// <summary>
                /// Все трансформации объекта.
                /// </summary>
                public _Transform Transform
                {
                    get { return transform; }
                }

                /// <summary>
                /// Получает абсолютный размер объекта.
                /// ПРЕДУПРЕЖДЕНИЕ: Временно возвращает локальный размер
                /// </summary>
                /// <returns>Размер объекта по каждой из осей (x, y, z).</returns>
                public Vector3 GetAbsoluteScale()
                {
                    return scale;
                }

                /// <summary>
                /// Устанавливает абсолютный размер объекта.
                /// </summary>
                /// <param name="scale">Размер объекта по каждой из осей (x, y, z).</param>
                public void SetAbsoluteScale(Vector3 scale)
                {
                    throw new NotImplementedException();
                }

                /// <summary>
                /// Получает размер объекта, относительно его родителя
                /// </summary>
                /// <returns>Размер объекта по каждой из осей (x, y, z).</returns>
                public Vector3 GetLocalScale()
                {
                    return scale;
                }

                /// <summary>
                /// Устанавливает размер объекта, относительно его родителя
                /// </summary>
                /// <param name="scale">Размер объекта по каждой из осей (x, y, z).</param>
                public void SetLocalScale(Vector3 scale)
                {
                    this.scale = scale;
                }
            }
        }
    }
}