﻿namespace Obligatorio1.Domain
{
    public class Session
    {
        public int SessionID { get; set; }
        public Guid AuthToken { get; set; }
        public User User { get; set; }
        public Session()
        {
            AuthToken = Guid.NewGuid();
        }
    }
}