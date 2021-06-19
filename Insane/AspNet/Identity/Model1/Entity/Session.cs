﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Insane.AspNet.Identity.Model1.Entity
{
    public class Session
    {
        public long Id { get; set; }
        public long PlatformId { get; set; }
        public long PermissionId { get; set; }
        public string Jti { get; set; } = null!;
        public string TokenHash { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string ClientUserAgent { get; set; } = null!;
        public string ClientFriendlyName { get; set; } = null!;
        public string ClientOS { get; set; } = null!;
        public string ClientIP { get; set; } = null!;
        public int ClientTimezone { get; set; }
        public decimal ClientLatitude { get; set; }
        public decimal ClientLongitude { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        public bool Revoked { get; set; }


        public Platform Platform { get; set; } = null!;
        public Permission Permission { get; set; } = null!;
    }
}
