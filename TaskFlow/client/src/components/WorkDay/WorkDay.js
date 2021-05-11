import React, { useContext, useEffect } from "react";
import { Card, CardBody } from "reactstrap";
import { JobContext } from "../../providers/JobProvider";
import { WorkDayContext } from "../../providers/WorkDayProvider";
import { useHistory } from "react-router-dom";
import { Button } from "reactstrap";
import { Link } from "react-router-dom";
const WorkDay = ({ workDay }) => {
  const { deleteWorkDay } = useContext(WorkDayContext);
  const history = useHistory();

  const workDayDelete = () => {
    deleteWorkDay(workDay.workDay.id);
  };

  return (
    <Card className="m-4">
      <CardBody>
        <p>
          <strong>{workDay.customer.name}</strong>
        </p>
        <p>
          <strong>{workDay.address.address}</strong>
        </p>
        <p>
          <strong>Description : {workDay.description}</strong>
        </p>
        <p>
          <strong>{workDay.CompletionDate}</strong>
        </p>
        <Link to={`/job/${workDay.id}`}>
          <Button type="button">Details</Button>
        </Link>

        <Button
          variant="secondary"
          onClick={workDayDelete}
          className="btn-primary"
        >
          Delete
        </Button>
      </CardBody>
    </Card>
  );
};

export default WorkDay;
