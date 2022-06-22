﻿using Discord;
using Qmmands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Yuki.Services.Database;

namespace Yuki.Commands.Preconditions
{
    public class RequireModeratorAttribute : CheckAttribute
    {
        public override ValueTask<CheckResult> CheckAsync(CommandContext c)
        {
            YukiCommandContext context = c as YukiCommandContext;

            if (context.Guild.OwnerId == context.User.Id)
            {
                return CheckResult.Successful;
            }

            foreach (ulong role in GuildSettings.GetGuild(context.Guild.Id).ModeratorRoles)
            {
                if ((context.User as IGuildUser).RoleIds.Contains(role))
                {
                    return CheckResult.Successful;
                }
            }

            return CheckResult.Unsuccessful("You must be a moderator to execute this command.");
        }
    }
}
