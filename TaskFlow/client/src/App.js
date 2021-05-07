import "./App.css";
import React from "react";
import { BrowserRouter as Router } from "react-router-dom";
import { UserProfileProvider } from "./providers/UserProfileProvider";
import Home from "./components/Home";
import ApplicationViews from "./components/ApplicationViews";
import { CustomerProvider } from "./providers/CustomerProvider";
import Header from "./components/Header";
import { JobProvider } from "./providers/JobProvider";
import { AddressProvider } from "./providers/AddressProvider";
import { NoteProvider } from "./providers/NoteProvider";

function App() {
  return (
    <Router>
      <UserProfileProvider>
        <JobProvider>
          <NoteProvider>
            <CustomerProvider>
              <AddressProvider>
                <Header />
                <ApplicationViews />
              </AddressProvider>
            </CustomerProvider>
          </NoteProvider>
        </JobProvider>
      </UserProfileProvider>
    </Router>
  );
}

export default App;
