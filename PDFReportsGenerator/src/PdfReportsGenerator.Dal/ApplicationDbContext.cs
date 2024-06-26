﻿using Microsoft.EntityFrameworkCore;
using PdfReportsGenerator.Dal.Entities;

namespace PdfReportsGenerator.Dal;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<ReportTask> ReportTasks { get; set; }
}