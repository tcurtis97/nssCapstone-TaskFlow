import React, { useContext, useEffect } from "react";
import { Card, CardBody, CardHeader, CardText } from "reactstrap";
import { NoteContext } from "../../providers/NoteProvider";
import { useHistory } from "react-router-dom";
import { Button } from "reactstrap";
import { Link } from "react-router-dom";

const Customer = ({ note }) => {
  const { deleteNote } = useContext(NoteContext);

  const NoteDelete = () => {
    deleteNote(note.id);
  };

  return (
    <Card className="m-4">
      <CardHeader>
        <strong>{note.userProfile.displayName}</strong>
      </CardHeader>
      <CardBody>
        <CardText>
          <strong>{note.noteText}</strong>
        </CardText>
        <CardText>
          <strong>{note.createDate}</strong>
        </CardText>
        <Link to={`/note/edit/${note.id}`}>
          <Button type="button">Edit</Button>
        </Link>
        <Button
          variant="secondary"
          onClick={NoteDelete}
          className="btn-primary"
        >
          Delete
        </Button>
      </CardBody>
    </Card>
  );
};

export default Customer;
