import React, { useEffect, useContext, useState } from "react";
import { JobContext } from "../../providers/JobProvider";

import { useParams } from "react-router-dom";

import { Link } from "react-router-dom";
import { CardHeader, CardText } from "reactstrap";

const JobDetails = () => {
  const [job, SetJob] = useState({
    customer: {},
    address: {},
  });

  const { GetJobByIdWithDetails } = useContext(JobContext);

  const { id } = useParams();

  useEffect(() => {
    console.log("useEffect", id);
    GetJobByIdWithDetails(id).then((response) => {
      SetJob(response);
    });
  }, []);

  if (!job) {
    return null;
  }

  return (
    <div className="container">
      <CardHeader>
        <strong>{job.customer.name}</strong>
      </CardHeader>
      <CardHeader>
        <strong>{job.customer.phoneNumber}</strong>
      </CardHeader>
      <CardHeader>
        <strong>{job.address.address}</strong>
      </CardHeader>

      <CardText>
        <strong>{job.descritpion}</strong>
      </CardText>
    </div>
  );
};

export default JobDetails;
