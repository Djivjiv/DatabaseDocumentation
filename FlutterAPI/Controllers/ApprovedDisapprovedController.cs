﻿using Consultation.Domain;
using Consultation.Domain.Enum;
using Consultation.Infrastructure.Data;
using FlutterAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlutterAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApprovedDisapprovedController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ApprovedDisapprovedController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllConsultationRequests()
        {
            var requests = await _context.ConsultationRequest
                .Include(c => c.Student)
                    .ThenInclude(s => s.EnrolledCourses)
                .ToListAsync();

            if (requests == null || !requests.Any())
            {
                return NotFound(new { Message = "No consultation requests found." });
            }

            var responses = requests.Select(request =>
            {
                var enrolledCourse = request.Student.EnrolledCourses.FirstOrDefault();

                return new ConsultationRequestViewModel
                {
                    ConsultationID = request.ConsultationID,
                    CourseCode = enrolledCourse?.CourseCode ?? "N/A",
                    StudentName = request.Student.StudentName,
                    DateSchedule = request.DateSchedule,
                    TimeStart = request.StartedTime,
                    TimeEnd = request.EndedTime,
                    Status = request.Status
                };
            }).ToList();



            return Ok(responses);
        }

        [HttpPut("api/consultation-requests/{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusModel usm)
        {
            var request = await _context.ConsultationRequest.FindAsync(id);
            if (request == null)
                return NotFound("Consultation request not found.");

            if (request.Status != Status.Pending)
                return BadRequest("Only pending requests can be updated.");

            if (usm.Status != Status.Approved && usm.Status != Status.Disapproved)
                return BadRequest("Invalid status. Use 'Approved' or 'Disapproved'.");

            request.Status = usm.Status;
            await _context.SaveChangesAsync();

            return Ok(request);
        }

    }
}
