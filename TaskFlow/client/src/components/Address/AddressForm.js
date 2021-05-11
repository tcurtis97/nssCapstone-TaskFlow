import React, { useContext, useEffect, useState } from "react";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { AddressContext } from "../../providers/AddressProvider";

import { useHistory, useParams } from "react-router-dom";

export const AddressForm = () => {
  const {
    addAddress,
    getAddressById,
    updateAddress,
    getAllAddresses,
  } = useContext(AddressContext);

  const [address, setAddress] = useState({});

  const { customerId } = useParams();

  const history = useHistory();
  const { addressId } = useParams();

  // function to take the values of the form fields and sets those values to state
  const handleControlledInputChange = (event) => {
    const newAddress = { ...address };
    let selectedVal = event.target.value;
    if (event.target.id.includes("Id")) {
      selectedVal = parseInt(selectedVal);
    }

    newAddress[event.target.id] = selectedVal;

    setAddress(newAddress);
  };

  // if there is an addressId in the url the function will run the updateAddress and send a Put request,
  // else the fucntion will run addAddress and run a post request
  const handleClickSaveAddress = () => {
    if (address.Address === "") {
      window.alert("Please enter an Address");
    } else {
      if (addressId) {
        updateAddress({
          id: addressId,
          address: address.address,
        }).then(() => history.push(`/address`));
      } else {
        addAddress({
          address: address.address,
          CustomerId: customerId,
        }).then(() => history.push(`/customer/${customerId}`));
      }
    }
  };

  // useEffect calls getAllAddresses and then if there is an addressId in the url, the addressId will be passed into getAddressById and set the
  // response to state for the edit feature to show the object being edited
  useEffect(() => {
    getAllAddresses().then(() => {
      if (addressId) {
        getAddressById(addressId).then((c) => {
          setAddress(c);
        });
      } else {
      }
    });
  }, []);

  return (
    <Form className="addressForm">
      <h2 className="addressForm__title">
        {addressId ? "Save Address" : "Add Address"}
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
            <Label htmlFor="address">Address name:</Label>
            <Input
              type="text"
              id="address"
              onChange={handleControlledInputChange}
              required
              autoFocus
              className="form-control"
              value={address.address}
              placeholder="Address"
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
            handleClickSaveAddress();
          }}
        >
          {addressId ? "Save Address" : "Add Address"}
        </Button>
      </div>
    </Form>
  );
};
