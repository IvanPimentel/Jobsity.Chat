﻿using System;

namespace Jobsity.Chat.Application.ViewModels.Base
{
    public abstract class BaseViewModel
    {

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
