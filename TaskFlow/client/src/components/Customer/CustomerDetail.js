import React, { useEffect, useContext, useState } from "react";
import { CustomerContext } from "../../providers/CustomerProvider";

import { useParams } from "react-router-dom";
import CustomerAddress from "./CustomerAddress";
import CustomerJob from "./CustomerJob";
import { Link } from "react-router-dom";
import { CardHeader } from "reactstrap";

const CustomerDetails = () => {
  const [customer, SetCustomer] = useState({
    addresses: [],
    jobs: [],
  });

  const { getCustomerByIdWithAddressWithJob } = useContext(CustomerContext);

  const { id } = useParams();

  useEffect(() => {
    console.log("useEffect", id);
    getCustomerByIdWithAddressWithJob(id).then((response) => {
      SetCustomer(response);
    });
  }, []);

  if (!customer) {
    return null;
  }

  return (
    <div className="container">
      <CardHeader>
        <strong>{customer.name}</strong>
        <strong>{customer.phoneNumber}</strong>
      </CardHeader>

      <CardHeader>
        <Link to={`/address/add`}>
          <Button type="button">Add address</Button>
        </Link>
        <strong>Addresses:</strong>
        {customer.addresses.map((a) => (
          <CustomerAddress key={a.id} address={a} />
        ))}
      </CardHeader>

      <CardHeader>
        <strong>Jobs:</strong>
        {customer.jobs.map((j) => (
          <CustomerJob key={j.id} job={j} />
        ))}{" "}
      </CardHeader>
    </div>
  );
};

export default CustomerDetails;
