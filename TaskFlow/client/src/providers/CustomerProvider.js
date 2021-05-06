import React, { useState, useContext } from "react";
import { UserProfileContext } from "./UserProfileProvider";
import "firebase/auth";
export const CustomerContext = React.createContext();

export const CustomerProvider = (props) => {
  const { getToken } = useContext(UserProfileContext);
  const [customers, setCustomers] = useState([]);
  const [customersWithAddress, setCustomersWithAddress] = useState([]);
  const [searchTerms, setSearchTerms] = useState("");

  const getAllCustomers = () => {
    return getToken().then((token) =>
      fetch("/api/customer", {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
        .then((res) => res.json())
        .then(setCustomers)
    );
  };

  const getCustomerById = (id) => {
    return getToken().then((token) =>
      fetch(`/api/customer/${id}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((res) => res.json())
    );
  };

  const addCustomer = (customer) => {
    return getToken().then((token) => {
      fetch(`/api/customer`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(customer), //this stringifies our post object meaning it changes our object into string object
      });
    });
  };

  const deleteCustomer = (customerId) =>
    getToken().then((token) =>
      fetch(`/api/customer/${customerId}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }).then(getAllCustomers)
    );

  const updateCustomer = (customer) => {
    return getToken().then((token) =>
      fetch(`/api/customer/${customer.id}`, {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify(customer),
      })
    );
  };

  const searchCustomers = (searchTerms) => {
    return getToken().then((token) =>
      fetch(`/api/customer/search?q=${searchTerms}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
        .then((res) => res.json())
        .then(setCustomers)
    );
  };

  const getCustomerByIdWithAddressWithJob = (id) => {
    return getToken().then((token) =>
      fetch(`/api/customer/GetCustomerByIdWithAddressWithJob${id}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((res) => res.json())
    );
  };

  const getCustomersWithAddress = () => {
    return getToken().then((token) =>
      fetch(`/api/customer/GetCustomersWithAddress`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
        .then((res) => res.json())
        .then(setCustomersWithAddress)
    );
  };

  return (
    <CustomerContext.Provider
      value={{
        customers,
        getAllCustomers,
        addCustomer,
        deleteCustomer,
        updateCustomer,
        getCustomerById,
        searchTerms,
        setSearchTerms,
        searchCustomers,
        getCustomerByIdWithAddressWithJob,
        customersWithAddress,
        setCustomersWithAddress,
        getCustomersWithAddress,
      }}
    >
      {props.children}
    </CustomerContext.Provider>
  );
};
