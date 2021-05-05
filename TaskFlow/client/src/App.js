import "./App.css";
import React from "react";
import { BrowserRouter as Router } from "react-router-dom";
import { UserProfileProvider } from "./providers/UserProfileProvider";
import Home from "./components/Home";
import ApplicationViews from "./components/ApplicationViews";
import { CustomerProvider } from "./providers/CustomerProvider";
import Header from "./components/Header";
import { JobProvider } from "./providers/JobProvider";

function App() {
  return (
    <Router>
      <UserProfileProvider>
        <JobProvider>
          <CustomerProvider>
            <Header />
            <ApplicationViews />
          </CustomerProvider>
        </JobProvider>
      </UserProfileProvider>
    </Router>
  );
}

export default App;
