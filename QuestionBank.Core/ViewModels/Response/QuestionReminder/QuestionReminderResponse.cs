﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.ViewModels.Response.QuestionReminder
{
	public class QuestionReminderResponse
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }
    }
}