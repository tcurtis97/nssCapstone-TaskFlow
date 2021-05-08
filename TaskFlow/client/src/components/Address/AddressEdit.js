import React, { useContext, useEffect, useState } from "react";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { AddressContext } from "../../providers/AddressProvider";

import { useHistory, useParams } from "react-router-dom";

export const AddressEdit = () => {
  const { getAddressById, updateAddress } = useContext(AddressContext);

  const [address, setAddress] = useState({
    Address: "",
  });

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
    updateAddress({
      id: addressId,
      Address: address.address,
    }).then(() => history.goBack());
  };

  useEffect(() => {
    getAddressById(addressId).then((c) => {
      setAddress(c);
    });
  }, []);

  return (
    <Form className="addressForm">
      <h2 className="addressForm__title">"Save Address"</h2>

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
            <Label htmlFor="Address">Address name:</Label>
            <Input
              type="text"
              id="Address"
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
          {"Save Address"}
        </Button>
      </div>
    </Form>
  );
};
export default AddressEdit;
