import React, { useContext, useEffect, useState } from "react";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { NoteContext } from "../../providers/NoteProvider";

import { useHistory, useParams } from "react-router-dom";

export const NoteForm = () => {
  const { addNote, getNoteById, updateNote, getAllNotes } =
    useContext(NoteContext);

  const [note, setNote] = useState({
    Note: "",
  });

  const { jobId } = useParams();

  const history = useHistory();
  const { noteId } = useParams();

  // function to take the values of the form fields and sets those values to state
  const handleControlledInputChange = (event) => {
    const newNote = { ...note };
    let selectedVal = event.target.value;
    if (event.target.id.includes("Id")) {
      selectedVal = parseInt(selectedVal);
    }

    newNote[event.target.id] = selectedVal;

    setNote(newNote);
  };

  // if there is an noteId in the url the function will run the updateNote and send a Put request,
  // else the fucntion will run addNote and run a post request
  const handleClickSaveNote = () => {
    if (note.noteText === "") {
      window.alert("Please enter an Note");
    } else {
      if (noteId) {
        updateNote({
          id: noteId,
          noteText: note.noteText,
        }).then(() => history.push(`/note`));
      } else {
        addNote({
          noteText: note.noteText,
          JobId: jobId,
        }).then(() => history.push(`/job/${jobId}`));
      }
    }
  };

  // useEffect calls getAllNotes and then if there is an noteId in the url, the noteId will be passed into getNoteById and set the
  // response to state for the edit feature to show the object being edited
  useEffect(() => {
    getAllNotes().then(() => {
      if (noteId) {
        getNoteById(noteId).then((c) => {
          setNote(c);
        });
      } else {
      }
    });
  }, []);

  return (
    <Form className="noteForm">
      <h2 className="noteForm__title">{noteId ? "Save Note" : "Add Note"}</h2>

      <Button
        variant
        className="back_button"
        onClick={() => {
          history.goBack();
        }}
      >
        Back
      </Button>

      <div className="form_background">
        <fieldset>
          <div className="form-group">
            <Label htmlFor="noteText">Note Text:</Label>
            <Input
              type="text"
              id="noteText"
              onChange={handleControlledInputChange}
              required
              autoFocus
              className="form-control"
              value={note.noteText}
              placeholder="NoteText"
            />
          </div>
        </fieldset>

        <Button
          style={{
            color: "black",
          }}
          className="add_button"
          onClick={(event) => {
            event.preventDefault();
            handleClickSaveNote();
          }}
        >
          {noteId ? "Save Note" : "Add Note"}
        </Button>
      </div>
    </Form>
  );
};
