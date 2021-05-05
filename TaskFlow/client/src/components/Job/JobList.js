import React, { useContext, useEffect } from "react";
import { JobContext } from "../../providers/JobProvider";
import Job from "./Job";
import { Link } from "react-router-dom";

const JobList = () => {
  const { jobs, getAllJobs } = useContext(JobContext);
  console.log(jobs);
  useEffect(() => {
    getAllJobs();
  }, []);

  return (
    <section>
      <Link to="/job/add" className="nav-link">
        New Job
      </Link>
      {jobs.map((j) => (
        <Job key={j.id} job={j} />
      ))}
    </section>
  );
};

export default JobList;
