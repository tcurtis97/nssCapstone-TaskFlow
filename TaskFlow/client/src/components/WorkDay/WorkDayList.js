import React, { useContext, useEffect } from "react";
import { JobContext } from "../../providers/JobProvider";
import Job from "./Job";
import { Link } from "react-router-dom";
import { Button } from "reactstrap";

const JobList = () => {
  const {
    jobs,
    getAllJobs,
    setViewingUncomplete,
    viewingUncomplete,
    getUncompleteJobs,
  } = useContext(JobContext);
  console.log(jobs, "jobs");

  useEffect(() => {
    getAllJobs();
  }, []);

  useEffect(() => {
    if (viewingUncomplete) {
      getUncompleteJobs();
    } else {
      getAllJobs();
    }
  }, [viewingUncomplete]);

  return (
    <section>
      <div>
        {viewingUncomplete ? <h1>Uncomplete Jobs</h1> : <h1>All Jobs</h1>}
        {viewingUncomplete ? (
          <Button
            color="success"
            onClick={() => {
              setViewingUncomplete(false);
            }}
          >
            View All Jobs
          </Button>
        ) : (
          <Button
            onClick={() => {
              setViewingUncomplete(true);
            }}
          >
            View Uncomplete Jobs
          </Button>
        )}
      </div>

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
