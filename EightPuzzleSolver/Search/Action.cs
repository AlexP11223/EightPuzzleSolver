namespace EightPuzzleSolver.Search
{
    public abstract class Action
    {
        /// <summary>
        /// true if it is a "No Operation" action
        /// </summary>
        public virtual bool IsNoOp()
        {
            return false;
        }

        public string Name => GetType().Name;

        public override string ToString()
        {
            return Name;
        }

        protected bool Equals(Action other)
        {
            return other.Name == this.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((Action) obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(Action left, Action right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Action left, Action right)
        {
            return !Equals(left, right);
        }
    }

    public class NoOp : Action
    {
        public override bool IsNoOp()
        {
            return true;
        }
    }
}
