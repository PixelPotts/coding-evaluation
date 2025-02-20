import Position from './Position';
import Name from './Name';
import Employee from './Employee';

abstract class Organization {
  protected root: Position;

  constructor() {
    this.root = this.createOrganization();
  }

  protected abstract createOrganization(): Position;

  printOrganization = (position: Position, prefix: string): string => {
    let str = `${prefix}+-${position}\n`;
    for (const p of position.getDirectReports()) {
      str = str.concat(this.printOrganization(p, `${prefix}  `));
    }
    return str;
  };

  // Hire the given person as an employee in the position that has that title
  // Return the newly filled position or undefined if no position has that title
  hire = (person: Name, title: string): Position | undefined => {
    let employeeId = 1; // Initialize the identifier counter

    const assignEmployeeId = (): number => {
      return employeeId++;
    };

    const search = (position: Position, title: string): Position | undefined => {
      if (position.getTitle() === title) {
        if (position.isFilled()) {
          return undefined;
        }
        const employee = new Employee(assignEmployeeId(), person);
        position.setEmployee(employee);
        return position;
      }
      for (const report of position.getDirectReports()) {
        const result = search(report, title);
        if (result) {
          return result;
        }
      }
      return undefined;
    };

    return search(this.root, title);
  };
;

  toString = (): string => this.printOrganization(this.root, '');
}

export default Organization;
