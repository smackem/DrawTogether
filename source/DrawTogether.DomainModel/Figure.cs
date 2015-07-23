namespace DrawTogether.DomainModel
{
    public abstract class Figure
    {
        readonly object sync = new object();
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
}
