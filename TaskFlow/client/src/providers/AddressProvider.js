import React, { useState, useContext } from "react";
import { UserProfileContext } from "./UserProfileProvider";
import "firebase/auth";
export const AddressContext = React.createContext();

export const AddressProvider = (props) => {
  const { getToken } = useContext(UserProfileContext);
  const [addresses, setAddresses] = useState([]);

  const getAllAddresses = () => {
    return getToken().then((token) =>
      fetch("/api/address", {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
        .then((res) => res.json())
        .then(setAddresses)
    );
  };

  const getAddressById = (id) => {
    return getToken().then((token) =>
      fetch(`/api/address/${id}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((res) => res.json())
    );
  };

  const addAddress = (address) => {
    return getToken().then((token) => {
      fetch(`/api/address`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(address),
      });
    });
  };

  const deleteAddress = (addressId) =>
    getToken().then((token) =>
      fetch(`/api/address/${addressId}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }).then(getAllAddresses)
    );

  const updateAddress = (address) => {
    return getToken().then((token) =>
      fetch(`/api/address/${address.id}`, {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify(address),
      })
    );
  };

  const GetAllAddressesByCustomerId = (id) => {
    return getToken().then((token) =>
      fetch(`/api/address/GetAllAddressesByCustomerId${id}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((res) => res.json())
    );
  };

  return (
    <AddressContext.Provider
      value={{
        addresses,
        getAllAddresses,
        addAddress,
        deleteAddress,
        updateAddress,
        getAddressById,
        GetAllAddressesByCustomerId,
      }}
    >
      {props.children}
    </AddressContext.Provider>
  );
};
