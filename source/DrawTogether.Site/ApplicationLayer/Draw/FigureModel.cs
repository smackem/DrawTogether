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

        public static FigureModel FromFigure(Figure figure)
        {
            var viewModel = figure.Accept(new FigureConverter(), null);
            viewModel.UserName = viewModel.UserName;
            viewModel.Color = ArgbUtils.ArgbToString(figure.Color);
            return viewModel;
        }

        public Figure ToFigure()
        {
            switch (Kind)
            {
                case FigureKind.Polygon:
                    return new PolygonFigure(UserName, ArgbUtils.ArgbFromString(Color),
                        Vertices.Select(v => v.ToVertex()));

                default:
                    throw new ArgumentException("Unsupported FigureKind: " + Kind);
            }
        }

        ///////////////////////////////////////////////////////////////////////

        class FigureConverter : IFigureVisitor<object, FigureModel>
        {
            public FigureModel visit(PolygonFigure figure, object state)
            {
                return new FigureModel
                {
                    Kind = FigureKind.Polygon,
                    Vertices = figure.Vertices
                        .Select(v => VertexModel.FromVertex(v))
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

        public static VertexModel FromVertex(Vertex vertex)
        {
            return new VertexModel { X = vertex.X, Y = vertex.Y };
        }

        public Vertex ToVertex()
        {
            return new Vertex(X, Y);
        }
    }
}
