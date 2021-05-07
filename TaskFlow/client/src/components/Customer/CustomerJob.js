import React, { useContext, useEffect } from "react";
import { Card, CardBody, CardHeader, CardText } from "reactstrap";
import { JobContext } from "../../providers/JobProvider";
import { useHistory } from "react-router-dom";
import { Button } from "reactstrap";
import { Link } from "react-router-dom";

const CustomerJob = ({ job }) => {
  const { deleteJob } = useContext(JobContext);

  const JobDelete = () => {
    deleteJob(job.id);
  };

  return (
    <Card className="m-4">
      <CardHeader>
        <strong>{job.descritpion}</strong>
      </CardHeader>
      <CardBody>
        <CardText>{job.createDate}</CardText>
        <Link to={`/Job/edit/${job.id}`}>
          <Button type="button">Edit</Button>
        </Link>
        <Button variant="secondary" onClick={JobDelete} className="btn-primary">
          Delete
        </Button>
      </CardBody>
    </Card>
  );
};

export default CustomerJob;
