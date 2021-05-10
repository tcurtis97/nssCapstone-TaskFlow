import React, { useContext, useEffect } from "react";
import { Card, CardBody } from "reactstrap";
import { JobContext } from "../../providers/JobProvider";
import { WorkDayContext } from "../../providers/WorkDayProvider";
import { useHistory } from "react-router-dom";
import { Button } from "reactstrap";
import { Link } from "react-router-dom";

const Job = ({ job }) => {
  const { deleteJob } = useContext(JobContext);
  const { addWorkDay } = useContext(WorkDayContext);

  const jobDelete = () => {
    deleteJob(job.id);
  };

  const WorkDayAdd = () => {
    addWorkDay({
      JobId: job.id,
      UserProfileId: 0,
    });
  };

  return (
    <Card className="m-4">
      <CardBody>
        <p>
          <strong>{job.customer.name}</strong>
        </p>
        <p>
          <strong>{job.address.address}</strong>
        </p>
        <p>
          <strong>Description : {job.description}</strong>
        </p>
        <p>
          <strong>{job.CompletionDate}</strong>
        </p>
        <Link to={`/job/${job.id}`}>
          <Button type="button">Details</Button>
        </Link>
        <Link to={`/job/edit/${job.id}`}>
          <Button type="button">Edit</Button>
        </Link>
        <Button variant="secondary" onClick={jobDelete} className="btn-primary">
          Delete
        </Button>

        {job.id !== job.workDay.jobId ? (
          <Button
            variant="secondary"
            onClick={WorkDayAdd}
            className="btn-primary"
          >
            Add to workday
          </Button>
        ) : (
          <div> Job added to work list by {job.userProfile.displayName}</div>
        )}
      </CardBody>
    </Card>
  );
};

export default Job;
