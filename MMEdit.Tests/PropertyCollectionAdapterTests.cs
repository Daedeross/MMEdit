namespace MMEdit.Tests
{
    using MMEdit.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;


    public class PropertyCollectionAdapterTests
    {
        public class SimpleClass
        {
            public byte Prop1 { get; set; }
            public int Prop2 { get; set; }
            public string Prop3 { get; set; } = string.Empty;

        }

        public class ComplexClass
        {
            public byte Prop1 { get; set; }
            public int[] Prop2 { get; set; } = Array.Empty<int>();
            public SimpleClass Prop3 { get; set; } = new(); 

        }

        public static IEnumerable<object[]> PrimitiveTypeData()
        {
            return new []
            {
                new object [] { typeof(int), 1 },
                new object [] { typeof(byte), (byte)1 },
                new object [] { typeof(string), "data"  },
                new object [] { typeof(DateTime), new DateTime(2020, 2, 1) },
            };
        }

        [Theory]
        [MemberData(nameof(PrimitiveTypeData))]
        public void MakeConstructorTest(Type type, object value)
        {
            bool setterCalled = false;

            Action<object?> setter = (o) => { setterCalled = true; };
            var input = new object[] { type, "test", value, setter };

            var adapter = new PropertyCollectionAdapter();

            var func = adapter.MakeConstructor(type);

            var result = func(type, "test", value, setter) as IProperty;

            Assert.NotNull(result);
            result.Commit();

            Assert.True(setterCalled);
        }

        [Theory]
        [MemberData(nameof(PrimitiveTypeData))]
        public void PropertyAdapterCreatesSimpleProperties(Type type, object value)
        {
            Action<object?> setter = (o) => { };
            var adapter = new PropertyCollectionAdapter();

            var property = adapter.CreateProperty(type, "root", value, setter);

            Assert.NotNull(property);

            Assert.Equal(type, property.Type);
            Assert.Equal(value, property.Value);
        }

        [Fact]
        public void PropertyAdapterCreatesClassProperties()
        {
            Action<object?> setter = (o) => { };
            var value = new SimpleClass
            {
                Prop1 = 1,
                Prop2 = 2,
                Prop3 = "3"
            };

            var adapter = new PropertyCollectionAdapter();

            var property = adapter.CreateProperty(typeof(SimpleClass), "root", value, setter);

            Assert.NotNull(property);
            AssertSimpleClass(property);
        }

        private static void AssertSimpleClass(IProperty property)
        {
            Assert.IsAssignableFrom<ICollectionProperty>(property);
            var collection = (ICollectionProperty)property;
            Assert.Equal(typeof(SimpleClass), property.Type);
            Assert.Collection(collection.Children.OrderBy(p => p.Name),
                p1 =>
                {
                    Assert.Equal(nameof(SimpleClass.Prop1), p1.Name);
                    Assert.Equal(typeof(byte), p1.Type);
                    Assert.IsType<byte>(p1.Value);
                    Assert.Equal<byte>(1, (byte)p1.Value);
                },
                p2 =>
                {
                    Assert.Equal(nameof(SimpleClass.Prop2), p2.Name);
                    Assert.Equal(typeof(int), p2.Type);
                    Assert.IsType<int>(p2.Value);
                    Assert.Equal(2, p2.Value);
                },
                p3 =>
                {
                    Assert.Equal(nameof(SimpleClass.Prop3), p3.Name);
                    Assert.Equal(typeof(string), p3.Type);
                    Assert.IsType<string>(p3.Value);
                    Assert.Equal("3", p3.Value);
                });
        }

        [Fact]
        public void PropertyAdapterCreatesRecursiveStructure()
        {
            Action<object?> setter = (o) => { };
            var prop3 = new SimpleClass
            {
                Prop1 = 1,
                Prop2 = 2,
                Prop3 = "3"
            };

            var value = new ComplexClass
            {
                Prop1 = 4,
                Prop2 = new int[] { 2, 3, 5 },
                Prop3 = prop3
            };

            var adapter = new PropertyCollectionAdapter();

            var property = adapter.CreateProperty(typeof(ComplexClass), "root", value, setter);

            Assert.NotNull(property);
            Assert.IsAssignableFrom<ICollectionProperty>(property);
            var collection = (ICollectionProperty)property;

            Assert.Equal(typeof(ComplexClass), property.Type);

            Assert.Collection(collection.Children.OrderBy(p => p.Name),
                p1 =>
                {
                    Assert.Equal(nameof(ComplexClass.Prop1), p1.Name);
                    Assert.Equal(typeof(byte), p1.Type);
                    Assert.IsType<byte>(p1.Value);
                    Assert.Equal<byte>(4, (byte)p1.Value);
                },
                p2 =>
                {
                    Assert.Equal(nameof(ComplexClass.Prop2), p2.Name);
                    Assert.Equal(typeof(int[]), p2.Type);
                    Assert.IsAssignableFrom<ICollectionProperty>(p2);
                    var collectionProperty = (ICollectionProperty)p2;
                    Assert.All(collectionProperty.Children,
                        child =>
                        {
                            Assert.Equal(typeof(int), child.Type);
                            Assert.IsType<int>(child.Value);
                        });
                    Assert.Collection(collectionProperty.Children,
                        c1 => Assert.Equal(2, c1.Value),
                        c2 => Assert.Equal(3, c2.Value),
                        c3 => Assert.Equal(5, c3.Value));
                },
                p3 =>
                {
                    Assert.Equal(nameof(ComplexClass.Prop3), p3.Name);
                    Assert.Equal(typeof(SimpleClass), p3.Type);
                    Assert.IsAssignableFrom<ICollectionProperty>(p3);
                    AssertSimpleClass((ICollectionProperty)p3);
                });

        }
    }
}
