using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.ViewModels;
using TechJobs.Models;
using System.Collections.Generic;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            Job somejob = jobData.Find(id);
            

            return View(somejob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.
            if (ModelState.IsValid)
            {
                Employer employer = jobData.Employers.Find(newJobViewModel.EmployerID);
                Location location = jobData.Locations.Find(newJobViewModel.LocationID);
                PositionType positiontype = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID);
                CoreCompetency corecompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID);
                Job newjob = new Job {
                    Name = newJobViewModel.Name,
                    Employer = employer,
                    Location = location,
                    PositionType = positiontype,
                    CoreCompetency = corecompetency

                };
                jobData.Jobs.Add(newjob);

                return Redirect("/Job?id=" + newjob.ID);

            }

            return View(newJobViewModel);
        }
    }
}
