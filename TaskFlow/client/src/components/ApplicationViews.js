import React, { useContext } from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import { UserProfileContext } from "../providers/UserProfileProvider";
import Login from "./Login";
import Register from "./Register";
import Home from "./Home";
import CustomerList from "./Customer/CustomerList";
import { CustomerForm } from "./Customer/CustomerForm";
import JobList from "./Job/JobList";
import SearchCustomer from "./Customer/SearchCustomer";
import CustomerDetail from "./Customer/CustomerDetail";
import { JobForm } from "./Job/JobForm";
import JobDetail from "./Job/JobDetail";
import { AddressForm } from "./Address/AddressForm";
import AddressEdit from "./Address/AddressEdit";
import { NoteForm } from "./Note/NoteForm";
import NoteEdit from "./Note/NoteEdit";

export default function ApplicationViews() {
  const { isLoggedIn } = useContext(UserProfileContext);

  return (
    <main>
      <Switch>
        <Route path="/" exact>
          {isLoggedIn ? <Home /> : <Redirect to="/login" />}
        </Route>

        {/* Customer ROUTES */}
        <Route path="/customer" exact>
          {isLoggedIn ? <SearchCustomer /> : <Redirect to="/login" />}
          {isLoggedIn ? <CustomerList /> : <Redirect to="/login" />}
        </Route>
        <Route path="/customer/edit/:customerId(\d+)" exact>
          {isLoggedIn ? <CustomerForm /> : <Redirect to="/login" />}
        </Route>
        <Route path="/customer/add" exact>
          {isLoggedIn ? <CustomerForm /> : <Redirect to="/login" />}
        </Route>
        <Route path="/customer/:id(\d+)" exact>
          {isLoggedIn ? <CustomerDetail /> : <Redirect to="/login" />}
        </Route>

        {/* JOB ROUTES */}
        <Route path="/job" exact>
          {isLoggedIn ? <JobList /> : <Redirect to="/login" />}
        </Route>
        <Route path="/job/add" exact>
          {isLoggedIn ? <JobForm /> : <Redirect to="/login" />}
        </Route>
        <Route path="/job/:id(\d+)" exact>
          {isLoggedIn ? <JobDetail /> : <Redirect to="/login" />}
        </Route>

        {/* Address ROUTES */}
        <Route path="/address/add/:customerId(\d+)" exact>
          {isLoggedIn ? <AddressForm /> : <Redirect to="/login" />}
        </Route>
        <Route path="/address/edit/:addressId(\d+)" exact>
          {isLoggedIn ? <AddressEdit /> : <Redirect to="/login" />}
        </Route>

        {/* Address ROUTES */}
        <Route path="/note/add/:jobId(\d+)" exact>
          {isLoggedIn ? <NoteForm /> : <Redirect to="/login" />}
        </Route>
        <Route path="/note/edit/:noteId(\d+)" exact>
          {isLoggedIn ? <NoteForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/login">
          <Login />
        </Route>

        <Route path="/register">
          <Register />
        </Route>
      </Switch>
    </main>
  );
}
