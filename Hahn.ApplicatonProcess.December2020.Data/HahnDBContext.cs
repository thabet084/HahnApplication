using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using Hahn.ApplicatonProcess.December2020.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hahn.ApplicatonProcess.December2020.Data
{
    public class HahnDBContext: DbContext
    {

        public HahnDBContext(DbContextOptions options) : base(options)
        {

        }
        /// <summary>
        /// Applicants
        /// </summary>
        public virtual DbSet<Applicant> Applicants { get; set; }

        /// <summary>
        /// Exception Logs
        /// </summary>
        public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

        }
    }
}
