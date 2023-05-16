module.exports = function (root) {
  return {
    getRoot: function () { return root; },

    printOrganization: function (position, prefix) {
      let str = `${prefix}+-${position.toString()}\n`;
      for (const p of position.getDirectReports()) {
        str = str.concat(this.printOrganization(p, `${prefix}  `));
      }
      return str;
    },
    // Hire the given person as an employee in the position that has that title
    // Return the newly filled position or undefined if no position has that title
    hire: function (person, title) {
      // recursive depth-first search function
      function search(position, title) {
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

      return search(root, title);
    },


    toString: function () {
      return this.printOrganization(root, '');
    }
  };
};
