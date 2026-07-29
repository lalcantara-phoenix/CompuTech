using CompuTech.Application.Dashboard.DTOs;
using MediatR;

namespace CompuTech.Application.Dashboard.Queries.GetDashboard;

public record GetDashboardQuery : IRequest<DashboardDto>;
