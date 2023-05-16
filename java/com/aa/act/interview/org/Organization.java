package com.aa.act.interview.org;

import java.util.Optional;

public abstract class Organization {

    private Position root;

    public Organization() {
        root = createOrganization();
    }

    protected abstract Position createOrganization();

    public Optional<Position> hire(Name person, String title) {
        Optional<Position> positionOptional = findPosition(root, title);
        positionOptional.ifPresent(position -> {
            int identifier = person.hashCode();
            Employee employee = new Employee(identifier, person);
            position.setEmployee(Optional.of(employee));
        });
        return positionOptional;
    }

    private Optional<Position> findPosition(Position position, String title) {
        if (position.getTitle().equals(title)) {
            return Optional.of(position);
        }

        for (Position directReport : position.getDirectReports()) {
            Optional<Position> nestedPosition = findPosition(directReport, title);
            if (nestedPosition.isPresent()) {
                return nestedPosition;
            }
        }

        return Optional.empty();
    }

    @Override
    public String toString() {
        return printOrganization(root, "");
    }

    private String printOrganization(Position pos, String prefix) {
        StringBuilder sb = new StringBuilder(prefix + "+-" + pos.toString() + "\n");
        for(Position p : pos.getDirectReports()) {
            sb.append(printOrganization(p, prefix + "\t"));
        }
        return sb.toString();
    }
}
