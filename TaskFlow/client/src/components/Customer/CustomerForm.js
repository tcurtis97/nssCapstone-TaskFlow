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
    if (customer.name === "" || customer.phoneNumber === "") {
      window.alert("Please enter a name");
    } else {
      setIsLoading(true);

      if (customerId) {
        updateCustomer({
          id: customerId,
          name: customer.name,
          phoneNumber: customer.phoneNumber,
        }).then(() => history.push(`/customer`));
      } else {
        addCustomer({
          name: customer.name,
          phoneNumber: customer.phoneNumber,
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
            <Label htmlFor="name">Customer name:</Label>
            <Input
              type="text"
              id="name"
              onChange={handleControlledInputChange}
              required
              autoFocus
              className="form-control"
              value={customer.name}
              placeholder="Customer name"
            />
          </div>
        </fieldset>

        <fieldset>
          <div className="form-group">
            <Label htmlFor="phoneNumber">Phone Number:</Label>
            <Input
              type="tel"
              id="phoneNumber"
              pattern="[0-9]{3}-[0-9]{3}-[0-9]{4}"
              onChange={handleControlledInputChange}
              required
              autoFocus
              className="form-control"
              value={customer.phoneNumber}
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
