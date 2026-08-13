var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AttendanceManagementSystem_Api>("api");

builder.Build().Run();