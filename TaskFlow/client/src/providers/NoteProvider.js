import React, { useState, useContext } from "react";
import { UserProfileContext } from "./UserProfileProvider";
import "firebase/auth";
export const NoteContext = React.createContext();

export const NoteProvider = (props) => {
  const { getToken } = useContext(UserProfileContext);
  const [notes, setNotes] = useState([]);

  const getAllNotes = () => {
    return getToken().then((token) =>
      fetch("/api/note", {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
        .then((res) => res.json())
        .then(setNotes)
    );
  };

  const getNoteById = (id) => {
    return getToken().then((token) =>
      fetch(`/api/note/${id}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((res) => res.json())
    );
  };

  const addNote = (note) => {
    return getToken().then((token) => {
      fetch(`/api/note`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(note),
      });
    });
  };

  const deleteNote = (noteId) =>
    getToken().then((token) =>
      fetch(`/api/note/${noteId}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }).then(getAllNotes)
    );

  const updateNote = (note) => {
    return getToken().then((token) =>
      fetch(`/api/note/${note.id}`, {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify(note),
      })
    );
  };

  const GetAllNotesByJobId = (id) => {
    return getToken().then((token) =>
      fetch(`/api/note/GetAllNotesByJobId${id}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((res) => res.json())
    );
  };

  return (
    <NoteContext.Provider
      value={{
        notes,
        getAllNotes,
        addNote,
        deleteNote,
        updateNote,
        getNoteById,
        GetAllNotesByJobId,
      }}
    >
      {props.children}
    </NoteContext.Provider>
  );
};
