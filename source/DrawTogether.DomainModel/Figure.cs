using System.Diagnostics.Contracts;

namespace DrawTogether.DomainModel
{
    [ContractClass(typeof(FigureContract))]
    public abstract class Figure
    {
        readonly string userName;

        protected Figure(string userName, Argb color)
        {
            this.userName = userName;

            Color = color;
        }

        public Argb Color { get; set; }

        public string UserName
        {
            get { return this.userName; }
        }

        public abstract TResult Accept<TState, TResult>(
            IFigureVisitor<TState, TResult> visitor, TState state);
    }

    [ContractClassFor(typeof(Figure))]
    abstract class FigureContract : Figure
    {
        FigureContract() : base(null, Argb.Empty)
        { }

        public override TResult Accept<TState, TResult>(IFigureVisitor<TState, TResult> visitor, TState state)
        {
            Contract.Requires(visitor != null);

            return default(TResult);
        }
    }
}
