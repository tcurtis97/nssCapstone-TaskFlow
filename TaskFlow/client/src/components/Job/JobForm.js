import React, { useContext, useEffect, useState } from "react";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { JobContext } from "../../providers/JobProvider";
import { CustomerContext } from "../../providers/CustomerProvider";
import { useHistory, useParams } from "react-router-dom";

export const JobForm = () => {
  const { addJob, getJobById, updateJob, getAllJobs } = useContext(JobContext);
  const { customers, getAllCustomers } = useContext(CustomerContext);

  const [job, setJob] = useState({});

  const history = useHistory();

  const handleControlledInputChange = (event) => {
    const newJob = { ...job };
    let selectedVal = event.target.value;
    if (event.target.id.includes("Id")) {
      selectedVal = parseInt(selectedVal);
    }

    newJob[event.target.id] = selectedVal;

    setJob(newJob);
  };

  const handleClickSaveJob = () => {
    if (job.description === "") {
      window.alert("Please enter a Descritpion");
    } else {
      addJob({
        Description: job.description,
        Customer: job.customer,
      }).then(() => history.push(`/job`));
    }
  };

  useEffect(() => {
    getAllCustomers();
  }, []);

  return (
    <Form className="customerForm">
      <h2 className="customerForm__title">Add Job</h2>

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
            <Label htmlFor="Description">Job Description:</Label>
            <Input
              type="text"
              id="Description"
              onChange={handleControlledInputChange}
              required
              autoFocus
              className="form-control"
              value={job.description}
              placeholder="Job Description"
            />
          </div>
        </fieldset>

        <fieldset>
          <div className="form-group">
            <Label htmlFor="ImageUrl">ImageUrl:</Label>
            <Input
              type="text"
              id="ImageUrl"
              onChange={handleControlledInputChange}
              required
              autoFocus
              className="form-control"
              value={job.ImageUrl}
              placeholder="ImageUrl"
            />
          </div>
        </fieldset>

        <FormGroup>
          <Label htmlFor="Customers">Customers:</Label>
          <Input
            type="text"
            id="Customer"
            onChange={handleControlledInputChange}
            required
            autoFocus
            className="form-control"
            value={job.customer}
            placeholder="Find a Customer"
            list="customerData"
          />
          <datalist id="customerData">
            {customers.map((c) => (
              <option key={c.Id} value={c.id} />
            ))}
          </datalist>
          ))
        </FormGroup>

        <Button
          style={{
            color: "black",
          }}
          className="add_button"
          onClick={(event) => {
            event.preventDefault();
            handleClickSaveJob();
          }}
        >
          Add Job
        </Button>
      </div>
    </Form>
  );
};
