import React, { useEffect, useContext, useState } from "react";
import { CustomerContext } from "../../providers/CustomerProvider";

import { useParams } from "react-router-dom";
import CustomerAddress from "./CustomerAddress";
import CustomerJob from "./CustomerJob";
import { Link } from "react-router-dom";
import { CardHeader } from "reactstrap";

const CustomerDetails = () => {
  const [customer, setCustomer] = useState({});

  const { GetCustomerByIdWithAddressWithJob } = useContext(CustomerContext);

  const { id } = useParams();

  useEffect(() => {
    console.log("useEffect", id);
    GetCustomerByIdWithAddressWithJob(id).then((response) => {
      setCustomer(response);
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

      {customer.addresses.map((a) => (
        <CustomerAddress key={a.id} address={a} />
      ))}

      {customer.jobs.map((j) => (
        <CustomerJob key={j.id} job={j} />
      ))}
    </div>
  );
};

export default CustomerDetails;
