using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Scene.SceneObject.Tests
{
    [TestClass]
    public class HierarchyTest
    {
        [TestMethod]
        // Ошибка, при попытке установить объект своим потомком
        public void notSelfChild()
        {
            var obj = new SceneObject();
            var obj2 = new SceneObject();
            var obj3 = new SceneObject();

            obj.Hierarchy.AddChild(obj2);
            obj2.Hierarchy.AddChild(obj3);

            // Вложенность 1-го уровня
            Assert.ThrowsException<ArgumentException>(() => obj.Hierarchy.Parent = obj);

            // Вложенность несколько уровней
            Assert.ThrowsException<ArgumentException>(() => obj.Hierarchy.Parent = obj3);
        }

        [TestMethod]
        // Добавление потомков
        public void addGetChild()
        {
            var obj = new SceneObject();
            var obj2 = new SceneObject();
            var obj3 = new SceneObject();

            obj.Hierarchy.AddChild(obj2);
            obj3.Hierarchy.Parent = obj;

            Assert.IsTrue(obj.Hierarchy.GetChildren().Contains(obj2) && obj.Hierarchy.GetChildren().Contains(obj3));
        }

        [TestMethod]
        // Отсуствие копий потомка при повторном добавлении
        public void addChildNoCopy()
        {
            var obj = new SceneObject();
            var obj2 = new SceneObject();

            obj.Hierarchy.AddChild(obj2);
            obj.Hierarchy.AddChild(obj2);
            obj.Hierarchy.AddChild(obj2);

            Assert.IsTrue(obj.Hierarchy.GetChildren().Count == 1);
        }

        [TestMethod]
        // Поиск по имени
        public void getByName()
        {
            var obj = new SceneObject("obj1");
            var obj2 = new SceneObject("obj2");
            var obj3 = new SceneObject("obj3");
            var obj4_2 = new SceneObject("obj2");
            var obj5 = new SceneObject("obj4");

            obj.Hierarchy.AddChild(obj2);
            obj.Hierarchy.AddChild(obj3);
            obj.Hierarchy.AddChild(obj4_2);
            obj.Hierarchy.AddChild(obj5);

            var found = obj.Hierarchy.GetChildrenByName("obj2");
            Assert.IsTrue(found.Contains(obj2) && found.Contains(obj4_2) && found.Count == 2);
        }

        [TestMethod]
        // Изменение родителя
        public void isParentChange()
        {
            var obj = new SceneObject();
            var obj2 = new SceneObject();
            var obj3 = new SceneObject();
            var obj4 = new SceneObject();

            obj.Hierarchy.AddChild(obj2);
            obj.Hierarchy.AddChild(obj3);

            obj2.Hierarchy.AddChild(obj3);
            obj4.Hierarchy.Parent = obj2;

            Assert.IsTrue(!obj.Hierarchy.GetChildren().Contains(obj3) && obj2.Hierarchy.GetChildren().Contains(obj3) && obj3.Hierarchy.Parent == obj2);
            Assert.IsTrue(!obj.Hierarchy.GetChildren().Contains(obj4) && obj2.Hierarchy.GetChildren().Contains(obj4) && obj4.Hierarchy.Parent == obj2);

        }
    }
}
