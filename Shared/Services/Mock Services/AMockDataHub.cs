﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Bogus;
using XebecPortal.UI.Pages.HR;
using XebecPortal.UI.Services.Models;
using Application = XebecPortal.UI.Services.Models.ApplicationModel;

namespace XebecPortal.UI.Services.MockServices
{
    public static class AMockDataHub
    {
        public enum ApplicationPhase
        {
            //get desciption
            [Description("Application Sent")] Application,
            Testing,
            InterviewStaff,
            InterviewCeo,
            Offer,
            Hired
        }

        public enum Status
        {
            Inprogress,
            Rejected
        }

        public static readonly List<Job> MockJobs = GetMockJobs().Generate(20);
        public static readonly List<XebecPortal.UI.Services.Models.AppUser> MockAppUsers = GetMockAppUsers().Generate(20);
        public static readonly List<Application> MockApplications = GetMockApplications().Generate(50);
        
        private static Faker<XebecPortal.UI.Services.Models.AppUser> GetMockAppUsers()
        {
            string[] roles = {"candidate", "developer", "hr"};
            return new Faker<XebecPortal.UI.Services.Models.AppUser>()
                .RuleFor(user => user.Id, f => f.IndexFaker)
                .RuleFor(user => user.Name, f => f.Name.FirstName())
                .RuleFor(user => user.Surname, f => f.Name.LastName())
                .RuleFor(user => user.Role, f => "candidate");
        }

        private static Faker<Application> GetMockApplications()
        {
            return new Faker<Application>()
                .StrictMode(true)
                .RuleFor(a => a.Id, f => f.IndexFaker)
                .RuleFor(a => a.JobId, f => f.PickRandom(MockJobs).Id)
                .RuleFor(a => a.AppUserId, f => f.PickRandom(MockAppUsers).Id)
                .RuleFor(a => a.TimeApplied, f => f.Date.Recent(150));
        }

        private static Faker<Job> GetMockJobs()
        {
            return new Faker<Job>()
                    .RuleFor(job => job.Id, f => f.IndexFaker)
                    .RuleFor(job => job.Title, f => f.Name.JobTitle())
                    .RuleFor(job => job.Description, f => f.Name.JobDescriptor())
                    
                ;
        }
    }
}