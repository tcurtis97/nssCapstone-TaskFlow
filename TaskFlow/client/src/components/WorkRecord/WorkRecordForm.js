import React, { useContext, useEffect, useState } from "react";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { WorkRecordContext } from "../../providers/WorkRecordProvider";

import { useHistory, useParams } from "react-router-dom";

export const WorkRecordForm = () => {
  const {
    addWorkRecord,
    getWorkRecordById,
    updateWorkRecord,
    getAllWorkRecords,
  } = useContext(WorkRecordContext);

  const [workRecord, setWorkRecord] = useState({
    WorkRecord: "",
  });

  const { jobId } = useParams();

  const [isLoading, setIsLoading] = useState(true);

  const history = useHistory();
  const { workRecordId } = useParams();

  const handleControlledInputChange = (event) => {
    const newWorkRecord = { ...workRecord };
    let selectedVal = event.target.value;
    if (event.target.id.includes("Id")) {
      selectedVal = parseInt(selectedVal);
    }

    newWorkRecord[event.target.id] = selectedVal;

    setWorkRecord(newWorkRecord);
  };

  const handleClickSaveWorkRecord = () => {
    if (workRecord.NoteText === "" || workRecord.TimeOnJob === "") {
      window.alert("Please enter an WorkRecord");
    } else {
      setIsLoading(true);

      if (workRecordId) {
        updateWorkRecord({
          id: workRecordId,
          NoteText: workRecord.NoteText,
          TimeOnJob: workRecord.TimeOnJob,
        }).then(() => history.push(`/workRecord`));
      } else {
        addWorkRecord({
          NoteText: workRecord.NoteText,
          TimeOnJob: workRecord.TimeOnJob,
          JobId: jobId,
        }).then(() => history.push(`/job/${jobId}`));
        // for (values of fields) {
        //   addWorkRecord({
        //     WorkRecord: values.value,
        //   })
      }
    }
  };

  useEffect(() => {
    getAllWorkRecords().then(() => {
      if (workRecordId) {
        getWorkRecordById(workRecordId).then((c) => {
          setWorkRecord(c);
          setIsLoading(false);
        });
      } else {
        setIsLoading(false);
      }
    });
  }, []);

  return (
    <Form className="workRecordForm">
      <h2 className="workRecordForm__title">
        {workRecordId ? "Save WorkRecord" : "Add WorkRecord"}
      </h2>

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
            <Label htmlFor="NoteText">WorkRecord Text:</Label>
            <Input
              type="text"
              id="NoteText"
              onChange={handleControlledInputChange}
              required
              autoFocus
              className="form-control"
              value={workRecord.NoteText}
              placeholder="NoteText"
            />
          </div>
        </fieldset>

        <fieldset>
          <div className="form-group">
            <Label htmlFor="TimeOnJob">WorkRecord Text:</Label>
            <Input
              type="number"
              id="TimeOnJob"
              onChange={handleControlledInputChange}
              required
              autoFocus
              className="form-control"
              value={workRecord.TimeOnJob}
              placeholder="TimeOnJob"
            />
          </div>
        </fieldset>

        <Button
          style={{
            color: "black",
          }}
          className="add_button"
          disabled={isLoading}
          onClick={(event) => {
            event.preventDefault();
            handleClickSaveWorkRecord();
          }}
        >
          {workRecordId ? "Save WorkRecord" : "Add WorkRecord"}
        </Button>
      </div>
    </Form>
  );
};
