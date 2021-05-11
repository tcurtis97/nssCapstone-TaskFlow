import React, { useContext, useEffect } from "react";
import { Card, CardBody, CardHeader, CardText } from "reactstrap";
import { WorkRecordContext } from "../../providers/WorkRecordProvider";
import { useHistory } from "react-router-dom";
import { Button } from "reactstrap";
import { Link } from "react-router-dom";

const JobWorkRecord = ({ workRecord }) => {
  const { deleteWorkRecord } = useContext(WorkRecordContext);

  const WorkRecordDelete = () => {
    deleteWorkRecord(workRecord.id);
  };

  return (
    <Card className="m-4">
      <CardHeader>
        <strong>{workRecord.userProfile.displayName}</strong>
      </CardHeader>
      <CardBody>
        <CardText>
          <strong>{workRecord.noteText}</strong>
        </CardText>
        <CardText>
          <strong>{workRecord.createDate}</strong>
        </CardText>
        <CardText>
          <strong>{workRecord.timeOnJob}</strong>
        </CardText>
        <Link to={`/workRecord/edit/${workRecord.id}`}>
          <Button type="button">Edit</Button>
        </Link>
        <Button
          variant="secondary"
          onClick={WorkRecordDelete}
          className="btn-primary"
        >
          Delete
        </Button>
      </CardBody>
    </Card>
  );
};

export default JobWorkRecord;
