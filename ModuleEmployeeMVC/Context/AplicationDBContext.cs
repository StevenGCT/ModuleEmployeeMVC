using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ModuleEmployeeMVC.Models;

namespace ModuleEmployeeMVC.Context
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext()
        {
        }

        public AplicationDBContext(DbContextOptions<AplicationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Schedule> Schedules { get; set; }
    }
}
