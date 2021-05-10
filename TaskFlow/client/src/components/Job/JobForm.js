import React, { useContext, useEffect, useState } from "react";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { JobContext } from "../../providers/JobProvider";
import { CustomerContext } from "../../providers/CustomerProvider";
import { AddressContext } from "../../providers/AddressProvider";
import { useHistory, useParams } from "react-router-dom";

export const JobForm = () => {
  const { addJob, getJobById, updateJob, getAllJobs } = useContext(JobContext);
  const { customers, getAllCustomers } = useContext(CustomerContext);
  const { GetAllAddressesByCustomerId } = useContext(AddressContext);

  const [Addresses, SetAddresses] = useState([]);
  console.log(Addresses, "string");

  const [job, setJob] = useState({
    description: "",
    customerId: 0,
    addressId: 0,
  });

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
        description: job.description,
        customerId: job.customerId,
        addressId: job.addressId,
      }).then(() => history.push(`/job`));
    }
  };

  useEffect(() => {
    getAllCustomers();
  }, []);

  // useEffect(() => {
  //   GetAllAddressesByCustomerId(job.customerId).then((response) => {
  //     SetAddresses(response);
  //   });
  // }, []);

  const getAddresses = () => {
    GetAllAddressesByCustomerId(job.customerId).then((response) => {
      SetAddresses(response);
    });
  };

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
            <Label htmlFor="description">Job Description:</Label>
            <Input
              type="text"
              id="description"
              onChange={handleControlledInputChange}
              required
              autoFocus
              className="form-control"
              value={job.description}
              placeholder="Job Description"
            />
          </div>
        </fieldset>

        {/* <fieldset>
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
        </fieldset> */}

        <FormGroup>
          <select
            id="customerId"
            onSelect={getAddresses}
            onChange={handleControlledInputChange}
          >
            <option value="0">Select a customer </option>
            {customers.map((c) => (
              <option key={c.id} value={c.id}>
                {c.name}
              </option>
            ))}
          </select>
        </FormGroup>

        {job.customerId !== 0 ? (
          <div className="Address_card">
            <select id="addressId" onChange={handleControlledInputChange}>
              <option value="0">Select an address </option>
              {Addresses.map((a) => (
                <option key={a.id} value={a.id}>
                  {a.address}
                </option>
              ))}
            </select>
          </div>
        ) : (
          <div> Please Choose a customer</div>
        )}

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
