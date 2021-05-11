import React, { useEffect, useContext, useState } from "react";
import { CustomerContext } from "../../providers/CustomerProvider";
import { AddressContext } from "../../providers/AddressProvider";
import { JobContext } from "../../providers/JobProvider";
import { useParams, useHistory } from "react-router-dom";
import CustomerAddress from "./CustomerAddress";
import CustomerJob from "./CustomerJob";
import { Link } from "react-router-dom";
import { CardHeader } from "reactstrap";
import { Button } from "reactstrap";

const CustomerDetails = () => {
  const [customer, SetCustomer] = useState({});
  const [addresses, SetAddresses] = useState([]);
  const [jobs, SetJobs] = useState([]);

  const { getCustomerById } = useContext(CustomerContext);
  const { GetAllAddressesByCustomerId } = useContext(AddressContext);
  const { GetAllJobsByCustomerId } = useContext(JobContext);

  const { id } = useParams();
  const history = useHistory();

  useEffect(() => {
    console.log("useEffect", id);
    getCustomerById(id).then((response) => {
      SetCustomer(response);
    });
  }, []);

  useEffect(() => {
    GetAllAddressesByCustomerId(id).then((response) => {
      SetAddresses(response);
    });
  }, []);

  useEffect(() => {
    GetAllJobsByCustomerId(id).then((response) => {
      SetJobs(response);
    });
  }, []);

  if (!customer) {
    return null;
  }

  return (
    <div className="container">
      <Button
        variant
        className="back_button"
        onClick={() => {
          history.goBack();
        }}
      >
        Back
      </Button>

      <CardHeader>
        <strong>{customer.name}</strong>
        <strong>{customer.phoneNumber}</strong>
      </CardHeader>

      <CardHeader>
        <Link to={`/address/add/${customer.id}`}>
          <Button type="button">Add address</Button>
        </Link>
        <strong>Addresses:</strong>
        {addresses.map((a) => (
          <CustomerAddress key={a.id} address={a} />
        ))}
      </CardHeader>

      <CardHeader>
        <strong>Jobs:</strong>
        {jobs.map((j) => (
          <CustomerJob key={j.id} job={j} />
        ))}
      </CardHeader>
    </div>
  );
};

export default CustomerDetails;
