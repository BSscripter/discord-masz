﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace masz.Models
{
    public class GuildConfig : ICloneable
    {
        public int Id { get; set; }
        public string GuildId { get; set; }
        public string[] ModRoles { get; set; }
        public string[] AdminRoles { get; set; }
        public string[] MutedRoles { get; set; }
        public bool ModNotificationDM { get; set; }
        public string ModPublicNotificationWebhook { get; set; }
        public string ModInternalNotificationWebhook { get; set; }
        public bool StrictModPermissionCheck { get; set; }
        public bool ExecuteWhoisOnJoin { get; set; }
        public bool PublishModeratorInfo { get; set; }

        public object Clone()
        {
            return new GuildConfig {
                Id = this.Id,
                GuildId = this.GuildId,
                ModRoles = this.ModRoles,
                AdminRoles = this.AdminRoles,
                MutedRoles = this.MutedRoles,
                ModNotificationDM = this.ModNotificationDM,
                ModPublicNotificationWebhook = this.ModPublicNotificationWebhook,
                ModInternalNotificationWebhook = this.ModInternalNotificationWebhook,
                StrictModPermissionCheck = this.StrictModPermissionCheck,
                ExecuteWhoisOnJoin = this.ExecuteWhoisOnJoin
            };
        }
    }
}
