using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        public Optional<Position> Hire(Name person, string title)
        {
            Optional<Position> positionOptional = FindPosition(root, title);
            positionOptional.IfPresent(position => {
                int identifier = person.GetHashCode();
                Employee employee = new Employee(identifier, person);
                position.SetEmployee(Optional<Employee>.Of(employee));
            });
            return positionOptional;
        }

        private Optional<Position> FindPosition(Position position, string title)
        {
            if (position.GetTitle().Equals(title))
            {
                return Optional<Position>.Of(position);
            }

            foreach (Position directReport in position.GetDirectReports())
            {
                Optional<Position> nestedPosition = FindPosition(directReport, title);
                if (nestedPosition.IsPresent())
                {
                    return nestedPosition;
                }
            }

            return Optional<Position>.Empty();
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "\t"));
            }
            return sb.ToString();
        }
    }
}

public class Optional<T>
{
    private T _value;

    public Optional(T value)
    {
        if (value == null)
            throw new ArgumentNullException();
        _value = value;
    }

    public bool IsPresent() => _value != null;

    public T Value
    {
        get
        {
            if (_value == null)
                throw new InvalidOperationException();
            return _value;
        }
    }

    public void IfPresent(Action<T> action)
    {
        if (IsPresent())
            action(_value);
    }

    public static Optional<T> Of(T value) => new Optional<T>(value);
    public static Optional<T> Empty() => new Optional<T>(default(T));
}
