import React, { useEffect, useContext, useState } from "react";
import { JobContext } from "../../providers/JobProvider";
import { NoteContext } from "../../providers/NoteProvider";
import { useParams } from "react-router-dom";

import { Link } from "react-router-dom";
import { CardHeader, CardText, Button } from "reactstrap";

const JobDetails = () => {
  const [job, SetJob] = useState({
    customer: {},
    address: {},
  });

  const { GetJobByIdWithDetails } = useContext(JobContext);
  const { getAllNotes, notes } = useContext(NoteContext);

  const { id } = useParams();

  useEffect(() => {
    console.log("useEffect", id);
    GetJobByIdWithDetails(id).then((response) => {
      SetJob(response).then(getAllNotes);
    });
  }, []);

  let jobNotes = notes?.map((n) => n.jobId == job.id);
  console.log(jobNotes);

  if (!job) {
    return null;
  }

  return (
    <div className="container">
      <CardHeader>
        <strong>{job.customer.name}</strong>
      </CardHeader>
      <CardHeader>
        <strong>{job.customer.phoneNumber}</strong>
      </CardHeader>
      <CardHeader>
        <strong>{job.address.address}</strong>
      </CardHeader>

      <CardText>
        <strong>{job.descritpion}</strong>
      </CardText>

      <Link to={`/note/add/${job.id}`}>
        <Button type="button">Add Note</Button>
      </Link>
    </div>
  );
};

export default JobDetails;
