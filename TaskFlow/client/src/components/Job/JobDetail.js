import React, { useEffect, useContext, useState } from "react";
import { JobContext } from "../../providers/JobProvider";
import { NoteContext } from "../../providers/NoteProvider";
import { WorkRecordContext } from "../../providers/WorkRecordProvider";
import { useParams } from "react-router-dom";
import JobNote from "./JobNote";
import JobWorkRecord from "./JobWorkRecord";

import { Link } from "react-router-dom";
import { CardHeader, CardText, Button } from "reactstrap";

const JobDetails = () => {
  const [job, SetJob] = useState({
    customer: {},
    address: {},
  });

  const [notes, SetNotes] = useState([]);
  const [workRecords, SetWorkRecords] = useState([]);
  console.log(notes);
  const { GetJobByIdWithDetails, CompleteJob } = useContext(JobContext);
  const { GetAllNotesByJobId } = useContext(NoteContext);
  const { GetAllWorkRecordsByJobId } = useContext(WorkRecordContext);

  const { id } = useParams();

  useEffect(() => {
    console.log("useEffect", id);
    GetJobByIdWithDetails(id).then((response) => {
      SetJob(response);
    });
  }, []);

  useEffect(() => {
    GetAllNotesByJobId(id).then((response) => {
      SetNotes(response);
    });
  }, []);

  useEffect(() => {
    GetAllWorkRecordsByJobId(id).then((response) => {
      SetWorkRecords(response);
    });
  }, []);

  const JobComplete = () => {
    CompleteJob(job.id);
  };

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

      <CardHeader>
        <Link to={`/workRecord/add/${job.id}`}>
          <Button type="button">Add WorkRecord</Button>
        </Link>
        <strong>Work Records:</strong>
        {workRecords.map((w) => (
          <JobWorkRecord key={w.id} workRecord={w} />
        ))}
      </CardHeader>

      <CardHeader>
        <Link to={`/note/add/${job.id}`}>
          <Button type="button">Add note</Button>
        </Link>
        <strong>Notes:</strong>
        {notes.map((n) => (
          <JobNote key={n.id} note={n} />
        ))}
      </CardHeader>

      <Link to={`/note/add/${job.id}`}>
        <Button type="button">Add Note</Button>
      </Link>

      <Button variant="secondary" onClick={JobComplete} className="btn-primary">
        Comeplete Job
      </Button>
    </div>
  );
};

export default JobDetails;
