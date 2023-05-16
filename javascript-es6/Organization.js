class Organization {
  constructor(root) {
    this.root = root;

    this.printOrganization = (position, prefix) => {
      let str = `${prefix}+-${position.toString()}\n`;
      for (const p of position.getDirectReports()) {
        str = str.concat(this.printOrganization(p, `${prefix}  `));
      }
      return str;
    };

    this.hire = (person, title) => {
      const search = (position, title) => {
        if (position.getTitle() === title) {
          position.setEmployee(person);
          return position;
        }
        for (const report of position.getDirectReports()) {
          const result = search(report, title);
          if (result) {
            return result;
          }
        }
        return undefined;
      }

      return search(this.root, title);
    };

    this.toString = () => this.printOrganization(this.root, '');
  }
}

export default Organization;
