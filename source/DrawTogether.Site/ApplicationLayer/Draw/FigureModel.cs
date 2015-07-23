using DrawTogether.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawTogether.Site.ApplicationLayer.Draw
{
    public class FigureModel
    {
        public FigureKind Kind { get; set; }
        public string UserName { get; set; }
        public string Color { get; set; }
        public Vertex[] Vertices { get; set; }

        public static Figure FigureFromViewModel(FigureModel model)
        {
            switch (model.Kind)
            {
                case FigureKind.Polygon:
                    return new PolygonFigure(model.UserName, ArgbUtils.ArgbFromString(model.Color), model.Vertices);

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
                    Vertices = figure.Vertices.ToArray(),
                };
            }
        }
    }

    public enum FigureKind
    {
        Polygon,
    }
}