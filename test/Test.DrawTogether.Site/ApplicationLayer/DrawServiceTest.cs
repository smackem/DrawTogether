using DrawTogether.DomainModel;
using DrawTogether.Site.ApplicationLayer.Draw;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DrawTogether.Site.ApplicationLayer
{
    [TestFixture]
    public class DrawServiceTest
    {
        [Test]
        public void TestFigureModel()
        {
            var serializer = JsonSerializer.Create();
            var figureModel = new FigureModel
            {
                Kind = FigureKind.Polygon,
                UserName = "user",
                Color = "#ffee99",
                Vertices = new[]
                {
                    new VertexModel { X = 10, Y = 20 },
                }
            };

            var buffer = new StringBuilder();
            using (var writer = new StringWriter(buffer))
                serializer.Serialize(writer, figureModel);

            var text = buffer.ToString();
            Console.WriteLine("{0}", text);

            FigureModel deserialized;

            using (var reader = new JsonTextReader(new StringReader(text)))
                deserialized = serializer.Deserialize<FigureModel>(reader);

            Assert.That(deserialized, Is.Not.Null);
            Assert.That(deserialized.Kind, Is.EqualTo(figureModel.Kind));
            Assert.That(deserialized.Color, Is.EqualTo(figureModel.Color));
            Assert.That(deserialized.UserName, Is.EqualTo(figureModel.UserName));
            Assert.That(deserialized.Vertices.Length, Is.EqualTo(1));
            Assert.That(deserialized.Vertices[0].X, Is.EqualTo(figureModel.Vertices[0].X));
            Assert.That(deserialized.Vertices[0].Y, Is.EqualTo(figureModel.Vertices[0].Y));
        }
    }
}
