import React, { useContext, useEffect } from "react";
import { Card, CardBody } from "reactstrap";
import { JobContext } from "../../providers/JobProvider";
import { useHistory } from "react-router-dom";
import { Button } from "reactstrap";
import { Link } from "react-router-dom";

const Job = ({ job }) => {
  const { deleteJob } = useContext(JobContext);

  const jobDelete = () => {
    deleteJob(job.id);
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
          <strong>Description : {job.descritpion}</strong>
        </p>
        <p>
          <strong>{job.CompletionDate}</strong>
        </p>
        <Link to={`/job/edit/${job.id}`}>
          <Button type="button">Edit</Button>
        </Link>
        <Button variant="secondary" onClick={jobDelete} className="btn-primary">
          Delete
        </Button>
      </CardBody>
    </Card>
  );
};

export default Job;
