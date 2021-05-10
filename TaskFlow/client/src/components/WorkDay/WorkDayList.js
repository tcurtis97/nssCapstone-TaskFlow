import React, { useContext, useEffect, useState } from "react";
import { WorkDayContext } from "../../providers/WorkDayProvider";
import { JobContext } from "../../providers/JobProvider";
import WorkDay from "./WorkDay";

const WorkDayList = () => {
  const { GetJobsByWorkDay } = useContext(JobContext);

  const [jobs, setJobs] = useState([]);
  console.log(jobs, "stirng");

  useEffect(() => {
    GetJobsByWorkDay().then((response) => {
      setJobs(response);
    });
  }, []);

  return (
    <section>
      {jobs.map((j) => (
        <WorkDay key={j.id} workDay={j} />
      ))}
    </section>
  );
};

export default WorkDayList;
