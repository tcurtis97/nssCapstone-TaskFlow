import React, { useContext, useEffect } from "react";
import { CustomerContext } from "../../providers/CustomerProvider";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";

export const SearchBar = () => {
  const { searchTerm, setSearchTerms } = useContext(CustomerContext);

  const handleChange = (event) => {
    setSearchTerms(event.target.value);
  };

  return (
    <div className="App">
      <Input
        type="text"
        id="searchbar"
        placeholder="Search"
        value={searchTerm}
        onChange={handleChange}
      />
    </div>
  );
};
export default SearchBar;
