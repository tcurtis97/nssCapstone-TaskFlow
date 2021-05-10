import React, { useState, useContext } from "react";
import { UserProfileContext } from "./UserProfileProvider";
import "firebase/auth";
import { useHistory, useParams } from "react-router-dom";
export const WorkDayContext = React.createContext();

export const WorkDayProvider = (props) => {
  const { getToken } = useContext(UserProfileContext);
  const [workDay, setWorkDay] = useState([]);
  const history = useHistory();

  const addWorkDay = (workDay) => {
    return getToken()
      .then((token) => {
        fetch(`/api/workDay`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify(workDay),
        });
      })
      .then(history.go());
  };

  const deleteWorkDay = (workDayId) =>
    getToken().then((token) =>
      fetch(`/api/workDay/${workDayId}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }).then(history.go())
    );

  const updateWorkDay = (workDay) => {
    return getToken().then((token) =>
      fetch(`/api/workDay/${workDay.id}`, {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify(workDay),
      })
    );
  };

  return (
    <WorkDayContext.Provider
      value={{
        workDay,
        addWorkDay,
        deleteWorkDay,
        updateWorkDay,
      }}
    >
      {props.children}
    </WorkDayContext.Provider>
  );
};
