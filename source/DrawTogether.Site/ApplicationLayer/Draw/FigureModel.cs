using DrawTogether.DomainModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawTogether.Site.ApplicationLayer.Draw
{
    public class FigureModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public FigureKind Kind { get; set; }

        public string UserName { get; set; }
        public string Color { get; set; }
        public VertexModel[] Vertices { get; set; }

        public static Figure FigureFromViewModel(FigureModel model)
        {
            switch (model.Kind)
            {
                case FigureKind.Polygon:
                    return new PolygonFigure(model.UserName, ArgbUtils.ArgbFromString(model.Color),
                        model.Vertices.Select(v => VertexModel.VertexFromViewModel(v)));

                default:
                    throw new ArgumentException("Unsupported FigureKind: " + model.Kind);
            }
        }

        public static FigureModel ViewModelFromFigure(Figure figure)
        {
            var viewModel = figure.Accept(new FigureConverter(), null);
            viewModel.UserName = viewModel.UserName;
            viewModel.Color = ArgbUtils.ArgbToString(figure.Color);
            return viewModel;
        }

        class FigureConverter : IFigureVisitor<object, FigureModel>
        {
            public FigureModel visit(PolygonFigure figure, object state)
            {
                return new FigureModel
                {
                    Kind = FigureKind.Polygon,
                    Vertices = figure.Vertices
                        .Select(v => VertexModel.ViewModelFromVertex(v))
                        .ToArray(),
                };
            }
        }
    }

    public enum FigureKind
    {
        Polygon,
    }

    public class VertexModel
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static VertexModel ViewModelFromVertex(Vertex vertex)
        {
            return new VertexModel { X = vertex.X, Y = vertex.Y };
        }

        public static Vertex VertexFromViewModel(VertexModel model)
        {
            return new Vertex(model.X, model.Y);
        }
    }

    public class TestModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public FigureKind Kind { get; set; }
        public string UserName { get; set; }
        public string Color { get; set; }
        public VertexModel[] Vertices { get; set; }
    }
}