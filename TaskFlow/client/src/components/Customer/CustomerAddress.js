import React, { useContext, useEffect } from "react";
import { Card, CardBody, CardHeader, CardText } from "reactstrap";
import { AddressContext } from "../../providers/AddressProvider";
import { useHistory } from "react-router-dom";
import { Button } from "reactstrap";
import { Link } from "react-router-dom";

const CustomerAddress = ({ address }) => {
  const { deleteAddress } = useContext(AddressContext);

  const addressDelete = () => {
    deleteAddress(address.id);
  };

  return (
    <Card className="m-4">
      <CardHeader>
        <strong>{address.address}</strong>
      </CardHeader>
      <CardBody>
        <Link to={`/address/edit/${address.id}`}>
          <Button type="button">Edit</Button>
        </Link>
        <Button
          variant="secondary"
          onClick={addressDelete}
          className="btn-primary"
        >
          Delete
        </Button>
      </CardBody>
    </Card>
  );
};

export default CustomerAddress;
