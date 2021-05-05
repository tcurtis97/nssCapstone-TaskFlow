import React, { useContext, useEffect, useState } from "react";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { CustomerContext } from "../../providers/CustomerProvider";

import { useHistory, useParams } from "react-router-dom";

export const CustomerForm = () => {
  const {
    addCustomer,
    getCustomerById,
    updateCustomer,
    getAllCustomers,
  } = useContext(CustomerContext);

  const [customer, setCustomer] = useState({
    Name: "",
    PhoneNumber: "",
  });

  const [isLoading, setIsLoading] = useState(true);

  const history = useHistory();
  const { customerId } = useParams();

  const handleControlledInputChange = (event) => {
    const newCustomer = { ...customer };
    let selectedVal = event.target.value;
    if (event.target.id.includes("Id")) {
      selectedVal = parseInt(selectedVal);
    }

    newCustomer[event.target.id] = selectedVal;

    setCustomer(newCustomer);
  };

  const handleClickSaveCustomer = () => {
    if (customer.Name === "" || customer.PhoneNumber === "") {
      window.alert("Please enter a name");
    } else {
      setIsLoading(true);

      if (customerId) {
        updateCustomer({
          id: customerId,
          Name: customer.Name,
          PhoneNumber: customer.PhoneNumber,
        }).then(() => history.push(`/customer`));
      } else {
        addCustomer({
          Name: customer.Name,
          PhoneNumber: customer.PhoneNumber,
        }).then(() => history.push(`/customer`));
      }
    }
  };

  useEffect(() => {
    getAllCustomers().then(() => {
      if (customerId) {
        getCustomerById(customerId).then((c) => {
          setCustomer(c);
          setIsLoading(false);
        });
      } else {
        setIsLoading(false);
      }
    });
  }, []);

  return (
    <Form className="customerForm">
      <h2 className="customerForm__title">
        {customerId ? "Save Customer" : "Add Customer"}
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
            <Label htmlFor="Name">Customer name:</Label>
            <Input
              type="text"
              id="Name"
              onChange={handleControlledInputChange}
              required
              autoFocus
              className="form-control"
              value={customer.Name}
              placeholder="Customer name"
            />
          </div>
        </fieldset>

        <fieldset>
          <div className="form-group">
            <Label htmlFor="PhoneNumber">Phone Number:</Label>
            <Input
              type="text"
              id="PhoneNumber"
              onChange={handleControlledInputChange}
              required
              autoFocus
              className="form-control"
              value={customer.PhoneNumber}
              placeholder="Phone Number"
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
            handleClickSaveCustomer();
          }}
        >
          {customerId ? "Save Customer" : "Add Customer"}
        </Button>
      </div>
    </Form>
  );
};
