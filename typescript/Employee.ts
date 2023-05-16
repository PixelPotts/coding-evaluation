import Name from './Name';

class Employee {
  constructor(private readonly identifier: number, private readonly name: Name) { }

  toString = () => `${this.name.toString()}`;
}

export default Employee;
