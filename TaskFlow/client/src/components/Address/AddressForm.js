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

  const [isLoading, setIsLoading] = useState(true);

  const history = useHistory();
  const { addressId } = useParams();

  const handleControlledInputChange = (event) => {
    const newAddress = { ...address };
    let selectedVal = event.target.value;
    if (event.target.id.includes("Id")) {
      selectedVal = parseInt(selectedVal);
    }

    newAddress[event.target.id] = selectedVal;

    setAddress(newAddress);
  };

  const handleClickSaveAddress = () => {
    if (address.Address === "") {
      window.alert("Please enter an Address");
    } else {
      setIsLoading(true);

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
        // for (values of fields) {
        //   addAddress({
        //     Address: values.value,
        //   })
      }
    }
  };

  useEffect(() => {
    getAllAddresses().then(() => {
      if (addressId) {
        getAddressById(addressId).then((c) => {
          setAddress(c);
          setIsLoading(false);
        });
      } else {
        setIsLoading(false);
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
          disabled={isLoading}
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
