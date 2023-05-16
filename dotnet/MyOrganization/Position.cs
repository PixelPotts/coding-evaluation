using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal class Position
    {
        private string title;
        private Optional<Employee> employee;
        private HashSet<Position> directReports;

        public Position(string title)
        {
            this.title = title;
            employee = Optional<Employee>.Empty();
            directReports = new HashSet<Position>();
        }

        public Position(string title, Employee employee) : this(title)
        {
            if (employee != null)
                SetEmployee(Optional<Employee>.Of(employee));
        }

        public string GetTitle()
        {
            return title;
        }

        public void SetEmployee(Optional<Employee> employee)
        {
            this.employee = employee;
        }

        public Optional<Employee> GetEmployee()
        {
            return employee;
        }

        public bool IsFilled()
        {
            return employee.IsPresent();
        }

        public bool AddDirectReport(Position position)
        {
            if (position == null)
                throw new Exception("position cannot be null");
            return directReports.Add(position);
        }

        public bool RemovePosition(Position position)
        {
            return directReports.Remove(position);
        }

        public ImmutableList<Position> GetDirectReports()
        {
            return directReports.ToImmutableList();
        }

        public override string ToString()
        {
            return title + (employee.IsPresent() ? ": " + employee.Value.ToString() : "");
        }
    }

}
