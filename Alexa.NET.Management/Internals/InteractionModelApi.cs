﻿using System;
using System.Threading.Tasks;
using Alexa.NET.Management.Skills;
using Refit;

namespace Alexa.NET.Management.Internals
{
    public class InteractionModelApi : IInteractionModelApi
    {
        private IClientInteractionModelApi Client { get; }

        public InteractionModelApi(Uri baseAddress, Func<Task<string>> getToken)
        {
            Client = RestService.For<IClientInteractionModelApi>(
                baseAddress.ToString(),
                new RefitSettings
                {
                    AuthorizationHeaderValueGetter = getToken
                });
        }

        public Task<SkillInteraction> Get(string skillId, string locale)
        {
            return Client.Get(skillId, locale);
        }

        public async Task<string> GetTag(string skillId, string locale)
        {
            var message = await Client.GetTag(skillId, locale);
            return message.Headers.ETag.Tag;
        }

        public Task Update(string skillId, string locale, SkillInteraction interaction)
        {
            return Client.Update(skillId, locale, interaction);
        }

        public Task<BuildStatus> Status(string skillId, string locale)
        {
            return Client.Status(skillId, locale);
        }
    }
}