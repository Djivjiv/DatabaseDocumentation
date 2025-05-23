﻿using Consultation.Domain;
using Consultation.Domain.Enum;
using FlutterAPI.ViewModel;

namespace FlutterAPI.Controllers
{
    public class ActionLogController
    {
        public static ActionLog ActionLogInput(string message, string AccountName,
            UserType UserType, int id)
        {
            ActionLog viewModel = new ActionLog()
            {
                UserID = id,
                ActionLogID = 0,
                Date = DateTime.Now,
                Description = message,
                Time = TimeOnly.FromDateTime(DateTime.Now),

            };
            return viewModel;
        }
    }
}
