﻿using System;

namespace MVCApp.Models
{
    public class Log
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid JobId { get; set; }
    }
}