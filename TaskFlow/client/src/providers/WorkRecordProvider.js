import React, { useState, useContext } from "react";
import { UserProfileContext } from "./UserProfileProvider";
import "firebase/auth";
export const WorkRecordContext = React.createContext();

export const WorkRecordProvider = (props) => {
  const { getToken } = useContext(UserProfileContext);
  const [workRecords, setWorkRecords] = useState([]);

  const getAllWorkRecords = () => {
    return getToken().then((token) =>
      fetch("/api/workRecord", {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
        .then((res) => res.json())
        .then(setWorkRecords)
    );
  };

  const getWorkRecordById = (id) => {
    return getToken().then((token) =>
      fetch(`/api/workRecord/${id}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((res) => res.json())
    );
  };

  const addWorkRecord = (workRecord) => {
    return getToken().then((token) => {
      fetch(`/api/workRecord`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(workRecord),
      });
    });
  };

  const deleteWorkRecord = (workRecordId) =>
    getToken().then((token) =>
      fetch(`/api/workRecord/${workRecordId}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }).then(getAllWorkRecords)
    );

  const updateWorkRecord = (workRecord) => {
    return getToken().then((token) =>
      fetch(`/api/workRecord/${workRecord.id}`, {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify(workRecord),
      })
    );
  };

  const GetAllWorkRecordsByJobId = (id) => {
    return getToken().then((token) =>
      fetch(`/api/workRecord/GetAllWorkRecordsByJobId${id}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((res) => res.json())
    );
  };

  return (
    <WorkRecordContext.Provider
      value={{
        workRecords,
        getAllWorkRecords,
        addWorkRecord,
        deleteWorkRecord,
        updateWorkRecord,
        getWorkRecordById,
        GetAllWorkRecordsByJobId,
      }}
    >
      {props.children}
    </WorkRecordContext.Provider>
  );
};
